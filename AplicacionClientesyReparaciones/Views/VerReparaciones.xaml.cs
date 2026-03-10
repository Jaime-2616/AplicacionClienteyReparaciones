using System;
using System.Windows;
using System.Windows.Controls;
using AplicacionClientesyReparaciones.Models;
using AplicacionClientesyReparaciones;

namespace AplicacionClientesyReparaciones.Views
{
	public partial class VerReparaciones : UserControl
    {
        public VerReparaciones()
        {
            InitializeComponent();
			Loaded += VerReparaciones_Loaded;
        }

    private async void NuevaReparacion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var contenido = new CrearReparacion();
            var ventana = new System.Windows.Window
            {
                Title = "Crear reparación",
                Content = contenido,
				Width = 500,
				Height = 350,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = System.Windows.Application.Current.MainWindow
            };
            ventana.ShowDialog();
			await CargarReparacionesAsync();
        }

		private async void VerReparaciones_Loaded(object sender, RoutedEventArgs e)
		{
			await CargarReparacionesAsync();
		}

		private async System.Threading.Tasks.Task CargarReparacionesAsync()
		{
			try
			{
				var client = await SupabaseService.GetClientAsync();
				var response = await client.From<Reparacion>().Get();
				if (this.FindName("ReparacionesDataGrid") is DataGrid dataGrid)
				{
					dataGrid.ItemsSource = response.Models;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al cargar reparaciones: {ex.Message}", "Supabase", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
    }
}
