namespace Cigral
{
    partial class UC_Stock
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            chkVencidos = new CheckBox();
            txtBuscar = new TextBox();
            chkProximosAVencer = new CheckBox();
            iconBtnSearch = new FontAwesome.Sharp.IconButton();
            cmbDireccionOrden = new ComboBox();
            label3 = new Label();
            btnExcel = new FontAwesome.Sharp.IconButton();
            label1 = new Label();
            cmbOrdenar = new ComboBox();
            label4 = new Label();
            txtLote = new TextBox();
            label5 = new Label();
            txtSerie = new TextBox();
            lblTotalProductos = new Label();
            lblPagina = new Label();
            btnSiguiente = new Button();
            btnAnterior = new Button();
            dgvStock = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            CodProd = new DataGridViewTextBoxColumn();
            Producto = new DataGridViewTextBoxColumn();
            Lote = new DataGridViewTextBoxColumn();
            Serie = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            Vencimiento = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            panel3 = new Panel();
            iconBtnBack = new FontAwesome.Sharp.IconButton();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(labelTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1696, 56);
            panel1.TabIndex = 1;
            // 
            // labelTitulo
            // 
            labelTitulo.Anchor = AnchorStyles.None;
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Underline);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(647, 10);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(329, 32);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "DISPONIBILIDAD DE STOCK";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Controls.Add(lblTotalProductos);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 56);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1696, 132);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Stock de Productos";
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
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(chkVencidos, 0, 0);
            tableLayoutPanel1.Controls.Add(txtBuscar, 1, 1);
            tableLayoutPanel1.Controls.Add(chkProximosAVencer, 1, 0);
            tableLayoutPanel1.Controls.Add(iconBtnSearch, 2, 1);
            tableLayoutPanel1.Controls.Add(cmbDireccionOrden, 4, 0);
            tableLayoutPanel1.Controls.Add(label3, 3, 0);
            tableLayoutPanel1.Controls.Add(btnExcel, 7, 1);
            tableLayoutPanel1.Controls.Add(label1, 5, 0);
            tableLayoutPanel1.Controls.Add(cmbOrdenar, 6, 0);
            tableLayoutPanel1.Controls.Add(label4, 3, 1);
            tableLayoutPanel1.Controls.Add(txtLote, 4, 1);
            tableLayoutPanel1.Controls.Add(label5, 5, 1);
            tableLayoutPanel1.Controls.Add(txtSerie, 6, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1690, 106);
            tableLayoutPanel1.TabIndex = 12;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(16, 69);
            label2.Name = "label2";
            label2.Size = new Size(133, 21);
            label2.TabIndex = 0;
            label2.Text = "Buscar Productos:";
            // 
            // chkVencidos
            // 
            chkVencidos.Anchor = AnchorStyles.Right;
            chkVencidos.AutoSize = true;
            chkVencidos.Font = new Font("Segoe UI", 12F);
            chkVencidos.Location = new Point(3, 14);
            chkVencidos.Margin = new Padding(3, 2, 3, 2);
            chkVencidos.Name = "chkVencidos";
            chkVencidos.Size = new Size(146, 25);
            chkVencidos.TabIndex = 8;
            chkVencidos.Text = "Ocultar Vencidos";
            chkVencidos.UseVisualStyleBackColor = true;
            chkVencidos.CheckedChanged += chkVencidos_CheckedChanged;
            chkVencidos.TextChanged += chkVencidos_CheckedChanged;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Left;
            txtBuscar.Font = new Font("Segoe UI", 12F);
            txtBuscar.Location = new Point(155, 65);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(205, 29);
            txtBuscar.TabIndex = 9;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // chkProximosAVencer
            // 
            chkProximosAVencer.Anchor = AnchorStyles.None;
            chkProximosAVencer.AutoSize = true;
            chkProximosAVencer.Font = new Font("Segoe UI", 12F);
            chkProximosAVencer.Location = new Point(155, 14);
            chkProximosAVencer.Name = "chkProximosAVencer";
            chkProximosAVencer.Size = new Size(228, 25);
            chkProximosAVencer.TabIndex = 10;
            chkProximosAVencer.Text = "Próximos a Vencer (6 meses)";
            chkProximosAVencer.UseVisualStyleBackColor = true;
            chkProximosAVencer.CheckedChanged += chkProximosAVencer_CheckedChanged;
            // 
            // iconBtnSearch
            // 
            iconBtnSearch.Anchor = AnchorStyles.None;
            iconBtnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            iconBtnSearch.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconBtnSearch.IconColor = Color.Black;
            iconBtnSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconBtnSearch.IconSize = 25;
            iconBtnSearch.Location = new Point(389, 65);
            iconBtnSearch.Margin = new Padding(3, 2, 3, 2);
            iconBtnSearch.Name = "iconBtnSearch";
            iconBtnSearch.Size = new Size(49, 29);
            iconBtnSearch.TabIndex = 6;
            toolTip1.SetToolTip(iconBtnSearch, "Buscar");
            iconBtnSearch.UseVisualStyleBackColor = true;
            iconBtnSearch.Click += iconBtnSearch_Click;
            // 
            // cmbDireccionOrden
            // 
            cmbDireccionOrden.Anchor = AnchorStyles.None;
            cmbDireccionOrden.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDireccionOrden.Font = new Font("Segoe UI", 12F);
            cmbDireccionOrden.FormattingEnabled = true;
            cmbDireccionOrden.Items.AddRange(new object[] { "Ascendente", "Descendente" });
            cmbDireccionOrden.Location = new Point(638, 12);
            cmbDireccionOrden.Name = "cmbDireccionOrden";
            cmbDireccionOrden.Size = new Size(198, 29);
            cmbDireccionOrden.TabIndex = 16;
            cmbDireccionOrden.SelectedIndexChanged += cmbDireccionOrden_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(444, 16);
            label3.Name = "label3";
            label3.Size = new Size(188, 21);
            label3.TabIndex = 17;
            label3.Text = "Ascendente/Descendente:";
            // 
            // btnExcel
            // 
            btnExcel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExcel.BackColor = Color.Transparent;
            btnExcel.Cursor = Cursors.Hand;
            btnExcel.FlatAppearance.BorderSize = 0;
            btnExcel.FlatStyle = FlatStyle.Flat;
            btnExcel.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnExcel.IconColor = Color.Black;
            btnExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExcel.IconSize = 50;
            btnExcel.Location = new Point(1640, 56);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(47, 47);
            btnExcel.TabIndex = 14;
            toolTip1.SetToolTip(btnExcel, "Exportar a Excel");
            btnExcel.UseVisualStyleBackColor = false;
            btnExcel.Click += btnExcel_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(842, 16);
            label1.Name = "label1";
            label1.Size = new Size(99, 21);
            label1.TabIndex = 15;
            label1.Text = "Ordenar por:";
            // 
            // cmbOrdenar
            // 
            cmbOrdenar.Anchor = AnchorStyles.Left;
            cmbOrdenar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrdenar.Font = new Font("Segoe UI", 12F);
            cmbOrdenar.FormattingEnabled = true;
            cmbOrdenar.Items.AddRange(new object[] { "Producto", "Vencimiento", "Cantidad" });
            cmbOrdenar.Location = new Point(947, 12);
            cmbOrdenar.Name = "cmbOrdenar";
            cmbOrdenar.Size = new Size(198, 29);
            cmbOrdenar.TabIndex = 12;
            cmbOrdenar.SelectedIndexChanged += cmbOrdenar_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(539, 69);
            label4.Name = "label4";
            label4.Size = new Size(93, 21);
            label4.TabIndex = 18;
            label4.Text = "Buscar Lote:";
            // 
            // txtLote
            // 
            txtLote.Anchor = AnchorStyles.Left;
            txtLote.Font = new Font("Segoe UI", 12F);
            txtLote.Location = new Point(638, 65);
            txtLote.Name = "txtLote";
            txtLote.Size = new Size(198, 29);
            txtLote.TabIndex = 19;
            txtLote.TextChanged += txtLote_TextChanged;
            txtLote.KeyDown += txtLote_KeyDown;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(843, 69);
            label5.Name = "label5";
            label5.Size = new Size(98, 21);
            label5.TabIndex = 20;
            label5.Text = "Buscar Serie:";
            // 
            // txtSerie
            // 
            txtSerie.Anchor = AnchorStyles.Left;
            txtSerie.Font = new Font("Segoe UI", 12F);
            txtSerie.Location = new Point(947, 65);
            txtSerie.Name = "txtSerie";
            txtSerie.Size = new Size(198, 29);
            txtSerie.TabIndex = 21;
            txtSerie.TextChanged += txtSerie_TextChanged;
            txtSerie.KeyDown += txtSerie_KeyDown;
            // 
            // lblTotalProductos
            // 
            lblTotalProductos.AutoSize = true;
            lblTotalProductos.Location = new Point(24, 98);
            lblTotalProductos.Name = "lblTotalProductos";
            lblTotalProductos.Size = new Size(0, 21);
            lblTotalProductos.TabIndex = 11;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Right;
            lblPagina.Font = new Font("Segoe UI", 12F);
            lblPagina.Location = new Point(1494, 12);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(135, 21);
            lblPagina.TabIndex = 20;
            lblPagina.Text = "Página 1/1";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Right;
            btnSiguiente.Location = new Point(1635, 10);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(29, 29);
            btnSiguiente.TabIndex = 19;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.Right;
            btnAnterior.Location = new Point(1461, 10);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(29, 29);
            btnAnterior.TabIndex = 18;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // dgvStock
            // 
            dgvStock.BackgroundColor = Color.White;
            dgvStock.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.SteelBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStock.Columns.AddRange(new DataGridViewColumn[] { Id, CodProd, Producto, Lote, Serie, Cantidad, Vencimiento, Estado });
            dgvStock.Dock = DockStyle.Fill;
            dgvStock.EnableHeadersVisualStyles = false;
            dgvStock.Location = new Point(0, 188);
            dgvStock.Margin = new Padding(3, 2, 3, 2);
            dgvStock.Name = "dgvStock";
            dgvStock.RowHeadersWidth = 51;
            dgvStock.Size = new Size(1696, 756);
            dgvStock.TabIndex = 4;
            // 
            // Id
            // 
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.Visible = false;
            Id.Width = 150;
            // 
            // CodProd
            // 
            CodProd.HeaderText = "CodProd";
            CodProd.Name = "CodProd";
            CodProd.Visible = false;
            // 
            // Producto
            // 
            Producto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Producto.HeaderText = "Producto";
            Producto.MinimumWidth = 6;
            Producto.Name = "Producto";
            // 
            // Lote
            // 
            Lote.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Lote.HeaderText = "Lote";
            Lote.MinimumWidth = 6;
            Lote.Name = "Lote";
            Lote.Width = 150;
            // 
            // Serie
            // 
            Serie.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Serie.HeaderText = "Serie";
            Serie.MinimumWidth = 6;
            Serie.Name = "Serie";
            Serie.Width = 150;
            // 
            // Cantidad
            // 
            Cantidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Cantidad.HeaderText = "Cantidad";
            Cantidad.MinimumWidth = 6;
            Cantidad.Name = "Cantidad";
            Cantidad.Width = 150;
            // 
            // Vencimiento
            // 
            Vencimiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Vencimiento.HeaderText = "Vencimiento";
            Vencimiento.MinimumWidth = 6;
            Vencimiento.Name = "Vencimiento";
            Vencimiento.Width = 150;
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.MinimumWidth = 6;
            Estado.Name = "Estado";
            Estado.Width = 125;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblPagina);
            panel3.Controls.Add(iconBtnBack);
            panel3.Controls.Add(btnSiguiente);
            panel3.Controls.Add(btnAnterior);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 876);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1696, 68);
            panel3.TabIndex = 5;
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
            iconBtnBack.Click += iconBtnBack_Click;
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 200;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // UC_Stock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel3);
            Controls.Add(dgvStock);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UC_Stock";
            Size = new Size(1696, 944);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitulo;
        private GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton iconBtnSearch;
        private TextBox textSearch;
        private Label label2;
        private CheckBox checkBox2;
        private DataGridView dgvStock;
        private DataGridViewTextBoxColumn Column1;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton iconBtnBack;
        private CheckBox chkVencidos;
        private TextBox txtBuscar;
        private System.Windows.Forms.Timer timerBusqueda;
        private CheckBox chkProximosAVencer;
        private Label lblTotalProductos;
        private ComboBox cmbOrdenar;
        private FontAwesome.Sharp.IconButton btnExcel;
        private Label label1;
        private ToolTip toolTip1;
        private ComboBox cmbDireccionOrden;
        private Label label3;
        private Label lblPagina;
        private Button btnSiguiente;
        private Button btnAnterior;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn CodProd;
        private DataGridViewTextBoxColumn Producto;
        private DataGridViewTextBoxColumn Lote;
        private DataGridViewTextBoxColumn Serie;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn Vencimiento;
        private DataGridViewTextBoxColumn Estado;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label4;
        private TextBox txtLote;
        private Label label5;
        private TextBox txtSerie;
    }
}
