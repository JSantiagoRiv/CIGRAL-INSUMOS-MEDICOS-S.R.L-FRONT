namespace Cigral
{
    partial class UC_Remitos
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
            label3 = new Label();
            txtNroRemito = new TextBox();
            dtpHasta = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            dtpDesde = new DateTimePicker();
            rbEgresos = new RadioButton();
            rbIngresos = new RadioButton();
            dgvRemitos = new DataGridView();
            panel3 = new Panel();
            lblPagina = new Label();
            btnSiguiente = new Button();
            btnAnterior = new Button();
            iconBtnBack = new FontAwesome.Sharp.IconButton();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRemitos).BeginInit();
            panel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDarkDark;
            panel1.Controls.Add(labelTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1696, 56);
            panel1.TabIndex = 6;
            // 
            // labelTitulo
            // 
            labelTitulo.Anchor = AnchorStyles.None;
            labelTitulo.BackColor = Color.Transparent;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Underline);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(647, 10);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(320, 31);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "HISTORIAL DE REMITOS";
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
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtros de Busqueda";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(598, 25);
            label3.Name = "label3";
            label3.Size = new Size(118, 21);
            label3.TabIndex = 7;
            label3.Text = "Nro. de Remito:";
            // 
            // txtNroRemito
            // 
            txtNroRemito.Anchor = AnchorStyles.Left;
            txtNroRemito.Font = new Font("Segoe UI", 12F);
            txtNroRemito.Location = new Point(722, 21);
            txtNroRemito.Name = "txtNroRemito";
            txtNroRemito.PlaceholderText = "Ingrese Nro. de Remito";
            txtNroRemito.Size = new Size(170, 29);
            txtNroRemito.TabIndex = 6;
            // 
            // dtpHasta
            // 
            dtpHasta.Anchor = AnchorStyles.Left;
            dtpHasta.Font = new Font("Segoe UI", 12F);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(451, 21);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(141, 29);
            dtpHasta.TabIndex = 5;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(393, 25);
            label2.Name = "label2";
            label2.Size = new Size(52, 21);
            label2.TabIndex = 4;
            label2.Text = "Hasta:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(184, 25);
            label1.Name = "label1";
            label1.Size = new Size(56, 21);
            label1.TabIndex = 3;
            label1.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Anchor = AnchorStyles.Left;
            dtpDesde.Font = new Font("Segoe UI", 12F);
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(246, 21);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(141, 29);
            dtpDesde.TabIndex = 2;
            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            // 
            // rbEgresos
            // 
            rbEgresos.Anchor = AnchorStyles.None;
            rbEgresos.AutoSize = true;
            rbEgresos.Font = new Font("Segoe UI", 12F);
            rbEgresos.Location = new Point(96, 23);
            rbEgresos.Name = "rbEgresos";
            rbEgresos.Size = new Size(82, 25);
            rbEgresos.TabIndex = 1;
            rbEgresos.Text = "Egresos";
            rbEgresos.UseVisualStyleBackColor = true;
            rbEgresos.CheckedChanged += rbEgresos_CheckedChanged;
            // 
            // rbIngresos
            // 
            rbIngresos.Anchor = AnchorStyles.None;
            rbIngresos.AutoSize = true;
            rbIngresos.Checked = true;
            rbIngresos.Font = new Font("Segoe UI", 12F);
            rbIngresos.Location = new Point(3, 23);
            rbIngresos.Name = "rbIngresos";
            rbIngresos.Size = new Size(87, 25);
            rbIngresos.TabIndex = 0;
            rbIngresos.TabStop = true;
            rbIngresos.Text = "Ingresos";
            rbIngresos.UseVisualStyleBackColor = true;
            rbIngresos.CheckedChanged += rbIngresos_CheckedChanged;
            // 
            // dgvRemitos
            // 
            dgvRemitos.AllowUserToAddRows = false;
            dgvRemitos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRemitos.BackgroundColor = Color.White;
            dgvRemitos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRemitos.Dock = DockStyle.Fill;
            dgvRemitos.Location = new Point(0, 154);
            dgvRemitos.Name = "dgvRemitos";
            dgvRemitos.ReadOnly = true;
            dgvRemitos.RowHeadersVisible = false;
            dgvRemitos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRemitos.Size = new Size(1696, 790);
            dgvRemitos.TabIndex = 8;
            dgvRemitos.CellContentClick += dgvRemitos_CellContentClick;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblPagina);
            panel3.Controls.Add(btnSiguiente);
            panel3.Controls.Add(btnAnterior);
            panel3.Controls.Add(iconBtnBack);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 876);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1696, 68);
            panel3.TabIndex = 9;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Right;
            lblPagina.Font = new Font("Segoe UI", 12F);
            lblPagina.Location = new Point(1510, 12);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(119, 21);
            lblPagina.TabIndex = 23;
            lblPagina.Text = "Página 1/1";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Right;
            btnSiguiente.Location = new Point(1635, 10);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(29, 29);
            btnSiguiente.TabIndex = 22;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.Right;
            btnAnterior.Location = new Point(1475, 10);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(29, 29);
            btnAnterior.TabIndex = 21;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
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
            // timerBusqueda
            // 
            timerBusqueda.Interval = 200;
            timerBusqueda.Tick += timerBusqueda_Tick;
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
            tableLayoutPanel1.Controls.Add(label3, 6, 0);
            tableLayoutPanel1.Controls.Add(rbIngresos, 0, 0);
            tableLayoutPanel1.Controls.Add(rbEgresos, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 2, 0);
            tableLayoutPanel1.Controls.Add(dtpHasta, 5, 0);
            tableLayoutPanel1.Controls.Add(dtpDesde, 3, 0);
            tableLayoutPanel1.Controls.Add(label2, 4, 0);
            tableLayoutPanel1.Controls.Add(txtNroRemito, 7, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1690, 72);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // UC_Remitos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel3);
            Controls.Add(dgvRemitos);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "UC_Remitos";
            Size = new Size(1696, 944);
            Load += UC_Remitos_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRemitos).EndInit();
            panel3.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitulo;
        private GroupBox groupBox1;
        private RadioButton rbIngresos;
        private RadioButton rbEgresos;
        private DateTimePicker dtpDesde;
        private Label label1;
        private Label label3;
        private TextBox txtNroRemito;
        private DateTimePicker dtpHasta;
        private Label label2;
        private DataGridView dgvRemitos;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton iconBtnBack;
        private Label lblPagina;
        private Button btnSiguiente;
        private Button btnAnterior;
        private System.Windows.Forms.Timer timerBusqueda;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
