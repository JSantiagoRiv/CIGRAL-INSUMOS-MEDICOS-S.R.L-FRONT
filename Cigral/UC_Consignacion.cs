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

                // Acá pondrías la lógica de lblPagina.Text = $"{_paginaActual} de {resultado.totalPages}"; (Si lo usás)
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
    }
}
