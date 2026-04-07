using Cigral.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Cigral.Services
{
    /// <summary>
    /// Clase estática global que maneja todas las comunicaciones HTTP con la API del backend.
    /// </summary>
    public static class ApiServices
    {
        //Dirección de prueba del servidor local
        #if DEBUG
        private static readonly string BaseUrl = "https://localhost:7221/api";
        #else
        // Dirección principal del servidor. Todas las peticiones empiezan desde aquí.
        private static readonly string BaseUrl = "https://api.cigral.com.ar/api";
        #endif

        private static readonly HttpClient _client = new HttpClient();

        // --- DATOS EN MEMORIA DEL SISTEMA (SESIÓN) ---
        // Private Set: Cualquier pantalla puede leer quién está logueado, pero solo el Login puede modificarlos.

        public static string AuthHeaderKey { get; set; }
        public static string TokenActual { get; private set; }
        public static string UsuarioActual { get; private set; }
        public static bool EsAdmin { get; private set; }

        /// <summary>
        /// Prepara el HttpClient para cada petición.
        /// Automáticamente inyecta el Token JWT en las cabeceras si el usuario ya está logueado.
        /// </summary>
        public static void ConfigurarHeaders()
        {
            _client.DefaultRequestHeaders.Clear();

                _client.DefaultRequestHeaders.Add("x-cigral-auth", "f7fa19b4-80ae-47ea-8c8b-0ecbb5995942");

            if (!string.IsNullOrEmpty(TokenActual))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenActual);
            }
        }

        // =========================================================================================
        // MÓDULO 1: AUTENTICACIÓN Y SEGURIDAD
        // =========================================================================================

        /// <summary>
        /// Intenta iniciar sesión en el servidor. Si es exitoso, guarda el Token en memoria.
        /// </summary>
        public static async Task<bool> Login(string usuario, string clave)
        {
            try
            {
                ConfigurarHeaders();

                var loginData = new LoginRequest { Username = usuario, Password = clave };
                string json = JsonConvert.SerializeObject(loginData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // USAMOS _client DIRECTAMENTE, SIN EL USING
                HttpResponseMessage response = await _client.PostAsync($"{BaseUrl}/Auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var datos = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

                    TokenActual = datos.Token;
                    UsuarioActual = datos.Username;
                    EsAdmin = datos.EsAdmin;

                    // ¡MUY IMPORTANTE! Inyectamos el token en el cliente global para el resto de la sesión
                    ConfigurarHeaders();

                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                         response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Credenciales Inválidas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    await MostrarErrorBackend(response, "Fallo del Servidor en Login");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico de conexión: {ex.Message}", "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // =========================================================================================
        // MÓDULO 2: REMITOS Y MOVIMIENTOS EN BLOQUE
        // =========================================================================================

        /// <summary>
        /// Envía el paquete completo de un remito de ingreso al servidor.
        /// </summary>
        /// <returns>El ID del remito generado, o 0 si falló.</returns>
        public static async Task<int> GuardarIngreso(RemitoIngresoRequest remito)
        {
            
                try
                {
                    var json = JsonConvert.SerializeObject(remito);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{BaseUrl}/Remitos/ingreso", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();

                        // Opción 1: Retorna el número suelto
                        if (int.TryParse(responseString, out int idDirecto)) return idDirecto;

                        // Opción 2: Retorna un objeto JSON
                        try
                        {
                            dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
                            return (int)jsonResponse.id;
                        }
                        catch
                        {
                            MessageBox.Show("El ingreso se guardó correctamente, pero no se pudo obtener el ID para imprimir el remito.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }
                    }
                    else
                    {
                        await MostrarErrorBackend(response, "Error al Guardar Ingreso");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error de conexión:\n{ex.Message}", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
        }

        /// <summary>
        /// Envía el paquete completo de un remito de egreso al servidor.
        /// </summary>
        public static async Task<int> CrearRemitoEgreso(RemitoEgresoDto remito)
        {
                try
                {
                    string json = JsonConvert.SerializeObject(remito);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{BaseUrl}/Remitos/egreso", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        try
                        {
                            var obj = JObject.Parse(res);
                            return obj["id"]?.Value<int>() ?? 1;
                        }
                        catch
                        {
                            if (int.TryParse(res, out int idCrudo)) return idCrudo;
                            return 1; // Retorno de seguridad
                        }
                    }
                    else
                    {
                        await MostrarErrorBackend(response, "Error al registrar Egreso");
                        return 0;
                    }
                }
                catch { return 0; }
        }

        /// <summary>
        /// Obtiene cuál será el próximo número de remito (correlativo) para un depósito específico.
        /// </summary>
        public static async Task<string> ObtenerSiguienteNumeroRemito(int depositoId, bool esIngreso)
        {
                try
                {
                    string url = $"{BaseUrl}/Remitos/siguiente-nro";
                    var requestData = new { depositoId = depositoId, esIngreso = esIngreso };

                    // Truco para enviar un Body en un GET request
                    var request = new HttpRequestMessage(HttpMethod.Get, url)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json")
                    };

                    var response = await _client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        dynamic resultado = JsonConvert.DeserializeObject(json);
                        return resultado.siguienteNumeroRemito;
                    }
                    return "";
                }
                catch { return ""; }
        }

        /// <summary>
        /// Descarga en caché y abre el archivo PDF correspondiente a un remito de ingreso o egreso.
        /// </summary>
        public static async Task DescargarAbrirPdfRemito(int idRemito, bool esIngreso = false)
        {
                try
                {
                    string tipoEndpoint = esIngreso ? "ingreso" : "egreso";
                    var response = await _client.GetAsync($"{BaseUrl}/Remitos/{tipoEndpoint}/{idRemito}/pdf");

                    if (response.IsSuccessStatusCode)
                    {
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        string nombreArchivo = $"Remito_{tipoEndpoint.ToUpper()}_{idRemito}_{DateTime.Now.Ticks}.pdf";
                        string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), nombreArchivo);

                        System.IO.File.WriteAllBytes(path, bytes);
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
                    }
                    else
                    {
                        MessageBox.Show($"El remito se creó, pero no se pudo descargar el PDF del {tipoEndpoint}.", "Aviso PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar abrir el PDF: " + ex.Message, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        /// <summary>
        /// Descarga los bytes crudos del PDF de un ingreso (versión anterior que devuelve byte[]).
        /// </summary>
        public static async Task<byte[]> ObtenerRemitoPdf(int idRemito)
        {
                try
                {
                    var response = await _client.GetAsync($"{BaseUrl}/Remitos/ingreso/{idRemito}/pdf");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    return null;
                }
                catch { return null; }
        }

        /// <summary>
        /// Consulta el historial general de remitos generados en el sistema (con soporte para paginación y filtros).
        /// </summary>
        /// <summary>
        /// Consulta el historial general de remitos generados en el sistema (con paginación de servidor).
        /// </summary>
        public static async Task<PaginadoResponse<RemitoHistorialDto>> ObtenerHistorialRemitos(bool esIngreso, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string nroRemito = "", int pageNumber = 1, int pageSize = 25)
        {
                try
                {
                    string tipoEndpoint = esIngreso ? "ingreso" : "egreso";

                    // 1. Armamos la URL dinámica con los parámetros de página
                    string url = $"{BaseUrl}/Remitos/{tipoEndpoint}?PageNumber={pageNumber}&PageSize={pageSize}";

                    if (fechaDesde.HasValue) url += $"&FechaDesde={fechaDesde.Value.ToString("yyyy-MM-dd")}";
                    if (fechaHasta.HasValue) url += $"&FechaHasta={fechaHasta.Value.ToString("yyyy-MM-dd")}";
                    if (!string.IsNullOrWhiteSpace(nroRemito)) url += $"&NumeroRemito={nroRemito.Trim()}";

                    var response = await _client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // 2. Usamos el súper-molde genérico
                        var resultado = JsonConvert.DeserializeObject<PaginadoResponse<RemitoHistorialDto>>(json);

                        if (resultado == null || resultado.items == null)
                            return new PaginadoResponse<RemitoHistorialDto> { items = new List<RemitoHistorialDto>() };

                        return resultado;
                    }
                    else
                    {
                        await MostrarErrorBackend(response, "Error al traer historial de remitos");
                        return new PaginadoResponse<RemitoHistorialDto> { items = new List<RemitoHistorialDto>() };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno al buscar remitos:\n{ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PaginadoResponse<RemitoHistorialDto> { items = new List<RemitoHistorialDto>() };
                }
            }

        // =========================================================================================
        // MÓDULO 3: STOCK Y EXISTENCIAS (MOVIMIENTOS MANUALES)
        // =========================================================================================

        /// <summary>
        /// Aumenta el stock de un producto individualmente (movimiento interno).
        /// </summary>
        public static async Task<bool> AumentarStock(AumentarExistenciaRequest request)
        {
                try
                {
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{BaseUrl}/Existencias/aumentar", content);

                    if (response.IsSuccessStatusCode) return true;
                    else
                    {
                        await MostrarErrorBackend(response, "Error al aumentar stock");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fallo de conexión:\n{ex.Message}", "Fallo Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
        }

        /// <summary>
        /// Disminuye el stock de un producto individualmente (movimiento interno).
        /// </summary>
        // Le agregamos el parámetro 'nombreProducto'
        public static async Task<bool> DisminuirStock(DisminuirStockDto item, string nombreProducto = "Producto")
        {
                try
                {
                    string json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{BaseUrl}/Existencias/disminuir", content);

                    if (response.IsSuccessStatusCode) return true;
                    else
                    {
                        //Le manda al título de la ventana de error
                        await MostrarErrorBackend(response, $"Error al descontar: {nombreProducto}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno de conexión:\n{ex.Message}", "Fallo de Red", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
        }

        /// <summary>
        /// Trae todo el inventario físico almacenado en los estantes.
        /// </summary>
        public static async Task<PaginadoResponse<ExistenciaDto>> ObtenerExistencias(string? buscar = null,
            bool ocultarCero = false,
            bool soloVencidos = false,
            int ordenarPor = 0,
            bool esDescendente = false,
            int pageNumber = 1,
            int diasParaVencer = 0,
            int? productoId = null,
            string? codigoLote = null,
            string? numeroSerie = null )
        {
                try
                {
                    string url = $"{BaseUrl}/Existencias?PageNumber={pageNumber}&PageSize=25";

                    if (!string.IsNullOrEmpty(buscar)) url += $"&NombreProducto={buscar}";
                    url += $"&ordenarPor={ordenarPor}&esDescendente={esDescendente.ToString().ToLower()}";
                    if (ocultarCero) url += "&OcultarCero=true";
                    if (soloVencidos) url += "$&SoloConVencimiento=true";
                    if (diasParaVencer != 0) url += $"&DiasParaVencer={diasParaVencer}";
                    if (!string.IsNullOrEmpty(numeroSerie)) url += $"&NumSerie={numeroSerie}";
                    if (!string.IsNullOrEmpty(codigoLote)) url += $"&CodigoLote={codigoLote}";
                    if (productoId != null) url += $"&ProductoId={productoId}";

                    var response = await _client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var paquete = JsonConvert.DeserializeObject<PaginadoResponse<ExistenciaDto>>(json);
                        if (paquete != null && paquete.items != null) return paquete;
                    }
                    return new PaginadoResponse<ExistenciaDto>();
                }
                catch { return new PaginadoResponse<ExistenciaDto>(); }
        }

        /// <summary>
        /// Busca el primer lote que tenga stock disponible de un producto particular.
        /// </summary>
        public static async Task<ExistenciaDto> BuscarProductoParaEgreso(int productoId)
        {
                try
                {
                    string url = $"{BaseUrl}/Existencias?ProductoId={productoId}&OrdenarPor=2&EsDescendente=false&PageNumber=1&PageSize=10";
                    var response = await _client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var jsonParseado = JObject.Parse(json);
                        var listaExistencias = jsonParseado["items"].ToObject<List<ExistenciaDto>>();

                        // Devolvemos el primero que tenga stock positivo
                        if (listaExistencias != null && listaExistencias.Count > 0)
                        {
                            return listaExistencias.FirstOrDefault(x => x.Cantidad > 0);
                        }
                    }
                    return null;
                }
                catch { return null; }
        }

        /// <summary>
        /// Consulta existencias que están próximas a vencer (Para métricas del Dashboard).
        /// </summary>
        public static async Task<List<VencimientoDto>> ObtenerVencimientos(int? diasDesde = null, int? diasHasta = null, bool incluirVencidos = false)
        {
                try
                {
                    string url = $"{BaseUrl}/Existencias/proximos-vencer?";
                    var parametros = new List<string>();

                    if (diasDesde.HasValue) parametros.Add($"diasDesde={diasDesde.Value}");
                    if (diasHasta.HasValue) parametros.Add($"diasHasta={diasHasta.Value}");
                    parametros.Add($"incluirVencidos={incluirVencidos.ToString().ToLower()}");

                    url += string.Join("&", parametros);

                    var response = await _client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<VencimientoDto>>(json);
                    }
                    return new List<VencimientoDto>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al traer datos del dashboard:\n{ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<VencimientoDto>();
                }
        }

        // =========================================================================================
        // MÓDULO 4: CATÁLOGO, ESCÁNER Y MAESTROS
        // =========================================================================================

        /// <summary>
        /// Llama a la herramienta "X-Ray Parser" del backend para desarmar códigos de barras largos (EAN-128, etc.).
        /// </summary>
        public static async Task<ParserResponse> ParsearCodigoBarras(string codigoCrudo)
        {
            try
            {
                string urlEncode = System.Net.WebUtility.UrlEncode(codigoCrudo);
                var response = await _client.GetAsync($"{BaseUrl}/Parser/analyze?rawCode={urlEncode}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ParserResponse>(json);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Atrapamos el 400 de forma controlada.
                    // Leemos el texto plano que mandaste desde el backend ("El código escaneado no contiene...")
                    string errorMessage = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(errorMessage, "Código no reconocido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null; // Retornamos null sin lanzar excepciones para no activar el 2do mensaje
                }
                else
                {
                    // Solo entra aquí si es un error 500 (Servidor caído) o 401 (Token vencido)
                    await MostrarErrorBackend(response, "X-Ray Parser: Código Rechazado");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de red al intentar comunicarse con el servidor:\n{ex.Message}", "Fallo de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Registra un nuevo producto en el catálogo maestro.
        /// </summary>
        public static async Task<int> CrearProductoNuevo(ProductoCreateRequest producto)
        {
                try
                {
                    var json = JsonConvert.SerializeObject(producto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{BaseUrl}/Productos", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var productoCreado = JsonConvert.DeserializeObject<ProductoCreateResponse>(jsonResponse);
                        return productoCreado.Id;
                    }
                    else
                    {
                        await MostrarErrorBackend(response, $"Error al crear '{producto.Nombre}'");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fallo de conexión al crear producto:\n{ex.Message}", "Fallo Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
        }

        /// <summary>
        /// Busca productos en el catálogo maestro usando el texto ingresado.
        /// </summary>
        public static async Task<PaginadoResponse<ProductoResponseDto>> ObtenerProductosCatalogo(string filtroNombre, string filtroGlobal = "", int pageNumber = 1, int pageSize = 25)
        {
                try
                {
                    string url = $"{BaseUrl}/Productos?PageNumber={pageNumber}&PageSize={pageSize}";

                    if(!string.IsNullOrWhiteSpace(filtroNombre)) url += $"&Nombre={Uri.EscapeDataString(filtroNombre.Trim())}";
                    if(!string.IsNullOrWhiteSpace(filtroGlobal)) url += $"&BusquedaGlobal={Uri.EscapeDataString(filtroGlobal.Trim())}";

                    var response = await _client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var resultado = JsonConvert.DeserializeObject<PaginadoResponse<ProductoResponseDto>>(json);
                        if (resultado == null || resultado.items == null) return new PaginadoResponse<ProductoResponseDto>();
                        return resultado;
                    }
                    else
                    {
                        MessageBox.Show($"El servidor rebotó la búsqueda en el catálogo.\nStatus: {response.StatusCode}\nDetalle:\n{json}", "Error del Backend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return new PaginadoResponse<ProductoResponseDto>();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno al buscar en el catálogo:\n{ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PaginadoResponse<ProductoResponseDto>();
                }
        }

        public static async Task<bool> UpdateProductoGTIN(int productoId, string nuevoGTIN)
        {
                try
                {
                    string json = JsonConvert.SerializeObject(nuevoGTIN);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PutAsync($"{BaseUrl}/Productos/actualizar-gtin/{productoId}", content);
                    if (response.IsSuccessStatusCode) return true;
                    else
                    {
                        await MostrarErrorBackend(response, $"Error al actualizar GTIN del producto ID {productoId}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fallo de conexión al actualizar GTIN:\n{ex.Message}", "Fallo Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
        }

        public static async Task<int> UpdateProducto(ProductoUpdateDto producto, int id)
        {
                try
                {
                    string json = JsonConvert.SerializeObject(producto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _client.PutAsync($"{BaseUrl}/Productos/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var productoResponse = JsonConvert.DeserializeObject<ProductoResponseDto>(jsonResponse);
                        return productoResponse.id;
                    }
                    else
                    {
                        await MostrarErrorBackend(response, $"Error al actualizar algun campo de '{producto.nombre}'");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fallo de conexión al modificar el producto:\n{ex.Message}", "Fallo Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
        }

        /// <summary>
        /// Intenta crear la marca. Si ya existe, el backend la rechaza pero aseguramos su existencia.
        /// </summary>
        public static async Task<bool> GarantizarMarcaExiste(string nombreMarca)
        {
                var marcaNueva = new { nombre = nombreMarca };
                var json = JsonConvert.SerializeObject(marcaNueva);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{BaseUrl}/Marcas", content);

                // OK (200), Creado (201), Conflicto/Ya Existe (409/400)
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Conflict || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return true;
                }
                return false;
        }

        /// <summary>
        /// Trae la lista completa de depósitos.
        /// </summary>
        public static async Task<List<DepositoDto>> ObtenerDepositos()
        {
                try
                {
                    var response = await _client.GetAsync($"{BaseUrl}/Depositos");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var resultado = JsonConvert.DeserializeObject<DepositosResponse>(json);
                        return resultado?.Items ?? new List<DepositoDto>();
                    }
                    return new List<DepositoDto>();
                }
                catch { return new List<DepositoDto>(); }
        }

        /// <summary>
        /// Trae la lista de proveedores.
        /// </summary>
        public static async Task<List<ProveedorDto>> ObtenerProveedores(string busqueda)
        {
                try
                {
                    string url = $"{BaseUrl}/Proveedores?pageSize=25&PageNumber=1";
                    if (string.IsNullOrWhiteSpace(busqueda) == false)
                    {
                        url += $"&RazonSocial={Uri.EscapeDataString(busqueda.Trim())}";
                    }
                    var response = await _client.GetAsync(url);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var paquete = JsonConvert.DeserializeObject<PaginadoResponse<ProveedorDto>>(json);
                        if (paquete != null && paquete.items != null) return paquete.items;
                    }
                    return new List<ProveedorDto>();
                }
                catch { return new List<ProveedorDto>(); }
        }

        /// <summary>
        /// Crea un proveedor nuevo desde la ventana modal de ingresos.
        /// </summary>
        public static async Task<int> CrearProveedorExpress(ProveedorDto nuevoProveedor)
        {
                try
                {
                    string json = JsonConvert.SerializeObject(nuevoProveedor);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{BaseUrl}/Proveedores", content);

                    string res = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            var obj = JObject.Parse(res);
                            return obj["id"]?.Value<int>() ?? 0;
                        }
                        catch
                        {
                            if (int.TryParse(res, out int idCrudo)) return idCrudo;
                            return 0;
                        }
                    }
                    else
                    {
                        await MostrarErrorBackend(response, "Error al crear Proveedor");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno de conexión:\n{ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
        }

        /// <summary>
        /// Trae la lista de clientes para armar remitos de egreso.
        /// </summary>
        public static async Task<List<ClienteDto>> ObtenerClientes(string busqueda = "")
        {
                try
                {
                    // 1. Armamos la URL base. Le pedimos 50 resultados para la lista.
                    // (Asegurate de que sea /Cliente o /Clientes según tu API)
                    string url = $"{BaseUrl}/Clientes?PageNumber=1&PageSize=50";

                    // 2. Si el usuario tipeó algo, le agregamos el parámetro "RazonSocial" a la URL
                    if (!string.IsNullOrWhiteSpace(busqueda))
                    {
                        url += $"&RazonSocial={Uri.EscapeDataString(busqueda.Trim())}";
                    }

                    var response = await _client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // 3. Como Swagger nos mostró que devuelve { "items": [...], "totalCount": ... }
                        // Usamos 'dynamic' para ir directo a buscar la propiedad "items" sin tener que crear clases nuevas.
                        dynamic resultado = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                        if (resultado != null && resultado.items != null)
                        {
                            // Transformamos esos "items" en tu lista de ClienteDto
                            return resultado.items.ToObject<List<ClienteDto>>();
                        }

                        return new List<ClienteDto>();
                    }

                    return new List<ClienteDto>();
                }
                catch (Exception)
                {
                    // Si falla la red, devolvemos vacío para no romper la pantalla
                    return new List<ClienteDto>();
                }
        }

        /// <summary>
        /// Crea un cliente nuevo desde la ventana modal de egresos.
        /// </summary>
        public static async Task<int> CrearClienteExpress(ClienteDto nuevoCliente)
        {
                try
                {
                    string json = JsonConvert.SerializeObject(nuevoCliente);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{BaseUrl}/Clientes", content);

                    string res = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            var obj = JObject.Parse(res);
                            return obj["id"]?.Value<int>() ?? 0;
                        }
                        catch
                        {
                            if (int.TryParse(res, out int idCrudo)) return idCrudo;
                            return 0;
                        }
                    }
                    else
                    {
                        await MostrarErrorBackend(response, "Rechazo al crear Cliente");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno de conexión:\n{ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
        }

        // =========================================================================================
        // MÓDULO 5: AUDITORÍA Y HERRAMIENTAS GLOBALES
        // =========================================================================================

        /// <summary>
        /// Consulta la tabla de auditoría para ver los movimientos históricos de stock.
        /// </summary>
        /// <summary>
        /// Consulta la tabla de auditoría para ver los movimientos históricos de stock (con paginación de servidor).
        /// </summary>
        public static async Task<PaginadoResponse<AuditoriaItemDto>> ObtenerAuditoria(int? tipoMovimiento = null, string nombreProducto = "", string lote = "", string serie = "", int pageNumber = 1, int pageSize = 25)
        {
                try
                {
                    // 1. Armamos la URL con las variables de paginación dinámicas en vez de fijas
                    string url = $"{BaseUrl}/Auditoria?PageNumber={pageNumber}&PageSize={pageSize}";

                    if (tipoMovimiento.HasValue && tipoMovimiento.Value > 0) url += $"&Tipo={tipoMovimiento.Value}";

                    if (!string.IsNullOrWhiteSpace(nombreProducto))
                        url += $"&NombreProducto={Uri.EscapeDataString(nombreProducto.Trim())}";

                    if (!string.IsNullOrWhiteSpace(lote))
                        url += $"&CodigoLote={lote.Trim()}";

                    if (!string.IsNullOrWhiteSpace(serie))
                        url += $"&NumeroSerie={Uri.EscapeDataString(serie.Trim())}";

                
                    var response = await _client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // 2. Usa el súper-molde genérico que armamos antes
                        var resultado = JsonConvert.DeserializeObject<PaginadoResponse<AuditoriaItemDto>>(json);

                        // Si viene vacío, devuelve el molde con una lista vacía para que no rompa
                        if (resultado == null || resultado.items == null)
                        {
                            return new PaginadoResponse<AuditoriaItemDto> { items = new List<AuditoriaItemDto>() };
                        }

                        // 3. Devuelv el PAQUETE COMPLETO (lista + totalPages + etc)
                        return resultado;
                    }
                    else
                    {
                        MessageBox.Show($"El servidor rebotó la consulta de auditoría.\nStatus: {response.StatusCode}\nDetalle:\n{json}", "Error del Backend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return new PaginadoResponse<AuditoriaItemDto> { items = new List<AuditoriaItemDto>() };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno al leer los datos de auditoría:\n{ex.Message}", "Error de Conversión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PaginadoResponse<AuditoriaItemDto> { items = new List<AuditoriaItemDto>() };
                }
        }

        /// <summary>
        /// Muestra carteles de error prolijos interceptando las respuestas JSON del backend.
        /// Atrapa errores de DomainException (HTTP 400) y de FluentValidation (HTTP 422).
        /// </summary>
        private static async Task<List<int>> MostrarErrorBackend(HttpResponseMessage response, string tituloVentana = "Error de Servidor")
        {
            List<int> filasConError = new List<int>();

            try
            {
                string errorJson = await response.Content.ReadAsStringAsync();

                // Errores de Validación (Listas/Grillas) - Ej: Stock Insuficiente en varias filas
                if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity && errorJson.Contains("\"errores\":"))
                {
                    var valError = JsonConvert.DeserializeObject<Cigral.Models.ValidationErrorResponse>(errorJson);

                    if (valError != null && valError.errores != null)
                    {
                        string mensajeLimpio = valError.mensaje + "\n\n";
                        foreach (var err in valError.errores)
                        {
                            mensajeLimpio += $"- {err.mensaje}\n";
                            filasConError.Add(err.orden);
                        }

                        MessageBox.Show(mensajeLimpio, "Validación de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return filasConError;
                    }
                }


                // Errores de Formato del Framework(HTTP 400 BadRequest)
                // Ocurre cuando se mandan tipos de datos incorrectos (ej: un string vacío en vez de null para una fecha)
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest && errorJson.Contains("\"errors\":"))
                {
                    var badRequestObj = JObject.Parse(errorJson);
                    var diccionarioErrores = badRequestObj["errors"];

                    if (diccionarioErrores != null)
                    {
                        string mensajeLimpio = "Por favor, verificá el formato de los datos ingresados:\n\n";

                        // Recorre todas las propiedades que fallaron (FechaVencimiento, etc)
                        foreach (var propiedad in diccionarioErrores.Children<JProperty>())
                        {
                            foreach (var error in propiedad.Value)
                            {
                                mensajeLimpio += $"- {error}\n";
                            }
                        }

                        MessageBox.Show(mensajeLimpio, "Datos Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return filasConError; // Retorna vacío porque no tenemos el número de fila acá
                    }
                }




                // Errores de Dominio Estándar 
                var errorObj = JsonConvert.DeserializeObject<Cigral.Models.DomainErrorResponse>(errorJson);

                if (errorObj != null && !string.IsNullOrEmpty(errorObj.code) && !string.IsNullOrEmpty(errorObj.message))
                {
                    MessageBox.Show($"{errorObj.message}\n\n(Código: {errorObj.code} - {errorObj.codeValue})",
                                    tituloVentana, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // 3. Fallo (Base de datos caída, etc)
                    MessageBox.Show($"Error no controlado en el servidor.\nStatus: {response.StatusCode}\nDetalle: {errorJson}",
                                    "Fallo del Servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show($"Ocurrió un error inesperado al comunicarse con el servidor (Status: {response.StatusCode}).",
                                tituloVentana, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return filasConError;
        }

        // MODULO ENTIDADES

        // Le agregamos "= null" para que los parámetros sean opcionales al llamarla
        public static async Task<List<EntidadDto>> ObtenerEntidades(string razonSocial = null, string cuit = null)
        {
                try
                {
                    // 1. Iniciamos con la URL base y el paginado
                    string url = $"{BaseUrl}/Clientes/entidades?pageSize=25";

                    // 2. Agregamos Razón Social solo si no está vacía y la codificamos
                    if (!string.IsNullOrWhiteSpace(razonSocial))
                    {
                        url += $"&RazonSocial={Uri.EscapeDataString(razonSocial)}";
                    }

                    // 3. Agregamos Cuit solo si no está vacío y lo codificamos
                    if (!string.IsNullOrWhiteSpace(cuit))
                    {
                        url += $"&Cuit={Uri.EscapeDataString(cuit)}";
                    }

                    // 4. Hacemos la petición con la URL ya ensamblada y segura
                    var response = await _client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var paquete = JsonConvert.DeserializeObject<PaginadoResponse<EntidadDto>>(json);
                        if (paquete != null && paquete.items != null) return paquete.items;
                    }
                    return new List<EntidadDto>();
                }
                catch
                {
                    return new List<EntidadDto>();
                }
        }

        public static async Task<int> UpdateEntidad(EntidadDto entidad)
        {
                try
                {
                    string json = JsonConvert.SerializeObject(entidad);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = entidad.TipoEntidad=="Cliente" ? await _client.PutAsync($"{BaseUrl}/Clientes/{entidad.IdOriginal}", content) : await _client.PutAsync($"{BaseUrl}/Proveedores/{entidad.IdOriginal}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var entidadResponse = JsonConvert.DeserializeObject<EntidadResponse>(jsonResponse);
                        return entidadResponse.Id;
                    }
                    else
                    {
                        await MostrarErrorBackend(response, $"Error al actualizar algun campo de '{entidad.RazonSocial}'");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fallo de conexión al modificar la entidad:\n{ex.Message}", "Fallo Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
        }

        /// <summary>
        /// Obtiene los datos frescos de un producto por su ID para refrescar la grilla.
        /// </summary>
        public static async Task<ProductoResponseDto> ObtenerProductoPorId(int id)
        {
           
                try
                {
                    // Asegurate de que la ruta sea correcta según tu backend (ej. /Productos/{id})
                    var response = await _client.GetAsync($"{BaseUrl}/Productos/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ProductoResponseDto>(json);
                    }
                    return null;
                }
                catch { return null; }
        }


    }
}