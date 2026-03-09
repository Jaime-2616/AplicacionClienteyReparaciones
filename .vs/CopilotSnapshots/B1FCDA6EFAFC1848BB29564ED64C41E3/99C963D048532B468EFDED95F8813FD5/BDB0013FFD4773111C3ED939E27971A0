using System.Windows.Controls;

namespace AplicacionClientesyReparaciones.Views
{
	public partial class VerReparaciones : UserControl
    {
        public VerReparaciones()
        {
            InitializeComponent();
        }

    private void NuevaReparacion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var contenido = new CrearReparacion();
            var ventana = new System.Windows.Window
            {
                Title = "Crear reparación",
                Content = contenido,
                SizeToContent = System.Windows.SizeToContent.WidthAndHeight,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = System.Windows.Application.Current.MainWindow
            };
            ventana.ShowDialog();
        }
    }
}
