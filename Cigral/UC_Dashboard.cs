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
    public partial class UC_Dashboard : UserControl
    {
        public UC_Dashboard()
        {
            InitializeComponent();
        }

        private void labelTitulo_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void UC_Dashboard_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            await CargarDashboard();
        }

        private async Task CargarDashboard()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                dgvVencidos.Rows.Clear();
                dgvProximos.Rows.Clear();

                // 🔥 EL TRUCO: Hacemos 1 sola llamada pidiendo TODO el espectro de fechas 
                // Desde hace -10.000 días (pasado) hasta 1.500 días (futuro), obligando a que traiga los vencidos
                var listaCompleta = await ApiServices.ObtenerVencimientos(-10000, 180, true);

                if (this.IsDisposed) return;

                // Recorremos la lista una sola vez y repartimos
                foreach (var item in listaCompleta)
                {
                    // Si los días son 0 o negativos, está VENCIDO -> Va a la izquierda (Rojo)
                    if (item.diasParaVencer <= 0)
                    {
                        int index = dgvVencidos.Rows.Add(
                            item.productoNombre,
                            item.codigoLote,
                            item.fechaVencimiento?.ToString("dd/MM/yyyy") ?? "-",
                            item.diasParaVencer,
                            item.depositoNombre,
                            item.cantidad
                        );

                        dgvVencidos.Rows[index].DefaultCellStyle.BackColor = Color.MistyRose;
                        dgvVencidos.Rows[index].DefaultCellStyle.ForeColor = Color.DarkRed;
                        dgvVencidos.Rows[index].DefaultCellStyle.SelectionBackColor = Color.Firebrick;
                    }
                    // Si los días son mayores a 0, está PRÓXIMO A VENCER -> Va a la derecha (Amarillo)
                    else
                    {
                        int index = dgvProximos.Rows.Add(
                            item.productoNombre,
                            item.codigoLote,
                            item.fechaVencimiento?.ToString("dd/MM/yyyy") ?? "-",
                            item.diasParaVencer,
                            item.depositoNombre,
                            item.cantidad
                        );

                        dgvProximos.Rows[index].DefaultCellStyle.BackColor = Color.LightYellow;
                        dgvProximos.Rows[index].DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
                        dgvProximos.Rows[index].DefaultCellStyle.SelectionBackColor = Color.Orange;
                    }
                }

                dgvVencidos.ClearSelection();
                dgvProximos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
