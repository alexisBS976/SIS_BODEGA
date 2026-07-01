namespace SIS_BODEGA
{
    partial class FrmSistema
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            groupBox1 = new GroupBox();
            bntAgregar = new Button();
            cmbProducto = new ComboBox();
            txtMonto = new TextBox();
            txtCantidad = new TextBox();
            btnCobrar = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            btnDetalle = new Button();
            btnVerProductos = new Button();
            btnReporte = new Button();
            dgvReportes = new DataGridView();
            groupBox3 = new GroupBox();
            txtNuevaCantidad = new TextBox();
            label8 = new Label();
            btnInvModificar = new Button();
            btnConsultar = new Button();
            txtStock = new TextBox();
            label9 = new Label();
            cmbNombre = new ComboBox();
            label5 = new Label();
            btnBorrar = new Button();
            txtIdEliminar = new TextBox();
            label7 = new Label();
            txtPagoCon = new TextBox();
            btnCierre = new Button();
            btnMasVendidos = new Button();
            btnVuelto = new Button();
            label6 = new Label();
            lblVuelto = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 16.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(278, 35);
            label1.Name = "label1";
            label1.Size = new Size(468, 35);
            label1.TabIndex = 0;
            label1.Text = "--- SISTEMA BODEGUITA KEVIN ---";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ButtonFace;
            groupBox1.Controls.Add(bntAgregar);
            groupBox1.Controls.Add(cmbProducto);
            groupBox1.Controls.Add(txtMonto);
            groupBox1.Controls.Add(txtCantidad);
            groupBox1.Controls.Add(btnCobrar);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(39, 99);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(438, 272);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "VENTAS";
            // 
            // bntAgregar
            // 
            bntAgregar.BackColor = Color.FromArgb(0, 192, 0);
            bntAgregar.Font = new Font("Segoe UI Emoji", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            bntAgregar.Location = new Point(17, 197);
            bntAgregar.Name = "bntAgregar";
            bntAgregar.Size = new Size(201, 55);
            bntAgregar.TabIndex = 8;
            bntAgregar.Text = "AGREGAR PRODUCTOS";
            bntAgregar.UseVisualStyleBackColor = false;
            bntAgregar.Click += bntAgregar_Click;
            // 
            // cmbProducto
            // 
            cmbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProducto.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(96, 42);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(265, 28);
            cmbProducto.TabIndex = 7;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(83, 150);
            txtMonto.Name = "txtMonto";
            txtMonto.ReadOnly = true;
            txtMonto.Size = new Size(231, 27);
            txtMonto.TabIndex = 6;
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(98, 101);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(166, 27);
            txtCantidad.TabIndex = 5;
            // 
            // btnCobrar
            // 
            btnCobrar.BackColor = Color.FromArgb(255, 128, 128);
            btnCobrar.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnCobrar.Location = new Point(265, 200);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(157, 52);
            btnCobrar.TabIndex = 3;
            btnCobrar.Text = "COBRAR";
            btnCobrar.UseVisualStyleBackColor = false;
            btnCobrar.Click += btnVenta_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(17, 153);
            label4.Name = "label4";
            label4.Size = new Size(60, 20);
            label4.TabIndex = 2;
            label4.Text = "Monto:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(17, 101);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 1;
            label3.Text = "Cantidad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 45);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 0;
            label2.Text = "Producto:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblVuelto);
            groupBox2.Controls.Add(btnDetalle);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(btnVerProductos);
            groupBox2.Controls.Add(btnVuelto);
            groupBox2.Controls.Add(btnReporte);
            groupBox2.Controls.Add(btnMasVendidos);
            groupBox2.Controls.Add(dgvReportes);
            groupBox2.Controls.Add(btnCierre);
            groupBox2.Controls.Add(txtPagoCon);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(548, 99);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(574, 624);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "REPORTES Y ADMINISTRACION";
            // 
            // btnDetalle
            // 
            btnDetalle.BackColor = Color.Fuchsia;
            btnDetalle.Location = new Point(204, 26);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(165, 32);
            btnDetalle.TabIndex = 6;
            btnDetalle.Text = "DETALLE VENTA";
            btnDetalle.UseVisualStyleBackColor = false;
            btnDetalle.Click += btnDetalle_Click;
            // 
            // btnVerProductos
            // 
            btnVerProductos.BackColor = Color.FromArgb(192, 192, 0);
            btnVerProductos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnVerProductos.ForeColor = SystemColors.ButtonFace;
            btnVerProductos.Location = new Point(375, 18);
            btnVerProductos.Name = "btnVerProductos";
            btnVerProductos.Size = new Size(193, 40);
            btnVerProductos.TabIndex = 17;
            btnVerProductos.Text = "VER PRODUCTOS";
            btnVerProductos.UseVisualStyleBackColor = false;
            btnVerProductos.Click += btnVerProductos_Click;
            // 
            // btnReporte
            // 
            btnReporte.BackColor = Color.FromArgb(128, 64, 0);
            btnReporte.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnReporte.ForeColor = SystemColors.ButtonFace;
            btnReporte.Location = new Point(5, 21);
            btnReporte.Name = "btnReporte";
            btnReporte.Size = new Size(193, 40);
            btnReporte.TabIndex = 16;
            btnReporte.Text = "VER REPORTES";
            btnReporte.UseVisualStyleBackColor = false;
            btnReporte.Click += btnReporte_Click;
            // 
            // dgvReportes
            // 
            dgvReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReportes.Location = new Point(22, 67);
            dgvReportes.Name = "dgvReportes";
            dgvReportes.RowHeadersWidth = 51;
            dgvReportes.Size = new Size(530, 287);
            dgvReportes.TabIndex = 14;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtNuevaCantidad);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(btnInvModificar);
            groupBox3.Controls.Add(btnConsultar);
            groupBox3.Controls.Add(txtStock);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(cmbNombre);
            groupBox3.Controls.Add(label5);
            groupBox3.Location = new Point(39, 386);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(438, 281);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "INVENTARIO";
            // 
            // txtNuevaCantidad
            // 
            txtNuevaCantidad.Location = new Point(145, 109);
            txtNuevaCantidad.Name = "txtNuevaCantidad";
            txtNuevaCantidad.Size = new Size(130, 27);
            txtNuevaCantidad.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(15, 112);
            label8.Name = "label8";
            label8.Size = new Size(124, 20);
            label8.TabIndex = 18;
            label8.Text = "Nueva Cantidad:";
            // 
            // btnInvModificar
            // 
            btnInvModificar.BackColor = Color.Brown;
            btnInvModificar.ForeColor = SystemColors.ButtonHighlight;
            btnInvModificar.Location = new Point(290, 90);
            btnInvModificar.Name = "btnInvModificar";
            btnInvModificar.Size = new Size(132, 60);
            btnInvModificar.TabIndex = 17;
            btnInvModificar.Text = "MODIFICAR STOCK";
            btnInvModificar.UseVisualStyleBackColor = false;
            btnInvModificar.Click += btnInvModificar_Click;
            // 
            // btnConsultar
            // 
            btnConsultar.BackColor = Color.Fuchsia;
            btnConsultar.Location = new Point(290, 178);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(132, 65);
            btnConsultar.TabIndex = 16;
            btnConsultar.Text = "CONSULTAR STOCK";
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(75, 194);
            txtStock.Name = "txtStock";
            txtStock.ReadOnly = true;
            txtStock.Size = new Size(209, 27);
            txtStock.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(17, 197);
            label9.Name = "label9";
            label9.Size = new Size(52, 20);
            label9.TabIndex = 9;
            label9.Text = "Stock:";
            // 
            // cmbNombre
            // 
            cmbNombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbNombre.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbNombre.FormattingEnabled = true;
            cmbNombre.Location = new Point(100, 44);
            cmbNombre.Name = "cmbNombre";
            cmbNombre.Size = new Size(291, 28);
            cmbNombre.TabIndex = 9;
            cmbNombre.SelectedIndexChanged += cmbNombre_SelectedIndexChanged_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(17, 47);
            label5.Name = "label5";
            label5.Size = new Size(77, 20);
            label5.TabIndex = 9;
            label5.Text = "Producto:";
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(930, 39);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(152, 29);
            btnBorrar.TabIndex = 4;
            btnBorrar.Text = "Borrar Reportes";
            btnBorrar.UseVisualStyleBackColor = true;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // txtIdEliminar
            // 
            txtIdEliminar.Location = new Point(797, 37);
            txtIdEliminar.Name = "txtIdEliminar";
            txtIdEliminar.Size = new Size(125, 27);
            txtIdEliminar.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(46, 534);
            label7.Name = "label7";
            label7.Size = new Size(77, 20);
            label7.TabIndex = 10;
            label7.Text = "Pago con:";
            // 
            // txtPagoCon
            // 
            txtPagoCon.Location = new Point(129, 531);
            txtPagoCon.Name = "txtPagoCon";
            txtPagoCon.Size = new Size(192, 27);
            txtPagoCon.TabIndex = 11;
            txtPagoCon.TextChanged += txtPagoCon_TextChanged;
            // 
            // btnCierre
            // 
            btnCierre.BackColor = Color.Lime;
            btnCierre.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCierre.Location = new Point(46, 362);
            btnCierre.Name = "btnCierre";
            btnCierre.Size = new Size(191, 89);
            btnCierre.TabIndex = 12;
            btnCierre.Text = "CIERRE DE CAJA";
            btnCierre.UseVisualStyleBackColor = false;
            btnCierre.Click += btnCierre_Click;
            // 
            // btnMasVendidos
            // 
            btnMasVendidos.BackColor = Color.OliveDrab;
            btnMasVendidos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMasVendidos.Location = new Point(343, 360);
            btnMasVendidos.Name = "btnMasVendidos";
            btnMasVendidos.Size = new Size(191, 89);
            btnMasVendidos.TabIndex = 13;
            btnMasVendidos.Text = "TOP PRODUCTOS MÁS VENDIDOS";
            btnMasVendidos.UseVisualStyleBackColor = false;
            btnMasVendidos.Click += btnMasVendidos_Click;
            // 
            // btnVuelto
            // 
            btnVuelto.BackColor = Color.FromArgb(255, 255, 192);
            btnVuelto.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVuelto.Location = new Point(368, 533);
            btnVuelto.Name = "btnVuelto";
            btnVuelto.Size = new Size(166, 76);
            btnVuelto.TabIndex = 14;
            btnVuelto.Text = "CALCULAR VUELTO";
            btnVuelto.UseVisualStyleBackColor = false;
            btnVuelto.Click += btnVuelto_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(57, 581);
            label6.Name = "label6";
            label6.Size = new Size(59, 20);
            label6.TabIndex = 15;
            label6.Text = "Vuelto:";
            // 
            // lblVuelto
            // 
            lblVuelto.AutoSize = true;
            lblVuelto.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVuelto.Location = new Point(129, 574);
            lblVuelto.Name = "lblVuelto";
            lblVuelto.Size = new Size(0, 28);
            lblVuelto.TabIndex = 16;
            // 
            // FrmSistema
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 735);
            Controls.Add(txtIdEliminar);
            Controls.Add(btnBorrar);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSistema";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmSistema";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ComboBox cmbProducto;
        private TextBox txtMonto;
        private TextBox txtCantidad;
        private Button btnCobrar;
        private Label label4;
        private Label label3;
        private Label label2;
        private DataGridView dgvReportes;
        private Button bntAgregar;
        private Button btnInvModificar;
        private Button btnConsultar;
        private TextBox txtStock;
        private Label label9;
        private ComboBox cmbNombre;
        private Label label5;
        private TextBox txtNuevaCantidad;
        private Label label8;
        private Button btnReporte;
        private Button btnVerProductos;
        private Button btnBorrar;
        private TextBox txtIdEliminar;
        private Button btnDetalle;
        private Label lblVuelto;
        private Label label6;
        private Button btnVuelto;
        private Button btnMasVendidos;
        private Button btnCierre;
        private TextBox txtPagoCon;
        private Label label7;
    }
}