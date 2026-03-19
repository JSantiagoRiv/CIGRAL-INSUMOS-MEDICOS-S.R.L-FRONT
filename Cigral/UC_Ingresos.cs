using Cigral.Models;
using System.Text.Json;
using Cigral.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq; // Necesario para el .Select() del buscador
using System.Threading.Tasks;

namespace Cigral
{
    /// <summary>
    /// Pantalla encargada de gestionar la entrada de mercadería al sistema.
    /// Permite ingresar productos vía escáner o carga manual, y guardarlos como un Remito oficial o como un movimiento de stock simple.
    /// </summary>
    public partial class UC_Ingresos : UserControl
    {
        public UC_Ingresos()
        {
            InitializeComponent();
            // Le pedimos que apenas termine de dibujarse la pantalla, ejecute el evento Load
            this.Load += UC_Ingresos_Load;
        }

        /// <summary>
        /// Se ejecuta al abrir la pantalla. Prepara combos y el cursor inicial.
        /// </summary>
        private async void UC_Ingresos_Load(object sender, EventArgs e)
        {
            // Cargamos listas desde la API
            await CargarProveedores();

            var depositos = await ApiServices.ObtenerDepositos();
            cmbDeposito.DataSource = depositos;
            cmbDeposito.DisplayMember = "Nombre"; // Lo que lee el usuario
            cmbDeposito.ValueMember = "Id";       // El dato real que viaja a la BD

            // Usamos ActiveControl en lugar de .Focus() porque en los UserControls 
            // a veces el Focus() falla si la pantalla todavía se está dibujando.
            this.ActiveControl = textScanner;
        }

        private async Task CargarProveedores()
        {
            var lista = await ApiServices.ObtenerProveedores();
            comboProv.DataSource = lista;
            comboProv.DisplayMember = "RazonSocial";
            comboProv.ValueMember = "Id";
            comboProv.SelectedIndex = -1; // Arranca vacío
        }

        // --- LÓGICA DE GUARDADO ---

        /// <summary>
        /// Confirma y guarda todo el ingreso en la base de datos (sea con o sin remito).
        /// </summary>
        private async void buttonConfirm_Click(object sender, EventArgs e)
        {
            // 🔥 1. HACEMOS TODAS LAS VALIDACIONES PRIMERO
            dgvIngreso.EndEdit();
            dgvIngreso.CurrentCell = null; // Truco extra: "Desenfocamos" la celda para asegurar que los datos en edición se apliquen.

            if (cmbDeposito.SelectedValue == null)
            {
                MessageBox.Show("No hay ningún Depósito disponible para guardar el ingreso.\nPor favor, asegúrese de que existan depósitos creados en el sistema.", "Falta Depósito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (chkConRemito.Checked)
            {
                if (comboProv.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un Proveedor para continuar.", "Falta Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textRemito.Text))
                {
                    MessageBox.Show("Ha marcado la opción de remito. Debe ingresar el Número de Remito para continuar.", "Falta Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Control de Series Duplicadas dentro del mismo escaneo
                HashSet<string> series = new HashSet<string>();
                foreach (DataGridViewRow fila in dgvIngreso.Rows)
                {
                    if (fila.IsNewRow) continue;
                    string serie = fila.Cells["Serie"].Value?.ToString()?.Trim();

                    if (!string.IsNullOrEmpty(serie))
                    {
                        if (series.Contains(serie))
                        {
                            MessageBox.Show($"El número de serie '{serie}' está repetido.\nCada número de serie debe ser único.", "Serie duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        series.Add(serie);
                    }
                }
            }

            if (dgvIngreso.Rows.Count == 0 || (dgvIngreso.Rows.Count == 1 && dgvIngreso.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay productos escaneados para ingresar.", "Remito Vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. PREGUNTAMOS SI ESTÁ SEGURO
            var confirmacion = MessageBox.Show("¿Desea confirmar este ingreso de mercadería?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.No) return;

            // 3. AHORA SÍ: BLOQUEAMOS EL BOTÓN CONTRA CLICS ANSIOSOS
            buttonConfirm.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                // PASO PREVIO: Creación de productos nuevos al vuelo (los que se crearon manualmente)
                foreach (DataGridViewRow fila in dgvIngreso.Rows)
                {
                    if (fila.IsNewRow) continue;

                    int idActual = Convert.ToInt32(fila.Cells["id"].Value);

                    if (idActual == 0) // Si no tiene ID, es un producto fantasma que hay que crear primero en BD
                    {
                        string nombreIngresado = fila.Cells["Producto"].Value?.ToString() ?? "Producto Sin Nombre";
                        string gtinOculto = fila.Tag?.ToString() ?? "";

                        await ApiServices.GarantizarMarcaExiste("S/M");

                        var requestNuevoProd = new ProductoCreateRequest
                        {
                            Nombre = nombreIngresado,
                            Descripcion = nombreIngresado,
                            Gtin = gtinOculto,
                            EsUnitario = false,
                            Precio = 0,
                            Marca = "S/M",
                            CodigoGenerico = "GEN-" + DateTime.Now.ToString("yyMMddHHmmss"),
                            ProductoSinCodigo = true // Bandera para que el sistema sepa que es genérico
                        };

                        int nuevoId = await ApiServices.CrearProductoNuevo(requestNuevoProd);

                        if (nuevoId > 0)
                        {
                            fila.Cells["id"].Value = nuevoId; // Le inyectamos el ID real recién creado
                        }
                        else
                        {
                            MessageBox.Show($"No se pudo crear el producto '{nombreIngresado}'. Se cancela el ingreso.", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // VARIABLES DE GUARDADO
                int depositoSeleccionado = (int)cmbDeposito.SelectedValue;
                bool todoExito = true;
                int idRemitoGenerado = 0;

                // DECISIÓN: ¿CON REMITO O SIN REMITO?
                if (chkConRemito.Checked)
                {
                    // CAMINO A: Guardado en bloque como un Remito Oficial
                    var requestRemito = new RemitoIngresoRequest
                    {
                        DepositoId = depositoSeleccionado,
                        EntidadId = (int)comboProv.SelectedValue,
                        NumeroRemito = textRemito.Text,
                        Observaciones = "Ingreso desde el sistema",
                        Detalles = new List<RemitoDetalleRequest>()
                    };

                    foreach (DataGridViewRow fila in dgvIngreso.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        int idProducto = 0;
                        if (fila.Cells["id"].Value != null) int.TryParse(fila.Cells["id"].Value.ToString(), out idProducto);
                        if (idProducto == 0) continue;

                        int cantidadSegura = 0;
                        if (fila.Cells["Cantidad"].Value != null) int.TryParse(fila.Cells["Cantidad"].Value.ToString(), out cantidadSegura);
                        if (cantidadSegura <= 0) cantidadSegura = 1;

                        string loteAEnviar = fila.Cells["Lote"].Value?.ToString();
                        if (string.IsNullOrWhiteSpace(loteAEnviar)) loteAEnviar = "S/L";

                        DateTime? fechaVencimientoSegura = null;
                        string valorCelda = fila.Cells["Vencimiento"].Value?.ToString();
                        if (DateTime.TryParse(valorCelda, out DateTime fechaParseada)) fechaVencimientoSegura = fechaParseada;

                        requestRemito.Detalles.Add(new RemitoDetalleRequest
                        {
                            ProductoId = idProducto,
                            CodigoLote = loteAEnviar,
                            NumeroSerie = fila.Cells["Serie"].Value?.ToString() ?? "",
                            FechaVencimiento = fechaVencimientoSegura,
                            Cantidad = cantidadSegura
                        });
                    }

                    idRemitoGenerado = await ApiServices.GuardarIngreso(requestRemito);
                    todoExito = (idRemitoGenerado > 0);
                }
                else
                {
                    // CAMINO B: Guardado individual (Movimientos Internos de aumento de stock)
                    foreach (DataGridViewRow fila in dgvIngreso.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        int idProducto = 0;
                        if (fila.Cells["id"].Value != null) int.TryParse(fila.Cells["id"].Value.ToString(), out idProducto);
                        if (idProducto == 0) continue;

                        int cantidadSegura = 0;
                        if (fila.Cells["Cantidad"].Value != null) int.TryParse(fila.Cells["Cantidad"].Value.ToString(), out cantidadSegura);
                        if (cantidadSegura <= 0) cantidadSegura = 1;

                        string loteAEnviar = fila.Cells["Lote"].Value?.ToString();
                        if (string.IsNullOrWhiteSpace(loteAEnviar)) loteAEnviar = "S/L";

                        DateTime? fechaVencimientoSegura = null;
                        string valorCelda = fila.Cells["Vencimiento"].Value?.ToString();
                        if (DateTime.TryParse(valorCelda, out DateTime fechaParseada)) fechaVencimientoSegura = fechaParseada;

                        var requestExistencia = new AumentarExistenciaRequest
                        {
                            DepositoId = depositoSeleccionado,
                            ProductoId = idProducto,
                            NumSerie = fila.Cells["Serie"].Value?.ToString() ?? "",
                            CodigoLote = loteAEnviar,
                            FechaVencimiento = fechaVencimientoSegura,
                            Cantidad = cantidadSegura
                        };

                        bool exito = await ApiServices.AumentarStock(requestExistencia);
                        if (!exito) todoExito = false;
                    }
                }

                // SI TODO SALIÓ BIEN
                if (todoExito)
                {
                    MessageBox.Show("¡Ingreso registrado con éxito!", "Misión Cumplida", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (chkConRemito.Checked && idRemitoGenerado > 0)
                    {
                        DialogResult respuesta = MessageBox.Show("¿Desea imprimir el comprobante del remito ahora?", "Imprimir Remito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            await DescargarYAbrirPdf(idRemitoGenerado);
                        }
                    }

                    LimpiarTodo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message);
            }
            finally
            {
                // 4. VOLVEMOS A PRENDER EL BOTÓN
                buttonConfirm.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        // --- NAVEGACIÓN Y CANCELACIÓN ---

        private void iconBtnbBack_Click(object sender, EventArgs e)
        {
            FormMain principal = this.ParentForm as FormMain;

            // Resetea el color del menú izquierdo al volver.
            if (principal != null)
            {
                principal.ResetearMenu();
            }

            this.Dispose(); // Destruye esta pantalla
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Validar si hay algo para borrar. Si está vacío, no molestamos con alertas.
            if (dgvIngreso.Rows.Count == 0 && string.IsNullOrWhiteSpace(textRemito.Text)) return;

            // Cinturón de seguridad
            var confirmacion = MessageBox.Show(
                "¿Desea cancelar todo el ingreso actual?\n\nSe perderán todos los productos escaneados y el número de remito.",
                "Cancelar Ingreso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2); // Foco en "NO" por seguridad

            if (confirmacion == DialogResult.Yes)
            {
                LimpiarTodo();
            }
        }

        /// <summary>
        /// Deja la pantalla en blanco para un nuevo ingreso.
        /// </summary>
        private void LimpiarTodo()
        {
            dgvIngreso.Rows.Clear();
            textRemito.Clear();
            chkConRemito.Checked = false;
            comboProv.SelectedIndex = -1;
            textScanner.Focus();
        }

        // --- LÓGICA DE LECTURA DE CÓDIGOS DE BARRAS ---

        /// <summary>
        /// Intercepta el final de la lectura del escáner (tecla Enter) y procesa el código.
        /// </summary>
        private async void textScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido de de Windows

                string codigoCrudo = textScanner.Text.Trim();
                textScanner.Clear(); // Limpia rápido para el próximo escaneo

                if (string.IsNullOrEmpty(codigoCrudo)) return;

                // 1. LLAMA AL PARSER
                var productoParseado = await ApiServices.ParsearCodigoBarras(codigoCrudo);

                // 2. VALIDAR SERIE DUPLICADA EN LA GRILLA
                foreach (DataGridViewRow fila in dgvIngreso.Rows)
                {
                    if (fila.IsNewRow) continue;

                    string serieExistente = fila.Cells["Serie"].Value?.ToString();

                    if (!string.IsNullOrEmpty(productoParseado.NumeroSerie) && serieExistente == productoParseado.NumeroSerie)
                    {
                        MessageBox.Show($"El número de serie {productoParseado.NumeroSerie} ya fue escaneado.", "Serie duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textScanner.Focus();
                        return;
                    }
                }

                if (productoParseado != null)
                {
                    // 3. BUSCA SI YA ESTÁ EN LA GRILLA (Para sumar cantidad)
                    bool productoYaEnGrilla = false;

                    foreach (DataGridViewRow filaExistente in dgvIngreso.Rows)
                    {
                        if (filaExistente.IsNewRow) continue;

                        string gtinFila = filaExistente.Tag?.ToString() ?? "";

                        if (gtinFila == productoParseado.Gtin)
                        {
                            int cantidadActual = Convert.ToInt32(filaExistente.Cells["Cantidad"].Value);
                            int cantidadNueva = productoParseado.Cantidad > 0 ? productoParseado.Cantidad : 1;

                            filaExistente.Cells["Cantidad"].Value = cantidadActual + cantidadNueva;
                            productoYaEnGrilla = true;
                            break;
                        }
                    }

                    if (productoYaEnGrilla)
                    {
                        textScanner.Focus();
                        return;
                    }

                    // 4. SI NO ESTABA, CREA LA FILA NUEVA
                    int indexNuevaFila = dgvIngreso.Rows.Add();
                    DataGridViewRow fila = dgvIngreso.Rows[indexNuevaFila];

                    fila.Tag = productoParseado.Gtin; // Guarda el GTIN oculto en el Tag de la fila

                    fila.Cells["Lote"].Value = productoParseado.Lote;
                    if (productoParseado.FechaVencimiento.HasValue)
                    {
                        fila.Cells["Vencimiento"].Value = productoParseado.FechaVencimiento.Value.ToString("dd/MM/yyyy");
                    }

                    fila.Cells["Serie"].Value = productoParseado.NumeroSerie;
                    fila.Cells["Cantidad"].Value = productoParseado.Cantidad > 0 ? productoParseado.Cantidad : 1;
                    fila.Cells["InfoAdicional"].Value = FormatearInfoAdicional(productoParseado.InformacionAdicional);

                    // 5. EVALUA SI ES CONOCIDO O DESCONOCIDO
                    if (productoParseado.ExisteProducto)
                    {
                        // PRODUCTO EXISTENTE: Lo muesra directo
                        fila.Cells["id"].Value = productoParseado.ProductoId;
                        fila.Cells["Producto"].Value = productoParseado.NombreProducto;
                        textScanner.Focus();
                    }
                    else
                    {
                        // PRODUCTO NUEVO: Frena y le pide al usuario que lo bautice
                        string nombreProducto = PedirNombreProducto(productoParseado.Gtin);

                        if (string.IsNullOrWhiteSpace(nombreProducto))
                        {
                            // Si cancela, borramos la fila
                            dgvIngreso.Rows.Remove(fila);
                            textScanner.Focus();
                            return;
                        }

                        Cursor = Cursors.WaitCursor;

                        // Crea el producto rápido en BD asociado a marca S/M
                        await ApiServices.GarantizarMarcaExiste("S/M");
                        var requestNuevoProd = new ProductoCreateRequest
                        {
                            Nombre = nombreProducto,
                            Descripcion = nombreProducto,
                            Gtin = productoParseado.Gtin,
                            EsUnitario = false,
                            Precio = 0,
                            Marca = "S/M"
                        };

                        int nuevoId = await ApiServices.CrearProductoNuevo(requestNuevoProd);
                        Cursor = Cursors.Default;

                        if (nuevoId > 0)
                        {
                            fila.Cells["id"].Value = nuevoId;
                            fila.Cells["Producto"].Value = nombreProducto;
                        }
                        else
                        {
                            MessageBox.Show("No se pudo crear el producto.");
                            dgvIngreso.Rows.Remove(fila);
                        }

                        textScanner.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Hubo un error al intentar decodificar el código de barras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Borra una fila específica de la grilla pidiendo confirmación.
        /// </summary>
        private void dgvIngreso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvIngreso.Columns[e.ColumnIndex].Name == "colEliminar")
            {
                string nombreProducto = dgvIngreso.Rows[e.RowIndex].Cells["Producto"].Value?.ToString() ?? "este producto";

                var confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar {nombreProducto}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacion == DialogResult.Yes)
                {
                    dgvIngreso.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        // --- ATAJOS DE TECLADO INTERNOS ---

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                buttonCancel_Click(null, null);
                return true;
            }

            if (keyData == Keys.F5)
            {
                buttonConfirm_Click(null, null);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // --- LÓGICA DE CAMPOS COMPLEMENTARIOS ---

        /// <summary>
        /// Activa/Desactiva campos dependiendo de si es ingreso formal (Remito) o movimiento manual.
        /// </summary>
        private async void chkConRemito_CheckedChanged(object sender, EventArgs e)
        {
            comboProv.Enabled = chkConRemito.Checked;
            btnAgregarProveedor.Enabled = chkConRemito.Checked;

            if (chkConRemito.Checked)
            {
                textRemito.ReadOnly = true; // Solo lectura para evitar que el operario escriba encima del automático
                await ActualizarNumeroRemito();
            }
            else
            {
                textRemito.ReadOnly = false;
                textRemito.Clear();
                comboProv.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Pide a la API el número correlativo que le toca al remito de este depósito.
        /// </summary>
        private async Task ActualizarNumeroRemito()
        {
            if (chkConRemito.Checked && cmbDeposito.SelectedValue != null)
            {
                if (int.TryParse(cmbDeposito.SelectedValue.ToString(), out int idDeposito))
                {
                    textRemito.Text = "Cargando...";
                    // Le pasa 'true' porque es un remito de Ingreso
                    string proximoRemito = await ApiServices.ObtenerSiguienteNumeroRemito(idDeposito, true);
                    textRemito.Text = proximoRemito;
                }
            }
        }

        // --- MANEJO DE ARCHIVOS PDF ---

        private async Task DescargarYAbrirPdf(int idRemito)
        {
            try
            {
                byte[] pdfBytes = await ApiServices.ObtenerRemitoPdf(idRemito);

                if (pdfBytes != null && pdfBytes.Length > 0)
                {
                    string rutaTemp = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"Remito_Ingreso_{idRemito}.pdf");
                    System.IO.File.WriteAllBytes(rutaTemp, pdfBytes);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = rutaTemp,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("El servidor devolvió un archivo vacío o no se encontró el PDF.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar abrir el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HELPER DE INTERFAZ: PROMPTS Y MODALES CREADOS POR CÓDIGO ---

        /// <summary>
        /// Levanta una ventanita rápida para pedirle al operario el nombre de un producto escaneado que no existe en BD.
        /// </summary>
        private string PedirNombreProducto(string gtin)
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 170,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Producto no registrado",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblInfo = new Label() { Left = 20, Top = 20, AutoSize = true, Text = $"GTIN: {gtin}" };
            Label lblNombre = new Label() { Left = 20, Top = 55, AutoSize = true, Text = "Nombre:" };
            TextBox txtNombre = new TextBox() { Left = 90, Top = 52, Width = 220 };
            Button btnGuardar = new Button() { Text = "Guardar", Left = 210, Width = 100, Top = 85, DialogResult = DialogResult.OK };

            prompt.Controls.Add(lblInfo); prompt.Controls.Add(lblNombre);
            prompt.Controls.Add(txtNombre); prompt.Controls.Add(btnGuardar);
            prompt.AcceptButton = btnGuardar;

            if (prompt.ShowDialog() == DialogResult.OK) return txtNombre.Text.Trim();
            return null;
        }

        /// <summary>
        /// Traduce el JSON crudo del campo Adicional a un formato "Clave: Valor | Clave: Valor".
        /// </summary>
        private string FormatearInfoAdicional(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return "";
            try
            {
                var datos = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                List<string> partes = new List<string>();
                foreach (var item in datos) partes.Add($"{item.Key}: {item.Value}");
                return string.Join(" | ", partes);
            }
            catch { return json; }
        }

        /// <summary>
        /// Abre modal de creación exprés de Proveedor.
        /// </summary>
        private async void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 350,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Alta de Proveedor Nuevo",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label lblRazon = new Label() { Left = 20, Top = 20, Width = 100, Text = "Razón Social:" }; TextBox txtRazon = new TextBox() { Left = 130, Top = 20, Width = 230 };
            Label lblGln = new Label() { Left = 20, Top = 50, Width = 100, Text = "GLN (13 núm):" }; TextBox txtGln = new TextBox() { Left = 130, Top = 50, Width = 230 };
            Label lblEmail = new Label() { Left = 20, Top = 80, Width = 100, Text = "Email:" }; TextBox txtEmail = new TextBox() { Left = 130, Top = 80, Width = 230 };
            Label lblCuit = new Label() { Left = 20, Top = 110, Width = 100, Text = "CUIT:" }; TextBox txtCuit = new TextBox() { Left = 130, Top = 110, Width = 230 };
            Label lblTel = new Label() { Left = 20, Top = 140, Width = 100, Text = "Teléfono:" }; TextBox txtTel = new TextBox() { Left = 130, Top = 140, Width = 230 };
            Label lblDir = new Label() { Left = 20, Top = 170, Width = 100, Text = "Dirección:" }; TextBox txtDir = new TextBox() { Left = 130, Top = 170, Width = 230 };
            Button confirmation = new Button() { Text = "Guardar", Left = 260, Top = 220, Width = 100, DialogResult = DialogResult.OK };

            // Helper visual para que la primera letra siempre sea mayúscula en nombres y direcciones
            EventHandler capitalizarPrimeraLetra = (senderObj, ev) =>
            {
                TextBox txt = senderObj as TextBox;
                if (txt.Text.Length == 1)
                {
                    txt.Text = txt.Text.ToUpper();
                    txt.SelectionStart = txt.Text.Length;
                }
            };
            txtRazon.TextChanged += capitalizarPrimeraLetra;
            txtDir.TextChanged += capitalizarPrimeraLetra;

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
                    var nuevoProveedor = new ProveedorDto
                    {
                        RazonSocial = txtRazon.Text.Trim(),
                        Gln = txtGln.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Cuit = txtCuit.Text.Trim(),
                        Telefono = txtTel.Text.Trim(),
                        Direccion = txtDir.Text.Trim()
                    };

                    int idNuevo = await ApiServices.CrearProveedorExpress(nuevoProveedor);

                    if (idNuevo > 0)
                    {
                        await CargarProveedores(); // Recarga la lista desde la BD
                        comboProv.SelectedValue = idNuevo; // Autoselecciona el recién creado
                        MessageBox.Show("¡Proveedor creado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Cursor = Cursors.Default; }
            }
        }

        /// <summary>
        /// Abre modal de búsqueda manual (cuando el código de barras no pasa o no existe). 
        /// Permite buscar en catálogo general y crear productos genéricos "Sin Código".
        /// </summary>
        private async void lblIngresoManual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form buscador = new Form()
            {
                Width = 600,
                Height = 450,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Buscador de Productos",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };

            TextBox txtBuscar = new TextBox() { Left = 20, Top = 20, Width = 380 };
            Button btnBuscar = new Button() { Text = "Buscar", Left = 410, Top = 18, Width = 80, Cursor = Cursors.Hand };
            Button btnNuevo = new Button() { Text = "+", Left = 500, Top = 18, Width = 60, BackColor = Color.LightGreen, Cursor = Cursors.Hand };

            DataGridView dgv = new DataGridView()
            {
                Left = 20,
                Top = 60,
                Width = 540,
                Height = 320,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.FixedSingle,
                GridColor = Color.LightGray
            };

            // Oculta la columna del ID internamente para que quede limpio
            dgv.DataBindingComplete += (senderGrid, ev) =>
            {
                if (dgv.Columns.Contains("ID")) dgv.Columns["ID"].Visible = false;
            };

            buscador.Controls.Add(txtBuscar); buscador.Controls.Add(btnBuscar);
            buscador.Controls.Add(btnNuevo); buscador.Controls.Add(dgv);

            int idSeleccionado = 0;
            string nombreSeleccionado = "";

            Func<Task> realizarBusqueda = async () =>
            {
                buscador.Cursor = Cursors.WaitCursor;
                dgv.DataSource = null;

                var lista = await ApiServices.ObtenerProductosCatalogo(txtBuscar.Text.Trim());

                // Mapeo dinámico para ajustar cómo se ven las columnas
                var vistaResultados = lista.Select(x => new
                {
                    ID = x.id,
                    Producto = x.nombre,
                    Codigo = string.IsNullOrEmpty(x.gtin) ? x.codigoGenerico : x.gtin
                }).ToList();

                dgv.DataSource = vistaResultados;
                buscador.Cursor = Cursors.Default;
            };

            btnBuscar.Click += async (s, ev) => await realizarBusqueda();
            buscador.AcceptButton = btnBuscar;

            dgv.CellMouseDoubleClick += (s, ev) =>
            {
                if (ev.RowIndex >= 0)
                {
                    idSeleccionado = Convert.ToInt32(dgv.Rows[ev.RowIndex].Cells["ID"].Value);
                    nombreSeleccionado = dgv.Rows[ev.RowIndex].Cells["Producto"].Value.ToString();
                    buscador.DialogResult = DialogResult.OK;
                    buscador.Close();
                }
            };

            dgv.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter && dgv.CurrentRow != null)
                {
                    ev.Handled = true;
                    idSeleccionado = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value);
                    nombreSeleccionado = dgv.CurrentRow.Cells["Producto"].Value.ToString();
                    buscador.DialogResult = DialogResult.OK;
                    buscador.Close();
                }
            };

            // LÓGICA DEL BOTÓN "+" (Alta Rápida Genérica)
            btnNuevo.Click += async (s, ev) =>
            {
                Form formNuevo = new Form()
                {
                    Width = 400,
                    Height = 250,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Alta de Producto Sin Código",
                    StartPosition = FormStartPosition.CenterParent,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label lblNombre = new Label() { Left = 20, Top = 20, Width = 100, Text = "Nombre (*):" }; TextBox txtNombre = new TextBox() { Left = 130, Top = 20, Width = 230 };
                Label lblDesc = new Label() { Left = 20, Top = 50, Width = 100, Text = "Descripción:" }; TextBox txtDesc = new TextBox() { Left = 130, Top = 50, Width = 230 };
                Label lblMarca = new Label() { Left = 20, Top = 80, Width = 100, Text = "Marca:" }; TextBox txtMarca = new TextBox() { Left = 130, Top = 80, Width = 230 };
                CheckBox chkUnitario = new CheckBox() { Left = 130, Top = 110, Width = 230, Text = "¿Es producto unitario?", Checked = true };
                Button btnGuardarProd = new Button() { Text = "Guardar", Left = 260, Top = 160, Width = 100, Cursor = Cursors.Hand };

                EventHandler capitalizar = (senderObj, eventArgs) =>
                {
                    TextBox txt = senderObj as TextBox;
                    if (txt.Text.Length == 1) { txt.Text = txt.Text.ToUpper(); txt.SelectionStart = txt.Text.Length; }
                };
                txtNombre.TextChanged += capitalizar; txtDesc.TextChanged += capitalizar; txtMarca.TextChanged += capitalizar;

                formNuevo.Controls.Add(lblNombre); formNuevo.Controls.Add(txtNombre); formNuevo.Controls.Add(lblDesc); formNuevo.Controls.Add(txtDesc);
                formNuevo.Controls.Add(lblMarca); formNuevo.Controls.Add(txtMarca); formNuevo.Controls.Add(chkUnitario); formNuevo.Controls.Add(btnGuardarProd);
                formNuevo.AcceptButton = btnGuardarProd;

                btnGuardarProd.Click += async (senderBtn, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    {
                        MessageBox.Show("El nombre del producto es obligatorio.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    formNuevo.Cursor = Cursors.WaitCursor;
                    btnGuardarProd.Enabled = false;

                    string marcaIngresada = string.IsNullOrWhiteSpace(txtMarca.Text) ? "S/M" : txtMarca.Text.Trim();
                    await ApiServices.GarantizarMarcaExiste(marcaIngresada);

                    string codigoInventado = DateTime.Now.ToString("yyyyMMddHHmmss");

                    var nuevoProducto = new ProductoCreateRequest
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Descripcion = txtDesc.Text.Trim(),
                        Marca = marcaIngresada,
                        EsUnitario = chkUnitario.Checked,
                        Precio = 0,
                        CodigoGenerico = codigoInventado,
                        ProductoSinCodigo = true,
                        Gtin = codigoInventado
                    };

                    int idCreado = await ApiServices.CrearProductoNuevo(nuevoProducto);

                    if (idCreado > 0)
                    {
                        idSeleccionado = idCreado;
                        nombreSeleccionado = txtNombre.Text.Trim();
                        formNuevo.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        btnGuardarProd.Enabled = true;
                    }
                    formNuevo.Cursor = Cursors.Default;
                };

                if (formNuevo.ShowDialog() == DialogResult.OK)
                {
                    buscador.DialogResult = DialogResult.OK;
                    buscador.Close();
                }
            };

            buscador.Shown += async (s, ev) => await realizarBusqueda();

            // EJECUCIÓN DEL RESULTADO DE BÚSQUEDA
            if (buscador.ShowDialog() == DialogResult.OK)
            {
                int indexNuevaFila = dgvIngreso.Rows.Add();
                DataGridViewRow fila = dgvIngreso.Rows[indexNuevaFila];

                fila.Cells["Producto"].Value = nombreSeleccionado;
                // Guarda el ID del producto escondido para que el sistema sepa a qué pegarle
                fila.Cells["id"].Value = idSeleccionado;

                // Mueve el foco a Lote para que el operario escriba de corrido
                dgvIngreso.CurrentCell = fila.Cells["Lote"];
                dgvIngreso.BeginEdit(true);
            }
        }
    }
}