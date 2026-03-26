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
    public partial class UC_Entidades : UserControl
    {

        string Busqueda = string.Empty;
        int idParaLaApi = 0; // Variable para almacenar el ID oculto
        string tipoEntidad = string.Empty;
        public UC_Entidades()
        {
            InitializeComponent();
            this.Load += UC_Entidades_Load; // Asegura que el evento Load esté suscrito al método correcto.
        }

        private void labelTitulo_Click(object sender, EventArgs e)
        {

        }

        private async void UC_Entidades_Load(object sender, EventArgs e)
        {
            // 1. FUNDAMENTAL: Evita que la tabla cree columnas automáticas para el ID y otros datos
            Cursor.Current = Cursors.WaitCursor;

            dgvEntidades.AutoGenerateColumns = false;

            // 2. Vincular tus columnas visuales con las propiedades exactas de tu EntidadDto.
            // El texto entre comillas del lado derecho DEBE ser exactamente igual al nombre de la propiedad en tu clase C#.
            // Asumiendo que el "Name" de tus columnas visuales son colRazonSocial, colCuit, colTipoEntidad:
            RazonSocial.DataPropertyName = "RazonSocial";
            Cuit.DataPropertyName = "Cuit";
            TipoEntidad.DataPropertyName = "TipoEntidad";
            try {
                await CargarEntidades(); // Carga los datos al iniciar el control.
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private async Task CargarEntidades()
        {
            var listaEntidades = await ApiServices.ObtenerEntidades(razonSocial: Busqueda);

            if (this.IsDisposed) return;

            dgvEntidades.DataSource = listaEntidades;
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            razonSocialBox.Text = string.Empty;
            emailBox.Text = string.Empty;
            glnBox.Text = string.Empty;
            cuitBox.Text = string.Empty;
            telefonoBox.Text = string.Empty;
            direccionBox.Text = string.Empty;
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
            await CargarEntidades();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dgvEntidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que se hizo clic en una fila válida y no en los encabezados de las columnas (RowIndex = -1)
            if (e.RowIndex >= 0)
            {
                // 1. Obtener la fila seleccionada
                DataGridViewRow fila = dgvEntidades.Rows[e.RowIndex];

                // 2. Recuperar el objeto original casteando el DataBoundItem
                EntidadDto entidadSeleccionada = (EntidadDto)fila.DataBoundItem;

                // 3. ¡Listo! Aquí tienes tu ID extraído directamente del objeto
                idParaLaApi = entidadSeleccionada.IdOriginal;
                tipoEntidad = entidadSeleccionada.TipoEntidad;

                razonSocialBox.Text = entidadSeleccionada.RazonSocial;
                emailBox.Text = entidadSeleccionada.Email;
                glnBox.Text = entidadSeleccionada.Gln;
                cuitBox.Text = entidadSeleccionada.Cuit;
                telefonoBox.Text = entidadSeleccionada.Telefono;
                direccionBox.Text = entidadSeleccionada.Direccion;

                modificarButton.Enabled = false; // Deshabilitar el botón hasta que se realice un cambio en los campos

                // Aquí ya puedes hacer la petición a la base de datos o la API
                // MessageBox.Show($"El ID oculto es: {idParaLaApi}");
                // await CargarDetallesDelCliente(idParaLaApi);
            }
        }

        private void razonSocialBox_TextChanged(object sender, EventArgs e)
        {
            modificarButton.Enabled = true;
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            modificarButton.Enabled = false;
            PantallaCarga pantallaCarga = new PantallaCarga();
            pantallaCarga.Show(this);

            try { 
                var entidadModificada = new EntidadDto
                {
                    IdOriginal = idParaLaApi, // Usamos el ID oculto para identificar qué entidad modificar
                    RazonSocial = razonSocialBox.Text,
                    TipoEntidad = tipoEntidad,
                    Email = emailBox.Text,
                    Gln = glnBox.Text,
                    Cuit = cuitBox.Text,
                    Telefono = telefonoBox.Text,
                    Direccion = direccionBox.Text
                };

                var resultado = await ApiServices.UpdateEntidad(entidadModificada);
            
                if (resultado != 0)
                {
                    await CargarEntidades();
                    MessageBox.Show($"Entidad '{entidadModificada.RazonSocial}' actualizada correctamente.", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
