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
    /// <summary>
    /// Pantalla inicial del sistema. 
    /// Se encarga de capturar las credenciales del usuario y gestionar el acceso a la aplicación.
    /// </summary>
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // --- LÓGICA DE AUTENTICACIÓN ---

        /// <summary>
        /// Valida los campos, envía la petición al backend y maneja la redirección si el acceso es concedido.
        /// </summary>
        private async void button1_Click(object sender, EventArgs e)
        {
            // 1. Validación simple de frontend (Evita viajes innecesarios a la API si están vacíos)
            if (string.IsNullOrWhiteSpace(textUsuario.Text) || string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Ingresa usuario y contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Efecto visual de "Cargando" (Bloquea el botón para que el usuario ansioso no haga doble clic)
            btnLogin.Enabled = false;
            btnLogin.Text = "Verificando...";

            // 3. LLAMADA AL BACKEND (Toda la lógica de tokens y errores está encapsulada acá)
            bool acceso = await ApiServices.Login(textUsuario.Text, textPassword.Text);

            if (acceso)
            {
                // SI ENTRÓ:
                // Instancia el menú principal
                FormMain menu = new FormMain();

                // Oculta el login de fondo
                this.Hide();

                // Muestra el menú y frena la ejecución del Login hasta que el menú se cierre
                menu.ShowDialog();

                // Cuando el usuario cierra el menú principal (la X de arriba a la derecha), matamos la app cerrando el Login
                this.Close();
            }
            else
            {
                // SI REBOTÓ:
                // ApiServices ya se encargó de mostrar el cartel rojo con el motivo del rechazo.
                // solo devuelve el botón a la normalidad para que vuelva a intentar.
                btnLogin.Enabled = true;
                btnLogin.Text = "INGRESAR";
            }
        }

        
    }
}