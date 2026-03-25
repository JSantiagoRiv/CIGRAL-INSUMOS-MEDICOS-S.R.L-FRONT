using Cigral.Models;
using Cigral.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq; // Necesario para filtrar las listas con LINQ (.Where, .Sum)
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cigral
{
    /// <summary>
    /// Pantalla encargada de visualizar, filtrar y exportar el stock actual de mercadería.
    /// </summary>
    public partial class UC_Stock : UserControl
    {

        // --- VARIABLES DE PAGINACIÓN ---
        private int _paginaActual = 1;
        private int _filasPorPagina = 25;
        private int _totalPaginas = 1;
        private List<ExistenciaDto> _listaCompleta = new List<ExistenciaDto>();


        public UC_Stock()
        {
            InitializeComponent();
            this.Load += UC_Stock_Load;
        }

        // --- CARGA DE DATOS INICIAL ---

        /// <summary>
        /// Se ejecuta apenas la pantalla termina de dibujarse. 
        /// Configura los combos por defecto y trae el stock completo de la base de datos.
        /// </summary>
        private async void UC_Stock_Load(object sender, EventArgs e)
        {
            // Trae los datos antes de setear los combos para que no hagan doble búsqueda accidental
            await CargarDatosFiltrados();

            cmbOrdenar.SelectedIndex = -1;       // -1 significa que arranca vacío (sin orden específico)
            cmbDireccionOrden.SelectedIndex = 0; // 0 es "Ascendente" por defecto
        }

        // --- MOTOR DE BÚSQUEDA Y FILTRADO ---

        /// <summary>
        /// Método central de la pantalla. Recolecta los filtros visuales, 
        /// pide los datos a la API y luego aplica los filtros locales (CheckBoxes) usando LINQ.
        /// </summary>
        private async Task CargarDatosFiltrados()
        {
            // 1. Pone el cursor en espera (el circulito dando vueltas) 
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // --- PARTE A: DATOS DESDE EL BACKEND ---

                // 1. Captura lo que el usuario puso en el buscador de texto
                string textoABuscar = txtBuscar.Text.Trim();

                // 2. Mapeo del ComboBox a los números que espera el Backend para ordenar
                int ordenBackend = 0; // Por defecto ordena por ID
                if (cmbOrdenar.SelectedIndex == 0) ordenBackend = 1;      // Producto
                else if (cmbOrdenar.SelectedIndex == 1) ordenBackend = 2; // Vencimiento
                else if (cmbOrdenar.SelectedIndex == 2) ordenBackend = 3; // Cantidad

                // 3. Captura la dirección (0 = Ascendente, 1 = Descendente)
                bool descendente = cmbDireccionOrden.SelectedIndex == 1;

                // 4. Dispara a la API (Trae la lista cruda filtrada por texto y ordenada)
                var listaFiltrada = await ApiServices.ObtenerExistencias(textoABuscar, false, false, ordenBackend, descendente);

                //Para quw no se rompa
                if (this.IsDisposed) return;

                // --- PARTE B: FILTROS LOCALES RÁPIDOS ---

                // A) Ocultar Stock Cero
                if (chkOcultarCero.Checked)
                {
                    // Nos quedam SOLO con los productos que tienen al menos 1 de cantidad
                    listaFiltrada = listaFiltrada.Where(p => p.Cantidad > 0).ToList();
                }

                // B) Ocultar Vencidos
                if (chkVencidos.Checked)
                {
                    // queda con los que cumplen UNA de estas condiciones:
                    // - No tienen fecha de vencimiento asignada (!HasValue)
                    // - O tienen fecha, y esa fecha es igual o mayor a hoy (no están vencidos)
                    listaFiltrada = listaFiltrada.Where(p =>
                        !p.FechaVencimiento.HasValue ||
                        p.FechaVencimiento.Value.Date >= DateTime.Now.Date
                    ).ToList();
                }

                // C) Próximos a Vencer (Menos de 6 meses)
                if (chkProximosAVencer.Checked)
                {
                    DateTime fechaLimite = DateTime.Now.Date.AddMonths(6);

                    // Filtro: Tiene que tener fecha, NO estar vencido hoy, y vencer antes de nuestra fecha límite
                    listaFiltrada = listaFiltrada.Where(p =>
                        p.FechaVencimiento.HasValue &&
                        p.FechaVencimiento.Value.Date >= DateTime.Now.Date &&
                        p.FechaVencimiento.Value.Date <= fechaLimite
                    ).ToList();
                }

                // --- PARTE C: ACTUALIZACIÓN VISUAL ---

                // 1. Guardamos la lista ya filtrada en la caja principal para que la paginación la lea
                _listaCompleta = listaFiltrada;

                // 2. Actualizamos el contador del Label sumando el stock físico real
                int totalFisico = _listaCompleta.Sum(p => p.Cantidad);
                lblTotalProductos.Text = $"Total de productos: {totalFisico}";

                // 3. Como cambiaron los filtros (el usuario buscó algo), reseteamos a la página 1 a la fuerza
                _paginaActual = 1;

                // 4. En vez de mandar la lista directa a la grilla, llamamos a la función que la corta de a 25
                MostrarPaginaActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Devuelve el cursor a la normalidad
                Cursor.Current = Cursors.Default;
            }
        }

        // --- RENDERIZADO VISUAL ---

        /// <summary>
        /// Toma la lista final filtrada y dibuja fila por fila en la grilla visual.
        /// </summary>
        private void ActualizarGrilla(List<ExistenciaDto> lista)
        {
            // Limpia los renglones viejos
            dgvStock.Rows.Clear();

            // Si la lista vino vacía (nula), corta acá
            if (lista == null) return;

            // Recorre la lista inyectando los datos
            foreach (var item in lista)
            {
                int rowIndex = dgvStock.Rows.Add();
                DataGridViewRow fila = dgvStock.Rows[rowIndex];

                fila.Cells["Id"].Value = item.Id;
                fila.Cells["CodProd"].Value = item.ProductoId;
                fila.Cells["Producto"].Value = item.ProductoNombre;
                fila.Cells["Lote"].Value = item.CodigoLote;
                fila.Cells["Serie"].Value = item.NumSerie;
                fila.Cells["Cantidad"].Value = item.Cantidad;

                // Lógica de visualización de Fechas y Estado
                if (item.FechaVencimiento.HasValue)
                {
                    fila.Cells["Vencimiento"].Value = item.FechaVencimiento.Value.ToString("dd/MM/yyyy");

                    // Marca visualmente con texto si ya superó la fecha actual
                    if (item.FechaVencimiento.Value.Date < DateTime.Now.Date)
                    {
                        fila.Cells["Estado"].Value = "VENCIDO";
                    }
                    else
                    {
                        fila.Cells["Estado"].Value = "OK";
                    }
                }
                else
                {
                    // Si no requiere vencimiento, rellenamos con guiones
                    fila.Cells["Vencimiento"].Value = "-";
                    fila.Cells["Estado"].Value = "-";
                }
            }
        }

        // --- EXPORTACIÓN ---

        /// <summary>
        /// Exporta todo el contenido actual de la grilla a un archivo CSV (apto para Excel).
        /// </summary>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvStock.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Abre un cuadro de diálogo para que el usuario elija dónde guardar su archivo
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo Excel/CSV (*.csv)|*.csv"; // Exportamos como CSV para máxima compatibilidad
            sfd.FileName = "Stock_Productos_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Usa StringBuilder ----> más eficiente en memoria para armar textos grandes
                    StringBuilder sb = new StringBuilder();

                    // 1. Arma los encabezados de las columnas (separados por punto y coma)
                    string[] titulos = new string[dgvStock.Columns.Count];
                    for (int i = 0; i < dgvStock.Columns.Count; i++)
                    {
                        titulos[i] = dgvStock.Columns[i].HeaderText;
                    }
                    sb.AppendLine(string.Join(";", titulos));

                    // 2. Recorre las filas y columnas para extraer los datos de la grilla
                    foreach (DataGridViewRow fila in dgvStock.Rows)
                    {
                        if (!fila.IsNewRow)
                        {
                            string[] celdas = new string[dgvStock.Columns.Count];
                            for (int i = 0; i < dgvStock.Columns.Count; i++)
                            {
                                // Si la celda está vacía, pone un string vacío ("") para no desfasar la columna
                                celdas[i] = fila.Cells[i].Value?.ToString() ?? "";
                            }
                            sb.AppendLine(string.Join(";", celdas));
                        }
                    }

                    // 3. Guarda el archivo directamente en el disco
                    System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Exportado con éxito.", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Le pide a Windows que abra el archivo automáticamente con su programa por defecto (Excel)
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- EVENTOS REACTIVOS (Disparadores de Búsqueda) ---

        private async void chkOcultarCero_CheckedChanged(object sender, EventArgs e) => await CargarDatosFiltrados();
        private async void chkVencidos_CheckedChanged(object sender, EventArgs e) => await CargarDatosFiltrados();
        private async void chkProximosAVencer_CheckedChanged(object sender, EventArgs e) => await CargarDatosFiltrados();
        private async void cmbOrdenar_SelectedIndexChanged(object sender, EventArgs e) => await CargarDatosFiltrados();
        private async void cmbDireccionOrden_SelectedIndexChanged(object sender, EventArgs e) => await CargarDatosFiltrados();
        private async void iconBtnSearch_Click(object sender, EventArgs e) => await CargarDatosFiltrados();

        // --- OPTIMIZACIÓN DE BÚSQUEDA EN TEXTO (Debounce) ---

        /// <summary>
        /// Cada vez que el usuario teclea una letra, frenamos el reloj y lo volvemos a arrancar desde cero.
        /// Así, la búsqueda automática solo arranca si el usuario deja de escribir por los milisegundos que dura el Timer.
        /// </summary>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private async void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop(); // Frena el timer para que no se ejecute en loop infinito
            await CargarDatosFiltrados();
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Si el usuario es ansioso y aprieta Enter, no esperamos al Timer, frena y busca rap9do.
                e.SuppressKeyPress = true; // Evita el "plink" de error de Windows al apretar enter en un TextBox de una línea.
                timerBusqueda.Stop();
                await CargarDatosFiltrados();
            }
        }

        // --- NAVEGACIÓN ---

        private void iconBtnBack_Click(object sender, EventArgs e)
        {
            FormMain principal = this.ParentForm as FormMain;
            if (principal != null)
            {
                principal.ResetearMenu();
            }
            this.Dispose();
        }

        private void MostrarPaginaActual()
        {
            if (_listaCompleta == null || _listaCompleta.Count == 0)
            {
                lblPagina.Text = "Página 0 de 0";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                ActualizarGrilla(new List<ExistenciaDto>());
                return;
            }

            // Calculamos cuántas páginas hay en total
            _totalPaginas = (int)Math.Ceiling((double)_listaCompleta.Count / _filasPorPagina);

            // Cortamos la lista usando LINQ
            var porcion = _listaCompleta
                            .Skip((_paginaActual - 1) * _filasPorPagina)
                            .Take(_filasPorPagina)
                            .ToList();

            // Dibujamos la grilla
            ActualizarGrilla(porcion);

            // Actualizamos botones (asegurate de tener los controles en el diseño)
            lblPagina.Text = $"Página {_paginaActual} de {_totalPaginas}";
            btnAnterior.Enabled = _paginaActual > 1;
            btnSiguiente.Enabled = _paginaActual < _totalPaginas;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaActual > 1)
            {
                _paginaActual--;
                MostrarPaginaActual();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (_paginaActual < _totalPaginas)
            {
                _paginaActual++;
                MostrarPaginaActual();
            }
        }

        
    }
}