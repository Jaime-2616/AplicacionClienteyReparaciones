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
			ContenidoPrincipal.Content = new VerUsuarios();
        }
        private void VerReparaciones(object sender, RoutedEventArgs e)
        {
			ContenidoPrincipal.Content = new VerReparaciones();
        }
        private void CrearReparaciones(object sender, RoutedEventArgs e)
        {
			ContenidoPrincipal.Content = new CrearReparacion();
        }
    }
}
