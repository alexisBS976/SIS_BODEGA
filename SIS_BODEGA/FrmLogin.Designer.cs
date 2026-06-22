namespace SIS_BODEGA
{
    partial class FrmLogin
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
            groupBox1 = new GroupBox();
            btnLogin = new Button();
            txtContrasena = new TextBox();
            txtUsuario = new TextBox();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.BackColor = SystemColors.ControlLight;
            groupBox1.Controls.Add(btnLogin);
            groupBox1.Controls.Add(txtContrasena);
            groupBox1.Controls.Add(txtUsuario);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(49, 78);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(547, 306);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.YellowGreen;
            btnLogin.Font = new Font("Segoe UI Variable Text Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = SystemColors.ActiveCaptionText;
            btnLogin.Location = new Point(173, 224);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(186, 56);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "INICIAR SESION";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(187, 159);
            txtContrasena.MaxLength = 4;
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(280, 27);
            txtContrasena.TabIndex = 4;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(187, 65);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(280, 27);
            txtUsuario.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(28, 155);
            label4.Name = "label4";
            label4.Size = new Size(153, 28);
            label4.TabIndex = 2;
            label4.Text = "CONTRASEÑA:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(28, 65);
            label2.Name = "label2";
            label2.Size = new Size(108, 28);
            label2.TabIndex = 0;
            label2.Text = "USUARIO:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(181, 23);
            label1.Name = "label1";
            label1.Size = new Size(290, 43);
            label1.TabIndex = 1;
            label1.Text = "BIENVENIDO!! ";
            // 
            // FrmLogin
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(640, 420);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmLogin";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtContrasena;
        private TextBox txtUsuario;
        private Label label4;
        private Label label2;
        private Label label1;
        private Button btnLogin;
    }
}
