using System;
using System.Windows;
using System.Windows.Controls;
using AplicacionClientesyReparaciones.Models;
using AplicacionClientesyReparaciones;

namespace AplicacionClientesyReparaciones.Views
{
	public partial class VerUsuarios : UserControl
    {
        public VerUsuarios()
        {
            InitializeComponent();
			Loaded += VerUsuarios_Loaded;
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
			_ = CargarClientesAsync();
		}

		private async void VerUsuarios_Loaded(object sender, RoutedEventArgs e)
		{
			await CargarClientesAsync();
		}

		private async System.Threading.Tasks.Task CargarClientesAsync()
		{
			try
			{
				var client = await SupabaseService.GetClientAsync();
				var response = await client.From<Cliente>().Get();
				if (this.FindName("ClientesDataGrid") is DataGrid dataGrid)
				{
					dataGrid.ItemsSource = response.Models;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Supabase", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ClientesDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (sender is DataGrid grid && grid.SelectedItem is Cliente cliente)
			{
				var ventana = new DetalleClienteWindow(cliente)
				{
					Owner = Application.Current.MainWindow
				};
				ventana.ShowDialog();
			}
		}
    }
}
