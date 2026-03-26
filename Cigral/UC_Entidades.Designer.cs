namespace Cigral
{
    partial class UC_Entidades
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            labelTitulo = new Label();
            dgvEntidades = new DataGridView();
            RazonSocial = new DataGridViewTextBoxColumn();
            Cuit = new DataGridViewTextBoxColumn();
            TipoEntidad = new DataGridViewTextBoxColumn();
            txtBuscar = new TextBox();
            label2 = new Label();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            razonSocialBox = new TextBox();
            label1 = new Label();
            glnBox = new TextBox();
            label3 = new Label();
            emailBox = new TextBox();
            label4 = new Label();
            telefonoBox = new TextBox();
            label5 = new Label();
            label6 = new Label();
            cuitBox = new TextBox();
            direccionBox = new TextBox();
            label7 = new Label();
            modificarButton = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntidades).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(labelTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1696, 56);
            panel1.TabIndex = 7;
            // 
            // labelTitulo
            // 
            labelTitulo.Anchor = AnchorStyles.None;
            labelTitulo.BackColor = Color.Transparent;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Underline);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(662, 13);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(320, 31);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "GESTION DE ENTIDADES";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            labelTitulo.Click += labelTitulo_Click;
            // 
            // dgvEntidades
            // 
            dgvEntidades.AllowUserToAddRows = false;
            dgvEntidades.AllowUserToDeleteRows = false;
            dgvEntidades.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dgvEntidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntidades.BackgroundColor = Color.White;
            dgvEntidades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntidades.Columns.AddRange(new DataGridViewColumn[] { RazonSocial, Cuit, TipoEntidad });
            dgvEntidades.Location = new Point(47, 161);
            dgvEntidades.Name = "dgvEntidades";
            dgvEntidades.ReadOnly = true;
            dgvEntidades.RowHeadersVisible = false;
            dgvEntidades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntidades.Size = new Size(655, 745);
            dgvEntidades.TabIndex = 12;
            dgvEntidades.CellClick += dgvEntidades_CellContentClick;
            dgvEntidades.CellContentClick += dgvEntidades_CellContentClick;
            // 
            // RazonSocial
            // 
            RazonSocial.FillWeight = 60F;
            RazonSocial.HeaderText = "RazónSocial";
            RazonSocial.Name = "RazonSocial";
            RazonSocial.ReadOnly = true;
            // 
            // Cuit
            // 
            Cuit.FillWeight = 20F;
            Cuit.HeaderText = "Cuit";
            Cuit.Name = "Cuit";
            Cuit.ReadOnly = true;
            // 
            // TipoEntidad
            // 
            TipoEntidad.FillWeight = 20F;
            TipoEntidad.HeaderText = "Tipo de Entidad";
            TipoEntidad.Name = "TipoEntidad";
            TipoEntidad.ReadOnly = true;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 12F);
            txtBuscar.Location = new Point(190, 110);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Ingrese Razón Social o Cuit";
            txtBuscar.Size = new Size(417, 29);
            txtBuscar.TabIndex = 14;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(51, 109);
            label2.Name = "label2";
            label2.Size = new Size(115, 21);
            label2.TabIndex = 13;
            label2.Text = "Buscar entidad:";
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 200;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // razonSocialBox
            // 
            razonSocialBox.Font = new Font("Segoe UI", 12F);
            razonSocialBox.Location = new Point(901, 180);
            razonSocialBox.Name = "razonSocialBox";
            razonSocialBox.Size = new Size(417, 29);
            razonSocialBox.TabIndex = 16;
            razonSocialBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(762, 179);
            label1.Name = "label1";
            label1.Size = new Size(101, 21);
            label1.TabIndex = 15;
            label1.Text = "Razón Social:";
            label1.Click += label1_Click;
            // 
            // glnBox
            // 
            glnBox.Font = new Font("Segoe UI", 12F);
            glnBox.Location = new Point(901, 253);
            glnBox.Name = "glnBox";
            glnBox.Size = new Size(417, 29);
            glnBox.TabIndex = 18;
            glnBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(762, 252);
            label3.Name = "label3";
            label3.Size = new Size(44, 21);
            label3.TabIndex = 17;
            label3.Text = "GLN:";
            label3.Click += label3_Click;
            // 
            // emailBox
            // 
            emailBox.Font = new Font("Segoe UI", 12F);
            emailBox.Location = new Point(901, 325);
            emailBox.Name = "emailBox";
            emailBox.Size = new Size(417, 29);
            emailBox.TabIndex = 20;
            emailBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(762, 324);
            label4.Name = "label4";
            label4.Size = new Size(51, 21);
            label4.TabIndex = 19;
            label4.Text = "Email:";
            // 
            // telefonoBox
            // 
            telefonoBox.Font = new Font("Segoe UI", 12F);
            telefonoBox.Location = new Point(901, 483);
            telefonoBox.Name = "telefonoBox";
            telefonoBox.Size = new Size(417, 29);
            telefonoBox.TabIndex = 22;
            telefonoBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(762, 482);
            label5.Name = "label5";
            label5.Size = new Size(71, 21);
            label5.TabIndex = 21;
            label5.Text = "Teléfono:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(762, 401);
            label6.Name = "label6";
            label6.Size = new Size(41, 21);
            label6.TabIndex = 21;
            label6.Text = "Cuit:";
            // 
            // cuitBox
            // 
            cuitBox.Font = new Font("Segoe UI", 12F);
            cuitBox.Location = new Point(901, 402);
            cuitBox.Name = "cuitBox";
            cuitBox.Size = new Size(417, 29);
            cuitBox.TabIndex = 22;
            cuitBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // direccionBox
            // 
            direccionBox.Font = new Font("Segoe UI", 12F);
            direccionBox.Location = new Point(901, 567);
            direccionBox.Name = "direccionBox";
            direccionBox.Size = new Size(417, 29);
            direccionBox.TabIndex = 24;
            direccionBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(762, 566);
            label7.Name = "label7";
            label7.Size = new Size(78, 21);
            label7.TabIndex = 23;
            label7.Text = "Dirección:";
            // 
            // modificarButton
            // 
            modificarButton.Enabled = false;
            modificarButton.Location = new Point(1452, 561);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(135, 34);
            modificarButton.TabIndex = 25;
            modificarButton.Text = "Modificar";
            modificarButton.UseVisualStyleBackColor = true;
            modificarButton.Click += modificarButton_Click;
            // 
            // UC_Entidades
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(modificarButton);
            Controls.Add(direccionBox);
            Controls.Add(label7);
            Controls.Add(cuitBox);
            Controls.Add(label6);
            Controls.Add(telefonoBox);
            Controls.Add(label5);
            Controls.Add(emailBox);
            Controls.Add(label4);
            Controls.Add(glnBox);
            Controls.Add(label3);
            Controls.Add(razonSocialBox);
            Controls.Add(label1);
            Controls.Add(txtBuscar);
            Controls.Add(label2);
            Controls.Add(dgvEntidades);
            Controls.Add(panel1);
            Name = "UC_Entidades";
            Size = new Size(1696, 944);
            Load += UC_Entidades_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEntidades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label labelTitulo;
        private DataGridView dgvEntidades;
        private TextBox txtBuscar;
        private Label label2;
        private System.Windows.Forms.Timer timerBusqueda;
        private DataGridViewTextBoxColumn RazonSocial;
        private DataGridViewTextBoxColumn Cuit;
        private DataGridViewTextBoxColumn TipoEntidad;
        private TextBox razonSocialBox;
        private Label label1;
        private TextBox glnBox;
        private Label label3;
        private TextBox emailBox;
        private Label label4;
        private TextBox telefonoBox;
        private Label label5;
        private Label label6;
        private TextBox cuitBox;
        private TextBox direccionBox;
        private Label label7;
        private Button modificarButton;
    }
}
