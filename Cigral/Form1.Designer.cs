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
            panelHeader = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panelContainer = new Panel();
            panel1 = new Panel();
            btnEntidades = new FontAwesome.Sharp.IconButton();
            pictureBox2 = new PictureBox();
            btnHistorialRemitos = new FontAwesome.Sharp.IconButton();
            label2 = new Label();
            btnInicio = new FontAwesome.Sharp.IconButton();
            btnEgresos = new FontAwesome.Sharp.IconButton();
            btnAuditoria = new FontAwesome.Sharp.IconButton();
            btnStock = new FontAwesome.Sharp.IconButton();
            iconButtonIngreso = new FontAwesome.Sharp.IconButton();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
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
            // panelContainer
            // 
            panelContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContainer.Font = new Font("Segoe UI", 9F);
            panelContainer.Location = new Point(208, 97);
            panelContainer.Margin = new Padding(0);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(1696, 944);
            panelContainer.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSlateGray;
            panel1.Controls.Add(btnEntidades);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(btnHistorialRemitos);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnInicio);
            panel1.Controls.Add(btnEgresos);
            panel1.Controls.Add(btnAuditoria);
            panel1.Controls.Add(btnStock);
            panel1.Controls.Add(iconButtonIngreso);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 97);
            panel1.Margin = new Padding(3, 2, 0, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(208, 944);
            panel1.TabIndex = 2;
            // 
            // btnEntidades
            // 
            btnEntidades.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEntidades.IconChar = FontAwesome.Sharp.IconChar.Info;
            btnEntidades.IconColor = Color.Black;
            btnEntidades.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEntidades.ImageAlign = ContentAlignment.MiddleLeft;
            btnEntidades.Location = new Point(1, 480);
            btnEntidades.Margin = new Padding(3, 2, 3, 2);
            btnEntidades.Name = "btnEntidades";
            btnEntidades.Size = new Size(207, 92);
            btnEntidades.TabIndex = 10;
            btnEntidades.Text = "GESTION DE ENTIDADES (F6)";
            btnEntidades.TextAlign = ContentAlignment.MiddleLeft;
            btnEntidades.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEntidades.UseVisualStyleBackColor = true;
            btnEntidades.Click += btnEntidades_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(3, 768);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(202, 140);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // btnHistorialRemitos
            // 
            btnHistorialRemitos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHistorialRemitos.IconChar = FontAwesome.Sharp.IconChar.Info;
            btnHistorialRemitos.IconColor = Color.Black;
            btnHistorialRemitos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHistorialRemitos.ImageAlign = ContentAlignment.MiddleLeft;
            btnHistorialRemitos.Location = new Point(1, 384);
            btnHistorialRemitos.Margin = new Padding(3, 2, 3, 2);
            btnHistorialRemitos.Name = "btnHistorialRemitos";
            btnHistorialRemitos.Size = new Size(207, 92);
            btnHistorialRemitos.TabIndex = 8;
            btnHistorialRemitos.Text = "HISTORIAL DE REMITOS (F5)";
            btnHistorialRemitos.TextAlign = ContentAlignment.MiddleLeft;
            btnHistorialRemitos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHistorialRemitos.UseVisualStyleBackColor = true;
            btnHistorialRemitos.Click += btnHistorialRemitos_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(0, 929);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 7;
            label2.Text = "Versión 1.0.0";
            // 
            // btnInicio
            // 
            btnInicio.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnInicio.IconChar = FontAwesome.Sharp.IconChar.House;
            btnInicio.IconColor = Color.Black;
            btnInicio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnInicio.ImageAlign = ContentAlignment.MiddleLeft;
            btnInicio.Location = new Point(0, 576);
            btnInicio.Margin = new Padding(3, 2, 3, 2);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(207, 92);
            btnInicio.TabIndex = 6;
            btnInicio.Text = "   INICIO";
            btnInicio.TextAlign = ContentAlignment.MiddleLeft;
            btnInicio.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnInicio.UseVisualStyleBackColor = true;
            btnInicio.Click += btnInicio_Click;
            // 
            // btnEgresos
            // 
            btnEgresos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEgresos.IconChar = FontAwesome.Sharp.IconChar.BoxesPacking;
            btnEgresos.IconColor = Color.Black;
            btnEgresos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEgresos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEgresos.Location = new Point(0, 96);
            btnEgresos.Margin = new Padding(3, 2, 3, 2);
            btnEgresos.Name = "btnEgresos";
            btnEgresos.Size = new Size(207, 92);
            btnEgresos.TabIndex = 5;
            btnEgresos.Text = "EGRESO DE PRODUCTOS (F2)";
            btnEgresos.TextAlign = ContentAlignment.MiddleLeft;
            btnEgresos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEgresos.UseVisualStyleBackColor = true;
            btnEgresos.Click += btnEgresos_Click;
            // 
            // btnAuditoria
            // 
            btnAuditoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAuditoria.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnAuditoria.IconColor = Color.Black;
            btnAuditoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAuditoria.ImageAlign = ContentAlignment.MiddleLeft;
            btnAuditoria.Location = new Point(0, 288);
            btnAuditoria.Margin = new Padding(3, 2, 3, 2);
            btnAuditoria.Name = "btnAuditoria";
            btnAuditoria.Size = new Size(207, 92);
            btnAuditoria.TabIndex = 4;
            btnAuditoria.Text = "AUDITORIA DE PRODUCTOS (F4)";
            btnAuditoria.TextAlign = ContentAlignment.MiddleLeft;
            btnAuditoria.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAuditoria.UseVisualStyleBackColor = true;
            btnAuditoria.Click += btnAuditoria_Click;
            // 
            // btnStock
            // 
            btnStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStock.IconChar = FontAwesome.Sharp.IconChar.BoxesStacked;
            btnStock.IconColor = Color.Black;
            btnStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStock.ImageAlign = ContentAlignment.MiddleLeft;
            btnStock.Location = new Point(0, 192);
            btnStock.Margin = new Padding(3, 2, 3, 2);
            btnStock.Name = "btnStock";
            btnStock.Size = new Size(207, 92);
            btnStock.TabIndex = 3;
            btnStock.Text = "STOCK DISPONIBLE (F3)";
            btnStock.TextAlign = ContentAlignment.MiddleLeft;
            btnStock.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStock.UseVisualStyleBackColor = true;
            btnStock.Click += btnStock_Click;
            // 
            // iconButtonIngreso
            // 
            iconButtonIngreso.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            iconButtonIngreso.IconChar = FontAwesome.Sharp.IconChar.BoxOpen;
            iconButtonIngreso.IconColor = Color.Black;
            iconButtonIngreso.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonIngreso.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonIngreso.Location = new Point(0, 0);
            iconButtonIngreso.Margin = new Padding(3, 2, 3, 2);
            iconButtonIngreso.Name = "iconButtonIngreso";
            iconButtonIngreso.Size = new Size(207, 92);
            iconButtonIngreso.TabIndex = 1;
            iconButtonIngreso.Text = "INGRESO DE PRODUCTOS (F1)";
            iconButtonIngreso.TextAlign = ContentAlignment.MiddleLeft;
            iconButtonIngreso.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButtonIngreso.UseVisualStyleBackColor = true;
            iconButtonIngreso.Click += iconButton2_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(panelContainer);
            Controls.Add(panel1);
            Controls.Add(panelHeader);
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestion de Stock y Trazabilidad";
            WindowState = FormWindowState.Maximized;
            KeyDown += FormMain_KeyDown;
            panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Panel panel1;
        private Panel panelContainer;
        private FontAwesome.Sharp.IconButton iconButtonIngreso;
        private FontAwesome.Sharp.IconButton btnAuditoria;
        private FontAwesome.Sharp.IconButton btnStock;
        private PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton btnEgresos;
        private Label label1;
        private Label label2;
        private FontAwesome.Sharp.IconButton btnInicio;
        private FontAwesome.Sharp.IconButton btnHistorialRemitos;
        private PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton btnEntidades;
    }
}
