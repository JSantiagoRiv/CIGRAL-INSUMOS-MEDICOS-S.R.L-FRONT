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
    public partial class UC_Productos : UserControl
    {
        string Busqueda = string.Empty;
        int idParaLaApi = 0; // Variable para almacenar el ID oculto
        public UC_Productos()
        {
            InitializeComponent();
            this.Load += UC_Productos_Load;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
            Busqueda = txtBuscar.Text.Trim();
        }

        private async void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            var listaEntidades = await ApiServices.ObtenerProductosCatalogo(filtroNombre: "", filtroGlobal:Busqueda, pageSize: 27);

            if (this.IsDisposed) return;

            dgvProductos.DataSource = listaEntidades.items;
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            nombreBox.Text = string.Empty;
            gtinBox.Text = string.Empty;
            descripcionBox.Text = string.Empty;
            marcaBox.Text = string.Empty;
            codigoInternoBox.Text = string.Empty;
            codigoGenericoBox.Text = string.Empty;
        }

        private async void UC_Productos_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            dgvProductos.AutoGenerateColumns = false;

            // 2. Vincular tus columnas visuales con las propiedades exactas de tu EntidadDto.
            // El texto entre comillas del lado derecho DEBE ser exactamente igual al nombre de la propiedad en tu clase C#.
            Nombre.DataPropertyName = "nombre";
            Marca.DataPropertyName = "marca";
            GTIN.DataPropertyName = "gtin";
            CodigoInterno.DataPropertyName = "codigoInterno";
            try
            {
                await CargarProductos(); // Carga los datos al iniciar el control.
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que se hizo clic en una fila válida y no en los encabezados de las columnas (RowIndex = -1)
            if (e.RowIndex >= 0)
            {
                // 1. Obtener la fila seleccionada
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                // 2. Recuperar el objeto original casteando el DataBoundItem
                var entidadSeleccionada = (ProductoResponseDto)fila.DataBoundItem;

                // 3. ¡Listo! Aquí tienes tu ID extraído directamente del objeto
                idParaLaApi = entidadSeleccionada.id;

                nombreBox.Text = entidadSeleccionada.nombre;
                marcaBox.Text = entidadSeleccionada.marca;
                gtinBox.Text = entidadSeleccionada.gtin;
                descripcionBox.Text = entidadSeleccionada.descripcion;
                codigoGenericoBox.Text = entidadSeleccionada.codigoGenerico;
                codigoInternoBox.Text = entidadSeleccionada.codigoInterno;

                modificarButton.Enabled = false; // Deshabilitar el botón hasta que se realice un cambio en los campos

                // Aquí ya puedes hacer la petición a la base de datos o la API
                // MessageBox.Show($"El ID oculto es: {idParaLaApi}");
                // await CargarDetallesDelCliente(idParaLaApi);
            }
        }

        private void nombreBox_TextChanged(object sender, EventArgs e)
        {
            modificarButton.Enabled = true;
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            modificarButton.Enabled = false;
            PantallaCarga pantallaCarga = new PantallaCarga();
            pantallaCarga.Show(this);

            try
            {
                var productoModificado = new ProductoUpdateDto
                {
                    nombre = nombreBox.Text,

                    // Si la caja está vacía, enviamos null. Si tiene algo, enviamos el texto.
                    marca = string.IsNullOrWhiteSpace(marcaBox.Text) ? null : marcaBox.Text,
                    gtin = string.IsNullOrWhiteSpace(gtinBox.Text) ? null : gtinBox.Text,
                    codigoInterno = string.IsNullOrWhiteSpace(codigoInternoBox.Text) ? null : codigoInternoBox.Text,
                    codigoGenerico = string.IsNullOrWhiteSpace(codigoGenericoBox.Text) ? null : codigoGenericoBox.Text,
                    descripcion = string.IsNullOrWhiteSpace(descripcionBox.Text) ? null : descripcionBox.Text,

                    precio = 0
                };

                var resultado = await ApiServices.UpdateProducto(productoModificado, idParaLaApi);

                if (resultado != 0)
                {
                    await CargarProductos();
                    MessageBox.Show($"Entidad '{productoModificado.nombre}' actualizado correctamente.", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    modificarButton.Enabled = false;
                }
                else
                {
                    modificarButton.Enabled = true;
                }
            }
            finally
            {
                pantallaCarga.Close();
                this.Enabled = true;
            }
        }
    }
}
