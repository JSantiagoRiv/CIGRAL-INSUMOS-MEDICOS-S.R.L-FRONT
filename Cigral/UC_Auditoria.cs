using Cigral.Models;
using Cigral.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks; // Agregado para usar Task en el método asíncrono
using System.Windows.Forms;

namespace Cigral
{
    /// <summary>
    /// Pantalla encargada de mostrar el registro histórico (Auditoría) de todos los movimientos de stock.
    /// Registra ingresos, egresos, usuarios y cantidades de forma inmutable.
    /// </summary>
    public partial class UC_Auditoria : UserControl
    {

        private int _paginaActual = 1;
        private int _filasPorPagina = 25;
        public UC_Auditoria()
        {


            InitializeComponent();

            // Nota: Acá se agregan items manualmente, pero luego ConfigurarComboBox() 
            // lo pisa con un DataSource. Se deja intacto para no alterar la lógica original.
            cmbMov.Items.Clear(); // Limpia por si había basura
            cmbMov.Items.Add("TODOS");
            cmbMov.Items.Add("INGRESOS");
            cmbMov.Items.Add("EGRESOS");

            // Selecciona el primero para que no quede en blanco
            cmbMov.SelectedIndex = 0;

            // Bloquea para que no puedan escribir
            cmbMov.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // --- INICIALIZACIÓN ---

        /// <summary>
        /// Se ejecuta al cargar la pantalla. Prepara el ComboBox y carga la grilla completa.
        /// </summary>
        private async void UC_Auditoria_Load(object sender, EventArgs e)
        {
            ConfigurarComboBox();
            await CargarGrillaAuditoria(0); // 0 = Todos
        }

        // Evento duplicado autogenerado por Visual Studio (hace lo mismo que el Load de arriba)
        private async void UC_Auditoria_Load_1(object sender, EventArgs e)
        {
            ConfigurarComboBox();
            await CargarGrillaAuditoria(0); // 0 = Todos
        }

        /// <summary>
        /// Configura la lista desplegable de tipos de movimiento con IDs específicos para el backend.
        /// </summary>
        private void ConfigurarComboBox()
        {
            // opciones (cambiar el 1 y el 2 si el backend usa otros números)
            var opciones = new List<dynamic>
            {
                new { Id = 0, Nombre = "Todos" },
                new { Id = 1, Nombre = "Ingresos" },
                new { Id = 2, Nombre = "Egresos" }
            };

            cmbMov.DataSource = opciones;
            cmbMov.DisplayMember = "Nombre";
            cmbMov.ValueMember = "Id";

            // Engancha el evento para que cuando el usuario cambie de opción, filtre automáticamente
            cmbMov.SelectedIndexChanged += async (s, ev) =>
            {
                if (cmbMov.SelectedValue != null)
                {
                    _paginaActual = 1;
                    int tipoSeleccionado = (int)cmbMov.SelectedValue;
                    await CargarGrillaAuditoria(tipoSeleccionado);
                }
            };
        }

        // --- LÓGICA PRINCIPAL ---

        /// <summary>
        /// Consulta la API y dibuja los movimientos en la grilla según el filtro seleccionado.
        /// </summary>
        private async Task CargarGrillaAuditoria(int tipo)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                dgvAuditoria.Rows.Clear();

                int? parametroTipo = tipo == 0 ? (int?)null : tipo;

                // Leemos lo que escribió el usuario
                string textoBuscado = txtBusquedaNombre.Text;

                // Le pasamos el textoBuscado a la API
                var respuesta = await ApiServices.ObtenerAuditoria(parametroTipo, textoBuscado, _paginaActual, _filasPorPagina);


                // Para que no se rompa al navegar
                if (this.IsDisposed) return;

                if (respuesta == null || respuesta.items == null) return;

                // 2. Llena la grilla
                foreach (var item in respuesta.items)
                {
                    dgvAuditoria.Rows.Add(
                        item.tipo,
                        item.fechaMovimiento.ToString("dd/MM/yyyy HH:mm"),
                        item.productoNombre,
                        item.depositoNombre,
                        item.codigoLote,
                        item.numeroSerie,
                        item.cantidad,
                        item.stockAnterior,
                        item.stockNuevo,
                        item.remitoIngresoId,
                        item.remitoEgresoId,
                        item.usuario,
                        item.observaciones
                    );
                }

                // 3. Actualiza los botones y el texto de abajo
                int totalPaginas = respuesta.totalPages == 0 ? 1 : respuesta.totalPages;
                lblPagina.Text = $"Página {respuesta.pageNumber} de {totalPaginas}";

                // Apaga o prende según lo que diga el back
                btnAnterior.Enabled = respuesta.hasPreviousPage;
                btnSiguiente.Enabled = respuesta.hasNextPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar la grilla: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // --- EXPORTACIÓN ---

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

        /// <summary>
        /// Genera un archivo CSV con el contenido actual de la grilla de auditoría y lo abre.
        /// </summary>
        private void ExportarExcel()
        {
            if (dgvAuditoria.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo Excel/CSV (*.csv)|*.csv";
            sfd.FileName = "Auditoria_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Usa StringBuilder para armar el texto más rápido en memoria
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    // 1. Encabezados
                    string[] titulos = new string[dgvAuditoria.Columns.Count];
                    for (int i = 0; i < dgvAuditoria.Columns.Count; i++)
                    {
                        titulos[i] = dgvAuditoria.Columns[i].HeaderText;
                    }
                    sb.AppendLine(string.Join(";", titulos));

                    // 2. Filas de datos
                    foreach (DataGridViewRow fila in dgvAuditoria.Rows)
                    {
                        if (!fila.IsNewRow)
                        {
                            string[] celdas = new string[dgvAuditoria.Columns.Count];

                            for (int i = 0; i < dgvAuditoria.Columns.Count; i++)
                            {
                                celdas[i] = fila.Cells[i].Value?.ToString() ?? "";
                            }

                            sb.AppendLine(string.Join(";", celdas));
                        }
                    }

                    // 3. Guarda y abre el archivo
                    System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);

                    MessageBox.Show("Exportado con éxito.", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message);
                }
            }
        }

        // --- NAVEGACIÓN ---

        private void iconBtnBack_Click_1(object sender, EventArgs e)
        {
            // Busca el formulario principal
            FormMain principal = this.ParentForm as FormMain;

            // Si lo encuentra, resetea el menú (apaga el botón violeta)
            if (principal != null)
            {
                principal.ResetearMenu();
            }

            // Cierra la pantalla
            this.Dispose();
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            _paginaActual--;
            int tipoActual = cmbMov.SelectedValue != null ? (int)cmbMov.SelectedValue : 0;
            await CargarGrillaAuditoria(tipoActual);
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            _paginaActual++;
            int tipoActual = cmbMov.SelectedValue != null ? (int)cmbMov.SelectedValue : 0;
            await CargarGrillaAuditoria(tipoActual);
        }

        private void txtBusquedaNombre_TextChanged(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private async void txtBusquedaNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                timerBusqueda.Stop();

                _paginaActual = 1;
                int tipoActual = cmbMov.SelectedValue != null ? (int)cmbMov.SelectedValue : 0;
                await CargarGrillaAuditoria(tipoActual);
            }
        }

        private async void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();

            _paginaActual = 1;
            int tipoActual = cmbMov.SelectedValue != null ? (int)cmbMov.SelectedValue : 0;
            await CargarGrillaAuditoria(tipoActual);
        }
    }
}