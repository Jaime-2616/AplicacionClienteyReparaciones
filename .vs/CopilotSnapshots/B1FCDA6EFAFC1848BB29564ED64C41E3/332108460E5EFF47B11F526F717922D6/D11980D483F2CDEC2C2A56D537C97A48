using System.Windows;
namespace AplicacionClientesyReparaciones.Views
{
    public partial class PaginaPrincipal : Window
    {
        public PaginaPrincipal()
        {
            InitializeComponent();
        }

        private void VerClientes(object sender, RoutedEventArgs e)
        {
            var ventana = new VerUsuarios();
            this.Close();
            ventana.Show();
        }
        private void VerReparaciones(object sender, RoutedEventArgs e)
        {
            var ventana = new VerReparaciones();
            this.Close();
            ventana.Show();
        }
        private void CrearReparaciones(object sender, RoutedEventArgs e)
        {
            var ventana = new CrearReparacion();
            this.Close();
            ventana.Show();
        }
    }
}
