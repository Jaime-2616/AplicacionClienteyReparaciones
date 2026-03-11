using System;
using System.Windows;
using System.Windows.Controls;
using AplicacionClientesyReparaciones.Models;
using AplicacionClientesyReparaciones;

namespace AplicacionClientesyReparaciones.Views
{
    public partial class DetalleReparacionWindow : Window
    {
        private readonly Reparacion _reparacion;

        public DetalleReparacionWindow(Reparacion reparacion)
        {
            InitializeComponent();
            _reparacion = reparacion;
            DataContext = reparacion;
        }

        private async void GuardarEstado_Click(object sender, RoutedEventArgs e)
        {
            var estadoSeleccionado = (EstadoComboBox.SelectedValue ?? EstadoComboBox.Text)?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(estadoSeleccionado))
                return;

            _reparacion.Estado = estadoSeleccionado;

            try
            {
                var client = await SupabaseService.GetClientAsync();
                await client.From<Reparacion>().Update(_reparacion);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el estado: {ex.Message}", "Supabase", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
