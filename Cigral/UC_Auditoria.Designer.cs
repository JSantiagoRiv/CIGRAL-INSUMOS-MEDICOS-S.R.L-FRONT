namespace Cigral
{
    partial class UC_Auditoria
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panel1 = new Panel();
            labelTitulo = new Label();
            groupBox1 = new GroupBox();
            txtBusquedaNombre = new TextBox();
            lblBusquedaNombre = new Label();
            dtpHasta = new DateTimePicker();
            btnExportar = new FontAwesome.Sharp.IconButton();
            cmbMov = new ComboBox();
            label3 = new Label();
            label1 = new Label();
            dtpDesde = new DateTimePicker();
            lblPagina = new Label();
            btnSiguiente = new Button();
            btnAnterior = new Button();
            dgvAuditoria = new DataGridView();
            tipo = new DataGridViewTextBoxColumn();
            fechaMov = new DataGridViewTextBoxColumn();
            producto = new DataGridViewTextBoxColumn();
            deposito = new DataGridViewTextBoxColumn();
            lote = new DataGridViewTextBoxColumn();
            serie = new DataGridViewTextBoxColumn();
            cantidad = new DataGridViewTextBoxColumn();
            stockAnterior = new DataGridViewTextBoxColumn();
            stockPosterior = new DataGridViewTextBoxColumn();
            remitoIng = new DataGridViewTextBoxColumn();
            remitoEgr = new DataGridViewTextBoxColumn();
            usuario = new DataGridViewTextBoxColumn();
            detalle = new DataGridViewTextBoxColumn();
            panel3 = new Panel();
            iconBtnBack = new FontAwesome.Sharp.IconButton();
            toolTip1 = new ToolTip(components);
            timerBusqueda = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Indigo;
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
            labelTitulo.Size = new Size(341, 31);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "AUDITORIA DE PRODUCTOS";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtBusquedaNombre);
            groupBox1.Controls.Add(lblBusquedaNombre);
            groupBox1.Controls.Add(dtpHasta);
            groupBox1.Controls.Add(btnExportar);
            groupBox1.Controls.Add(cmbMov);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(dtpDesde);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 56);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1696, 98);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Consultar Auditoria";
            // 
            // txtBusquedaNombre
            // 
            txtBusquedaNombre.Font = new Font("Segoe UI", 12F);
            txtBusquedaNombre.Location = new Point(774, 37);
            txtBusquedaNombre.Name = "txtBusquedaNombre";
            txtBusquedaNombre.PlaceholderText = "Ingrese Nombre del Producto";
            txtBusquedaNombre.Size = new Size(234, 29);
            txtBusquedaNombre.TabIndex = 15;
            txtBusquedaNombre.TextChanged += txtBusquedaNombre_TextChanged;
            txtBusquedaNombre.KeyDown += txtBusquedaNombre_KeyDown;
            // 
            // lblBusquedaNombre
            // 
            lblBusquedaNombre.AutoSize = true;
            lblBusquedaNombre.Font = new Font("Segoe UI", 12F);
            lblBusquedaNombre.Location = new Point(647, 40);
            lblBusquedaNombre.Name = "lblBusquedaNombre";
            lblBusquedaNombre.Size = new Size(121, 21);
            lblBusquedaNombre.TabIndex = 14;
            lblBusquedaNombre.Text = "Buscar Nombre:";
            // 
            // dtpHasta
            // 
            dtpHasta.Font = new Font("Segoe UI", 12F);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(280, 37);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(125, 29);
            dtpHasta.TabIndex = 12;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportar.BackColor = Color.Transparent;
            btnExportar.Cursor = Cursors.Hand;
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.FlatStyle = FlatStyle.Flat;
            btnExportar.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnExportar.IconColor = Color.Black;
            btnExportar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExportar.IconSize = 50;
            btnExportar.Location = new Point(1606, 37);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(58, 46);
            btnExportar.TabIndex = 13;
            toolTip1.SetToolTip(btnExportar, "Exportar e imprimir");
            btnExportar.UseVisualStyleBackColor = false;
            btnExportar.Click += btnExportar_Click;
            // 
            // cmbMov
            // 
            cmbMov.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMov.Font = new Font("Segoe UI", 12F);
            cmbMov.FormattingEnabled = true;
            cmbMov.Location = new Point(439, 37);
            cmbMov.Name = "cmbMov";
            cmbMov.Size = new Size(176, 29);
            cmbMov.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(222, 43);
            label3.Name = "label3";
            label3.Size = new Size(52, 21);
            label3.TabIndex = 9;
            label3.Text = "Hasta:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(18, 43);
            label1.Name = "label1";
            label1.Size = new Size(56, 21);
            label1.TabIndex = 8;
            label1.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Font = new Font("Segoe UI", 12F);
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(80, 37);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(125, 29);
            dtpDesde.TabIndex = 7;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.None;
            lblPagina.Font = new Font("Segoe UI", 12F);
            lblPagina.Location = new Point(1510, 12);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(119, 21);
            lblPagina.TabIndex = 15;
            lblPagina.Text = "Pagina 1/1";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.None;
            btnSiguiente.Location = new Point(1635, 10);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(29, 29);
            btnSiguiente.TabIndex = 14;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.None;
            btnAnterior.Location = new Point(1475, 10);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(29, 29);
            btnAnterior.TabIndex = 13;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // dgvAuditoria
            // 
            dgvAuditoria.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditoria.BackgroundColor = Color.White;
            dgvAuditoria.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Indigo;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvAuditoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAuditoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditoria.Columns.AddRange(new DataGridViewColumn[] { tipo, fechaMov, producto, deposito, lote, serie, cantidad, stockAnterior, stockPosterior, remitoIng, remitoEgr, usuario, detalle });
            dgvAuditoria.EnableHeadersVisualStyles = false;
            dgvAuditoria.Location = new Point(0, 154);
            dgvAuditoria.Name = "dgvAuditoria";
            dgvAuditoria.ReadOnly = true;
            dgvAuditoria.Size = new Size(1696, 727);
            dgvAuditoria.TabIndex = 8;
            // 
            // tipo
            // 
            tipo.HeaderText = "Tipo";
            tipo.Name = "tipo";
            tipo.ReadOnly = true;
            // 
            // fechaMov
            // 
            fechaMov.HeaderText = "Fecha de Movimiento";
            fechaMov.Name = "fechaMov";
            fechaMov.ReadOnly = true;
            // 
            // producto
            // 
            producto.HeaderText = "Producto/s";
            producto.Name = "producto";
            producto.ReadOnly = true;
            // 
            // deposito
            // 
            deposito.HeaderText = "Depósito";
            deposito.Name = "deposito";
            deposito.ReadOnly = true;
            // 
            // lote
            // 
            lote.HeaderText = "Código de Lote";
            lote.Name = "lote";
            lote.ReadOnly = true;
            // 
            // serie
            // 
            serie.HeaderText = "Número de Serie";
            serie.Name = "serie";
            serie.ReadOnly = true;
            // 
            // cantidad
            // 
            cantidad.HeaderText = "Cantidad";
            cantidad.Name = "cantidad";
            cantidad.ReadOnly = true;
            // 
            // stockAnterior
            // 
            stockAnterior.HeaderText = "Stock Anterior";
            stockAnterior.Name = "stockAnterior";
            stockAnterior.ReadOnly = true;
            // 
            // stockPosterior
            // 
            stockPosterior.HeaderText = "Stock Posterior";
            stockPosterior.Name = "stockPosterior";
            stockPosterior.ReadOnly = true;
            // 
            // remitoIng
            // 
            remitoIng.HeaderText = "Remito de Ingreso";
            remitoIng.Name = "remitoIng";
            remitoIng.ReadOnly = true;
            // 
            // remitoEgr
            // 
            remitoEgr.HeaderText = "Remito de Egreso";
            remitoEgr.Name = "remitoEgr";
            remitoEgr.ReadOnly = true;
            // 
            // usuario
            // 
            usuario.HeaderText = "Usuario";
            usuario.Name = "usuario";
            usuario.ReadOnly = true;
            // 
            // detalle
            // 
            detalle.HeaderText = "Detalles";
            detalle.Name = "detalle";
            detalle.ReadOnly = true;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(lblPagina);
            panel3.Controls.Add(btnSiguiente);
            panel3.Controls.Add(iconBtnBack);
            panel3.Controls.Add(btnAnterior);
            panel3.Location = new Point(0, 876);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1696, 68);
            panel3.TabIndex = 10;
            // 
            // iconBtnBack
            // 
            iconBtnBack.BackColor = Color.Transparent;
            iconBtnBack.Cursor = Cursors.Hand;
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
            iconBtnBack.TabIndex = 12;
            toolTip1.SetToolTip(iconBtnBack, "Volver");
            iconBtnBack.UseVisualStyleBackColor = false;
            iconBtnBack.Click += iconBtnBack_Click_1;
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 200;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // UC_Auditoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel3);
            Controls.Add(dgvAuditoria);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "UC_Auditoria";
            Size = new Size(1696, 944);
            Load += UC_Auditoria_Load_1;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitulo;
        private GroupBox groupBox1;
        private DateTimePicker dateTimePicker2;
        private Label label3;
        private Label label1;
        private DateTimePicker dtpDesde;
        private ComboBox cmbMov;
        private DataGridView dgvAuditoria;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton iconBtnBack;
        private FontAwesome.Sharp.IconButton btnExportar;
        private ToolTip toolTip1;
        private DataGridViewTextBoxColumn tipo;
        private DataGridViewTextBoxColumn fechaMov;
        private DataGridViewTextBoxColumn producto;
        private DataGridViewTextBoxColumn deposito;
        private DataGridViewTextBoxColumn lote;
        private DataGridViewTextBoxColumn serie;
        private DataGridViewTextBoxColumn cantidad;
        private DataGridViewTextBoxColumn stockAnterior;
        private DataGridViewTextBoxColumn stockPosterior;
        private DataGridViewTextBoxColumn remitoIng;
        private DataGridViewTextBoxColumn remitoEgr;
        private DataGridViewTextBoxColumn usuario;
        private DataGridViewTextBoxColumn detalle;
        private DateTimePicker dtpHasta;
        private Button button2;
        private Label label2;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Label lblPagina;
        private Label lblBusquedaNombre;
        private TextBox txtBusquedaNombre;
        private System.Windows.Forms.Timer timerBusqueda;
    }
}
