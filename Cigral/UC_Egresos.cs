using Cigral.Models;
using Cigral.Services;
using System;
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

            // Conectamos los cables del buscador de clientes
            txtBuscarCliente.TextChanged += txtBuscarCliente_TextChanged;
            timerBusquedaCliente.Tick += timerBusquedaCliente_Tick;
            lstClientes.Click += lstClientes_Click;
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
            if (chkConRemito.Checked)
            {
                // Habilitamos el buscador y el botón +
                txtBuscarCliente.Enabled = true;
                btnAgregarCliente.Enabled = true;
                txtBuscarCliente.Focus();
                txtComprobante.Enabled = chkConRemito.Checked;

                Cursor = Cursors.WaitCursor;
                try
                {
                    int idDepositoSeleccionado = cmbDeposito.SelectedValue != null ? (int)cmbDeposito.SelectedValue : 1;
                    txtRemito.Text = await ApiServices.ObtenerSiguienteNumeroRemito(idDepositoSeleccionado, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkConRemito.Checked = false;
                }
                finally { Cursor = Cursors.Default; }
            }
            else
            {
                txtRemito.Clear();
                txtBuscarCliente.Clear(); // Limpiamos el texto
                idClienteSeleccionado = 0; // Borramos el ID
                txtBuscarCliente.Enabled = false; // Bloqueamos el buscador
                btnAgregarCliente.Enabled = false; // Bloqueamos el +
                txtComprobante.Clear();
            }
        }

        private void label5_Click(object sender, EventArgs e) { }

        private async void UC_Egresos_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtEscaner;

            try
            {
                var depositos = await ApiServices.ObtenerDepositos();
                cmbDeposito.DataSource = depositos;
                cmbDeposito.DisplayMember = "Nombre";
                cmbDeposito.ValueMember = "Id";
                cmbDeposito.SelectedIndex = -1;

                // Ya no cargamos todos los clientes de golpe acá, porque tenemos el buscador en vivo
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar depósitos: " + ex.Message);
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
                e.Handled = true;

                string codigoCrudo = txtEscaner.Text.Trim();
                txtEscaner.Clear();

                if (string.IsNullOrEmpty(codigoCrudo)) return;

                Cursor = Cursors.WaitCursor;

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

                        var productoIdeal = await ApiServices.BuscarProductoParaEgreso(productoParseado.ProductoId.Value);

                        if (productoIdeal != null)
                        {
                            cmbDeposito.SelectedValue = productoIdeal.DepositoId;

                            int cantidadFinalEscaneada = productoParseado.Cantidad > 0 ? productoParseado.Cantidad : 1;

                            if (cantidadFinalEscaneada > 1)
                            {
                                int elegida = PedirCantidadSueltas(productoIdeal.ProductoNombre, cantidadFinalEscaneada);

                                if (elegida == 0) return;

                                cantidadFinalEscaneada = elegida;
                            }

                            bool productoYaEnGrilla = false;

                            foreach (DataGridViewRow filaExistente in dgvEgreso.Rows)
                            {
                                if (filaExistente.IsNewRow) continue;

                                int idFila = Convert.ToInt32(filaExistente.Cells["id"].Value);

                                if (idFila == productoIdeal.ProductoId && filaExistente.Cells["Lote"].Value?.ToString() == productoIdeal.CodigoLote)
                                {
                                    int cantidadActual = Convert.ToInt32(filaExistente.Cells["Cantidad"].Value);
                                    int cantidadNueva = cantidadFinalEscaneada;

                                    if ((cantidadActual + cantidadNueva) > productoIdeal.Cantidad)
                                    {
                                        MessageBox.Show($"No podés retirar más cantidad. El stock máximo disponible para el lote {productoIdeal.CodigoLote} es {productoIdeal.Cantidad}.", "Límite de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        filaExistente.Cells["Cantidad"].Value = cantidadActual + cantidadNueva;
                                    }

                                    productoYaEnGrilla = true;
                                    break;
                                }
                            }

                            if (!productoYaEnGrilla)
                            {
                                int indexNuevaFila = dgvEgreso.Rows.Add();
                                DataGridViewRow fila = dgvEgreso.Rows[indexNuevaFila];

                                fila.Cells["id"].Value = productoIdeal.ProductoId;
                                fila.Cells["Producto"].Value = productoIdeal.ProductoNombre;
                                fila.Cells["Lote"].Value = productoIdeal.CodigoLote;

                                if (productoIdeal.FechaVencimiento.HasValue)
                                {
                                    fila.Cells["Vencimiento"].Value = productoIdeal.FechaVencimiento.Value.ToString("dd/MM/yyyy");
                                }

                                int cantEscaner = cantidadFinalEscaneada;
                                fila.Cells["Cantidad"].Value = cantEscaner > productoIdeal.Cantidad ? productoIdeal.Cantidad : cantEscaner;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontró stock disponible de este producto en el sistema.", "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string fechaStr = fila.Cells["Vencimiento"].Value?.ToString();
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
                }
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

                if (chkConRemito.Checked)
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
                            numeroSerie = fila.Cells["Serie"].Value?.ToString() ?? "",
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
                            numSerie = fila.Cells["Serie"].Value?.ToString() ?? "",
                            codigoLote = fila.Cells["Lote"].Value?.ToString() ?? "",
                            cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value)
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

        private int PedirCantidadSueltas(string nombreProducto, int cantidadEnCaja)
        {
            int cantidadElegida = 0;
            Form prompt = new Form()
            {
                Width = 380,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Apertura de caja detectada",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblTexto = new Label() { Left = 20, Top = 20, Width = 320, Height = 40, Text = $"El código pertenece a '{nombreProducto}' y trae {cantidadEnCaja} unidades.\n¿Cuántas unidades sueltas vas a retirar?" };
            NumericUpDown inputNum = new NumericUpDown() { Left = 20, Top = 70, Width = 120, Minimum = 1, Maximum = cantidadEnCaja, Value = 1, Font = new Font("Segoe UI", 12) };
            Button btnAceptar = new Button() { Text = "Aceptar", Left = 240, Top = 65, Width = 100, DialogResult = DialogResult.OK };

            prompt.Controls.Add(lblTexto); prompt.Controls.Add(inputNum); prompt.Controls.Add(btnAceptar);
            prompt.AcceptButton = btnAceptar;

            if (prompt.ShowDialog() == DialogResult.OK) cantidadElegida = (int)inputNum.Value;
            return cantidadElegida;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form buscadorStock = new Form()
            {
                Width = 700,
                Height = 400,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Buscar Stock en Estantes",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lblInfo = new Label() { Left = 20, Top = 15, Width = 60, Text = "Nombre:" };
            TextBox txtBuscar = new TextBox() { Left = 90, Top = 12, Width = 550 };

            DataGridView dgvResultados = new DataGridView()
            {
                Left = 20,
                Top = 50,
                Width = 640,
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

                    if (col.Name == "ProductoNombre") col.HeaderText = "Producto";
                    if (col.Name == "ProductoCodigo") col.HeaderText = "Código";
                    if (col.Name == "DepositoNombre") col.HeaderText = "Depósito";
                    if (col.Name == "CodigoLote") col.HeaderText = "Lote";
                    if (col.Name == "FechaVencimiento") col.HeaderText = "Vencimiento";
                    if (col.Name == "Cantidad") col.HeaderText = "Stock";
                }
            };

            buscadorStock.Controls.Add(lblInfo);
            buscadorStock.Controls.Add(txtBuscar);
            buscadorStock.Controls.Add(dgvResultados);

            buscadorStock.Load += async (s, args) =>
            {
                buscadorStock.Cursor = Cursors.WaitCursor;
                try
                {
                    var stockFisico = await ApiServices.ObtenerExistencias("", ocultarCero: false, soloVencidos: false);
                    dgvResultados.DataSource = stockFisico;
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
                    var stockFisico = await ApiServices.ObtenerExistencias(txtBuscar.Text.Trim(), ocultarCero: false, soloVencidos: false);
                    if (buscadorStock.IsDisposed) return;
                    dgvResultados.DataSource = stockFisico;
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

            txtBuscar.TextChanged += (s, ev) =>
            {
                timerBusquedaModal.Stop();
                timerBusquedaModal.Start();
            };

            txtBuscar.KeyDown += async (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    timerBusquedaModal.Stop();
                    await realizarBusqueda();
                }
            };

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

                    if (fechaV.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Este lote ya se encuentra vencido en el estante.\nPor seguridad, no se puede egresar.", "Stock Vencido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }

                int idProd = Convert.ToInt32(filaStock.Cells["ProductoId"].Value);
                string nombre = filaStock.Cells["ProductoNombre"].Value?.ToString() ?? "Desconocido";
                string lote = filaStock.Cells["CodigoLote"].Value?.ToString() ?? "S/L";

                if (filaStock.Cells["DepositoId"].Value != null)
                {
                    cmbDeposito.SelectedValue = Convert.ToInt32(filaStock.Cells["DepositoId"].Value);
                }

                int aRetirar = PedirCantidadSueltas($"{nombre} (Lote: {lote})", cantDisponible);
                if (aRetirar == 0) return;

                bool yaExiste = false;
                foreach (DataGridViewRow filaMain in dgvEgreso.Rows)
                {
                    if (filaMain.IsNewRow) continue;

                    int idExistente = Convert.ToInt32(filaMain.Cells["id"].Value);
                    string loteExistente = filaMain.Cells["Lote"].Value?.ToString();

                    if (idExistente == idProd && loteExistente == lote)
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

                if (!yaExiste)
                {
                    int indexNuevaFila = dgvEgreso.Rows.Add();
                    DataGridViewRow filaMain = dgvEgreso.Rows[indexNuevaFila];
                    filaMain.Cells["id"].Value = idProd;
                    filaMain.Cells["Producto"].Value = nombre;
                    filaMain.Cells["Lote"].Value = lote;
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
    }
}