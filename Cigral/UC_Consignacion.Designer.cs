namespace Cigral
{
    partial class UC_Consignacion
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
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            txtSerie = new TextBox();
            txtLote = new TextBox();
            txtEntidad = new TextBox();
            txtNombre = new TextBox();
            panel3 = new Panel();
            label5 = new Label();
            button1 = new Button();
            button2 = new Button();
            lblPagina = new Label();
            btnSiguiente = new Button();
            btnAnterior = new Button();
            iconBtnBack = new FontAwesome.Sharp.IconButton();
            dgvConsignaciones = new DataGridView();
            consignacionId = new DataGridViewTextBoxColumn();
            existenciaId = new DataGridViewTextBoxColumn();
            Producto = new DataGridViewTextBoxColumn();
            Cliente = new DataGridViewTextBoxColumn();
            Lote = new DataGridViewTextBoxColumn();
            Serie = new DataGridViewTextBoxColumn();
            Deposito = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            menuConsignaciones = new ContextMenuStrip(components);
            disminuirConsignacionToolStripMenuItem = new ToolStripMenuItem();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConsignaciones).BeginInit();
            menuConsignaciones.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
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
            labelTitulo.Location = new Point(647, 10);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(390, 31);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "BANCO DE STENT";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 56);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1696, 98);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtros de Busqueda";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 2, 0);
            tableLayoutPanel1.Controls.Add(label2, 4, 0);
            tableLayoutPanel1.Controls.Add(label4, 6, 0);
            tableLayoutPanel1.Controls.Add(txtSerie, 7, 0);
            tableLayoutPanel1.Controls.Add(txtLote, 5, 0);
            tableLayoutPanel1.Controls.Add(txtEntidad, 3, 0);
            tableLayoutPanel1.Controls.Add(txtNombre, 1, 0);
            tableLayoutPanel1.Font = new Font("Segoe UI", 12F);
            tableLayoutPanel1.Location = new Point(3, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1690, 72);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(3, 25);
            label3.Name = "label3";
            label3.Size = new Size(126, 21);
            label3.TabIndex = 7;
            label3.Text = "Buscar Producto:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(331, 25);
            label1.Name = "label1";
            label1.Size = new Size(115, 21);
            label1.TabIndex = 8;
            label1.Text = "Buscar Entidad:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(648, 25);
            label2.Name = "label2";
            label2.Size = new Size(93, 21);
            label2.TabIndex = 10;
            label2.Text = "Buscar Lote:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(943, 25);
            label4.Name = "label4";
            label4.Size = new Size(98, 21);
            label4.TabIndex = 12;
            label4.Text = "Buscar Serie:";
            // 
            // txtSerie
            // 
            txtSerie.Anchor = AnchorStyles.Left;
            txtSerie.Font = new Font("Segoe UI", 12F);
            txtSerie.Location = new Point(1047, 21);
            txtSerie.Name = "txtSerie";
            txtSerie.PlaceholderText = "Ingrese Número de Serie...";
            txtSerie.Size = new Size(190, 29);
            txtSerie.TabIndex = 13;
            txtSerie.TextChanged += txtSerie_TextChanged;
            txtSerie.KeyDown += txtSerie_KeyDown;
            // 
            // txtLote
            // 
            txtLote.Anchor = AnchorStyles.Left;
            txtLote.Font = new Font("Segoe UI", 12F);
            txtLote.Location = new Point(747, 21);
            txtLote.Name = "txtLote";
            txtLote.PlaceholderText = "Ingrese Número de Lote...";
            txtLote.Size = new Size(190, 29);
            txtLote.TabIndex = 16;
            txtLote.TextChanged += txtLote_TextChanged;
            txtLote.KeyDown += txtLote_KeyDown;
            // 
            // txtEntidad
            // 
            txtEntidad.Anchor = AnchorStyles.Left;
            txtEntidad.Location = new Point(452, 21);
            txtEntidad.Name = "txtEntidad";
            txtEntidad.PlaceholderText = "Ingrese Nombre de Entidad";
            txtEntidad.Size = new Size(190, 29);
            txtEntidad.TabIndex = 17;
            txtEntidad.TextChanged += txtEntidad_TextChanged;
            txtEntidad.KeyDown += txtEntidad_KeyDown;
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Left;
            txtNombre.Location = new Point(135, 21);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ingrese Nombre del Producto";
            txtNombre.Size = new Size(190, 29);
            txtNombre.TabIndex = 18;
            txtNombre.TextChanged += txtNombre_TextChanged;
            txtNombre.KeyDown += txtNombre_KeyDown;
            // 
            // panel3
            // 
            panel3.Controls.Add(label5);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(lblPagina);
            panel3.Controls.Add(btnSiguiente);
            panel3.Controls.Add(btnAnterior);
            panel3.Controls.Add(iconBtnBack);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 876);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1696, 68);
            panel3.TabIndex = 10;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(1510, 12);
            label5.Name = "label5";
            label5.Size = new Size(119, 21);
            label5.TabIndex = 26;
            label5.Text = "Página 1/1";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.Location = new Point(1635, 10);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 25;
            button1.Text = ">";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Right;
            button2.Location = new Point(1475, 10);
            button2.Name = "button2";
            button2.Size = new Size(29, 29);
            button2.TabIndex = 24;
            button2.Text = "<";
            button2.UseVisualStyleBackColor = true;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Right;
            lblPagina.Font = new Font("Segoe UI", 12F);
            lblPagina.Location = new Point(3006, -4);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(119, 21);
            lblPagina.TabIndex = 23;
            lblPagina.Text = "Página 1/1";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Right;
            btnSiguiente.Location = new Point(3131, -6);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(29, 29);
            btnSiguiente.TabIndex = 22;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.Right;
            btnAnterior.Location = new Point(2971, -6);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(29, 29);
            btnAnterior.TabIndex = 21;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            // 
            // iconBtnBack
            // 
            iconBtnBack.BackColor = Color.Transparent;
            iconBtnBack.FlatAppearance.BorderSize = 0;
            iconBtnBack.FlatStyle = FlatStyle.Flat;
            iconBtnBack.ForeColor = Color.White;
            iconBtnBack.IconChar = FontAwesome.Sharp.IconChar.ArrowCircleLeft;
            iconBtnBack.IconColor = Color.Black;
            iconBtnBack.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconBtnBack.IconSize = 50;
            iconBtnBack.Location = new Point(16, 13);
            iconBtnBack.Margin = new Padding(3, 2, 3, 2);
            iconBtnBack.Name = "iconBtnBack";
            iconBtnBack.Size = new Size(58, 46);
            iconBtnBack.TabIndex = 2;
            iconBtnBack.UseVisualStyleBackColor = false;
            // 
            // dgvConsignaciones
            // 
            dgvConsignaciones.BackgroundColor = Color.White;
            dgvConsignaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConsignaciones.Columns.AddRange(new DataGridViewColumn[] { consignacionId, existenciaId, Producto, Cliente, Lote, Serie, Deposito, Cantidad });
            dgvConsignaciones.ContextMenuStrip = menuConsignaciones;
            dgvConsignaciones.Dock = DockStyle.Fill;
            dgvConsignaciones.Location = new Point(0, 154);
            dgvConsignaciones.MultiSelect = false;
            dgvConsignaciones.Name = "dgvConsignaciones";
            dgvConsignaciones.ReadOnly = true;
            dgvConsignaciones.RowHeadersVisible = false;
            dgvConsignaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsignaciones.Size = new Size(1696, 722);
            dgvConsignaciones.TabIndex = 11;
            dgvConsignaciones.CellContentClick += dgvConsignaciones_CellContentClick;
            // 
            // consignacionId
            // 
            consignacionId.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            consignacionId.DataPropertyName = "id";
            consignacionId.HeaderText = "consignacionId";
            consignacionId.Name = "consignacionId";
            consignacionId.ReadOnly = true;
            consignacionId.Visible = false;
            // 
            // existenciaId
            // 
            existenciaId.DataPropertyName = "existenciaId";
            existenciaId.HeaderText = "existenciaId";
            existenciaId.Name = "existenciaId";
            existenciaId.ReadOnly = true;
            existenciaId.Visible = false;
            // 
            // Producto
            // 
            Producto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Producto.DataPropertyName = "productoNombre";
            Producto.FillWeight = 40F;
            Producto.HeaderText = "Producto";
            Producto.Name = "Producto";
            Producto.ReadOnly = true;
            // 
            // Cliente
            // 
            Cliente.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cliente.DataPropertyName = "clienteRazonSocial";
            Cliente.FillWeight = 20F;
            Cliente.HeaderText = "Cliente";
            Cliente.Name = "Cliente";
            Cliente.ReadOnly = true;
            // 
            // Lote
            // 
            Lote.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Lote.DataPropertyName = "codigoLote";
            Lote.FillWeight = 10F;
            Lote.HeaderText = "Lote";
            Lote.Name = "Lote";
            Lote.ReadOnly = true;
            // 
            // Serie
            // 
            Serie.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Serie.DataPropertyName = "numSerie";
            Serie.FillWeight = 10F;
            Serie.HeaderText = "Serie";
            Serie.Name = "Serie";
            Serie.ReadOnly = true;
            // 
            // Deposito
            // 
            Deposito.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Deposito.DataPropertyName = "depositoNombre";
            Deposito.FillWeight = 20F;
            Deposito.HeaderText = "Deposito";
            Deposito.Name = "Deposito";
            Deposito.ReadOnly = true;
            // 
            // Cantidad
            // 
            Cantidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cantidad.DataPropertyName = "cantidad";
            Cantidad.FillWeight = 5F;
            Cantidad.HeaderText = "Cantidad";
            Cantidad.Name = "Cantidad";
            Cantidad.ReadOnly = true;
            // 
            // menuConsignaciones
            // 
            menuConsignaciones.Items.AddRange(new ToolStripItem[] { disminuirConsignacionToolStripMenuItem });
            menuConsignaciones.Name = "menuConsignaciones";
            menuConsignaciones.Size = new Size(200, 48);
            // 
            // disminuirConsignacionToolStripMenuItem
            // 
            disminuirConsignacionToolStripMenuItem.Name = "disminuirConsignacionToolStripMenuItem";
            disminuirConsignacionToolStripMenuItem.Size = new Size(199, 22);
            disminuirConsignacionToolStripMenuItem.Text = "Modificar consignacion";
            disminuirConsignacionToolStripMenuItem.Click += disminuirConsignacionToolStripMenuItem_Click;
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 300;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // UC_Consignacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvConsignaciones);
            Controls.Add(panel3);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "UC_Consignacion";
            Size = new Size(1696, 944);
            Load += UC_Consignacion_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvConsignaciones).EndInit();
            menuConsignaciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitulo;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label3;
        private TextBox txtNroRemito;
        private Panel panel3;
        private Label lblPagina;
        private Button btnSiguiente;
        private Button btnAnterior;
        private FontAwesome.Sharp.IconButton iconBtnBack;
        private DataGridView dgvConsignaciones;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private Label label4;
        private TextBox txtSerie;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox txtLote;
        private TextBox txtEntidad;
        private TextBox txtNombre;
        private Label label5;
        private Button button1;
        private Button button2;
        private System.Windows.Forms.Timer timerBusqueda;
        private DataGridViewTextBoxColumn consignacionId;
        private DataGridViewTextBoxColumn existenciaId;
        private DataGridViewTextBoxColumn Producto;
        private DataGridViewTextBoxColumn Cliente;
        private DataGridViewTextBoxColumn Lote;
        private DataGridViewTextBoxColumn Serie;
        private DataGridViewTextBoxColumn Deposito;
        private DataGridViewTextBoxColumn Cantidad;
        private ContextMenuStrip menuConsignaciones;
        private ToolStripMenuItem disminuirConsignacionToolStripMenuItem;
    }
}
