namespace Cigral
{
    partial class UC_Ingresos
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
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            panel1 = new Panel();
            labelTitulo = new Label();
            groupBox1 = new GroupBox();
            btnAgregarProveedor = new FontAwesome.Sharp.IconButton();
            chkConRemito = new CheckBox();
            labelDeposito = new Label();
            cmbDeposito = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            textRemito = new TextBox();
            label1 = new Label();
            comboProv = new ComboBox();
            label2 = new Label();
            panel2 = new Panel();
            lblIngresoManual = new LinkLabel();
            label5 = new Label();
            textScanner = new TextBox();
            label4 = new Label();
            dgvIngreso = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            Producto = new DataGridViewTextBoxColumn();
            Lote = new DataGridViewTextBoxColumn();
            Vencimiento = new DataGridViewTextBoxColumn();
            Serie = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            InfoAdicional = new DataGridViewTextBoxColumn();
            colEliminar = new DataGridViewButtonColumn();
            panel3 = new Panel();
            iconBtnBack = new FontAwesome.Sharp.IconButton();
            buttonCancel = new Button();
            buttonConfirm = new Button();
            label6 = new Label();
            textBox1 = new TextBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIngreso).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MediumSeaGreen;
            panel1.Controls.Add(labelTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1456, 56);
            panel1.TabIndex = 0;
            // 
            // labelTitulo
            // 
            labelTitulo.Anchor = AnchorStyles.None;
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Underline);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(527, 10);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(306, 32);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "INGRESO DE PRODUCTOS";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(btnAgregarProveedor);
            groupBox1.Controls.Add(chkConRemito);
            groupBox1.Controls.Add(labelDeposito);
            groupBox1.Controls.Add(cmbDeposito);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textRemito);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(comboProv);
            groupBox1.Controls.Add(label2);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 56);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1456, 98);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Proveedor";
            // 
            // btnAgregarProveedor
            // 
            btnAgregarProveedor.Enabled = false;
            btnAgregarProveedor.FlatStyle = FlatStyle.Flat;
            btnAgregarProveedor.IconChar = FontAwesome.Sharp.IconChar.Add;
            btnAgregarProveedor.IconColor = Color.Black;
            btnAgregarProveedor.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnAgregarProveedor.IconSize = 25;
            btnAgregarProveedor.Location = new Point(272, 35);
            btnAgregarProveedor.Margin = new Padding(0, 3, 3, 3);
            btnAgregarProveedor.Name = "btnAgregarProveedor";
            btnAgregarProveedor.Size = new Size(33, 29);
            btnAgregarProveedor.TabIndex = 13;
            btnAgregarProveedor.UseVisualStyleBackColor = true;
            btnAgregarProveedor.Click += btnAgregarProveedor_Click;
            // 
            // chkConRemito
            // 
            chkConRemito.AutoSize = true;
            chkConRemito.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkConRemito.Location = new Point(1050, 39);
            chkConRemito.Name = "chkConRemito";
            chkConRemito.Size = new Size(143, 25);
            chkConRemito.TabIndex = 8;
            chkConRemito.Text = "Ingr. con Remito";
            chkConRemito.UseVisualStyleBackColor = true;
            chkConRemito.CheckedChanged += chkConRemito_CheckedChanged;
            // 
            // labelDeposito
            // 
            labelDeposito.AutoSize = true;
            labelDeposito.Font = new Font("Segoe UI", 12F);
            labelDeposito.Location = new Point(1199, 39);
            labelDeposito.Name = "labelDeposito";
            labelDeposito.Size = new Size(75, 21);
            labelDeposito.TabIndex = 7;
            labelDeposito.Text = "Depósito:";
            // 
            // cmbDeposito
            // 
            cmbDeposito.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeposito.Font = new Font("Segoe UI", 12F);
            cmbDeposito.FormattingEnabled = true;
            cmbDeposito.Location = new Point(1280, 35);
            cmbDeposito.Margin = new Padding(3, 2, 3, 2);
            cmbDeposito.Name = "cmbDeposito";
            cmbDeposito.Size = new Size(153, 29);
            cmbDeposito.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Segoe UI", 12F);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(646, 35);
            dateTimePicker1.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(130, 29);
            dateTimePicker1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(587, 39);
            label3.Name = "label3";
            label3.Size = new Size(53, 21);
            label3.TabIndex = 4;
            label3.Text = "Fecha:";
            // 
            // textRemito
            // 
            textRemito.Enabled = false;
            textRemito.Font = new Font("Segoe UI", 12F);
            textRemito.Location = new Point(426, 35);
            textRemito.Margin = new Padding(3, 2, 3, 2);
            textRemito.Name = "textRemito";
            textRemito.Size = new Size(155, 29);
            textRemito.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(311, 39);
            label1.Name = "label1";
            label1.Size = new Size(118, 21);
            label1.TabIndex = 2;
            label1.Text = "Nro. de Remito:";
            // 
            // comboProv
            // 
            comboProv.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProv.Enabled = false;
            comboProv.Font = new Font("Segoe UI", 12F);
            comboProv.FormattingEnabled = true;
            comboProv.Location = new Point(97, 35);
            comboProv.Margin = new Padding(3, 2, 3, 2);
            comboProv.Name = "comboProv";
            comboProv.Size = new Size(172, 29);
            comboProv.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(6, 38);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 0;
            label2.Text = "Proveedor:";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Controls.Add(lblIngresoManual);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(textScanner);
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 154);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1456, 139);
            panel2.TabIndex = 2;
            // 
            // lblIngresoManual
            // 
            lblIngresoManual.Anchor = AnchorStyles.None;
            lblIngresoManual.AutoSize = true;
            lblIngresoManual.Location = new Point(798, 101);
            lblIngresoManual.Name = "lblIngresoManual";
            lblIngresoManual.Size = new Size(62, 15);
            lblIngresoManual.TabIndex = 3;
            lblIngresoManual.TabStop = true;
            lblIngresoManual.Text = "Click aqui.";
            lblIngresoManual.LinkClicked += lblIngresoManual_LinkClicked;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(479, 101);
            label5.Name = "label5";
            label5.Size = new Size(324, 15);
            label5.TabIndex = 2;
            label5.Text = "¿El producto que desea ingresar no posee código de barras? ";
            // 
            // textScanner
            // 
            textScanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textScanner.Font = new Font("Segoe UI", 16F);
            textScanner.Location = new Point(252, 39);
            textScanner.Margin = new Padding(3, 2, 3, 2);
            textScanner.Name = "textScanner";
            textScanner.Size = new Size(895, 36);
            textScanner.TabIndex = 1;
            textScanner.TextAlign = HorizontalAlignment.Center;
            textScanner.KeyDown += textScanner_KeyDown;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(575, 16);
            label4.Name = "label4";
            label4.Size = new Size(201, 21);
            label4.TabIndex = 0;
            label4.Text = "Escanee el código de barras";
            // 
            // dgvIngreso
            // 
            dgvIngreso.BackgroundColor = Color.White;
            dgvIngreso.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.MediumSeaGreen;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = Color.White;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dgvIngreso.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dgvIngreso.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIngreso.Columns.AddRange(new DataGridViewColumn[] { id, Producto, Lote, Vencimiento, Serie, Cantidad, InfoAdicional, colEliminar });
            dgvIngreso.Dock = DockStyle.Fill;
            dgvIngreso.EnableHeadersVisualStyles = false;
            dgvIngreso.Location = new Point(0, 293);
            dgvIngreso.Margin = new Padding(3, 2, 3, 2);
            dgvIngreso.Name = "dgvIngreso";
            dgvIngreso.RowHeadersWidth = 51;
            dgvIngreso.Size = new Size(1456, 385);
            dgvIngreso.TabIndex = 3;
            dgvIngreso.CellContentClick += dgvIngreso_CellContentClick;
            // 
            // id
            // 
            dataGridViewCellStyle10.SelectionBackColor = Color.White;
            dataGridViewCellStyle10.SelectionForeColor = Color.Black;
            id.DefaultCellStyle = dataGridViewCellStyle10;
            id.HeaderText = "id";
            id.Name = "id";
            id.ReadOnly = true;
            id.Visible = false;
            // 
            // Producto
            // 
            Producto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.SelectionBackColor = Color.White;
            dataGridViewCellStyle11.SelectionForeColor = Color.Black;
            Producto.DefaultCellStyle = dataGridViewCellStyle11;
            Producto.FillWeight = 30.9278278F;
            Producto.HeaderText = "Producto";
            Producto.MinimumWidth = 6;
            Producto.Name = "Producto";
            // 
            // Lote
            // 
            Lote.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.SelectionBackColor = Color.White;
            dataGridViewCellStyle12.SelectionForeColor = Color.Black;
            Lote.DefaultCellStyle = dataGridViewCellStyle12;
            Lote.FillWeight = 376.2887F;
            Lote.HeaderText = "Lote";
            Lote.MinimumWidth = 6;
            Lote.Name = "Lote";
            Lote.Width = 120;
            // 
            // Vencimiento
            // 
            Vencimiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle13.SelectionBackColor = Color.White;
            dataGridViewCellStyle13.SelectionForeColor = Color.Black;
            Vencimiento.DefaultCellStyle = dataGridViewCellStyle13;
            Vencimiento.FillWeight = 30.9278278F;
            Vencimiento.HeaderText = "Vencimiento";
            Vencimiento.MinimumWidth = 6;
            Vencimiento.Name = "Vencimiento";
            Vencimiento.Width = 120;
            // 
            // Serie
            // 
            Serie.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle14.SelectionBackColor = Color.White;
            dataGridViewCellStyle14.SelectionForeColor = Color.Black;
            Serie.DefaultCellStyle = dataGridViewCellStyle14;
            Serie.FillWeight = 30.9278278F;
            Serie.HeaderText = "Serie";
            Serie.MinimumWidth = 6;
            Serie.Name = "Serie";
            Serie.Width = 120;
            // 
            // Cantidad
            // 
            Cantidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle15.SelectionBackColor = Color.White;
            dataGridViewCellStyle15.SelectionForeColor = Color.Black;
            Cantidad.DefaultCellStyle = dataGridViewCellStyle15;
            Cantidad.FillWeight = 30.9278278F;
            Cantidad.HeaderText = "Cantidad";
            Cantidad.MinimumWidth = 6;
            Cantidad.Name = "Cantidad";
            Cantidad.Width = 120;
            // 
            // InfoAdicional
            // 
            InfoAdicional.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            InfoAdicional.DataPropertyName = "InformacionAdicional";
            InfoAdicional.HeaderText = "Información Adicional";
            InfoAdicional.Name = "InfoAdicional";
            InfoAdicional.ReadOnly = true;
            InfoAdicional.Width = 300;
            // 
            // colEliminar
            // 
            dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = Color.White;
            dataGridViewCellStyle16.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle16.ForeColor = Color.Red;
            dataGridViewCellStyle16.SelectionBackColor = Color.Transparent;
            dataGridViewCellStyle16.SelectionForeColor = Color.Red;
            colEliminar.DefaultCellStyle = dataGridViewCellStyle16;
            colEliminar.FlatStyle = FlatStyle.Popup;
            colEliminar.HeaderText = "Acciones";
            colEliminar.Name = "colEliminar";
            colEliminar.Text = "ELIMINAR";
            colEliminar.UseColumnTextForButtonValue = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(iconBtnBack);
            panel3.Controls.Add(buttonCancel);
            panel3.Controls.Add(buttonConfirm);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 610);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1456, 68);
            panel3.TabIndex = 4;
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
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.BackColor = Color.Firebrick;
            buttonCancel.Cursor = Cursors.Hand;
            buttonCancel.FlatAppearance.BorderSize = 0;
            buttonCancel.FlatAppearance.MouseDownBackColor = Color.Brown;
            buttonCancel.FlatAppearance.MouseOverBackColor = Color.LightCoral;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(1045, 10);
            buttonCancel.Margin = new Padding(3, 2, 3, 2);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(178, 50);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "CANCELAR INGRESO (F12)";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonConfirm
            // 
            buttonConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonConfirm.BackColor = Color.MediumSeaGreen;
            buttonConfirm.Cursor = Cursors.Hand;
            buttonConfirm.FlatAppearance.BorderSize = 0;
            buttonConfirm.FlatAppearance.MouseDownBackColor = Color.SeaGreen;
            buttonConfirm.FlatAppearance.MouseOverBackColor = Color.SpringGreen;
            buttonConfirm.FlatStyle = FlatStyle.Flat;
            buttonConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonConfirm.ForeColor = Color.White;
            buttonConfirm.Location = new Point(1255, 10);
            buttonConfirm.Margin = new Padding(3, 2, 3, 2);
            buttonConfirm.Name = "buttonConfirm";
            buttonConfirm.Size = new Size(178, 50);
            buttonConfirm.TabIndex = 0;
            buttonConfirm.Text = "CONFIRMAR INGRESO (F5)";
            buttonConfirm.UseVisualStyleBackColor = false;
            buttonConfirm.Click += buttonConfirm_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(782, 39);
            label6.Name = "label6";
            label6.Size = new Size(131, 21);
            label6.TabIndex = 14;
            label6.Text = "Compr. Asociado:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(919, 36);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(112, 29);
            textBox1.TabIndex = 15;
            // 
            // UC_Ingresos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel3);
            Controls.Add(dgvIngreso);
            Controls.Add(panel2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UC_Ingresos";
            Size = new Size(1456, 678);
            Load += UC_Ingresos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIngreso).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitulo;
        private GroupBox groupBox1;
        private Label label2;
        private Label label3;
        private TextBox textRemito;
        private Label label1;
        private ComboBox comboProv;
        private DateTimePicker dateTimePicker1;
        private Panel panel2;
        private Label label4;
        private TextBox textScanner;
        private DataGridView dgvIngreso;
        private Panel panel3;
        private Button buttonConfirm;
        private Button buttonCancel;
        private FontAwesome.Sharp.IconButton iconBtnBack;
        private Label labelDeposito;
        private ComboBox cmbDeposito;
        private CheckBox chkConRemito;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn Producto;
        private DataGridViewTextBoxColumn Lote;
        private DataGridViewTextBoxColumn Vencimiento;
        private DataGridViewTextBoxColumn Serie;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn InfoAdicional;
        private DataGridViewButtonColumn colEliminar;
        private FontAwesome.Sharp.IconButton btnAgregarProveedor;
        private LinkLabel lblIngresoManual;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
    }
}
