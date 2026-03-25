namespace Cigral
{
    partial class UC_Dashboard
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
            panel1 = new Panel();
            labelTitulo = new Label();
            dgvProximos = new DataGridView();
            colProductoV = new DataGridViewTextBoxColumn();
            colLoteV = new DataGridViewTextBoxColumn();
            colVencimientoV = new DataGridViewTextBoxColumn();
            colDiasV = new DataGridViewTextBoxColumn();
            colUbicacionV = new DataGridViewTextBoxColumn();
            colCantidadV = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            colProducto = new DataGridViewTextBoxColumn();
            colLote = new DataGridViewTextBoxColumn();
            colVencimiento = new DataGridViewTextBoxColumn();
            colDias = new DataGridViewTextBoxColumn();
            colUbicacion = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            dgvVencidos = new DataGridView();
            colProductoN = new DataGridViewTextBoxColumn();
            colLoteN = new DataGridViewTextBoxColumn();
            colVencimientoN = new DataGridViewTextBoxColumn();
            colDiasN = new DataGridViewTextBoxColumn();
            colUbicacionN = new DataGridViewTextBoxColumn();
            colCantidadN = new DataGridViewTextBoxColumn();
            lblFecha = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProximos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvVencidos).BeginInit();
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
            labelTitulo.Text = "INFO DE VENCIMIENTOS";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            labelTitulo.Click += labelTitulo_Click;
            // 
            // dgvProximos
            // 
            dgvProximos.AllowUserToAddRows = false;
            dgvProximos.AllowUserToDeleteRows = false;
            dgvProximos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dgvProximos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProximos.BackgroundColor = Color.White;
            dgvProximos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProximos.Columns.AddRange(new DataGridViewColumn[] { colProductoV, colLoteV, colVencimientoV, colDiasV, colUbicacionV, colCantidadV });
            dgvProximos.Location = new Point(854, 118);
            dgvProximos.Name = "dgvProximos";
            dgvProximos.ReadOnly = true;
            dgvProximos.RowHeadersVisible = false;
            dgvProximos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProximos.Size = new Size(655, 802);
            dgvProximos.TabIndex = 8;
            // 
            // colProductoV
            // 
            colProductoV.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colProductoV.HeaderText = "Producto";
            colProductoV.Name = "colProductoV";
            colProductoV.ReadOnly = true;
            colProductoV.Width = 180;
            // 
            // colLoteV
            // 
            colLoteV.HeaderText = "Lote";
            colLoteV.Name = "colLoteV";
            colLoteV.ReadOnly = true;
            // 
            // colVencimientoV
            // 
            colVencimientoV.HeaderText = "Vencimiento";
            colVencimientoV.Name = "colVencimientoV";
            colVencimientoV.ReadOnly = true;
            // 
            // colDiasV
            // 
            colDiasV.HeaderText = "Días Restantes";
            colDiasV.Name = "colDiasV";
            colDiasV.ReadOnly = true;
            // 
            // colUbicacionV
            // 
            colUbicacionV.HeaderText = "Ubicación";
            colUbicacionV.Name = "colUbicacionV";
            colUbicacionV.ReadOnly = true;
            // 
            // colCantidadV
            // 
            colCantidadV.HeaderText = "Cantidad";
            colCantidadV.Name = "colCantidadV";
            colCantidadV.ReadOnly = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(383, 90);
            label1.Name = "label1";
            label1.Size = new Size(225, 25);
            label1.TabIndex = 9;
            label1.Text = "PRODUCTOS VENCIDOS";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.Location = new Point(1082, 90);
            label2.Name = "label2";
            label2.Size = new Size(206, 25);
            label2.TabIndex = 10;
            label2.Text = "PRÓXIMOS A VENCER";
            label2.Click += label2_Click;
            // 
            // colProducto
            // 
            colProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colProducto.HeaderText = "Producto";
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            colProducto.Width = 180;
            // 
            // colLote
            // 
            colLote.HeaderText = "Lote";
            colLote.Name = "colLote";
            colLote.ReadOnly = true;
            // 
            // colVencimiento
            // 
            colVencimiento.HeaderText = "Venicimiento";
            colVencimiento.Name = "colVencimiento";
            colVencimiento.ReadOnly = true;
            // 
            // colDias
            // 
            colDias.HeaderText = "D. Restantes";
            colDias.Name = "colDias";
            colDias.ReadOnly = true;
            // 
            // colUbicacion
            // 
            colUbicacion.HeaderText = "Ubicación";
            colUbicacion.Name = "colUbicacion";
            colUbicacion.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.HeaderText = "Cantidad";
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            // 
            // dgvVencidos
            // 
            dgvVencidos.AllowUserToAddRows = false;
            dgvVencidos.AllowUserToDeleteRows = false;
            dgvVencidos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dgvVencidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVencidos.BackgroundColor = Color.White;
            dgvVencidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVencidos.Columns.AddRange(new DataGridViewColumn[] { colProductoN, colLoteN, colVencimientoN, colDiasN, colUbicacionN, colCantidadN });
            dgvVencidos.Location = new Point(177, 118);
            dgvVencidos.Name = "dgvVencidos";
            dgvVencidos.ReadOnly = true;
            dgvVencidos.RowHeadersVisible = false;
            dgvVencidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVencidos.Size = new Size(655, 802);
            dgvVencidos.TabIndex = 11;
            // 
            // colProductoN
            // 
            colProductoN.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            colProductoN.DefaultCellStyle = dataGridViewCellStyle1;
            colProductoN.HeaderText = "Producto";
            colProductoN.Name = "colProductoN";
            colProductoN.ReadOnly = true;
            colProductoN.Width = 180;
            // 
            // colLoteN
            // 
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            colLoteN.DefaultCellStyle = dataGridViewCellStyle2;
            colLoteN.HeaderText = "Lote";
            colLoteN.Name = "colLoteN";
            colLoteN.ReadOnly = true;
            // 
            // colVencimientoN
            // 
            colVencimientoN.HeaderText = "Vencimiento";
            colVencimientoN.Name = "colVencimientoN";
            colVencimientoN.ReadOnly = true;
            // 
            // colDiasN
            // 
            colDiasN.HeaderText = "Días Restantes";
            colDiasN.Name = "colDiasN";
            colDiasN.ReadOnly = true;
            // 
            // colUbicacionN
            // 
            colUbicacionN.HeaderText = "Ubicación";
            colUbicacionN.Name = "colUbicacionN";
            colUbicacionN.ReadOnly = true;
            // 
            // colCantidadN
            // 
            colCantidadN.HeaderText = "Cantidad";
            colCantidadN.Name = "colCantidadN";
            colCantidadN.ReadOnly = true;
            // 
            // lblFecha
            // 
            lblFecha.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFecha.Location = new Point(1393, 68);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(116, 25);
            lblFecha.TabIndex = 12;
            lblFecha.Text = "09/03/2026";
            // 
            // UC_Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblFecha);
            Controls.Add(dgvVencidos);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvProximos);
            Controls.Add(panel1);
            Name = "UC_Dashboard";
            Size = new Size(1696, 944);
            Load += UC_Dashboard_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProximos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvVencidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label labelTitulo;
        private DataGridView dataGridView1;
        private DataGridView dgvProximos;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn colProductoV;
        private DataGridViewTextBoxColumn colLoteV;
        private DataGridViewTextBoxColumn colVencimientoV;
        private DataGridViewTextBoxColumn colDiasV;
        private DataGridViewTextBoxColumn colUbicacionV;
        private DataGridViewTextBoxColumn colCantidadV;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colLote;
        private DataGridViewTextBoxColumn colVencimiento;
        private DataGridViewTextBoxColumn colDias;
        private DataGridViewTextBoxColumn colUbicacion;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridView dgvVencidos;
        private Label lblFecha;
        private DataGridViewTextBoxColumn colProductoN;
        private DataGridViewTextBoxColumn colLoteN;
        private DataGridViewTextBoxColumn colVencimientoN;
        private DataGridViewTextBoxColumn colDiasN;
        private DataGridViewTextBoxColumn colUbicacionN;
        private DataGridViewTextBoxColumn colCantidadN;
    }
}
