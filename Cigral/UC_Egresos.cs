using Cigral.Models;
using Cigral.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cigral
{
    public partial class UC_Egresos : UserControl
    {
        public UC_Egresos()
        {
            InitializeComponent();
        }


        private void iconBtnBack_Click(object sender, EventArgs e)
        {
            FormMain principal = this.ParentForm as FormMain;

            // 2. Si lo encontramos, le decimos: "¡Che, resetea el menú!"
            if (principal != null)
            {
                principal.ResetearMenu();
            }

            // 3. Ahora sí, nos vamos
            this.Dispose();
        }

        private async void chkConRemito_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConRemito.Checked)
            {
                // Habilitamos el combo y el botón +
                cmbCliente.Enabled = true;
                btnAgregarCliente.Enabled = true;
                cmbCliente.Focus();

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
                cmbCliente.SelectedIndex = -1; // Deseleccionamos el cliente
                cmbCliente.Enabled = false;    // Bloqueamos el combo
                btnAgregarCliente.Enabled = false; // Bloqueamos el +
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private async void UC_Egresos_Load(object sender, EventArgs e)
        {
            // 1. Ponemos el foco en el escáner para que arranque listo
            this.ActiveControl = txtEscaner;

            // 2. Cargamos los depósitos (Asegurate de que el nombre del método en tu ApiServices sea el correcto)
            try
            {
                var depositos = await ApiServices.ObtenerDepositos(); // O como se llame tu función
                cmbDeposito.DataSource = depositos;
                cmbDeposito.DisplayMember = "Nombre"; // Lo que lee el usuario
                cmbDeposito.ValueMember = "Id";       // El número oculto
                cmbDeposito.SelectedIndex = -1; // Lo dejamos en blanco para que elijan uno

                await CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar depósitos: " + ex.Message);
            }
        }




        private async Task CargarClientes()
        {
            try
            {
                var listaClientes = await ApiServices.ObtenerClientes();

                cmbCliente.DataSource = null;
                cmbCliente.DataSource = listaClientes;
                cmbCliente.DisplayMember = "razonSocial";
                cmbCliente.ValueMember = "id";
                cmbCliente.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes en el combo: " + ex.Message);
            }
        }



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
                    // --- PASO 1: EL PARSER DESARMA EL CÓDIGO ---
                    var productoParseado = await ApiServices.ParsearCodigoBarras(codigoCrudo);

                    if (productoParseado != null)
                    {
                        if (!productoParseado.ExisteProducto)
                        {
                            MessageBox.Show("Este producto no está registrado en el sistema. No se puede egresar.", "Producto Desconocido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // --- PASO 2: BUSCAMOS EL STOCK POR SU ID EXACTO ---
                        var productoIdeal = await ApiServices.BuscarProductoParaEgreso(productoParseado.ProductoId.Value);

                        if (productoIdeal != null)
                        {
                            // --- PASO 3: AUTO-SELECCIONAMOS EL DEPÓSITO ---
                            cmbDeposito.SelectedValue = productoIdeal.DepositoId;

                            // --- EL PEAJE DE CANTIDAD --- 
                            // Calculamos cuánto trae el código de barras por defecto (ej: 10)
                            int cantidadFinalEscaneada = productoParseado.Cantidad > 0 ? productoParseado.Cantidad : 1;

                            // Si trae más de 1 unidad (ej: caja de bolsas Hollister), frenamos todo
                            if (cantidadFinalEscaneada > 1)
                            {
                                // Llamamos a la ventanita
                                int elegida = PedirCantidadSueltas(productoIdeal.ProductoNombre, cantidadFinalEscaneada);

                                if (elegida == 0) // El operario se arrepintió y cerró la ventanita con la X
                                {
                                    return; // Cortamos la carga acá nomás
                                }

                                // Pisamos la cantidad original de la caja con las unidades que tipeó (ej: 2)
                                cantidadFinalEscaneada = elegida;
                            }
                            

                            // --- PASO 4: LO AGREGAMOS A LA GRILLA DE EGRESOS ---
                            bool productoYaEnGrilla = false;

                            foreach (DataGridViewRow filaExistente in dgvEgreso.Rows)
                            {
                                if (filaExistente.IsNewRow) continue;

                                int idFila = Convert.ToInt32(filaExistente.Cells["id"].Value);

                                // Comparamos por ID y por Lote
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
            // 1. Validaciones
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

            if (chkConRemito.Checked && (cmbCliente.SelectedIndex == -1 || cmbCliente.SelectedValue == null))
            {
                MessageBox.Show("Si el egreso es con remito, debe seleccionar un cliente de la lista.", "Falta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCliente.Focus();
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
                        // Si la fecha de vencimiento ya pasó (es menor a hoy)...
                        if (fechaParseada.Date < DateTime.Now.Date)
                        {
                            string nombreProd = fila.Cells["Producto"].Value?.ToString();
                            string loteProd = fila.Cells["Lote"].Value?.ToString();

                            MessageBox.Show($"¡OPERACIÓN BLOQUEADA!\n\nEl producto '{nombreProd}' (Lote: {loteProd}) se encuentra vencido desde el {fechaStr}.\n\nPor normas de seguridad, no se puede egresar mercadería vencida. Se retirará el producto de la lista.", "Producto Vencido", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                            dgvEgreso.Rows.Remove(fila);

                            return; // Cortamos el proceso acá, no se descuenta stock ni se hace el remito
                        }
                    }
                }
            }

            var confirmacion = MessageBox.Show("¿Desea confirmar este egreso?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.No) return;

            // APAGA EL BOTÓN ACÁ (Justo después de que el operario dijo "Sí, quiero confirmar")
            btnConfirmar.Enabled = false;
            Cursor = Cursors.WaitCursor;
            int depositoSeleccionado = (int)cmbDeposito.SelectedValue;

            try
            {

                // Limpia colores de errores anteriores
                foreach (DataGridViewRow fila in dgvEgreso.Rows)
                {
                    fila.DefaultCellStyle.BackColor = Color.White;
                }
                // CAMINO A: CON REMITO OFICIAL
                if (chkConRemito.Checked)
                {
                    var remitoNuevo = new RemitoEgresoDto
                    {
                        depositoId = depositoSeleccionado,
                        entidadId = Convert.ToInt32(cmbCliente.SelectedValue), // AHORA SÍ MANDAMOS EL ID REAL
                        numeroRemito = txtRemito.Text,
                        observaciones = "Generado por sistema",
                        detalles = new List<RemitoEgresoDetalleDto>()
                    };

                    // Llena la lista de detalles recorriendo la grilla
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

                    // Dispara a la API del compañero
                    int idRemitoGenerado = await ApiServices.CrearRemitoEgreso(remitoNuevo);

                    if (idRemitoGenerado > 0)
                    {
                        MessageBox.Show("¡Remito creado y stock descontado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Descarga y abre el PDF automáticamente
                        await ApiServices.DescargarAbrirPdfRemito(idRemitoGenerado);
                        LimpiarPantalla();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al generar el remito en el servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // CAMINO B: MOVIMIENTO INTERNO (SIN REMITO)
                else
                {
                    int itemsProcesados = 0;
                    int itemsConError = 0;

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

                        bool exito = await ApiServices.DisminuirStock(dto);
                        if (exito) itemsProcesados++;
                        else itemsConError++;
                    }

                    if (itemsConError == 0)
                    {
                        MessageBox.Show($"¡Stock descontado exitosamente! ({itemsProcesados} items).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarPantalla();
                    }
                    else
                    {
                        MessageBox.Show($"Se procesaron {itemsProcesados} productos, pero fallaron {itemsConError}.", "Aviso Parcial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Atrapa el error de validación específico de la API
                if (ex.Message.Contains("UnprocessableEntity") && ex.Message.Contains("{"))
                {
                    try
                    {
                        // Recorta el texto para quedarnos solo con el JSON puro
                        int startIndex = ex.Message.IndexOf("{");
                        string jsonError = ex.Message.Substring(startIndex);

                        // Convierte el JSON usando los moldes nuevos (Asegurate de tener el using Newtonsoft.Json;)
                        var errorObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Cigral.Models.ValidationErrorResponse>(jsonError);

                        if (errorObj != null && errorObj.errores != null && errorObj.errores.Count > 0)
                        {
                            string mensajeLimpio = errorObj.mensaje + "\n\n";

                            foreach (var detalle in errorObj.errores)
                            {
                                mensajeLimpio += $"- {detalle.mensaje}\n";

                                // Pintamos de rojo la fila correspondiente
                                int indiceFila = detalle.orden;
                                if (indiceFila >= 0 && indiceFila < dgvEgreso.Rows.Count)
                                {
                                    dgvEgreso.Rows[indiceFila].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
                                }
                            }

                            MessageBox.Show(mensajeLimpio, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Cortamos acá para que no muestre el error genérico de abajo
                        }
                    }
                    catch
                    {
                        // Si falla la conversión del JSON, lo ignoramos y dejamos que muestre el error genérico crudo
                    }
                }

                // Si no era un error de validación (ej. se cortó la base de datos), muestra tu error original
                MessageBox.Show("Error crítico al procesar la operación:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                // 2. LO VOLVEMOS A PRENDER ACÁ (Así se habilita de vuelta termine bien o termine mal)
                btnConfirmar.Enabled = true;
            }
        }

        // Función para dejar la pantalla lista para el próximo turno
        private void LimpiarPantalla()
        {
            dgvEgreso.Rows.Clear();
            chkConRemito.Checked = false;
            txtRemito.Clear();
            cmbCliente.SelectedIndex = -1; // Limpia la selección
            txtEscaner.Focus();
        }

        private async void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            // 1. Armaado la ventana con mas espacio
            Form prompt = new Form()
            {
                Width = 400,
                Height = 350,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Alta de Cliente Nuevo",
                StartPosition = FormStartPosition.CenterScreen
            };

            // 2. Crea todas las etiquetas y cajas de texto
            Label lblRazon = new Label() { Left = 20, Top = 20, Width = 100, Text = "Razón Social:" };
            TextBox txtRazon = new TextBox() { Left = 130, Top = 20, Width = 230 };

            Label lblGln = new Label() { Left = 20, Top = 50, Width = 100, Text = "GLN (13 núm):" };
            TextBox txtGln = new TextBox() { Left = 130, Top = 50, Width = 230 };

            Label lblEmail = new Label() { Left = 20, Top = 80, Width = 100, Text = "Email:" };
            TextBox txtEmail = new TextBox() { Left = 130, Top = 80, Width = 230 };

            Label lblCuit = new Label() { Left = 20, Top = 110, Width = 100, Text = "CUIT:" };
            TextBox txtCuit = new TextBox() { Left = 130, Top = 110, Width = 230 };

            Label lblTel = new Label() { Left = 20, Top = 140, Width = 100, Text = "Teléfono:" };
            TextBox txtTel = new TextBox() { Left = 130, Top = 140, Width = 230 };

            Label lblDir = new Label() { Left = 20, Top = 170, Width = 100, Text = "Dirección:" };
            TextBox txtDir = new TextBox() { Left = 130, Top = 170, Width = 230 };

            Button confirmation = new Button() { Text = "Guardar", Left = 260, Top = 220, Width = 100, DialogResult = DialogResult.OK };

            // 3. Meto todo adentro de la ventana
            prompt.Controls.Add(lblRazon); prompt.Controls.Add(txtRazon);
            prompt.Controls.Add(lblGln); prompt.Controls.Add(txtGln);
            prompt.Controls.Add(lblEmail); prompt.Controls.Add(txtEmail);
            prompt.Controls.Add(lblCuit); prompt.Controls.Add(txtCuit);
            prompt.Controls.Add(lblTel); prompt.Controls.Add(txtTel);
            prompt.Controls.Add(lblDir); prompt.Controls.Add(txtDir);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation; // Hace que al apretar Enter se guarde

            // 4. Muestra la ventana y captura si le dan a Guardar
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                // Validación rápida para que no mande campos vacíos clave
                if (string.IsNullOrWhiteSpace(txtRazon.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("La Razón Social y el Email son obligatorios.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                try
                {
                    // Llena el molde con todo lo que escribió
                    var nuevoCliente = new ClienteDto
                    {
                        razonSocial = txtRazon.Text.Trim(),
                        gln = txtGln.Text.Trim(),
                        email = txtEmail.Text.Trim(),
                        cuit = txtCuit.Text.Trim(),
                        telefono = txtTel.Text.Trim(),
                        direccion = txtDir.Text.Trim()
                    };

                    // Dispara a la API
                    int idNuevo = await ApiServices.CrearClienteExpress(nuevoCliente);

                    if (idNuevo > 0)
                    {
                        // Éxito

                        await CargarClientes();

                        cmbCliente.SelectedValue = idNuevo; // Lo deja seleccionado
                        MessageBox.Show("¡Cliente creado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private int PedirCantidadSueltas(string nombreProducto, int cantidadEnCaja)
        {
            int cantidadElegida = 0; // Arranca en 0 por si cancelan

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

            Label lblTexto = new Label()
            {
                Left = 20,
                Top = 20,
                Width = 320,
                Height = 40,
                Text = $"El código pertenece a '{nombreProducto}' y trae {cantidadEnCaja} unidades.\n¿Cuántas unidades sueltas vas a retirar?"
            };

            // Usa NumericUpDown para que solo puedan poner números. 
            // El máximo es la cantidad que trae la caja.
            NumericUpDown inputNum = new NumericUpDown()
            {
                Left = 20,
                Top = 70,
                Width = 120,
                Minimum = 1,
                Maximum = cantidadEnCaja,
                Value = 1,
                Font = new Font("Segoe UI", 12)
            };

            Button btnAceptar = new Button() { Text = "Aceptar", Left = 240, Top = 65, Width = 100, DialogResult = DialogResult.OK };

            prompt.Controls.Add(lblTexto);
            prompt.Controls.Add(inputNum);
            prompt.Controls.Add(btnAceptar);
            prompt.AcceptButton = btnAceptar;

            // Si el usuario le da a Aceptar, guarda el número que eligió
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                cantidadElegida = (int)inputNum.Value;
            }

            return cantidadElegida; // Si cerró la ventana con la X, devuelve 0
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Armado la ventanita emergente
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

            // Ajust de los anchos para que quede más simétrico
            Label lblInfo = new Label() { Left = 20, Top = 15, Width = 60, Text = "Nombre:" };
            TextBox txtBuscar = new TextBox() { Left = 90, Top = 12, Width = 450 };
            Button btnBuscar = new Button() { Left = 550, Top = 10, Width = 100, Text = "Buscar" };

            // PUESTA A PUNTO ESTÉTICA 
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
                BackgroundColor = Color.White,    // Fondo blanco limpio
                RowHeadersVisible = false,        // Saca la columna gris inútil de la izquierda
                BorderStyle = BorderStyle.FixedSingle,
                GridColor = Color.LightGray
            };

            // Oculta IDs y pone nombres lindos
            dgvResultados.DataBindingComplete += (senderGrid, ev) =>
            {
                foreach (DataGridViewColumn col in dgvResultados.Columns)
                {
                    // 1. Ocultamos las columnas técnicas
                    if (col.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) ||
                        col.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                        col.Name == "ProductoSinCodigo")
                    {
                        col.Visible = false;
                    }

                    // 2. Le ponemos títulos limpios a las que quedan
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
            buscadorStock.Controls.Add(btnBuscar);
            buscadorStock.Controls.Add(dgvResultados);

            // CARGA AUTOMÁTICA AL ABRIR LA VENTANA
            buscadorStock.Load += async (s, args) =>
            {
                buscadorStock.Cursor = Cursors.WaitCursor;
                try
                {
                    // Le mandamos string vacío ("") para que traiga TODO el catálogo de entrada
                    var stockFisico = await ApiServices.ObtenerExistencias("", ocultarCero: false, soloVencidos: false);
                    dgvResultados.DataSource = stockFisico;
                }
                catch (Exception)
                {
                    // Si hay un micro-corte acá, no decimos nada, el usuario puede darle al botón Buscar
                }
                finally { buscadorStock.Cursor = Cursors.Default; }
            };

            // 2. Evento del botón Buscar (Manual)
            btnBuscar.Click += async (s, args) =>
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text)) return;
                buscadorStock.Cursor = Cursors.WaitCursor;

                try
                {
                    var stockFisico = await ApiServices.ObtenerExistencias(txtBuscar.Text.Trim(), ocultarCero: false, soloVencidos: false);
                    dgvResultados.DataSource = stockFisico;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar: " + ex.Message);
                }
                finally { buscadorStock.Cursor = Cursors.Default; }
            };

            buscadorStock.AcceptButton = btnBuscar;

            // 3. Evento de Doble Clic (Los patovas de Stock 0 y Vencimiento de millan)
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

                // 4. Inyectamos a la grilla principal
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

            // 5. Mostramos la ventana
            buscadorStock.ShowDialog();
        }
    }
}
