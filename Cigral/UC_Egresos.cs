using Cigral.Models;
using Cigral.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cigral
{
    public partial class UC_Egresos : UserControl
    {
        // Variables para el buscador predictivo
        private int idClienteSeleccionado = 0;
        private bool eligiendoDeLista = false;

        public UC_Egresos()
        {
            InitializeComponent();

            //  conexion del buscador de clientes
            txtBuscarCliente.TextChanged += txtBuscarCliente_TextChanged;
            timerBusquedaCliente.Tick += timerBusquedaCliente_Tick;
            lstClientes.Click += lstClientes_Click;

            this.ParentChanged += (s, e) =>
            {
                if (this.Parent == null)
                {
                    GuardarEnCache();
                }
            };
        }

        private void GuardarEnCache()
        {
            CacheOperaciones.CacheEgresos.Clear();
            foreach (DataGridViewRow fila in dgvEgreso.Rows)
            {
                if (fila.IsNewRow) continue;

                int id = 0;
                if (fila.Cells["id"].Value != null) int.TryParse(fila.Cells["id"].Value.ToString(), out id);
                if (id == 0) continue;

                CacheOperaciones.CacheEgresos.Add(new FilaEgresoCache
                {
                    ProductoId = id,
                    Lote = fila.Cells["Lote"].Value?.ToString() ?? "",
                    Vencimiento = fila.Cells["Vencimiento"].Value?.ToString() ?? "",
                    Serie = fila.Cells["Serie"].Value?.ToString() ?? "",
                    Cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value ?? 1)
                });
            }
        }

        private void iconBtnBack_Click(object sender, EventArgs e)
        {
            FormMain principal = this.ParentForm as FormMain;
            if (principal != null)
            {
                principal.ResetearMenu();
            }
            this.Dispose();
        }

        private async void chkConRemito_CheckedChanged(object sender, EventArgs e)
        {
            // Habilitamos el buscador y el botón +
            
            if (chkConRemito.Checked) consignacionCheck.Checked = false;

            txtBuscarCliente.Enabled = chkConRemito.Checked;
            btnAgregarCliente.Enabled = chkConRemito.Checked;
            txtBuscarCliente.Focus();
            txtComprobante.Enabled = chkConRemito.Checked;
            txtRemito.Enabled = chkConRemito.Checked;

            txtRemito.Clear();
            txtBuscarCliente.Clear(); // Limpiamos el texto
            idClienteSeleccionado = 0; // Borramos el ID
            txtComprobante.Clear();
        }

        private void label5_Click(object sender, EventArgs e) { }

        private async void UC_Egresos_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtEscaner;

            // 1. BLOQUEAMOS LA COLUMNA DE NOMBRE 
            if (dgvEgreso.Columns.Contains("Producto"))
            {
                dgvEgreso.Columns["Producto"].ReadOnly = true;
            }

            try
            {
                var depositos = await ApiServices.ObtenerDepositos();
                cmbDeposito.DataSource = depositos;
                cmbDeposito.DisplayMember = "Nombre";
                cmbDeposito.ValueMember = "Id";
                cmbDeposito.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar depósitos: " + ex.Message);
            }

            // 2. LÓGICA DE REVIVIR CACHÉ (Buscando los nombres siempre desde el backend)
            if (CacheOperaciones.CacheEgresos.Count > 0)
            {
                PantallaCarga pc = new PantallaCarga();
                pc.Show(this);
                this.Enabled = false;

                try
                {
                    // A) Obtenemos IDs únicos para no saturar al servidor
                    var idsUnicos = CacheOperaciones.CacheEgresos.Select(x => x.ProductoId).Distinct().ToList();
                    var dicNombres = new Dictionary<int, string>();

                    // B) Traemos los nombres actualizados de la Base de Datos
                    foreach (int id in idsUnicos)
                    {
                        var prodFresco = await ApiServices.ObtenerProductoPorId(id);
                        dicNombres[id] = prodFresco?.nombre ?? "Producto Inaccesible";
                    }

                    // C) Volcamos todo a la grilla de Egresos
                    foreach (var item in CacheOperaciones.CacheEgresos)
                    {
                        int index = dgvEgreso.Rows.Add();
                        DataGridViewRow fila = dgvEgreso.Rows[index];

                        fila.Cells["id"].Value = item.ProductoId;
                        fila.Cells["Producto"].Value = dicNombres.ContainsKey(item.ProductoId) ? dicNombres[item.ProductoId] : "Error";
                        fila.Cells["Lote"].Value = item.Lote;
                        fila.Cells["Vencimiento"].Value = item.Vencimiento;
                        fila.Cells["Serie"].Value = item.Serie;
                        fila.Cells["Cantidad"].Value = item.Cantidad;
                    }

                    // D) Vaciamos la caché local
                    CacheOperaciones.CacheEgresos.Clear();
                }
                finally
                {
                    pc.Close();
                    this.Enabled = true;
                }
            }
        }

        private void CmbDeposito_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbDeposito.SelectedValue != null && int.TryParse(cmbDeposito.SelectedValue.ToString(), out int idSeleccionado))
            {
                // Actualizamos la variable global
                Properties.Settings.Default.UltimoDepositoId = idSeleccionado;

                // Guardamos físicamente el cambio para que sobreviva al cerrar la aplicación
                Properties.Settings.Default.Save();
            }
        }

        // --- BUSCADOR PREDICTIVO DE CLIENTES ---

        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            if (eligiendoDeLista) return;

            idClienteSeleccionado = 0;

            timerBusquedaCliente.Stop();
            timerBusquedaCliente.Start();
        }

        private async void timerBusquedaCliente_Tick(object sender, EventArgs e)
        {
            timerBusquedaCliente.Stop();

            string busqueda = txtBuscarCliente.Text.Trim();

            if (busqueda.Length < 2)
            {
                lstClientes.Visible = false;
                return;
            }

            Cursor = Cursors.WaitCursor;
            try
            {
                // Buscamos en la API
                var listaClientes = await ApiServices.ObtenerClientes(busqueda);

                if (listaClientes != null && listaClientes.Count > 0)
                {
                    lstClientes.DataSource = listaClientes;
                    lstClientes.DisplayMember = "razonSocial";
                    lstClientes.ValueMember = "id";


                    lstClientes.Parent = this; // Hace que la lista sea hija de la pantalla principal, no de los paneles
                    Point posicionTextBox = txtBuscarCliente.Parent.PointToScreen(txtBuscarCliente.Location);
                    lstClientes.Location = this.PointToClient(new Point(posicionTextBox.X, posicionTextBox.Y + txtBuscarCliente.Height));

                    lstClientes.Height = 120;
                    lstClientes.Width = txtBuscarCliente.Width;

                    lstClientes.BringToFront(); // Lo trae al frente de TODO
                    lstClientes.Visible = true;
                }
                else
                {
                    lstClientes.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Ignoramos micro-errores de red mientras escribe
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void lstClientes_SelectedIndexChanged(object sender, EventArgs e) { }

        private void lstClientes_Click(object sender, EventArgs e)
        {
            if (lstClientes.SelectedItem != null)
            {
                eligiendoDeLista = true;

                // ATRAPAMOS EL ID OCULTO
                idClienteSeleccionado = Convert.ToInt32(lstClientes.SelectedValue);

                var cliente = (ClienteDto)lstClientes.SelectedItem;
                txtBuscarCliente.Text = cliente.razonSocial;

                lstClientes.Visible = false;
                eligiendoDeLista = false;
            }
        }

        // --- FIN BUSCADOR DE CLIENTES ---

        private async void txtEscaner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string codigoCrudo = txtEscaner.Text.Trim();
                txtEscaner.Clear();

                if (string.IsNullOrEmpty(codigoCrudo)) return;

                // --- NUEVA LÓGICA: CÓDIGOS DIVIDIDOS ---
                // Si mide exactamente 16 y arranca con 01 (Indicador GS1 para GTIN)
                if (codigoCrudo.Length == 16 && codigoCrudo.StartsWith("01"))
                {
                    string codigoConcatenado = PedirCodigoComplementario(codigoCrudo);

                    if (string.IsNullOrEmpty(codigoConcatenado))
                    {
                        // El usuario canceló la operación en el modal
                        txtEscaner.Focus();
                        return;
                    }

                    // Reemplazamos el código original corto, por la versión gigante concatenada
                    codigoCrudo = codigoConcatenado;
                }
                // ---------------------------------------

                Cursor = Cursors.WaitCursor;

                // 1. LLAMA AL PARSER (Ahora recibirá el código completo)

                try
                {
                    var productoParseado = await ApiServices.ParsearCodigoBarras(codigoCrudo);

                    if (productoParseado != null)
                    {
                        if (!productoParseado.ExisteProducto)
                        {
                            MessageBox.Show("Este producto no está registrado en el sistema. No se puede egresar.", "Producto Desconocido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var existenciasPosibles = await ApiServices.ObtenerExistencias(productoId: productoParseado.ProductoId, numeroSerie: productoParseado.NumeroSerie, lote: productoParseado.Lote);
                        if (existenciasPosibles == null)
                        {
                            MessageBox.Show("No se encontró stock disponible de este producto (con ese Lote/Serie) en el sistema.", "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var existenciaIndicada = existenciasPosibles.items.FirstOrDefault(x =>
                            x.ProductoId == productoParseado.ProductoId && // El ProductoId siempre debe coincidir
                            (String.IsNullOrEmpty(productoParseado.Lote) || x.CodigoLote == productoParseado.Lote) &&
                            (String.IsNullOrEmpty(productoParseado.NumeroSerie) || x.NumSerie == productoParseado.NumeroSerie) // Aquí evalúas NumeroSerie
                        );

                        if (existenciaIndicada != null)
                        {
                            cmbDeposito.SelectedValue = existenciaIndicada.DepositoId;

                            string codigoLote = string.IsNullOrEmpty(existenciaIndicada.CodigoLote) ? "" : existenciaIndicada.CodigoLote;
                            string numeroSerie = string.IsNullOrEmpty(existenciaIndicada.NumSerie) ? "" : existenciaIndicada.NumSerie;

                            int cantidadFinalEscaneada = existenciaIndicada.Cantidad;

                            // LÓGICA DE CANTIDADES:
                            // Si tiene número de serie, es un ítem único, la cantidad siempre es 1.
                            // Si NO tiene serie (es un lote a granel) y trae más de 1, preguntamos cuántas sacar.
                            if (!string.IsNullOrEmpty(numeroSerie))
                            {
                                cantidadFinalEscaneada = 1;
                            }
                            else if (cantidadFinalEscaneada > 1)
                            {
                                int elegida = PedirCantidadSueltas(existenciaIndicada.ProductoNombre, cantidadFinalEscaneada, codigoLote, numeroSerie);
                                if (elegida == 0) return; // El usuario canceló
                                cantidadFinalEscaneada = elegida;
                            }

                            bool productoYaEnGrilla = false;

                            // VALIDACIÓN CONTRA LA GRILLA
                            foreach (DataGridViewRow filaExistente in dgvEgreso.Rows)
                            {
                                if (filaExistente.IsNewRow) continue;

                                int idFila = Convert.ToInt32(filaExistente.Cells["id"].Value);
                                string loteFila = filaExistente.Cells["Lote"].Value?.ToString() ?? "";
                                string serieFila = filaExistente.Cells["Serie"].Value?.ToString() ?? "";

                                if (idFila == existenciaIndicada.ProductoId)
                                {
                                    // CASO A: El producto tiene un número de serie específico
                                    if (!string.IsNullOrEmpty(numeroSerie))
                                    {
                                        if (serieFila == numeroSerie && serieFila != "Sin Número de Serie")
                                        {
                                            MessageBox.Show($"El producto con número de serie '{numeroSerie}' ya fue escaneado y está en la lista.", "Serie Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return; // Bloqueamos el doble escaneo de una misma serie
                                        }
                                    }
                                    // CASO B: El producto se maneja por Lote (A granel, sin serie)
                                    else if (loteFila == codigoLote)
                                    {
                                        int cantidadActual = Convert.ToInt32(filaExistente.Cells["Cantidad"].Value);

                                        if ((cantidadActual + cantidadFinalEscaneada) > existenciaIndicada.Cantidad)
                                        {
                                            MessageBox.Show($"No podés retirar más cantidad. El stock máximo disponible para el lote {codigoLote} es {existenciaIndicada.Cantidad}.", "Límite de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        else
                                        {
                                            filaExistente.Cells["Cantidad"].Value = cantidadActual + cantidadFinalEscaneada;
                                        }

                                        productoYaEnGrilla = true;
                                        break;
                                    }
                                }
                            }

                            // SI NO ESTABA EN LA GRILLA, LO CREAMOS (Ahora sí, incluyendo la serie)
                            if (!productoYaEnGrilla)
                            {
                                int indexNuevaFila = dgvEgreso.Rows.Add();
                                DataGridViewRow fila = dgvEgreso.Rows[indexNuevaFila];
                                fila.Cells["ExistenciaId"].Value = existenciaIndicada.Id;
                                fila.Cells["id"].Value = existenciaIndicada.ProductoId;
                                fila.Cells["Producto"].Value = existenciaIndicada.ProductoNombre;
                                fila.Cells["Lote"].Value = codigoLote;
                                fila.Cells["Serie"].Value = numeroSerie; // ¡Línea corregida!

                                if (existenciaIndicada.FechaVencimiento.HasValue)
                                {
                                    fila.Cells["Vencimiento"].Value = existenciaIndicada.FechaVencimiento.Value.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    fila.Cells["Vencimiento"].Value = "-";
                                }

                                fila.Cells["Cantidad"].Value = cantidadFinalEscaneada;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontró stock disponible de este producto (con ese Lote/Serie) en el sistema.", "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar el escaneo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                    txtEscaner.Focus();
                }
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cmbDeposito.SelectedIndex == -1 || cmbDeposito.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un depósito.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvEgreso.Rows.Count == 0 || (dgvEgreso.Rows.Count == 1 && dgvEgreso.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay productos escaneados para egresar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // AHORA VALIDAMOS EL ID SELECCIONADO EN VEZ DEL COMBO
            if (chkConRemito.Checked && idClienteSeleccionado == 0)
            {
                MessageBox.Show("Si el egreso es con remito, debe buscar y seleccionar un cliente de la lista.", "Falta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscarCliente.Focus();
                return;
            }

            foreach (DataGridViewRow fila in dgvEgreso.Rows)
            {
                if (fila.IsNewRow) continue;

                /*string fechaStr = fila.Cells["Vencimiento"].Value?.ToString();
                if (!string.IsNullOrEmpty(fechaStr) && fechaStr != "-")
                {
                    if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaParseada))
                    {
                        if (fechaParseada.Date < DateTime.Now.Date)
                        {
                            string nombreProd = fila.Cells["Producto"].Value?.ToString();
                            string loteProd = fila.Cells["Lote"].Value?.ToString();

                            MessageBox.Show($"¡OPERACIÓN BLOQUEADA!\n\nEl producto '{nombreProd}' (Lote: {loteProd}) se encuentra vencido desde el {fechaStr}.\n\nPor normas de seguridad, no se puede egresar mercadería vencida. Se retirará el producto de la lista.", "Producto Vencido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            dgvEgreso.Rows.Remove(fila);
                            return;
                        }
                    }
                }*/
            }

            var confirmacion = MessageBox.Show("¿Desea confirmar este egreso?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.No) return;

            btnConfirmar.Enabled = false;
            this.Enabled = false;
            Cursor = Cursors.WaitCursor;
            PantallaCarga pantallaCarga = new PantallaCarga();
            pantallaCarga.Show(this);
            int depositoSeleccionado = (int)cmbDeposito.SelectedValue;

            try
            {
                foreach (DataGridViewRow fila in dgvEgreso.Rows)
                {
                    fila.DefaultCellStyle.BackColor = Color.White;
                }

                if (consignacionCheck.Checked)
                {
                    int itemsProcesados = 0;
                    int itemsConError = 0;

                    List<DataGridViewRow> filasParaBorrar = new List<DataGridViewRow>();
                    List<string> nombresFallidos = new List<string>();

                    foreach (DataGridViewRow fila in dgvEgreso.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        var consignacion = new ConsignacionAddDto
                        {
                            existenciaId = Convert.ToInt32(fila.Cells["ExistenciaId"].Value),
                            clienteId = idClienteSeleccionado,
                            cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value)
                        };

                        var response = await ApiServices.AumentarConsignacion(consignacion);
                        if (response != null)
                        {
                            itemsProcesados++;
                            filasParaBorrar.Add(fila);
                        }
                        else
                        {
                            itemsConError++;
                            fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
                        }

                        foreach (var filaExito in filasParaBorrar)
                        {
                            dgvEgreso.Rows.Remove(filaExito);
                        }

                        if (itemsConError == 0)
                        {
                            MessageBox.Show($"¡Consignaciones creadas/agregadas exitosamente! ({itemsProcesados} items).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarPantalla();
                        }
                        else
                        {
                            string mensajeAviso = $"Se procesaron {itemsProcesados} consignaciones correctamente.\n\nSin embargo, fallaron {itemsConError} productos:\n";
                        }
                    }
                }
                else if (chkConRemito.Checked)
                {
                    var remitoNuevo = new RemitoEgresoDto
                    {
                        depositoId = depositoSeleccionado,
                        entidadId = idClienteSeleccionado, // MANDAMOS EL ID QUE ATRAPAMOS
                        numeroRemito = txtRemito.Text,
                        observaciones = "Generado por sistema",
                        comprobanteAsociado = txtComprobante.Text.Trim(),
                        detalles = new List<RemitoEgresoDetalleDto>()
                    };

                    foreach (DataGridViewRow fila in dgvEgreso.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        var detalle = new RemitoEgresoDetalleDto
                        {
                            productoId = Convert.ToInt32(fila.Cells["Id"].Value),
                            codigoLote = fila.Cells["Lote"].Value?.ToString() ?? "",
                            numeroSerie = fila.Cells["Serie"].Value?.ToString() != "Sin Número de Serie" ? fila.Cells["Serie"].Value?.ToString() : null,
                            cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value)
                        };

                        string fechaStr = fila.Cells["Vencimiento"].Value?.ToString();
                        if (!string.IsNullOrEmpty(fechaStr) && fechaStr != "-")
                        {
                            if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaParseada))
                            {
                                detalle.fechaVencimiento = fechaParseada;
                            }
                        }
                        remitoNuevo.detalles.Add(detalle);
                    }

                    int idRemitoGenerado = await ApiServices.CrearRemitoEgreso(remitoNuevo);

                    if (idRemitoGenerado > 0)
                    {
                        MessageBox.Show("¡Remito creado y stock descontado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await ApiServices.DescargarAbrirPdfRemito(idRemitoGenerado);
                        LimpiarPantalla();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al generar el remito en el servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    int itemsProcesados = 0;
                    int itemsConError = 0;

                    List<DataGridViewRow> filasParaBorrar = new List<DataGridViewRow>();
                    List<string> nombresFallidos = new List<string>();

                    foreach (DataGridViewRow fila in dgvEgreso.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        var dto = new DisminuirStockDto
                        {
                            depositoId = depositoSeleccionado,
                            productoId = Convert.ToInt32(fila.Cells["Id"].Value),
                            numSerie = fila.Cells["Serie"].Value?.ToString() == "Sin Número de Serie" ? null : fila.Cells["Serie"].Value?.ToString(),
                            codigoLote = fila.Cells["Lote"].Value?.ToString() ?? "",
                            cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value),
                            informacionAdicional = ""
                        };

                        string fechaStr = fila.Cells["Vencimiento"].Value?.ToString();
                        if (!string.IsNullOrEmpty(fechaStr) && fechaStr != "-")
                        {
                            if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
                            {
                                dto.fechaVencimiento = fecha;
                            }
                        }

                        string nombreProd = fila.Cells["Producto"].Value?.ToString() ?? "Desconocido";
                        bool exito = await ApiServices.DisminuirStock(dto, nombreProd);

                        if (exito)
                        {
                            itemsProcesados++;
                            filasParaBorrar.Add(fila);
                        }
                        else
                        {
                            itemsConError++;
                            fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
                            nombresFallidos.Add(nombreProd);
                        }
                    }

                    foreach (var filaExito in filasParaBorrar)
                    {
                        dgvEgreso.Rows.Remove(filaExito);
                    }

                    if (itemsConError == 0)
                    {
                        MessageBox.Show($"¡Stock descontado exitosamente! ({itemsProcesados} items).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarPantalla();
                    }
                    else
                    {
                        string mensajeAviso = $"Se procesaron {itemsProcesados} productos correctamente.\n\nSin embargo, fallaron {itemsConError} productos:\n";
                        foreach (var nombre in nombresFallidos)
                        {
                            mensajeAviso += $"- {nombre}\n";
                        }
                        MessageBox.Show(mensajeAviso, "Aviso Parcial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UnprocessableEntity") && ex.Message.Contains("{"))
                {
                    try
                    {
                        int startIndex = ex.Message.IndexOf("{");
                        string jsonError = ex.Message.Substring(startIndex);

                        var errorObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Cigral.Models.ValidationErrorResponse>(jsonError);

                        if (errorObj != null && errorObj.errores != null && errorObj.errores.Count > 0)
                        {
                            string mensajeLimpio = errorObj.mensaje + "\n\n";

                            foreach (var detalle in errorObj.errores)
                            {
                                mensajeLimpio += $"- {detalle.mensaje}\n";

                                int indiceFila = detalle.orden;
                                if (indiceFila >= 0 && indiceFila < dgvEgreso.Rows.Count)
                                {
                                    dgvEgreso.Rows[indiceFila].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
                                }
                            }

                            MessageBox.Show(mensajeLimpio, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch { }
                }

                MessageBox.Show("Error crítico al procesar la operación:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnConfirmar.Enabled = true;
                this.Enabled = true;
                pantallaCarga.Close();
            }
        }

        private void LimpiarPantalla()
        {
            dgvEgreso.Rows.Clear();
            chkConRemito.Checked = false;
            txtRemito.Clear();
            txtBuscarCliente.Clear();
            idClienteSeleccionado = 0;
            txtEscaner.Focus();
        }

        private async void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 350,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Alta de Cliente Nuevo",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label lblRazon = new Label() { Left = 20, Top = 20, Width = 100, Text = "Razón Social:" }; TextBox txtRazon = new TextBox() { Left = 130, Top = 20, Width = 230 };
            Label lblGln = new Label() { Left = 20, Top = 50, Width = 100, Text = "GLN (13 núm):" }; TextBox txtGln = new TextBox() { Left = 130, Top = 50, Width = 230 };
            Label lblEmail = new Label() { Left = 20, Top = 80, Width = 100, Text = "Email:" }; TextBox txtEmail = new TextBox() { Left = 130, Top = 80, Width = 230 };
            Label lblCuit = new Label() { Left = 20, Top = 110, Width = 100, Text = "CUIT:" }; TextBox txtCuit = new TextBox() { Left = 130, Top = 110, Width = 230 };
            Label lblTel = new Label() { Left = 20, Top = 140, Width = 100, Text = "Teléfono:" }; TextBox txtTel = new TextBox() { Left = 130, Top = 140, Width = 230 };
            Label lblDir = new Label() { Left = 20, Top = 170, Width = 100, Text = "Dirección:" }; TextBox txtDir = new TextBox() { Left = 130, Top = 170, Width = 230 };
            Button confirmation = new Button() { Text = "Guardar", Left = 260, Top = 220, Width = 100, DialogResult = DialogResult.OK };

            prompt.Controls.Add(lblRazon); prompt.Controls.Add(txtRazon);
            prompt.Controls.Add(lblGln); prompt.Controls.Add(txtGln);
            prompt.Controls.Add(lblEmail); prompt.Controls.Add(txtEmail);
            prompt.Controls.Add(lblCuit); prompt.Controls.Add(txtCuit);
            prompt.Controls.Add(lblTel); prompt.Controls.Add(txtTel);
            prompt.Controls.Add(lblDir); prompt.Controls.Add(txtDir);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtRazon.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("La Razón Social y el Email son obligatorios.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                try
                {
                    var nuevoCliente = new ClienteDto
                    {
                        razonSocial = txtRazon.Text.Trim(),
                        gln = txtGln.Text.Trim(),
                        email = txtEmail.Text.Trim(),
                        cuit = txtCuit.Text.Trim(),
                        telefono = txtTel.Text.Trim(),
                        direccion = txtDir.Text.Trim()
                    };

                    int idNuevo = await ApiServices.CrearClienteExpress(nuevoCliente);

                    if (idNuevo > 0)
                    {
                        // AUTO-SELECCIONA AL CLIENTE RECIÉN CREADO EN LA CAJITA NUEVA
                        eligiendoDeLista = true;
                        txtBuscarCliente.Text = txtRazon.Text;
                        idClienteSeleccionado = idNuevo;
                        eligiendoDeLista = false;

                        MessageBox.Show("¡Cliente creado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Cursor = Cursors.Default; }
            }
        }

        private int PedirCantidadSueltas(string nombreProducto, int cantidadEnCaja, string? codigoLote = null, string? numeroSerie = null)
        {
            int cantidadElegida = 0;
            Form prompt = new Form()
            {
                Width = 400,   // Lo ensanchamos un poquito (de 380 a 400) para nombres largos
                Height = 240,  // Aumentamos el alto (de 180 a 240) para que entren los nuevos controles
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Apertura de caja detectada",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Arreglamos la concatenación agregando los saltos de línea (\n) correspondientes
            string mensaje = $"El código pertenece a '{nombreProducto}'.";
            if (!string.IsNullOrEmpty(codigoLote)) mensaje += $"\nLote: {codigoLote}";
            if (!string.IsNullOrEmpty(numeroSerie)) mensaje += $"\nSerie: {numeroSerie}";
            mensaje += $"\n\nTrae {cantidadEnCaja} unidades. ¿Cuántas unidades sueltas vas a retirar?";

            // Aumentamos el Height del Label de 40 a 90 para que entren hasta 6 líneas de texto sin cortarse
            Label lblTexto = new Label() { Left = 20, Top = 20, Width = 340, Height = 90, Text = mensaje };

            // Bajamos el input y el botón (Top pasó de 70 a 120) para que no se encimen con el Label más grande
            NumericUpDown inputNum = new NumericUpDown() { Left = 20, Top = 120, Width = 120, Minimum = 1, Maximum = cantidadEnCaja, Value = 1, Font = new Font("Segoe UI", 12) };
            Button btnAceptar = new Button() { Text = "Aceptar", Left = 240, Top = 120, Width = 100, DialogResult = DialogResult.OK };

            prompt.Controls.Add(lblTexto);
            prompt.Controls.Add(inputNum);
            prompt.Controls.Add(btnAceptar);

            prompt.AcceptButton = btnAceptar;

            if (prompt.ShowDialog() == DialogResult.OK) cantidadElegida = (int)inputNum.Value;
            return cantidadElegida;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form buscadorStock = new Form()
            {
                Width = 900,
                Height = 400,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Buscar Stock en Estantes",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            // 1. REORGANIZAMOS LOS CONTROLES SUPERIORES PARA QUE ENTREN LOS 3 CAMPOS
            Label lblInfo = new Label() { Left = 20, Top = 15, Width = 60, Text = "Nombre:" };
            TextBox txtBuscar = new TextBox() { Left = 80, Top = 12, Width = 250 }; // Redujimos el ancho

            Label lblLote = new Label() { Left = 340, Top = 15, Width = 40, Text = "Lote:" };
            TextBox txtLote = new TextBox() { Left = 380, Top = 12, Width = 150 };

            Label lblSerie = new Label() { Left = 540, Top = 15, Width = 45, Text = "Serie:" };
            TextBox txtSerie = new TextBox() { Left = 585, Top = 12, Width = 150 };

            DataGridView dgvResultados = new DataGridView()
            {
                Left = 20,
                Top = 50,
                Width = 840,
                Height = 280,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MultiSelect = false,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.FixedSingle,
                GridColor = Color.LightGray
            };

            dgvResultados.DataBindingComplete += (senderGrid, ev) =>
            {
                foreach (DataGridViewColumn col in dgvResultados.Columns)
                {
                    if (col.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) ||
                        col.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                        col.Name == "ProductoSinCodigo")
                    {
                        col.Visible = false;
                    }

                    if (col.Name == "Id") { col.HeaderText = "id"; col.Visible = false; };
                    if (col.Name == "ProductoNombre") col.HeaderText = "Producto";
                    if (col.Name == "ProductoCodigo") col.HeaderText = "Código";
                    if (col.Name == "DepositoNombre") { col.HeaderText = "Depósito"; col.Width = 80; }
                    if (col.Name == "CodigoLote") { col.HeaderText = "Lote"; col.Width = 80; }
                    if (col.Name == "NumSerie") { col.HeaderText = "Serie"; col.Width = 100; }
                    if (col.Name == "FechaVencimiento") { col.HeaderText = "Vencimiento"; col.Width = 80; }
                    if (col.Name == "Cantidad") { col.HeaderText = "Stock"; col.Width = 50; }
                    if (col.Name == "ProductoGtin") { col.HeaderText = "GTIN"; col.Width = 100; }
                    if (col.Name == "productoCodigoInterno") { col.HeaderText = "Cod. Int."; col.Width = 100; }
                }
            };

            buscadorStock.Controls.Add(lblInfo);
            buscadorStock.Controls.Add(txtBuscar);
            // 2. AGREGAMOS LOS NUEVOS CONTROLES AL FORMULARIO
            buscadorStock.Controls.Add(lblLote);
            buscadorStock.Controls.Add(txtLote);
            buscadorStock.Controls.Add(lblSerie);
            buscadorStock.Controls.Add(txtSerie);

            buscadorStock.Controls.Add(dgvResultados);

            buscadorStock.Load += async (s, args) =>
            {
                buscadorStock.Cursor = Cursors.WaitCursor;
                try
                {
                    var stockFisico = await ApiServices.ObtenerExistencias();
                    if (stockFisico != null && stockFisico.items != null)
                    {
                        dgvResultados.DataSource = stockFisico.items.ToList();
                    }
                }
                catch { }
                finally { buscadorStock.Cursor = Cursors.Default; }
            };

            Func<Task> realizarBusqueda = async () =>
            {
                if (buscadorStock.IsDisposed) return;

                buscadorStock.Cursor = Cursors.WaitCursor;
                try
                {
                    // 3. PASAMOS LOS VALORES DE LOTE Y SERIE AL MÉTODO DE LA API
                    var stockFisico = await ApiServices.ObtenerExistencias(
                        buscar: txtBuscar.Text.Trim(),
                        ocultarCero: false,
                        soloVencidos: false,
                        lote: txtLote.Text.Trim(),     // <- Nuevo parámetro
                        numeroSerie: txtSerie.Text.Trim()    // <- Nuevo parámetro
                    );

                    if (buscadorStock.IsDisposed) return;

                    if (stockFisico != null && stockFisico.items != null)
                    {
                        dgvResultados.DataSource = stockFisico.items.ToList();
                    }
                    else
                    {
                        dgvResultados.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    if (!buscadorStock.IsDisposed) MessageBox.Show("Error al buscar: " + ex.Message);
                }
                finally
                {
                    if (!buscadorStock.IsDisposed) buscadorStock.Cursor = Cursors.Default;
                }
            };

            System.Windows.Forms.Timer timerBusquedaModal = new System.Windows.Forms.Timer();
            timerBusquedaModal.Interval = 200;

            timerBusquedaModal.Tick += async (s, ev) =>
            {
                timerBusquedaModal.Stop();
                await realizarBusqueda();
            };

            // 4. ASIGNAMOS EVENTOS A LOS NUEVOS TEXTBOXES PARA LA BÚSQUEDA EN TIEMPO REAL
            EventHandler onTextChanged = (s, ev) =>
            {
                timerBusquedaModal.Stop();
                timerBusquedaModal.Start();
            };

            txtBuscar.TextChanged += onTextChanged;
            txtLote.TextChanged += onTextChanged;
            txtSerie.TextChanged += onTextChanged;

            KeyEventHandler onKeyDown = async (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    timerBusquedaModal.Stop();
                    await realizarBusqueda();
                }
            };

            txtBuscar.KeyDown += onKeyDown;
            txtLote.KeyDown += onKeyDown;
            txtSerie.KeyDown += onKeyDown;

            dgvResultados.CellDoubleClick += (s, args) =>
            {
                if (args.RowIndex < 0) return;
                var filaStock = dgvResultados.Rows[args.RowIndex];

                int cantDisponible = Convert.ToInt32(filaStock.Cells["Cantidad"].Value);

                if (cantDisponible <= 0)
                {
                    MessageBox.Show("Este producto no tiene stock disponible (Cantidad 0). No se puede egresar.", "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                string fechaVencStr = "-";
                if (filaStock.Cells["FechaVencimiento"].Value != null)
                {
                    DateTime fechaV = Convert.ToDateTime(filaStock.Cells["FechaVencimiento"].Value);
                    fechaVencStr = fechaV.ToString("dd/MM/yyyy");

                    /*if (fechaV.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Este lote ya se encuentra vencido en el estante.\nPor seguridad, no se puede egresar.", "Stock Vencido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    */
                }
                int idExistencia = Convert.ToInt32(filaStock.Cells["id"].Value);
                int idProd = Convert.ToInt32(filaStock.Cells["ProductoId"].Value);
                string nombre = filaStock.Cells["ProductoNombre"].Value?.ToString() ?? "Desconocido";
                string lote = filaStock.Cells["CodigoLote"].Value?.ToString() ?? "";

                string serie = "";
                if (dgvResultados.Columns.Contains("NumSerie"))
                {
                    serie = filaStock.Cells["NumSerie"].Value?.ToString() ?? "";
                }

                if (filaStock.Cells["DepositoId"].Value != null)
                {
                    cmbDeposito.SelectedValue = Convert.ToInt32(filaStock.Cells["DepositoId"].Value);
                }

                int aRetirar = PedirCantidadSueltas(nombre, cantDisponible, lote, serie);
                if (aRetirar == 0) return;

                bool yaExiste = false;
                foreach (DataGridViewRow filaMain in dgvEgreso.Rows)
                {
                    if (filaMain.IsNewRow) continue;

                    int idExistente = Convert.ToInt32(filaMain.Cells["ExistenciaId"].Value);
                    string loteExistente = filaMain.Cells["Lote"].Value?.ToString() ?? "";
                    string serieExistente = filaMain.Cells["Serie"].Value?.ToString() ?? "";

                    if (idExistente == idExistencia)
                    {
                        if (!string.IsNullOrEmpty(serie))
                        {
                            if (serieExistente == serie && serieExistente != "Sin Número de Serie")
                            {
                                MessageBox.Show($"Este producto con serie '{serie}' ya está en la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (loteExistente == lote)
                        {
                            int cantActualEnGrilla = Convert.ToInt32(filaMain.Cells["Cantidad"].Value);
                            if ((cantActualEnGrilla + aRetirar) > cantDisponible)
                            {
                                MessageBox.Show($"Límite alcanzado. Solo hay {cantDisponible} unidades disponibles de este lote.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                filaMain.Cells["Cantidad"].Value = cantActualEnGrilla + aRetirar;
                            }
                            yaExiste = true;
                            break;
                        }
                    }
                }

                if (!yaExiste)
                {
                    int indexNuevaFila = dgvEgreso.Rows.Add();
                    DataGridViewRow filaMain = dgvEgreso.Rows[indexNuevaFila];
                    filaMain.Cells["ExistenciaId"].Value = idExistencia;
                    filaMain.Cells["id"].Value = idProd;
                    filaMain.Cells["Producto"].Value = nombre;
                    filaMain.Cells["Lote"].Value = lote;
                    filaMain.Cells["Serie"].Value = serie;
                    filaMain.Cells["Vencimiento"].Value = fechaVencStr;
                    filaMain.Cells["Cantidad"].Value = aRetirar;
                }

                buscadorStock.DialogResult = DialogResult.OK;
                buscadorStock.Close();
            };

            buscadorStock.ShowDialog();
        }

        private void dgvEgreso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvEgreso.Columns[e.ColumnIndex].Name == "ColAcciones")
            {
                DialogResult respuesta = MessageBox.Show("¿Quitar este producto de la lista de egreso?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    dgvEgreso.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validar si hay algo para borrar. 

            // así que sumamos validación para que no moleste si la pantalla está recién abierta.
            bool grillaVacia = dgvEgreso.Rows.Count == 0 || (dgvEgreso.Rows.Count == 1 && dgvEgreso.Rows[0].IsNewRow);

            if (grillaVacia && string.IsNullOrWhiteSpace(txtRemito.Text)) return;


            var confirmacion = MessageBox.Show(
                "¿Desea cancelar todo el egreso actual?\n\nSe perderán todos los productos escaneados y la configuración del remito.",
                "Cancelar Egreso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2); // Foco en "NO" por seguridad para evitar clics accidentales

            if (confirmacion == DialogResult.Yes)
            {
                // Llamamos a la función que ya tenés creada más abajo para dejar todo en blanco
                LimpiarPantalla();
            }

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Levanta un modal para concatenar códigos de barras complementarios al GTIN base.
        /// </summary>
        private string PedirCodigoComplementario(string codigoBase)
        {
            // Limpiamos la base por las dudas
            string codigoFinal = codigoBase != null ? codigoBase.Trim() : "";

            Form prompt = new Form()
            {
                Width = 450,
                Height = 250, // ⬆️ AUMENTADO: Le dimos 30px más de alto a la ventana para que entre todo bien
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Código Dividido Detectado",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblInfo = new Label()
            {
                Left = 20,
                Top = 15,
                Width = 400,
                Height = 40,
                Text = "Se detectó un GTIN base. Escaneá los códigos complementarios (Lote, Vencimiento, Serie). Podés escanear varios antes de confirmar."
            };

            Label lblBase = new Label()
            {
                Left = 20,
                Top = 60,
                Width = 400,
                Height = 45, // Ocupa desde Y=60 hasta Y=105
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Text = $"Código actual: {codigoFinal}"
            };

            // ⬇️ BAJADO: Lo movimos de Top=85 a Top=115 para que quede debajo del Label
            TextBox txtScanExtra = new TextBox() { Left = 20, Top = 115, Width = 390 };

            // ⬇️ BAJADOS: Movimos los botones de Top=130 a Top=160
            // ATENCIÓN: Le quitamos el DialogResult automático al botón confirmar
            Button btnConfirmar = new Button() { Text = "Confirmar", Left = 210, Top = 160, Width = 100, Cursor = Cursors.Hand };
            Button btnCancelar = new Button() { Text = "Cancelar", Left = 320, Top = 160, Width = 90, DialogResult = DialogResult.Cancel, Cursor = Cursors.Hand };

            txtScanExtra.KeyDown += (senderObj, eArgs) =>
            {
                if (eArgs.KeyCode == Keys.Enter)
                {
                    eArgs.SuppressKeyPress = true;
                    string extra = txtScanExtra.Text.Trim();

                    if (!string.IsNullOrEmpty(extra))
                    {
                        codigoFinal += extra;
                        lblBase.Text = $"Código actual: {codigoFinal}";
                        txtScanExtra.Clear();
                    }
                }
            };

            // Lógica para atrapar textos pegados sin Enter
            btnConfirmar.Click += (sender, e) =>
            {
                string extra = txtScanExtra.Text.Trim();
                if (!string.IsNullOrEmpty(extra))
                {
                    codigoFinal += extra;
                }

                prompt.DialogResult = DialogResult.OK;
                prompt.Close();
            };

            prompt.Controls.Add(lblInfo);
            prompt.Controls.Add(lblBase);
            prompt.Controls.Add(txtScanExtra);
            prompt.Controls.Add(btnConfirmar);
            prompt.Controls.Add(btnCancelar);

            prompt.CancelButton = btnCancelar;
            prompt.Shown += (s, e) => txtScanExtra.Focus();

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return codigoFinal;
            }

            return null;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtComprobante_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRemito_TextChanged(object sender, EventArgs e)
        {

        }

        private void consignacionCheck_CheckedChanged(object sender, EventArgs e)
        {
            
            if (consignacionCheck.Checked) chkConRemito.Checked = false;

            // Habilitamos el buscador y el botón +
            txtBuscarCliente.Enabled = consignacionCheck.Checked;
            btnAgregarCliente.Enabled = consignacionCheck.Checked;
            txtBuscarCliente.Focus();

            txtRemito.Clear();
            txtBuscarCliente.Clear(); // Limpiamos el texto
            idClienteSeleccionado = 0; // Borramos el ID
            txtComprobante.Clear();
        }
    }
}