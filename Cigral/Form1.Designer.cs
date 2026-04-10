namespace Cigral
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            btnStock = new FontAwesome.Sharp.IconButton();
            btnAuditoria = new FontAwesome.Sharp.IconButton();
            btnEgresos = new FontAwesome.Sharp.IconButton();
            label2 = new Label();
            btnHistorialRemitos = new FontAwesome.Sharp.IconButton();
            pictureBox2 = new PictureBox();
            btnEntidades = new FontAwesome.Sharp.IconButton();
            btnGestionProductos = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panelHeader = new Panel();
            panelContainer = new Panel();
            iconButtonIngreso = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnConsignacion = new FontAwesome.Sharp.IconButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelHeader.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnStock
            // 
            btnStock.Anchor = AnchorStyles.Left;
            btnStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStock.IconChar = FontAwesome.Sharp.IconChar.BoxesStacked;
            btnStock.IconColor = Color.Black;
            btnStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStock.ImageAlign = ContentAlignment.MiddleLeft;
            btnStock.Location = new Point(3, 168);
            btnStock.Margin = new Padding(3, 2, 3, 2);
            btnStock.Name = "btnStock";
            btnStock.Size = new Size(188, 79);
            btnStock.TabIndex = 3;
            btnStock.Text = "STOCK DISPONIBLE (F3)";
            btnStock.TextAlign = ContentAlignment.MiddleLeft;
            btnStock.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStock.UseVisualStyleBackColor = true;
            btnStock.Click += btnStock_Click;
            // 
            // btnAuditoria
            // 
            btnAuditoria.Anchor = AnchorStyles.Left;
            btnAuditoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAuditoria.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnAuditoria.IconColor = Color.Black;
            btnAuditoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAuditoria.ImageAlign = ContentAlignment.MiddleLeft;
            btnAuditoria.Location = new Point(3, 251);
            btnAuditoria.Margin = new Padding(3, 2, 3, 2);
            btnAuditoria.Name = "btnAuditoria";
            btnAuditoria.Size = new Size(188, 79);
            btnAuditoria.TabIndex = 4;
            btnAuditoria.Text = "AUDITORIA DE PRODUCTOS (F4)";
            btnAuditoria.TextAlign = ContentAlignment.MiddleLeft;
            btnAuditoria.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAuditoria.UseVisualStyleBackColor = true;
            btnAuditoria.Click += btnAuditoria_Click;
            // 
            // btnEgresos
            // 
            btnEgresos.Anchor = AnchorStyles.Left;
            btnEgresos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEgresos.IconChar = FontAwesome.Sharp.IconChar.BoxesPacking;
            btnEgresos.IconColor = Color.Black;
            btnEgresos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEgresos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEgresos.Location = new Point(3, 85);
            btnEgresos.Margin = new Padding(3, 2, 3, 2);
            btnEgresos.Name = "btnEgresos";
            btnEgresos.Size = new Size(188, 79);
            btnEgresos.TabIndex = 5;
            btnEgresos.Text = "EGRESO DE PRODUCTOS (F2)";
            btnEgresos.TextAlign = ContentAlignment.MiddleLeft;
            btnEgresos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEgresos.UseVisualStyleBackColor = true;
            btnEgresos.Click += btnEgresos_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 174);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 7;
            label2.Text = "Versión 1.0.0";
            label2.Click += label2_Click;
            // 
            // btnHistorialRemitos
            // 
            btnHistorialRemitos.Anchor = AnchorStyles.Left;
            btnHistorialRemitos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHistorialRemitos.IconChar = FontAwesome.Sharp.IconChar.Info;
            btnHistorialRemitos.IconColor = Color.Black;
            btnHistorialRemitos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHistorialRemitos.ImageAlign = ContentAlignment.MiddleLeft;
            btnHistorialRemitos.Location = new Point(3, 334);
            btnHistorialRemitos.Margin = new Padding(3, 2, 3, 2);
            btnHistorialRemitos.Name = "btnHistorialRemitos";
            btnHistorialRemitos.Size = new Size(188, 79);
            btnHistorialRemitos.TabIndex = 8;
            btnHistorialRemitos.Text = "HISTORIAL DE REMITOS (F5)";
            btnHistorialRemitos.TextAlign = ContentAlignment.MiddleLeft;
            btnHistorialRemitos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHistorialRemitos.UseVisualStyleBackColor = true;
            btnHistorialRemitos.Click += btnHistorialRemitos_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Bottom;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(199, 149);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // btnEntidades
            // 
            btnEntidades.Anchor = AnchorStyles.Left;
            btnEntidades.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEntidades.IconChar = FontAwesome.Sharp.IconChar.Users;
            btnEntidades.IconColor = Color.Black;
            btnEntidades.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEntidades.ImageAlign = ContentAlignment.MiddleLeft;
            btnEntidades.Location = new Point(3, 417);
            btnEntidades.Margin = new Padding(3, 2, 3, 2);
            btnEntidades.Name = "btnEntidades";
            btnEntidades.Size = new Size(188, 79);
            btnEntidades.TabIndex = 10;
            btnEntidades.Text = "GESTION DE ENTIDADES (F6)";
            btnEntidades.TextAlign = ContentAlignment.MiddleLeft;
            btnEntidades.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEntidades.UseVisualStyleBackColor = true;
            btnEntidades.Click += btnEntidades_Click;
            // 
            // btnGestionProductos
            // 
            btnGestionProductos.Anchor = AnchorStyles.Left;
            btnGestionProductos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGestionProductos.IconChar = FontAwesome.Sharp.IconChar.Pencil;
            btnGestionProductos.IconColor = Color.Black;
            btnGestionProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGestionProductos.ImageAlign = ContentAlignment.MiddleLeft;
            btnGestionProductos.Location = new Point(3, 500);
            btnGestionProductos.Margin = new Padding(3, 2, 3, 2);
            btnGestionProductos.Name = "btnGestionProductos";
            btnGestionProductos.Size = new Size(188, 79);
            btnGestionProductos.TabIndex = 11;
            btnGestionProductos.Text = "GESTION DE PRODUCTOS (F7)";
            btnGestionProductos.TextAlign = ContentAlignment.MiddleLeft;
            btnGestionProductos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGestionProductos.UseVisualStyleBackColor = true;
            btnGestionProductos.Click += btnGestionProductos_Click;
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.Left;
            iconButton1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.House;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton1.Location = new Point(3, 666);
            iconButton1.Margin = new Padding(3, 2, 3, 2);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(188, 87);
            iconButton1.TabIndex = 12;
            iconButton1.Text = "INICIO (F11)";
            iconButton1.TextAlign = ContentAlignment.MiddleLeft;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logoCigral__1_;
            pictureBox1.Location = new Point(3, -10);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(202, 119);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(605, 25);
            label1.Name = "label1";
            label1.Size = new Size(858, 50);
            label1.TabIndex = 2;
            label1.Text = "SISTEMA DE GESTION DE STOCK Y TRAZABILIDAD";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.LightSlateGray;
            panelHeader.Controls.Add(label1);
            panelHeader.Controls.Add(pictureBox1);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 2, 3, 2);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1904, 97);
            panelHeader.TabIndex = 0;
            // 
            // panelContainer
            // 
            panelContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Font = new Font("Segoe UI", 9F);
            panelContainer.Location = new Point(205, 97);
            panelContainer.Margin = new Padding(0);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(1699, 944);
            panelContainer.TabIndex = 3;
            // 
            // iconButtonIngreso
            // 
            iconButtonIngreso.Anchor = AnchorStyles.Left;
            iconButtonIngreso.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            iconButtonIngreso.IconChar = FontAwesome.Sharp.IconChar.BoxOpen;
            iconButtonIngreso.IconColor = Color.Black;
            iconButtonIngreso.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonIngreso.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonIngreso.Location = new Point(3, 2);
            iconButtonIngreso.Margin = new Padding(3, 2, 3, 2);
            iconButtonIngreso.Name = "iconButtonIngreso";
            iconButtonIngreso.Size = new Size(188, 79);
            iconButtonIngreso.TabIndex = 1;
            iconButtonIngreso.Text = "INGRESO DE PRODUCTOS (F1)";
            iconButtonIngreso.TextAlign = ContentAlignment.MiddleLeft;
            iconButtonIngreso.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButtonIngreso.UseVisualStyleBackColor = true;
            iconButtonIngreso.Click += iconButton2_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.LightSlateGray;
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 97);
            panel1.Margin = new Padding(3, 2, 0, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(205, 944);
            panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoScroll = true;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(btnConsignacion, 0, 7);
            tableLayoutPanel2.Controls.Add(iconButtonIngreso, 0, 0);
            tableLayoutPanel2.Controls.Add(btnEgresos, 0, 1);
            tableLayoutPanel2.Controls.Add(iconButton1, 0, 8);
            tableLayoutPanel2.Controls.Add(btnStock, 0, 2);
            tableLayoutPanel2.Controls.Add(btnAuditoria, 0, 3);
            tableLayoutPanel2.Controls.Add(btnHistorialRemitos, 0, 4);
            tableLayoutPanel2.Controls.Add(btnEntidades, 0, 5);
            tableLayoutPanel2.Controls.Add(btnGestionProductos, 0, 6);
            tableLayoutPanel2.Dock = DockStyle.Left;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 9;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(202, 755);
            tableLayoutPanel2.TabIndex = 1;
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // btnConsignacion
            // 
            btnConsignacion.Anchor = AnchorStyles.Left;
            btnConsignacion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnConsignacion.IconChar = FontAwesome.Sharp.IconChar.ListAlt;
            btnConsignacion.IconColor = Color.Black;
            btnConsignacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnConsignacion.ImageAlign = ContentAlignment.MiddleLeft;
            btnConsignacion.Location = new Point(3, 583);
            btnConsignacion.Margin = new Padding(3, 2, 3, 2);
            btnConsignacion.Name = "btnConsignacion";
            btnConsignacion.Size = new Size(188, 79);
            btnConsignacion.TabIndex = 13;
            btnConsignacion.Text = "PRODUCTOS EN CONSIGNACION (F8)";
            btnConsignacion.TextAlign = ContentAlignment.MiddleLeft;
            btnConsignacion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnConsignacion.UseVisualStyleBackColor = true;
            btnConsignacion.Click += btnConsignacion_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pictureBox2, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 755);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(205, 189);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(panelContainer);
            Controls.Add(panel1);
            Controls.Add(panelHeader);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(1600, 900);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestion de Stock y Trazabilidad";
            WindowState = FormWindowState.Maximized;
            KeyDown += FormMain_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelHeader.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private FontAwesome.Sharp.IconButton btnStock;
        private FontAwesome.Sharp.IconButton btnAuditoria;
        private FontAwesome.Sharp.IconButton btnEgresos;
        private Label label2;
        private FontAwesome.Sharp.IconButton btnHistorialRemitos;
        private PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton btnEntidades;
        private FontAwesome.Sharp.IconButton btnGestionProductos;
        private FontAwesome.Sharp.IconButton iconButton1;
        private PictureBox pictureBox1;
        private Label label1;
        private Panel panelHeader;
        private Panel panelContainer;
        private FontAwesome.Sharp.IconButton iconButtonIngreso;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private FontAwesome.Sharp.IconButton btnConsignacion;
    }
}
