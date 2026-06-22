namespace SIS_BODEGA
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "admin" && txtContrasena.Text == "1234")
            {
                MessageBox.Show("Bienvenido! Logeado con éxito");

                FrmSistema sistema = new FrmSistema();
                sistema.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtUsuario.Focus();
        }
    }
}
