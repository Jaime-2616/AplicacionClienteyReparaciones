using System.Windows;
using System.Windows.Controls;

namespace AplicacionClientesyReparaciones.Views
{
	public partial class VerUsuarios : UserControl
    {
        public VerUsuarios()
        {
            InitializeComponent();
        }
		
		private void NuevoCliente_Click(object sender, RoutedEventArgs e)
		{
			var contenido = new AgregarCliente();
			var ventana = new Window
			{
				Title = "Agregar cliente",
				Content = contenido,
				SizeToContent = SizeToContent.WidthAndHeight,
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
				Owner = Application.Current.MainWindow
			};
			ventana.ShowDialog();
		}
    }
}
