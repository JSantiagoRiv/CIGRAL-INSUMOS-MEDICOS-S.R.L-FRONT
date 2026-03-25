namespace Cigral
{
    partial class UC_Egresos
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            panel3 = new Panel();
            iconBtnBack = new FontAwesome.Sharp.IconButton();
            btnConfirmar = new Button();
            button1 = new Button();
            txtEscaner = new TextBox();
            label2 = new Label();
            label4 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            groupBox1 = new GroupBox();
            txtComprobante = new TextBox();
            label7 = new Label();
            btnAgregarCliente = new FontAwesome.Sharp.IconButton();
            cmbCliente = new ComboBox();
            label6 = new Label();
            cmbDeposito = new ComboBox();
            chkConRemito = new CheckBox();
            txtRemito = new TextBox();
            label5 = new Label();
            labelTitulo = new Label();
            panel2 = new Panel();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            panel1 = new Panel();
            dgvEgreso = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Producto = new DataGridViewTextBoxColumn();
            Lote = new DataGridViewTextBoxColumn();
            Vencimiento = new DataGridViewTextBoxColumn();
            Serie = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            ColInfo = new DataGridViewTextBoxColumn();
            ColAcciones = new DataGridViewButtonColumn();
            panel3.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEgreso).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.Controls.Add(iconBtnBack);
            panel3.Controls.Add(btnConfirmar);
            panel3.Controls.Add(button1);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 876);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1696, 68);
            panel3.TabIndex = 9;
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
            iconBtnBack.TabIndex = 12;
            iconBtnBack.UseVisualStyleBackColor = false;
            iconBtnBack.Click += iconBtnBack_Click;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirmar.BackColor = Color.MediumSeaGreen;
            btnConfirmar.Cursor = Cursors.Hand;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.FlatAppearance.MouseDownBackColor = Color.SeaGreen;
            btnConfirmar.FlatAppearance.MouseOverBackColor = Color.SpringGreen;
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.Location = new Point(1495, 10);
            btnConfirmar.Margin = new Padding(3, 2, 3, 2);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(178, 50);
            btnConfirmar.TabIndex = 10;
            btnConfirmar.Text = "CONFIRMAR EGRESO (F5)";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.Firebrick;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Brown;
            button1.FlatAppearance.MouseOverBackColor = Color.LightCoral;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1285, 10);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(178, 50);
            button1.TabIndex = 11;
            button1.Text = "CANCELAR EGRESO (F12)";
            button1.UseVisualStyleBackColor = false;
            // 
            // txtEscaner
            // 
            txtEscaner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEscaner.Font = new Font("Segoe UI", 16F);
            txtEscaner.Location = new Point(252, 39);
            txtEscaner.Margin = new Padding(3, 2, 3, 2);
            txtEscaner.Name = "txtEscaner";
            txtEscaner.Size = new Size(1135, 36);
            txtEscaner.TabIndex = 1;
            txtEscaner.TextAlign = HorizontalAlignment.Center;
            txtEscaner.KeyDown += txtEscaner_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(17, 38);
            label2.Name = "label2";
            label2.Size = new Size(61, 21);
            label2.TabIndex = 0;
            label2.Text = "Cliente:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(695, 16);
            label4.Name = "label4";
            label4.Size = new Size(201, 21);
            label4.TabIndex = 0;
            label4.Text = "Escanee el código de barras";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Segoe UI", 12F);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(681, 35);
            dateTimePicker1.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(141, 29);
            dateTimePicker1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(622, 38);
            label3.Name = "label3";
            label3.Size = new Size(53, 21);
            label3.TabIndex = 4;
            label3.Text = "Fecha:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtComprobante);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnAgregarCliente);
            groupBox1.Controls.Add(cmbCliente);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cmbDeposito);
            groupBox1.Controls.Add(chkConRemito);
            groupBox1.Controls.Add(txtRemito);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 56);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1696, 98);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Cliente";
            // 
            // txtComprobante
            // 
            txtComprobante.Enabled = false;
            txtComprobante.Font = new Font("Segoe UI", 12F);
            txtComprobante.Location = new Point(1015, 35);
            txtComprobante.Name = "txtComprobante";
            txtComprobante.Size = new Size(159, 29);
            txtComprobante.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(839, 39);
            label7.Name = "label7";
            label7.Size = new Size(176, 21);
            label7.TabIndex = 13;
            label7.Text = "Comprobante Asocidao:";
            // 
            // btnAgregarCliente
            // 
            btnAgregarCliente.Enabled = false;
            btnAgregarCliente.FlatStyle = FlatStyle.Flat;
            btnAgregarCliente.IconChar = FontAwesome.Sharp.IconChar.Add;
            btnAgregarCliente.IconColor = Color.Black;
            btnAgregarCliente.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnAgregarCliente.IconSize = 25;
            btnAgregarCliente.Location = new Point(266, 35);
            btnAgregarCliente.Margin = new Padding(0, 3, 3, 3);
            btnAgregarCliente.Name = "btnAgregarCliente";
            btnAgregarCliente.Size = new Size(33, 29);
            btnAgregarCliente.TabIndex = 12;
            btnAgregarCliente.UseVisualStyleBackColor = true;
            btnAgregarCliente.Click += btnAgregarCliente_Click;
            // 
            // cmbCliente
            // 
            cmbCliente.Enabled = false;
            cmbCliente.Font = new Font("Segoe UI", 12F);
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(84, 35);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(176, 29);
            cmbCliente.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(1401, 38);
            label6.Name = "label6";
            label6.Size = new Size(75, 21);
            label6.TabIndex = 10;
            label6.Text = "Depósito:";
            // 
            // cmbDeposito
            // 
            cmbDeposito.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeposito.Font = new Font("Segoe UI", 12F);
            cmbDeposito.FormattingEnabled = true;
            cmbDeposito.Location = new Point(1482, 36);
            cmbDeposito.Name = "cmbDeposito";
            cmbDeposito.Size = new Size(191, 29);
            cmbDeposito.TabIndex = 9;
            // 
            // chkConRemito
            // 
            chkConRemito.AutoSize = true;
            chkConRemito.Font = new Font("Segoe UI", 12F);
            chkConRemito.Location = new Point(1185, 37);
            chkConRemito.Name = "chkConRemito";
            chkConRemito.Size = new Size(159, 25);
            chkConRemito.TabIndex = 8;
            chkConRemito.Text = "Egreso con Remito";
            chkConRemito.UseVisualStyleBackColor = true;
            chkConRemito.CheckedChanged += chkConRemito_CheckedChanged;
            // 
            // txtRemito
            // 
            txtRemito.Enabled = false;
            txtRemito.Font = new Font("Segoe UI", 12F);
            txtRemito.Location = new Point(435, 35);
            txtRemito.Margin = new Padding(3, 2, 3, 2);
            txtRemito.Name = "txtRemito";
            txtRemito.ReadOnly = true;
            txtRemito.Size = new Size(159, 29);
            txtRemito.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(311, 38);
            label5.Name = "label5";
            label5.Size = new Size(118, 21);
            label5.TabIndex = 6;
            label5.Text = "Nro. de Remito:";
            label5.Click += label5_Click;
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
            labelTitulo.Text = "EGRESO DE PRODUCTOS";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txtEscaner);
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 154);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1696, 139);
            panel2.TabIndex = 7;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.None;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(944, 101);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(62, 15);
            linkLabel1.TabIndex = 3;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Click aqui.";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(599, 101);
            label1.Name = "label1";
            label1.Size = new Size(349, 15);
            label1.TabIndex = 2;
            label1.Text = "¿El producto al que desea dar egreso no posee código de barras? ";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Firebrick;
            panel1.Controls.Add(labelTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1696, 56);
            panel1.TabIndex = 5;
            // 
            // dgvEgreso
            // 
            dgvEgreso.BackgroundColor = Color.White;
            dgvEgreso.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Firebrick;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvEgreso.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvEgreso.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEgreso.Columns.AddRange(new DataGridViewColumn[] { Id, Producto, Lote, Vencimiento, Serie, Cantidad, ColInfo, ColAcciones });
            dgvEgreso.Dock = DockStyle.Fill;
            dgvEgreso.EnableHeadersVisualStyles = false;
            dgvEgreso.Location = new Point(0, 293);
            dgvEgreso.Margin = new Padding(3, 2, 3, 2);
            dgvEgreso.Name = "dgvEgreso";
            dgvEgreso.RowHeadersVisible = false;
            dgvEgreso.RowHeadersWidth = 51;
            dgvEgreso.Size = new Size(1696, 651);
            dgvEgreso.TabIndex = 12;
            dgvEgreso.CellContentClick += dgvEgreso_CellContentClick;
            // 
            // Id
            // 
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            Id.DefaultCellStyle = dataGridViewCellStyle2;
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.Visible = false;
            // 
            // Producto
            // 
            Producto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.SelectionBackColor = Color.White;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Producto.DefaultCellStyle = dataGridViewCellStyle3;
            Producto.HeaderText = "Producto";
            Producto.MinimumWidth = 6;
            Producto.Name = "Producto";
            // 
            // Lote
            // 
            Lote.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            Lote.DefaultCellStyle = dataGridViewCellStyle4;
            Lote.HeaderText = "Lote";
            Lote.MinimumWidth = 6;
            Lote.Name = "Lote";
            Lote.Width = 120;
            // 
            // Vencimiento
            // 
            Vencimiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.SelectionBackColor = Color.White;
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            Vencimiento.DefaultCellStyle = dataGridViewCellStyle5;
            Vencimiento.HeaderText = "Vencimiento";
            Vencimiento.MinimumWidth = 6;
            Vencimiento.Name = "Vencimiento";
            Vencimiento.Width = 120;
            // 
            // Serie
            // 
            Serie.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.SelectionBackColor = Color.White;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            Serie.DefaultCellStyle = dataGridViewCellStyle6;
            Serie.HeaderText = "Serie";
            Serie.MinimumWidth = 6;
            Serie.Name = "Serie";
            Serie.Width = 120;
            // 
            // Cantidad
            // 
            Cantidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Cantidad.HeaderText = "Cantidad";
            Cantidad.MinimumWidth = 6;
            Cantidad.Name = "Cantidad";
            Cantidad.Width = 120;
            // 
            // ColInfo
            // 
            ColInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColInfo.HeaderText = "Información Adicional";
            ColInfo.Name = "ColInfo";
            ColInfo.ReadOnly = true;
            ColInfo.Width = 300;
            // 
            // ColAcciones
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.ForeColor = Color.Red;
            dataGridViewCellStyle7.SelectionBackColor = Color.Transparent;
            dataGridViewCellStyle7.SelectionForeColor = Color.Red;
            ColAcciones.DefaultCellStyle = dataGridViewCellStyle7;
            ColAcciones.FlatStyle = FlatStyle.Popup;
            ColAcciones.HeaderText = "Acciones";
            ColAcciones.Name = "ColAcciones";
            ColAcciones.ReadOnly = true;
            ColAcciones.Text = "ELIMINAR";
            ColAcciones.UseColumnTextForButtonValue = true;
            // 
            // UC_Egresos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel3);
            Controls.Add(dgvEgreso);
            Controls.Add(panel2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UC_Egresos";
            Size = new Size(1696, 944);
            Load += UC_Egresos_Load;
            panel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEgreso).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel3;
        private TextBox txtEscaner;
        private Label label2;
        private Label label4;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private ComboBox comboProv;
        private GroupBox groupBox1;
        private Label labelTitulo;
        private Panel panel2;
        private Panel panel1;
        private TextBox txtRemito;
        private Label label5;
        private Button button1;
        private Button btnConfirmar;
        private DataGridView dgvEgreso;
        private FontAwesome.Sharp.IconButton iconBtnBack;
        private CheckBox chkConRemito;
        private ComboBox cmbDeposito;
        private Label label6;
        private ComboBox cmbCliente;
        private FontAwesome.Sharp.IconButton btnAgregarCliente;
        private LinkLabel linkLabel1;
        private Label label1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Producto;
        private DataGridViewTextBoxColumn Lote;
        private DataGridViewTextBoxColumn Vencimiento;
        private DataGridViewTextBoxColumn Serie;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn ColInfo;
        private DataGridViewButtonColumn ColAcciones;
        private TextBox txtComprobante;
        private Label label7;
    }
}
