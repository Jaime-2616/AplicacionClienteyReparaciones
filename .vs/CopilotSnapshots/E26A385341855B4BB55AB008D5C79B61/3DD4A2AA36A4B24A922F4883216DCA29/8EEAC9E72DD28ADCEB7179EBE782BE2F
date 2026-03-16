using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using AplicacionClientesyReparaciones.Models;
using AplicacionClientesyReparaciones;

namespace AplicacionClientesyReparaciones.Views
{
    public partial class CrearReparacion : UserControl
    {
        private List<Cliente> _clientes = new();

        public CrearReparacion()
        {
            InitializeComponent();
            FechaEntregaDatePicker.SelectedDate = DateTime.Now.Date;
            Loaded += CrearReparacion_Loaded;
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            var ventana = Window.GetWindow(this);
            ventana?.Close();
        }

        private async void NuevoCliente_Click(object sender, RoutedEventArgs e)
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
            await CargarClientesAsync();
        }

        private async void CrearReparacion_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarClientesAsync();
        }

        private async System.Threading.Tasks.Task CargarClientesAsync()
        {
            try
            {
                var client = await SupabaseService.GetClientAsync();
                var response = await client.From<Cliente>().Get();

                _clientes = response.Models;
                ClientesComboBox.ItemsSource = _clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}",
                    "Supabase",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ClientesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientesComboBox.SelectedItem is Cliente clienteSeleccionado)
            {
                Telefono1TextBox.Text = clienteSeleccionado.Telefono1?.ToString() ?? "";
            }
            else
            {
                Telefono1TextBox.Text = "";
            }
        }

        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var telefonoValido = long.TryParse(Telefono1TextBox.Text, out var telefono1);
            var clienteSeleccionado = ClientesComboBox.SelectedItem as Cliente;

            DateOnly? fechaEntrega = null;

            if (FechaEntregaDatePicker.SelectedDate.HasValue)
            {
                fechaEntrega = DateOnly.FromDateTime(
                    FechaEntregaDatePicker.SelectedDate.Value
                );
            }

            var reparacion = new Reparacion
            {
                NombreCliente = clienteSeleccionado?.Nombre ?? "",
                Telefono1 = telefonoValido ? telefono1 : null,
                FechaDeEntrega = fechaEntrega,
                MaterialEntregado = MaterialEntregadoTextBox.Text,
                Descripcion = DescripcionTextBox.Text,
                ClienteId = clienteSeleccionado?.Id,
                Estado = "Pendiente"
            };

            try
            {
                var client = await SupabaseService.GetClientAsync();

                await client
                    .From<Reparacion>()
                    .Insert(reparacion);

                var ventana = Window.GetWindow(this);
                ventana?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la reparación: {ex.Message}",
                    "Supabase",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void DescripcionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}