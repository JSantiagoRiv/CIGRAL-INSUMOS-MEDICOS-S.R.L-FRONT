namespace Cigral
{
    partial class UC_Productos
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
            timerBusqueda = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            labelTitulo = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtBuscar = new TextBox();
            label2 = new Label();
            dgvProductos = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            Marca = new DataGridViewTextBoxColumn();
            GTIN = new DataGridViewTextBoxColumn();
            CodigoInterno = new DataGridViewTextBoxColumn();
            tableLayoutPanel3 = new TableLayoutPanel();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            label5 = new Label();
            label7 = new Label();
            nombreBox = new TextBox();
            marcaBox = new TextBox();
            descripcionBox = new TextBox();
            gtinBox = new TextBox();
            codigoGenericoBox = new TextBox();
            codigoInternoBox = new TextBox();
            modificarButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 200;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.0070763F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.9929237F));
            tableLayoutPanel1.Controls.Add(labelTitulo, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(dgvProductos, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 2);
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
            tableLayoutPanel1.TabIndex = 0;
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
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.47743F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.52257F));
            tableLayoutPanel2.Controls.Add(txtBuscar, 1, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 63);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(893, 34);
            tableLayoutPanel2.TabIndex = 32;
            // 
            // txtBuscar
            // 
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 12F);
            txtBuscar.Location = new Point(176, 3);
            txtBuscar.Margin = new Padding(3, 3, 45, 3);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Ingrese nombre del producto...";
            txtBuscar.Size = new Size(672, 29);
            txtBuscar.TabIndex = 31;
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
            label2.Size = new Size(167, 34);
            label2.TabIndex = 30;
            label2.Text = "Buscar producto:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[] { Nombre, Marca, GTIN, CodigoInterno });
            dgvProductos.Location = new Point(45, 120);
            dgvProductos.Margin = new Padding(20);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(808, 804);
            dgvProductos.TabIndex = 33;
            dgvProductos.CellClick += dgvProductos_CellContentClick;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            // 
            // Nombre
            // 
            Nombre.FillWeight = 55F;
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            // 
            // Marca
            // 
            Marca.FillWeight = 15F;
            Marca.HeaderText = "Marca";
            Marca.Name = "Marca";
            Marca.ReadOnly = true;
            // 
            // GTIN
            // 
            GTIN.FillWeight = 15F;
            GTIN.HeaderText = "GTIN";
            GTIN.Name = "GTIN";
            GTIN.ReadOnly = true;
            // 
            // CodigoInterno
            // 
            CodigoInterno.FillWeight = 15F;
            CodigoInterno.HeaderText = "Código Interno";
            CodigoInterno.Name = "CodigoInterno";
            CodigoInterno.ReadOnly = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label6, 0, 4);
            tableLayoutPanel3.Controls.Add(label4, 0, 3);
            tableLayoutPanel3.Controls.Add(label3, 0, 2);
            tableLayoutPanel3.Controls.Add(label1, 0, 1);
            tableLayoutPanel3.Controls.Add(label5, 0, 5);
            tableLayoutPanel3.Controls.Add(label7, 0, 6);
            tableLayoutPanel3.Controls.Add(nombreBox, 1, 1);
            tableLayoutPanel3.Controls.Add(marcaBox, 1, 2);
            tableLayoutPanel3.Controls.Add(descripcionBox, 1, 3);
            tableLayoutPanel3.Controls.Add(gtinBox, 1, 4);
            tableLayoutPanel3.Controls.Add(codigoGenericoBox, 1, 5);
            tableLayoutPanel3.Controls.Add(codigoInternoBox, 1, 6);
            tableLayoutPanel3.Controls.Add(modificarButton, 0, 7);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(902, 103);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 9;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 4.247945F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10.6198606F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10.6198606F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 28.12054F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10.6198606F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10.6198606F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10.6198606F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 14.532217F));
            tableLayoutPanel3.Size = new Size(791, 838);
            tableLayoutPanel3.TabIndex = 34;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(3, 425);
            label6.Name = "label6";
            label6.Size = new Size(134, 84);
            label6.TabIndex = 38;
            label6.Text = "GTIN:";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(3, 201);
            label4.Name = "label4";
            label4.Size = new Size(134, 224);
            label4.TabIndex = 35;
            label4.Text = "Descripción:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(3, 117);
            label3.Name = "label3";
            label3.Size = new Size(134, 84);
            label3.TabIndex = 33;
            label3.Text = "Marca:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(3, 33);
            label1.Name = "label1";
            label1.Size = new Size(134, 84);
            label1.TabIndex = 31;
            label1.Text = "Nombre:";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(3, 509);
            label5.Name = "label5";
            label5.Size = new Size(134, 84);
            label5.TabIndex = 39;
            label5.Text = "Codigo genérico:";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(3, 593);
            label7.Name = "label7";
            label7.Size = new Size(134, 84);
            label7.TabIndex = 41;
            label7.Text = "Codigo interno:";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // nombreBox
            // 
            nombreBox.Dock = DockStyle.Fill;
            nombreBox.Font = new Font("Segoe UI", 12F);
            nombreBox.Location = new Point(143, 36);
            nombreBox.Margin = new Padding(3, 3, 15, 3);
            nombreBox.Name = "nombreBox";
            nombreBox.Size = new Size(633, 29);
            nombreBox.TabIndex = 42;
            nombreBox.TextChanged += nombreBox_TextChanged;
            // 
            // marcaBox
            // 
            marcaBox.Dock = DockStyle.Fill;
            marcaBox.Font = new Font("Segoe UI", 12F);
            marcaBox.Location = new Point(143, 120);
            marcaBox.Margin = new Padding(3, 3, 15, 3);
            marcaBox.Name = "marcaBox";
            marcaBox.Size = new Size(633, 29);
            marcaBox.TabIndex = 43;
            marcaBox.TextChanged += nombreBox_TextChanged;
            // 
            // descripcionBox
            // 
            descripcionBox.Dock = DockStyle.Fill;
            descripcionBox.Font = new Font("Segoe UI", 12F);
            descripcionBox.Location = new Point(143, 204);
            descripcionBox.Margin = new Padding(3, 3, 15, 25);
            descripcionBox.Multiline = true;
            descripcionBox.Name = "descripcionBox";
            descripcionBox.Size = new Size(633, 196);
            descripcionBox.TabIndex = 44;
            descripcionBox.TextChanged += nombreBox_TextChanged;
            // 
            // gtinBox
            // 
            gtinBox.Dock = DockStyle.Fill;
            gtinBox.Font = new Font("Segoe UI", 12F);
            gtinBox.Location = new Point(143, 428);
            gtinBox.Margin = new Padding(3, 3, 15, 3);
            gtinBox.Name = "gtinBox";
            gtinBox.Size = new Size(633, 29);
            gtinBox.TabIndex = 45;
            gtinBox.TextChanged += nombreBox_TextChanged;
            // 
            // codigoGenericoBox
            // 
            codigoGenericoBox.Dock = DockStyle.Fill;
            codigoGenericoBox.Font = new Font("Segoe UI", 12F);
            codigoGenericoBox.Location = new Point(143, 512);
            codigoGenericoBox.Margin = new Padding(3, 3, 15, 3);
            codigoGenericoBox.Name = "codigoGenericoBox";
            codigoGenericoBox.Size = new Size(633, 29);
            codigoGenericoBox.TabIndex = 46;
            codigoGenericoBox.TextChanged += nombreBox_TextChanged;
            // 
            // codigoInternoBox
            // 
            codigoInternoBox.Dock = DockStyle.Fill;
            codigoInternoBox.Font = new Font("Segoe UI", 12F);
            codigoInternoBox.Location = new Point(143, 596);
            codigoInternoBox.Margin = new Padding(3, 3, 15, 3);
            codigoInternoBox.Name = "codigoInternoBox";
            codigoInternoBox.Size = new Size(633, 29);
            codigoInternoBox.TabIndex = 47;
            codigoInternoBox.TextChanged += nombreBox_TextChanged;
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
            modificarButton.Location = new Point(3, 680);
            modificarButton.Margin = new Padding(3, 3, 15, 3);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(773, 34);
            modificarButton.TabIndex = 48;
            modificarButton.Text = "EDITAR";
            modificarButton.UseVisualStyleBackColor = false;
            modificarButton.Click += modificarButton_Click;
            // 
            // UC_Productos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UC_Productos";
            Size = new Size(1696, 944);
            Load += UC_Productos_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerBusqueda;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelTitulo;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txtBuscar;
        private Label label2;
        private DataGridView dgvProductos;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Marca;
        private DataGridViewTextBoxColumn GTIN;
        private DataGridViewTextBoxColumn CodigoInterno;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label label5;
        private Label label7;
        private TextBox nombreBox;
        private TextBox marcaBox;
        private TextBox descripcionBox;
        private TextBox gtinBox;
        private TextBox codigoGenericoBox;
        private TextBox codigoInternoBox;
        private Button modificarButton;
    }
}
