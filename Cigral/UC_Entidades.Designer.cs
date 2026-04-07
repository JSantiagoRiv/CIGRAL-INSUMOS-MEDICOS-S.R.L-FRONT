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
            tableLayoutPanel1 = new TableLayoutPanel();
            labelTitulo = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnAgregarProveedor = new Button();
            btnAgregarCliente = new Button();
            txtBuscar = new TextBox();
            label2 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            glnBox = new TextBox();
            cuitBox = new TextBox();
            emailBox = new TextBox();
            telefonoBox = new TextBox();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            label5 = new Label();
            label7 = new Label();
            modificarButton = new Button();
            razonSocialBox = new TextBox();
            direccionBox = new TextBox();
            dgvEntidades = new DataGridView();
            RazonSocial = new DataGridViewTextBoxColumn();
            Cuit = new DataGridViewTextBoxColumn();
            TipoEntidad = new DataGridViewTextBoxColumn();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntidades).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.0070763F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.9929237F));
            tableLayoutPanel1.Controls.Add(labelTitulo, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 2);
            tableLayoutPanel1.Controls.Add(dgvEntidades, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1696, 944);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // labelTitulo
            // 
            labelTitulo.BackColor = SystemColors.GradientActiveCaption;
            tableLayoutPanel1.SetColumnSpan(labelTitulo, 2);
            labelTitulo.Dock = DockStyle.Fill;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Underline);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(3, 0);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(1690, 60);
            labelTitulo.TabIndex = 1;
            labelTitulo.Text = "GESTION DE PRODUCTOS";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel2.Controls.Add(btnAgregarProveedor, 3, 0);
            tableLayoutPanel2.Controls.Add(btnAgregarCliente, 2, 0);
            tableLayoutPanel2.Controls.Add(txtBuscar, 1, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 63);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(893, 34);
            tableLayoutPanel2.TabIndex = 32;
            // 
            // btnAgregarProveedor
            // 
            btnAgregarProveedor.Anchor = AnchorStyles.None;
            btnAgregarProveedor.BackColor = Color.LimeGreen;
            btnAgregarProveedor.FlatStyle = FlatStyle.Flat;
            btnAgregarProveedor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAgregarProveedor.Location = new Point(757, 3);
            btnAgregarProveedor.Name = "btnAgregarProveedor";
            btnAgregarProveedor.Size = new Size(130, 28);
            btnAgregarProveedor.TabIndex = 35;
            btnAgregarProveedor.Text = "+ PROVEEDOR";
            btnAgregarProveedor.UseVisualStyleBackColor = false;
            btnAgregarProveedor.Click += btnAgregarProveedor_Click;
            // 
            // btnAgregarCliente
            // 
            btnAgregarCliente.Anchor = AnchorStyles.None;
            btnAgregarCliente.BackColor = Color.LimeGreen;
            btnAgregarCliente.FlatStyle = FlatStyle.Flat;
            btnAgregarCliente.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAgregarCliente.Location = new Point(617, 3);
            btnAgregarCliente.Name = "btnAgregarCliente";
            btnAgregarCliente.Size = new Size(130, 28);
            btnAgregarCliente.TabIndex = 34;
            btnAgregarCliente.Text = "+ CLIENTE";
            btnAgregarCliente.UseVisualStyleBackColor = false;
            btnAgregarCliente.Click += btnAgregarCliente_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 12F);
            txtBuscar.Location = new Point(125, 3);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Ingrese Razón Social o Cuit";
            txtBuscar.Size = new Size(484, 29);
            txtBuscar.TabIndex = 33;
            txtBuscar.TextAlign = HorizontalAlignment.Center;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(116, 34);
            label2.TabIndex = 32;
            label2.Text = "Buscar entidad:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(glnBox, 1, 6);
            tableLayoutPanel3.Controls.Add(cuitBox, 1, 5);
            tableLayoutPanel3.Controls.Add(emailBox, 1, 4);
            tableLayoutPanel3.Controls.Add(telefonoBox, 1, 3);
            tableLayoutPanel3.Controls.Add(label6, 0, 4);
            tableLayoutPanel3.Controls.Add(label4, 0, 3);
            tableLayoutPanel3.Controls.Add(label3, 0, 2);
            tableLayoutPanel3.Controls.Add(label1, 0, 1);
            tableLayoutPanel3.Controls.Add(label5, 0, 5);
            tableLayoutPanel3.Controls.Add(label7, 0, 6);
            tableLayoutPanel3.Controls.Add(modificarButton, 0, 7);
            tableLayoutPanel3.Controls.Add(razonSocialBox, 1, 1);
            tableLayoutPanel3.Controls.Add(direccionBox, 1, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(902, 103);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 9;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 5.91338F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.8267622F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.8267622F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.8267622F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.8267622F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.8267622F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 17.7401428F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 17.2126675F));
            tableLayoutPanel3.Size = new Size(791, 838);
            tableLayoutPanel3.TabIndex = 34;
            // 
            // glnBox
            // 
            glnBox.Dock = DockStyle.Fill;
            glnBox.Font = new Font("Segoe UI", 12F);
            glnBox.Location = new Point(143, 520);
            glnBox.Margin = new Padding(3, 3, 15, 3);
            glnBox.Name = "glnBox";
            glnBox.Size = new Size(633, 29);
            glnBox.TabIndex = 36;
            glnBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // cuitBox
            // 
            cuitBox.Dock = DockStyle.Fill;
            cuitBox.Font = new Font("Segoe UI", 12F);
            cuitBox.Location = new Point(143, 426);
            cuitBox.Margin = new Padding(3, 3, 15, 3);
            cuitBox.Name = "cuitBox";
            cuitBox.Size = new Size(633, 29);
            cuitBox.TabIndex = 36;
            cuitBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // emailBox
            // 
            emailBox.Dock = DockStyle.Fill;
            emailBox.Font = new Font("Segoe UI", 12F);
            emailBox.Location = new Point(143, 332);
            emailBox.Margin = new Padding(3, 3, 15, 3);
            emailBox.Name = "emailBox";
            emailBox.Size = new Size(633, 29);
            emailBox.TabIndex = 36;
            emailBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // telefonoBox
            // 
            telefonoBox.Dock = DockStyle.Fill;
            telefonoBox.Font = new Font("Segoe UI", 12F);
            telefonoBox.Location = new Point(143, 238);
            telefonoBox.Margin = new Padding(3, 3, 15, 3);
            telefonoBox.Name = "telefonoBox";
            telefonoBox.Size = new Size(633, 29);
            telefonoBox.TabIndex = 36;
            telefonoBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(3, 329);
            label6.Name = "label6";
            label6.Size = new Size(134, 94);
            label6.TabIndex = 38;
            label6.Text = "EMail:";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(3, 235);
            label4.Name = "label4";
            label4.Size = new Size(134, 94);
            label4.TabIndex = 35;
            label4.Text = "Teléfono:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(3, 141);
            label3.Name = "label3";
            label3.Size = new Size(134, 94);
            label3.TabIndex = 33;
            label3.Text = "Dirección:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(3, 47);
            label1.Name = "label1";
            label1.Size = new Size(134, 94);
            label1.TabIndex = 31;
            label1.Text = "Razón Social:";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(3, 423);
            label5.Name = "label5";
            label5.Size = new Size(134, 94);
            label5.TabIndex = 39;
            label5.Text = "CUIT:";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(3, 517);
            label7.Name = "label7";
            label7.Size = new Size(134, 141);
            label7.TabIndex = 41;
            label7.Text = "GLN:";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // modificarButton
            // 
            modificarButton.BackColor = Color.Khaki;
            tableLayoutPanel3.SetColumnSpan(modificarButton, 2);
            modificarButton.Dock = DockStyle.Fill;
            modificarButton.Enabled = false;
            modificarButton.FlatStyle = FlatStyle.Flat;
            modificarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            modificarButton.ForeColor = Color.Black;
            modificarButton.Location = new Point(250, 661);
            modificarButton.Margin = new Padding(250, 3, 110, 3);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(431, 34);
            modificarButton.TabIndex = 48;
            modificarButton.Text = "EDITAR";
            modificarButton.UseVisualStyleBackColor = false;
            modificarButton.Click += modificarButton_Click;
            // 
            // razonSocialBox
            // 
            razonSocialBox.Dock = DockStyle.Fill;
            razonSocialBox.Font = new Font("Segoe UI", 12F);
            razonSocialBox.Location = new Point(143, 50);
            razonSocialBox.Margin = new Padding(3, 3, 15, 3);
            razonSocialBox.Name = "razonSocialBox";
            razonSocialBox.Size = new Size(633, 29);
            razonSocialBox.TabIndex = 49;
            razonSocialBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // direccionBox
            // 
            direccionBox.Dock = DockStyle.Fill;
            direccionBox.Font = new Font("Segoe UI", 12F);
            direccionBox.Location = new Point(143, 144);
            direccionBox.Margin = new Padding(3, 3, 15, 3);
            direccionBox.Name = "direccionBox";
            direccionBox.Size = new Size(633, 29);
            direccionBox.TabIndex = 50;
            direccionBox.TextChanged += razonSocialBox_TextChanged;
            // 
            // dgvEntidades
            // 
            dgvEntidades.AllowUserToAddRows = false;
            dgvEntidades.AllowUserToDeleteRows = false;
            dgvEntidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntidades.BackgroundColor = Color.White;
            dgvEntidades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntidades.Columns.AddRange(new DataGridViewColumn[] { RazonSocial, Cuit, TipoEntidad });
            dgvEntidades.Dock = DockStyle.Fill;
            dgvEntidades.Location = new Point(40, 140);
            dgvEntidades.Margin = new Padding(40);
            dgvEntidades.Name = "dgvEntidades";
            dgvEntidades.ReadOnly = true;
            dgvEntidades.RowHeadersVisible = false;
            dgvEntidades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntidades.Size = new Size(819, 764);
            dgvEntidades.TabIndex = 35;
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
            // timerBusqueda
            // 
            timerBusqueda.Interval = 200;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // UC_Entidades
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UC_Entidades";
            Size = new Size(1696, 944);
            Load += UC_Entidades_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntidades).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelTitulo;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label6;
        private Label label4;
        private Label label3;
        private Label label1;
        private Label label5;
        private Label label7;
        private Button modificarButton;
        private Label label2;
        private TextBox txtBuscar;
        private DataGridView dgvEntidades;
        private DataGridViewTextBoxColumn RazonSocial;
        private DataGridViewTextBoxColumn Cuit;
        private DataGridViewTextBoxColumn TipoEntidad;
        private Button btnAgregarCliente;
        private Button btnAgregarProveedor;
        private TextBox razonSocialBox;
        private TextBox direccionBox;
        private TextBox telefonoBox;
        private TextBox emailBox;
        private TextBox cuitBox;
        private TextBox glnBox;
        private System.Windows.Forms.Timer timerBusqueda;
    }
}
