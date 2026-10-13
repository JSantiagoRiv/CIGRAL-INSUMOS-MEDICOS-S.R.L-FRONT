using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cigral
{
    /// <summary>
    /// Formulario principal de la aplicación. 
    /// Contenedor base para la navegación estilo "Single Page Application" usando UserControls.
    /// </summary>
    public partial class FormMain : Form
    {

        // Variable global para guardar cuál es el botón del menú que está activo actualmente
        private IconButton currentBtn;

        public FormMain()
        {
            InitializeComponent();

            // Conecta el evento Load de la pantalla principal
            this.Load += FormMain_Load;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Arranca la aplicación simulando un clic en el botón de Inicio 
            // para que cargue el Dashboard por defecto.

        }

        // --- NAVEGACIÓN Y RENDERIZADO DE PANTALLAS ---

        /// <summary>
        /// Limpiar el panel central y carga el nuevo UserControl solicitado.
        /// </summary>
        /// <param name="control">La pantalla (UserControl) que se desea mostrar.</param>
        private void MostrarPantalla(UserControl control)
        {
            // Destruye el control anterior (si existe) para liberar memoria RAM y evitar Memory Leaks
            if (panelContainer.Controls.Count > 0)
            {
                panelContainer.Controls[0].Dispose();
            }

            // Limpia el contenedor visualmente
            panelContainer.Controls.Clear();

            // Ajusta la nueva pantalla para que ocupe todo el espacio disponible
            control.Dock = DockStyle.Fill;

            // Agrega la pantalla al panel y la trae al frente
            panelContainer.Controls.Add(control);
            control.BringToFront();
        }

        /// <summary>
        /// Cambia el estilo visual del botón clickeado para indicar en qué sección estamos.
        /// </summary>
        /// <param name="senderBtn">El botón que disparó el evento (al que se le hizo clic).</param>
        private void resaltarBoton(object senderBtn)
        {
            // 1. Apagamos el botón que estaba seleccionado antes (volvemos a colores por defecto)
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = Color.Black;
                currentBtn.IconColor = Color.Black;
            }

            // 2. Enciende el nuevo botón clickeado
            if (senderBtn != null)
            {
                // Convierte el objeto genérico a tipo IconButton para poder tocar sus propiedades
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(45, 45, 65); // Color de fondo activo
                currentBtn.ForeColor = Color.White;                // Texto blanco
                currentBtn.IconColor = Color.White;                // Ícono blanco
            }
        }

        /// <summary>
        /// Apaga visualmente todos los botones del menú. Útil cuando se vuelve a una pantalla neutra.
        /// </summary>
        public void ResetearMenu()
        {
            // Llena a resaltarBoton pasándole "null". 
            // Esto hará que se apague el botón anterior y no se encienda ninguno nuevo.
            resaltarBoton(null);
        }

        // --- ATAJOS DE TECLADO ---

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            // Mapeo de teclas F para navegación rápida por el sistema.
            // Se usa PerformClick() para reutilizar la lógica ya programada en los botones.
            if (e.KeyCode == Keys.F1)
            {
                iconButtonIngreso.PerformClick();
            }

            if (e.KeyCode == Keys.F2)
            {
                btnEgresos.PerformClick(); // Botón Egresos
            }

            if (e.KeyCode == Keys.F3)
            {
                btnStock.PerformClick(); // Botón Stock
            }

            if (e.KeyCode == Keys.F4)
            {
                btnAuditoria.PerformClick(); // Botón Auditoría
            }

            if (e.KeyCode == Keys.F5)
            {
                btnHistorialRemitos.PerformClick();  //Historial de Remitos
            }

            if (e.KeyCode == Keys.F6)
            {
                btnEntidades.PerformClick(); // Entidades
            }

            if (e.KeyCode == Keys.F7)
            {
                btnGestionProductos.PerformClick();
            }

            if (e.KeyCode == Keys.F11)  //Incio
            {
                iconButton1.PerformClick();
            }


        }

        // --- EVENTOS CLICK DE LOS BOTONES DEL MENÚ ---

        private void btnInicio_Click(object sender, EventArgs e)
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Dashboard());
        }

        private void iconButton2_Click(object sender, EventArgs e) // Botón Ingresos
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Ingresos());
        }

        private void btnEgresos_Click(object sender, EventArgs e) // Botón Egresos
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Egresos());
        }

        private void btnStock_Click(object sender, EventArgs e) // Botón Stock
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Stock());
        }

        private void btnAuditoria_Click(object sender, EventArgs e) // Botón Auditoría
        {
            MostrarPantalla(new UC_Auditoria());
            resaltarBoton(sender); // Nota: Acá el orden está invertido respecto a los demás, pero funcionalmente es lo mismo.
        }

        private void btnHistorialRemitos_Click(object sender, EventArgs e)
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Remitos());
        }

        private void btnEntidades_Click(object sender, EventArgs e)
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Entidades());
        }

        private void btnGestionProductos_Click(object sender, EventArgs e)
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Productos());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Dashboard());
        }



        private void btnConsignacion_Click(object sender, EventArgs e)
        {
            resaltarBoton(sender);
            MostrarPantalla(new UC_Consignacion());
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}