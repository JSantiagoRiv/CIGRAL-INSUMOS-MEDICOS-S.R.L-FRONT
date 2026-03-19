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
            // Le armamos las opciones a mano (Acá podés cambiar el 1 y el 2 si el backend usa otros números)
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

                // Le manda el tipo (si es 0, le mandamos null para que el backend traiga todos)
                int? parametroTipo = tipo == 0 ? (int?)null : tipo;

                var lista = await ApiServices.ObtenerAuditoria(parametroTipo);

                if (lista == null) return;

                foreach (var item in lista)
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
                        item.stockNuevo, // E }n el diseño dice "Stock Posterior"
                        item.remitoIngresoId,
                        item.remitoEgresoId,
                        item.usuario,
                        item.observaciones // En el diseño dice "Detalles"
                    );
                }
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
    }
}