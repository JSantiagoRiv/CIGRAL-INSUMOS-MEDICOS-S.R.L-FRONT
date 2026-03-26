using Cigral.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        // Dirección principal del servidor. Todas las peticiones empiezan desde aquí.
        private static readonly string BaseUrl = "https://api.cigral.com.ar/api";

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
        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl); // Indica la URL base

            // Si el Token no está vacío (el usuario pasó por el Login)
            if (!string.IsNullOrEmpty(AuthHeaderKey))
            {
                client.DefaultRequestHeaders.Add("x-cigral-auth", AuthHeaderKey);
            }

            // 3. Si el usuario está logueado, inyectamos el JWT Bearer
            if (!string.IsNullOrEmpty(TokenActual))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenActual);
            }

            return client;
        }

        // =========================================================================================
        // MÓDULO 1: AUTENTICACIÓN Y SEGURIDAD
        // =========================================================================================

        /// <summary>
        /// Intenta iniciar sesión en el servidor. Si es exitoso, guarda el Token en memoria.
        /// </summary>
        public static async Task<bool> Login(string usuario, string clave)
        {
            // El 'using' asegura que HttpClient se destruya y libere memoria al terminar.
            using (HttpClient client = GetClient())
            {
                try
                {
                    // 1. Armamos el paquete con los datos del usuario
                    var loginData = new LoginRequest
                    {
                        Username = usuario,
                        Password = clave
                    };

                    // 2. Lo convierte a formato JSON para mandarlo por red
                    string json = JsonConvert.SerializeObject(loginData);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    // 3. Dispara la petición al endpoint
                    HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/Auth/Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leemos y deserializamos la respuesta exitosa
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var datos = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

                        // Guardamos los datos de sesión globales
                        TokenActual = datos.Token;
                        UsuarioActual = datos.Username;
                        EsAdmin = datos.EsAdmin;

                        return true;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                             response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        // Credenciales incorrectas
                        MessageBox.Show("Usuario o contraseña incorrectos. Por favor, verificá tus datos.", "Credenciales Inválidas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        // Error no controlado del servidor, usamos la herramienta global
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
            using (HttpClient client = GetClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(remito);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Remitos/ingreso", content);

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
        }

        /// <summary>
        /// Envía el paquete completo de un remito de egreso al servidor.
        /// </summary>
        public static async Task<int> CrearRemitoEgreso(RemitoEgresoDto remito)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(remito);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Remitos/egreso", content);

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
        }

        /// <summary>
        /// Obtiene cuál será el próximo número de remito (correlativo) para un depósito específico.
        /// </summary>
        public static async Task<string> ObtenerSiguienteNumeroRemito(int depositoId, bool esIngreso)
        {
            using (HttpClient client = GetClient())
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

                    var response = await client.SendAsync(request);

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
        }

        /// <summary>
        /// Descarga en caché y abre el archivo PDF correspondiente a un remito de ingreso o egreso.
        /// </summary>
        public static async Task DescargarAbrirPdfRemito(int idRemito, bool esIngreso = false)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string tipoEndpoint = esIngreso ? "ingreso" : "egreso";
                    var response = await client.GetAsync($"{BaseUrl}/Remitos/{tipoEndpoint}/{idRemito}/pdf");

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
        }

        /// <summary>
        /// Descarga los bytes crudos del PDF de un ingreso (versión anterior que devuelve byte[]).
        /// </summary>
        public static async Task<byte[]> ObtenerRemitoPdf(int idRemito)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var response = await client.GetAsync($"{BaseUrl}/Remitos/ingreso/{idRemito}/pdf");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    return null;
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// Consulta el historial general de remitos generados en el sistema (con soporte para paginación y filtros).
        /// </summary>
        /// <summary>
        /// Consulta el historial general de remitos generados en el sistema (con paginación de servidor).
        /// </summary>
        public static async Task<PaginadoResponse<RemitoHistorialDto>> ObtenerHistorialRemitos(bool esIngreso, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string nroRemito = "", int pageNumber = 1, int pageSize = 25)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string tipoEndpoint = esIngreso ? "ingreso" : "egreso";

                    // 1. Armamos la URL dinámica con los parámetros de página
                    string url = $"{BaseUrl}/Remitos/{tipoEndpoint}?PageNumber={pageNumber}&PageSize={pageSize}";

                    if (fechaDesde.HasValue) url += $"&FechaDesde={fechaDesde.Value.ToString("yyyy-MM-dd")}";
                    if (fechaHasta.HasValue) url += $"&FechaHasta={fechaHasta.Value.ToString("yyyy-MM-dd")}";
                    if (!string.IsNullOrWhiteSpace(nroRemito)) url += $"&NumeroRemito={nroRemito.Trim()}";

                    var response = await client.GetAsync(url);
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
        }

        // =========================================================================================
        // MÓDULO 3: STOCK Y EXISTENCIAS (MOVIMIENTOS MANUALES)
        // =========================================================================================

        /// <summary>
        /// Aumenta el stock de un producto individualmente (movimiento interno).
        /// </summary>
        public static async Task<bool> AumentarStock(AumentarExistenciaRequest request)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Existencias/aumentar", content);

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
        }

        /// <summary>
        /// Disminuye el stock de un producto individualmente (movimiento interno).
        /// </summary>
        // Le agregamos el parámetro 'nombreProducto'
        public static async Task<bool> DisminuirStock(DisminuirStockDto item, string nombreProducto = "Producto")
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Existencias/disminuir", content);

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
        }

        /// <summary>
        /// Trae todo el inventario físico almacenado en los estantes.
        /// </summary>
        public static async Task<List<ExistenciaDto>> ObtenerExistencias(string buscar, bool ocultarCero, bool soloVencidos, int ordenarPor = 0, bool esDescendente = false)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = $"{BaseUrl}/Existencias?PageNumber=1&PageSize=100";

                    if (!string.IsNullOrEmpty(buscar)) url += $"&NombreProducto={buscar}";
                    url += $"&ordenarPor={ordenarPor}&esDescendente={esDescendente.ToString().ToLower()}";
                    if (ocultarCero) url += "&OculrarCero=true";
                    if (soloVencidos) url += "$SoloConVencimiento=true";

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var paquete = JsonConvert.DeserializeObject<PaginadoResponse<ExistenciaDto>>(json);
                        if (paquete != null && paquete.items != null) return paquete.items;
                    }
                    return new List<ExistenciaDto>();
                }
                catch { return new List<ExistenciaDto>(); }
            }
        }

        /// <summary>
        /// Busca el primer lote que tenga stock disponible de un producto particular.
        /// </summary>
        public static async Task<ExistenciaDto> BuscarProductoParaEgreso(int productoId)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = $"{BaseUrl}/Existencias?ProductoId={productoId}&OrdenarPor=2&EsDescendente=false&PageNumber=1&PageSize=10";
                    var response = await client.GetAsync(url);

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
        }

        /// <summary>
        /// Consulta existencias que están próximas a vencer (Para métricas del Dashboard).
        /// </summary>
        public static async Task<List<VencimientoDto>> ObtenerVencimientos(int? diasDesde = null, int? diasHasta = null, bool incluirVencidos = false)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = $"{BaseUrl}/Existencias/proximos-vencer?";
                    var parametros = new List<string>();

                    if (diasDesde.HasValue) parametros.Add($"diasDesde={diasDesde.Value}");
                    if (diasHasta.HasValue) parametros.Add($"diasHasta={diasHasta.Value}");
                    parametros.Add($"incluirVencidos={incluirVencidos.ToString().ToLower()}");

                    url += string.Join("&", parametros);

                    var response = await client.GetAsync(url);

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
        }

        // =========================================================================================
        // MÓDULO 4: CATÁLOGO, ESCÁNER Y MAESTROS
        // =========================================================================================

        /// <summary>
        /// Llama a la herramienta "X-Ray Parser" del backend para desarmar códigos de barras largos (EAN-128, etc.).
        /// </summary>
        public static async Task<ParserResponse> ParsearCodigoBarras(string codigoCrudo)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string urlEncode = System.Net.WebUtility.UrlEncode(codigoCrudo);
                    var response = await client.GetAsync($"{BaseUrl}/Parser/analyze?rawCode={urlEncode}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ParserResponse>(json);
                    }
                    else
                    {
                        await MostrarErrorBackend(response, "X-Ray Parser: Código Rechazado");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno de conexión:\n{ex.Message}", "X-Ray Parser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        /// <summary>
        /// Registra un nuevo producto en el catálogo maestro.
        /// </summary>
        public static async Task<int> CrearProductoNuevo(ProductoCreateRequest producto)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(producto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Productos", content);

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
        }

        /// <summary>
        /// Busca productos en el catálogo maestro usando el texto ingresado.
        /// </summary>
        public static async Task<List<ProductoResponseDto>> ObtenerProductosCatalogo(string filtroNombre)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = $"{BaseUrl}/Productos?Nombre={filtroNombre}&PageNumber=1&PageSize=100";
                    var response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var resultado = JsonConvert.DeserializeObject<ProductoPaginadoResponse>(json);
                        if (resultado == null || resultado.items == null) return new List<ProductoResponseDto>();
                        return resultado.items;
                    }
                    else
                    {
                        MessageBox.Show($"El servidor rebotó la búsqueda en el catálogo.\nStatus: {response.StatusCode}\nDetalle:\n{json}", "Error del Backend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return new List<ProductoResponseDto>();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error interno al buscar en el catálogo:\n{ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<ProductoResponseDto>();
                }
            }
        }

        /// <summary>
        /// Intenta crear la marca. Si ya existe, el backend la rechaza pero aseguramos su existencia.
        /// </summary>
        public static async Task<bool> GarantizarMarcaExiste(string nombreMarca)
        {
            using (var client = GetClient())
            {
                var marcaNueva = new { nombre = nombreMarca };
                var json = JsonConvert.SerializeObject(marcaNueva);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{BaseUrl}/Marcas", content);

                // OK (200), Creado (201), Conflicto/Ya Existe (409/400)
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Conflict || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Trae la lista completa de depósitos.
        /// </summary>
        public static async Task<List<DepositoDto>> ObtenerDepositos()
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var response = await client.GetAsync($"{BaseUrl}/Depositos");
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
        }

        /// <summary>
        /// Trae la lista de proveedores.
        /// </summary>
        public static async Task<List<ProveedorDto>> ObtenerProveedores()
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var response = await client.GetAsync($"{BaseUrl}/Proveedores?pageSize=100");
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
        }

        /// <summary>
        /// Crea un proveedor nuevo desde la ventana modal de ingresos.
        /// </summary>
        public static async Task<int> CrearProveedorExpress(ProveedorDto nuevoProveedor)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(nuevoProveedor);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Proveedores", content);

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
        }

        /// <summary>
        /// Trae la lista de clientes para armar remitos de egreso.
        /// </summary>
        public static async Task<List<ClienteDto>> ObtenerClientes()
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var response = await client.GetAsync($"{BaseUrl}/Clientes");
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var objetoJson = JObject.Parse(json);
                        if (objetoJson["items"] != null) return objetoJson["items"].ToObject<List<ClienteDto>>();

                        return JsonConvert.DeserializeObject<List<ClienteDto>>(json);
                    }
                    return new List<ClienteDto>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer la lista de clientes:\n{ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<ClienteDto>();
                }
            }
        }

        /// <summary>
        /// Crea un cliente nuevo desde la ventana modal de egresos.
        /// </summary>
        public static async Task<int> CrearClienteExpress(ClienteDto nuevoCliente)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(nuevoCliente);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{BaseUrl}/Clientes", content);

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
        public static async Task<PaginadoResponse<AuditoriaItemDto>> ObtenerAuditoria(int? tipoMovimiento = null, int pageNumber = 1, int pageSize = 25)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    // 1. Armamos la URL con las variables de paginación dinámicas en vez de fijas
                    string url = $"{BaseUrl}/Auditoria?PageNumber={pageNumber}&PageSize={pageSize}";

                    if (tipoMovimiento.HasValue && tipoMovimiento.Value > 0) url += $"&Tipo={tipoMovimiento.Value}";

                    var response = await client.GetAsync(url);
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
            using (HttpClient client = GetClient())
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
                    var response = await client.GetAsync(url);

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
        }

        public static async Task<int> UpdateEntidad(EntidadDto entidad)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(entidad);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = entidad.TipoEntidad=="Cliente" ? await client.PutAsync($"{BaseUrl}/Clientes/{entidad.IdOriginal}", content) : await client.PutAsync($"{BaseUrl}/Proveedores/{entidad.IdOriginal}", content);

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
        }


    }
    }