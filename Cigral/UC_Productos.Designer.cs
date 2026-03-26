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
            modificarButton = new Button();
            codigoInternoBox = new TextBox();
            label7 = new Label();
            gtinBox = new TextBox();
            label6 = new Label();
            codigoGenericoBox = new TextBox();
            label5 = new Label();
            descripcionBox = new TextBox();
            label4 = new Label();
            marcaBox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            txtBuscar = new TextBox();
            label2 = new Label();
            dgvProductos = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            Marca = new DataGridViewTextBoxColumn();
            GTIN = new DataGridViewTextBoxColumn();
            CodigoInterno = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            labelTitulo = new Label();
            nombreBox = new TextBox();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // modificarButton
            // 
            modificarButton.BackColor = Color.Khaki;
            modificarButton.Enabled = false;
            modificarButton.FlatStyle = FlatStyle.Flat;
            modificarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            modificarButton.ForeColor = Color.Black;
            modificarButton.Location = new Point(1259, 612);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(83, 34);
            modificarButton.TabIndex = 42;
            modificarButton.Text = "EDITAR";
            modificarButton.UseVisualStyleBackColor = false;
            modificarButton.Click += modificarButton_Click;
            // 
            // codigoInternoBox
            // 
            codigoInternoBox.Font = new Font("Segoe UI", 12F);
            codigoInternoBox.Location = new Point(1089, 555);
            codigoInternoBox.Name = "codigoInternoBox";
            codigoInternoBox.Size = new Size(417, 29);
            codigoInternoBox.TabIndex = 41;
            codigoInternoBox.TextChanged += nombreBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(950, 555);
            label7.Name = "label7";
            label7.Size = new Size(117, 21);
            label7.TabIndex = 40;
            label7.Text = "Codigo interno:";
            // 
            // gtinBox
            // 
            gtinBox.Font = new Font("Segoe UI", 12F);
            gtinBox.Location = new Point(1089, 404);
            gtinBox.Name = "gtinBox";
            gtinBox.Size = new Size(417, 29);
            gtinBox.TabIndex = 39;
            gtinBox.TextChanged += nombreBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(950, 404);
            label6.Name = "label6";
            label6.Size = new Size(48, 21);
            label6.TabIndex = 37;
            label6.Text = "GTIN:";
            // 
            // codigoGenericoBox
            // 
            codigoGenericoBox.Font = new Font("Segoe UI", 12F);
            codigoGenericoBox.Location = new Point(1089, 477);
            codigoGenericoBox.Name = "codigoGenericoBox";
            codigoGenericoBox.Size = new Size(417, 29);
            codigoGenericoBox.TabIndex = 38;
            codigoGenericoBox.TextChanged += nombreBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(950, 477);
            label5.Name = "label5";
            label5.Size = new Size(127, 21);
            label5.TabIndex = 36;
            label5.Text = "Codigo genérico:";
            // 
            // descripcionBox
            // 
            descripcionBox.Font = new Font("Segoe UI", 12F);
            descripcionBox.Location = new Point(1089, 310);
            descripcionBox.Multiline = true;
            descripcionBox.Name = "descripcionBox";
            descripcionBox.Size = new Size(417, 67);
            descripcionBox.TabIndex = 35;
            descripcionBox.TextChanged += nombreBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(950, 310);
            label4.Name = "label4";
            label4.Size = new Size(94, 21);
            label4.TabIndex = 34;
            label4.Text = "Descripción:";
            // 
            // marcaBox
            // 
            marcaBox.Font = new Font("Segoe UI", 12F);
            marcaBox.Location = new Point(1089, 244);
            marcaBox.Name = "marcaBox";
            marcaBox.Size = new Size(417, 29);
            marcaBox.TabIndex = 33;
            marcaBox.TextChanged += nombreBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(950, 244);
            label3.Name = "label3";
            label3.Size = new Size(56, 21);
            label3.TabIndex = 32;
            label3.Text = "Marca:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(950, 182);
            label1.Name = "label1";
            label1.Size = new Size(71, 21);
            label1.TabIndex = 30;
            label1.Text = "Nombre:";
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 12F);
            txtBuscar.Location = new Point(168, 125);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Ingrese nombre del producto...";
            txtBuscar.Size = new Size(534, 29);
            txtBuscar.TabIndex = 29;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(47, 128);
            label2.Name = "label2";
            label2.Size = new Size(126, 21);
            label2.TabIndex = 28;
            label2.Text = "Buscar producto:";
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
            dgvProductos.Location = new Point(47, 180);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(837, 745);
            dgvProductos.TabIndex = 27;
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
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(labelTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1696, 56);
            panel1.TabIndex = 26;
            // 
            // labelTitulo
            // 
            labelTitulo.Anchor = AnchorStyles.None;
            labelTitulo.BackColor = Color.Transparent;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Underline);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(660, 14);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(320, 31);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "GESTION DE PRODUCTOS";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nombreBox
            // 
            nombreBox.Font = new Font("Segoe UI", 12F);
            nombreBox.Location = new Point(1089, 182);
            nombreBox.Name = "nombreBox";
            nombreBox.Size = new Size(417, 29);
            nombreBox.TabIndex = 31;
            nombreBox.TextChanged += nombreBox_TextChanged;
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 200;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // UC_Productos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(modificarButton);
            Controls.Add(codigoInternoBox);
            Controls.Add(label7);
            Controls.Add(gtinBox);
            Controls.Add(label6);
            Controls.Add(codigoGenericoBox);
            Controls.Add(label5);
            Controls.Add(descripcionBox);
            Controls.Add(label4);
            Controls.Add(marcaBox);
            Controls.Add(label3);
            Controls.Add(nombreBox);
            Controls.Add(label1);
            Controls.Add(txtBuscar);
            Controls.Add(label2);
            Controls.Add(dgvProductos);
            Controls.Add(panel1);
            Name = "UC_Productos";
            Size = new Size(1696, 944);
            Load += UC_Productos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button modificarButton;
        private TextBox codigoInternoBox;
        private Label label7;
        private TextBox gtinBox;
        private Label label6;
        private TextBox codigoGenericoBox;
        private Label label5;
        private TextBox descripcionBox;
        private Label label4;
        private TextBox marcaBox;
        private Label label3;
        private Label label1;
        private TextBox txtBuscar;
        private Label label2;
        private DataGridView dgvProductos;
        private Panel panel1;
        private Label labelTitulo;
        private TextBox nombreBox;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Marca;
        private DataGridViewTextBoxColumn GTIN;
        private DataGridViewTextBoxColumn CodigoInterno;
        private System.Windows.Forms.Timer timerBusqueda;
    }
}
