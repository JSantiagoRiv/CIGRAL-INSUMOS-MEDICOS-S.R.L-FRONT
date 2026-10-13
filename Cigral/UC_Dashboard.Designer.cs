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
            colProducto = new DataGridViewTextBoxColumn();
            colLote = new DataGridViewTextBoxColumn();
            colVencimiento = new DataGridViewTextBoxColumn();
            colDias = new DataGridViewTextBoxColumn();
            colUbicacion = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            labelTitulo = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            label1 = new Label();
            dgvVencidos = new DataGridView();
            colProductoN = new DataGridViewTextBoxColumn();
            colLoteN = new DataGridViewTextBoxColumn();
            colVencimientoN = new DataGridViewTextBoxColumn();
            colDiasN = new DataGridViewTextBoxColumn();
            colUbicacionN = new DataGridViewTextBoxColumn();
            colCantidadN = new DataGridViewTextBoxColumn();
            dgvProximos = new DataGridView();
            colProductoV = new DataGridViewTextBoxColumn();
            colLoteV = new DataGridViewTextBoxColumn();
            colVencimientoV = new DataGridViewTextBoxColumn();
            colDiasV = new DataGridViewTextBoxColumn();
            colUbicacionV = new DataGridViewTextBoxColumn();
            colCantidadV = new DataGridViewTextBoxColumn();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVencidos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProximos).BeginInit();
            SuspendLayout();
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
            // labelTitulo
            // 
            labelTitulo.BackColor = SystemColors.GradientActiveCaption;
            tableLayoutPanel1.SetColumnSpan(labelTitulo, 2);
            labelTitulo.Dock = DockStyle.Fill;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Underline);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(0, 0);
            labelTitulo.Margin = new Padding(0);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(1696, 60);
            labelTitulo.TabIndex = 1;
            labelTitulo.Text = "INFO DE VENCIMIENTOS";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label2, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(labelTitulo, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvVencidos, 0, 2);
            tableLayoutPanel1.Controls.Add(dgvProximos, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1696, 944);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.Location = new Point(848, 60);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(848, 60);
            label2.TabIndex = 15;
            label2.Text = "PRÓXIMOS A VENCER";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(0, 60);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(848, 60);
            label1.TabIndex = 14;
            label1.Text = "PRODUCTOS VENCIDOS";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvVencidos
            // 
            dgvVencidos.AllowUserToAddRows = false;
            dgvVencidos.AllowUserToDeleteRows = false;
            dgvVencidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVencidos.BackgroundColor = Color.White;
            dgvVencidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVencidos.Columns.AddRange(new DataGridViewColumn[] { colProductoN, colLoteN, colVencimientoN, colDiasN, colUbicacionN, colCantidadN });
            dgvVencidos.Dock = DockStyle.Fill;
            dgvVencidos.Location = new Point(20, 140);
            dgvVencidos.Margin = new Padding(20);
            dgvVencidos.Name = "dgvVencidos";
            dgvVencidos.ReadOnly = true;
            dgvVencidos.RowHeadersVisible = false;
            dgvVencidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVencidos.Size = new Size(808, 784);
            dgvVencidos.TabIndex = 12;
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
            // dgvProximos
            // 
            dgvProximos.AllowUserToAddRows = false;
            dgvProximos.AllowUserToDeleteRows = false;
            dgvProximos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProximos.BackgroundColor = Color.White;
            dgvProximos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProximos.Columns.AddRange(new DataGridViewColumn[] { colProductoV, colLoteV, colVencimientoV, colDiasV, colUbicacionV, colCantidadV });
            dgvProximos.Dock = DockStyle.Fill;
            dgvProximos.Location = new Point(868, 140);
            dgvProximos.Margin = new Padding(20);
            dgvProximos.Name = "dgvProximos";
            dgvProximos.ReadOnly = true;
            dgvProximos.RowHeadersVisible = false;
            dgvProximos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProximos.Size = new Size(808, 784);
            dgvProximos.TabIndex = 13;
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
            // UC_Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UC_Dashboard";
            Size = new Size(1696, 944);
            Load += UC_Dashboard_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVencidos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProximos).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colLote;
        private DataGridViewTextBoxColumn colVencimiento;
        private DataGridViewTextBoxColumn colDias;
        private DataGridViewTextBoxColumn colUbicacion;
        private DataGridViewTextBoxColumn colCantidad;
        private Label labelTitulo;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dgvVencidos;
        private DataGridViewTextBoxColumn colProductoN;
        private DataGridViewTextBoxColumn colLoteN;
        private DataGridViewTextBoxColumn colVencimientoN;
        private DataGridViewTextBoxColumn colDiasN;
        private DataGridViewTextBoxColumn colUbicacionN;
        private DataGridViewTextBoxColumn colCantidadN;
        private DataGridView dgvProximos;
        private DataGridViewTextBoxColumn colProductoV;
        private DataGridViewTextBoxColumn colLoteV;
        private DataGridViewTextBoxColumn colVencimientoV;
        private DataGridViewTextBoxColumn colDiasV;
        private DataGridViewTextBoxColumn colUbicacionV;
        private DataGridViewTextBoxColumn colCantidadV;
        private Label label1;
        private Label label2;
    }
}
