namespace SIS_BODEGA
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            // Inicializa todos los componentes visuales del formulario diseñados en la interfaz
            InitializeComponent();
        }

        /// <summary>
        /// Gestiona el evento de clic en el botón de inicio de sesión.
        /// Valida las credenciales del usuario y permite o deniega el acceso al sistema principal.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validación de credenciales estáticas en código (Hardcoded)
            if (txtUsuario.Text == "admin" && txtContrasena.Text == "1234")
            {
                // Notifica al usuario que el acceso fue concedido de manera exitosa
                MessageBox.Show("Bienvenido! Logeado con éxito");

                // Instancia el formulario principal del sistema
                FrmSistema sistema = new FrmSistema();

                // Muestra el formulario del sistema en pantalla
                sistema.Show();

                // Oculta el formulario de login actual para mantener limpia la interfaz
                this.Hide();
            }
            else
            {
                // Alerta en caso de que el usuario o la contraseña no coincidan con los valores válidos
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
            // Limpieza de los campos de texto para prepararlos para un nuevo intento de ingreso
            txtUsuario.Clear();
            txtContrasena.Clear();
            // Devuelve el foco del teclado al campo de usuario para agilizar el reingreso de datos
            txtUsuario.Focus();
        }
    }
}
