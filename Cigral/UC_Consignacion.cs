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
    public partial class UC_Consignacion : UserControl
    {
        //Variables para la paginacion
        private int _paginaActual = 1;
        private int _filasPorPagina = 25;


        public UC_Consignacion()
        {
            InitializeComponent();
        }


        private async Task CargarDatosFiltrados()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // 1. Capturamos todo lo que escribió el operario
                string nombreBuscado = txtNombre.Text.Trim();
                string loteBuscado = txtLote.Text.Trim();
                string serieBuscada = txtSerie.Text.Trim();
                string entidadBuscada = txtEntidad.Text.Trim();

                // 2. Disparamos la búsqueda al backend
                var resultado = await ApiServices.ObtenerConsignaciones(nombreBuscado, loteBuscado, serieBuscada, entidadBuscada, _paginaActual, _filasPorPagina);

                if (this.IsDisposed) return;

                // 3. Llenamos la grilla y actualizamos los labels de páginas
                dgvConsignaciones.DataSource = resultado.items;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar consignaciones: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private async void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            _paginaActual = 1;
            _ = CargarDatosFiltrados();
        }

        private async void txtNombre_TextChanged(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private async void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el ruidito molesto de Windows
                timerBusqueda.Stop();
                _paginaActual = 1; // Siempre que buscamos algo nuevo, volvemos a la hoja 1
                _ = CargarDatosFiltrados();
            }
        }

        private async void txtEntidad_TextChanged(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private async void txtEntidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el ruidito molesto de Windows
                timerBusqueda.Stop();
                _paginaActual = 1; // Siempre que buscamos algo nuevo, volvemos a la hoja 1
                _ = CargarDatosFiltrados();
            }
        }

        private void txtLote_TextChanged(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private void txtLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el ruidito molesto de Windows
                timerBusqueda.Stop();
                _paginaActual = 1; // Siempre que buscamos algo nuevo, volvemos a la hoja 1
                _ = CargarDatosFiltrados();
            }
        }

        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el ruidito molesto de Windows
                timerBusqueda.Stop();
                _paginaActual = 1; // Siempre que buscamos algo nuevo, volvemos a la hoja 1
                _ = CargarDatosFiltrados();
            }
        }

        private void dgvConsignaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void UC_Consignacion_Load(object sender, EventArgs e)
        {
            dgvConsignaciones.AutoGenerateColumns = false;
            await CargarDatosFiltrados();
        }

        private async void disminuirConsignacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvConsignaciones.CurrentRow != null)
            {
                // Extraemos el objeto original completo que está "atado" a esta fila
                var consignacionSeleccionada = (GetConsignacionResponseDto?)dgvConsignaciones.CurrentRow.DataBoundItem;

                int idReal = consignacionSeleccionada.id;
                int idExistencia = consignacionSeleccionada.existenciaId;
                int idCliente = consignacionSeleccionada.clienteId;

                // 2. Armamos un mensaje claro para el usuario usando los datos del objeto
                string informacion = $"¿Esta seguro que desea modificar los productos en consignación de:\n\n" +
                                 $"- Producto: {consignacionSeleccionada.productoNombre}\n" +
                                 $"- Cliente: {consignacionSeleccionada.clienteRazonSocial}\n" +
                                 $"\n\nEsta acción modificará el inventario.";

                // 3. Mostramos el MessageBox con botones OK/Cancel y guardamos el resultado
                DialogResult confirmacion = MessageBox.Show(
                    informacion,
                    "Confirmar Operación",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.OK)
                {                    
                    int cantidadDisponible = await ApiServices.GetStockDisponible(consignacionSeleccionada.existenciaId);
                    int cantidadElegida = 0;
                    Form prompt = new Form()
                    {
                        Width = 400,   // Lo ensanchamos un poquito (de 380 a 400) para nombres largos
                        Height = 240,  // Aumentamos el alto (de 180 a 240) para que entren los nuevos controles
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "Modificar Consignación",
                        StartPosition = FormStartPosition.CenterScreen,
                        MaximizeBox = false,
                        MinimizeBox = false
                    };

                    // Arreglamos la concatenación agregando los saltos de línea (\n) correspondientes
                    string mensaje = $"Se va a modificar la consignación de {consignacionSeleccionada.productoNombre}.\n" +
                                     $"Cliente: {consignacionSeleccionada.clienteRazonSocial}";
                    
                    mensaje += $"\n\n¿Cuantas unidades desea liberar de consignación?";

                    // Aumentamos el Height del Label de 40 a 90 para que entren hasta 6 líneas de texto sin cortarse
                    Label lblTexto = new Label() { Left = 20, Top = 20, Width = 340, Height = 90, Text = mensaje };

                    // Bajamos el input y el botón (Top pasó de 70 a 120) para que no se encimen con el Label más grande
                    NumericUpDown inputNum = new NumericUpDown() { Left = 20, Top = 120, Width = 120, Minimum = 1, Maximum = consignacionSeleccionada.cantidad, Value = 1, Font = new Font("Segoe UI", 12) };
                    Button btnAceptar = new Button() { Text = "Aceptar", Left = 240, Top = 120, Width = 100, DialogResult = DialogResult.OK };

                    prompt.Controls.Add(lblTexto);
                    prompt.Controls.Add(inputNum);
                    prompt.Controls.Add(btnAceptar);

                    prompt.AcceptButton = btnAceptar;

                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        cantidadElegida = (int)inputNum.Value;
                        
                        
                            var response = await ApiServices.DisminuirConsignacion(new ConsignacionDisminuirDto
                            {
                                cantidad = cantidadElegida
                            }, consignacionSeleccionada.id);
                            if (response != null)
                            {
                                MessageBox.Show("Consignación disminuida exitosamente.");
                                await CargarDatosFiltrados();
                            }
                            else
                            {
                                MessageBox.Show("Error al disminuir la consignación.");
                            }
                        
                    }
                }
            }
            
        }
    }
}
