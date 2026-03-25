using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cigral.Services;

namespace Cigral
{
    /// <summary>
    /// Pantalla encargada de mostrar el historial de remitos (tanto de ingresos como de egresos).
    /// Permite filtrar por fechas, buscar por número y descargar los PDFs generados.
    /// </summary>
    public partial class UC_Remitos : UserControl
    {

        private int _paginaActual = 1;
        private int _filasPorPagina = 25;

        // Semáforo para evitar que dos búsquedas se ejecuten al mismo tiempo y rompan la grilla
        private bool _estaBuscando = false;

        public UC_Remitos()
        {
            InitializeComponent();
        }

        // --- INICIALIZACIÓN ---

        /// <summary>
        /// Se ejecuta al cargar la pantalla. Setea las fechas por defecto y dispara la primera búsqueda.
        /// </summary>
        private void UC_Remitos_Load(object sender, EventArgs e)
        {
            // Desenganch temporalmente los eventos para que no busque 2 veces al setear las fechas
            dtpDesde.ValueChanged -= dtpDesde_ValueChanged;

            // Pone las fechas por defecto: Desde el 1 de enero del año actual hasta hoy
            dtpDesde.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpHasta.Value = DateTime.Now;

            // Volvemos a enganchar el evento
            dtpDesde.ValueChanged += dtpDesde_ValueChanged;

            // Arrancamos la pantalla ejecutando la búsqueda automáticamente
            EjecutarBusqueda();
        }

        // --- LÓGICA PRINCIPAL ---

        /// <summary>
        /// Método central de la pantalla. Consulta la API y actualiza la grilla.
        /// Está blindado con un semáforo y control de memoria (IsDisposed).
        /// </summary>
        private async void EjecutarBusqueda()
        {
            // Si hay una búsqueda en curso, ignora esta nueva petición
            if (_estaBuscando) return;
            _estaBuscando = true; // Pone el semáforo en rojo

            btnBuscar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                bool buscarIngresos = rbIngresos.Checked;

                // Le manda a la API en qué página estamos y cuántas filas queremos
                var respuesta = await ApiServices.ObtenerHistorialRemitos(
                    buscarIngresos,
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date,
                    txtNroRemito.Text.Trim(),
                    _paginaActual,
                    _filasPorPagina
                );

                // chequea si el usuario ya cambió de pantalla.
                if (this.IsDisposed) return;

                // le pasa SOLO la lista interna (.items)
                dgvRemitos.DataSource = null;
                dgvRemitos.Columns.Clear();
                dgvRemitos.DataSource = respuesta.items;

                FormatearGrilla(buscarIngresos);


                int totalPaginas = respuesta.totalPages == 0 ? 1 : respuesta.totalPages;
                lblPagina.Text = $"Página {respuesta.pageNumber} de {totalPaginas}";

                btnAnterior.Enabled = respuesta.hasPreviousPage;
                btnSiguiente.Enabled = respuesta.hasNextPage;
            }
            catch (Exception ex)
            {
                // Solo muestra el error si la pantalla sigue viva
                if (!this.IsDisposed)
                {
                    MessageBox.Show("Error en la búsqueda:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // Solo reactiva los controles y el semáforo si la pantalla sigue viva
                if (!this.IsDisposed)
                {
                    _estaBuscando = false; // Semáforo verde
                    btnBuscar.Enabled = true;
                    Cursor = Cursors.Default;
                }
            }
        }

        // --- CONFIGURACIÓN VISUAL DE LA GRILLA ---

        /// <summary>
        /// Acomoda los títulos, esconde IDs, pinta las filas según el tipo de movimiento 
        /// e inyecta la columna especial con el botón para descargar el PDF.
        /// </summary>
        private void FormatearGrilla(bool sonIngresos)
        {
            if (dgvRemitos.Columns.Count == 0) return;

            // A. Oculta las columnas de IDs técnicos que el usuario no necesita ver
            if (dgvRemitos.Columns["id"] != null) dgvRemitos.Columns["id"].Visible = false;
            if (dgvRemitos.Columns["depositoId"] != null) dgvRemitos.Columns["depositoId"].Visible = false;
            if (dgvRemitos.Columns["entidadId"] != null) dgvRemitos.Columns["entidadId"].Visible = false;

            // B. Pone títulos limpios
            if (dgvRemitos.Columns["numeroRemito"] != null) dgvRemitos.Columns["numeroRemito"].HeaderText = "Nro. Remito";
            if (dgvRemitos.Columns["comprobanteAsociado"] != null) dgvRemitos.Columns["comprobanteAsociado"].HeaderText = "Comprobante Asociado";
            if (dgvRemitos.Columns["fecha"] != null) dgvRemitos.Columns["fecha"].HeaderText = "Fecha";
            if (dgvRemitos.Columns["depositoNombre"] != null) dgvRemitos.Columns["depositoNombre"].HeaderText = "Depósito";
            if (dgvRemitos.Columns["entidadNombre"] != null) dgvRemitos.Columns["entidadNombre"].HeaderText = "Entidad";
            if (dgvRemitos.Columns["observaciones"] != null) dgvRemitos.Columns["observaciones"].HeaderText = "Observaciones";

            // C. Pinta el fondo según si son ingresos (verde clarito) o egresos (rojo clarito)
            Color colorFondo = sonIngresos ? Color.FromArgb(235, 255, 235) : Color.FromArgb(255, 235, 235);
            foreach (DataGridViewRow row in dgvRemitos.Rows)
            {
                row.DefaultCellStyle.BackColor = colorFondo;
            }

            // D. Inyecta la columna con el Botón "Ver PDF"
            if (dgvRemitos.Columns["ColPdf"] == null)
            {
                DataGridViewButtonColumn btnPdf = new DataGridViewButtonColumn();
                btnPdf.Name = "ColPdf";
                btnPdf.HeaderText = "Documento";
                btnPdf.Text = "Ver PDF";
                btnPdf.UseColumnTextForButtonValue = true;
                btnPdf.FlatStyle = FlatStyle.Flat;
                btnPdf.DefaultCellStyle.BackColor = Color.LightSkyBlue; // Color para que resalte

                dgvRemitos.Columns.Add(btnPdf);
            }
        }

        // --- EVENTOS DE INTERFAZ Y REACCIONES ---

        /// <summary>
        /// Captura los clics adentro de la grilla. Si el clic fue en el botón del PDF, lo descarga.
        /// </summary>
        private async void dgvRemitos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si hicieron clic en el título de la columna o en una zona vacía, no hacemos nada
            if (e.RowIndex < 0) return;

            // Verifica si la columna que tocaron es justamente la de nuestro botón PDF
            if (dgvRemitos.Columns[e.ColumnIndex].Name == "ColPdf")
            {
                // Agarra el ID de esa fila
                int idRemitoSeleccionado = Convert.ToInt32(dgvRemitos.Rows[e.RowIndex].Cells["id"].Value);

                // Se fija si estamos en la pestaña de ingresos o egresos
                bool esIngreso = rbIngresos.Checked;

                Cursor = Cursors.WaitCursor;
                try
                {
                    // Descarga y abre el PDF usando la herramienta global
                    await ApiServices.DescargarAbrirPdfRemito(idRemitoSeleccionado, esIngreso);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar descargar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        // Búsqueda al hacer clic en el botón manualmente
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _paginaActual = 1; // Resetea la página
            EjecutarBusqueda();
        }

        // Búsqueda automática al cambiar a la pestaña de Ingresos
        private void rbIngresos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIngresos.Checked)
            {
                _paginaActual = 1;
                EjecutarBusqueda();
            }
        }

        // Búsqueda automática al cambiar a la pestaña de Egresos
        private void rbEgresos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEgresos.Checked)
            {
                _paginaActual = 1;
                EjecutarBusqueda();
            }
        }

        // Búsqueda automática al cambiar la fecha de inicio
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            _paginaActual = 1;
            EjecutarBusqueda();
        }

        // Búsqueda automática al cambiar la fecha de fin
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            _paginaActual = 1;
            EjecutarBusqueda();
        }


        // --- BOTONES DE PAGINACIÓN ---
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            _paginaActual--;
            EjecutarBusqueda();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _paginaActual++;
            EjecutarBusqueda();
        }
    }
}