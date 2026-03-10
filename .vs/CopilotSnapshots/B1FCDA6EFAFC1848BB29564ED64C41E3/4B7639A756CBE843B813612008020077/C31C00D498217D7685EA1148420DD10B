using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AplicacionClientesyReparaciones.Models;
using AplicacionClientesyReparaciones;

namespace AplicacionClientesyReparaciones.Views
{
	public partial class AgregarCliente : UserControl
	{
		public AgregarCliente()
		{
			InitializeComponent();
		}

		private void Salir(object sender, RoutedEventArgs e)
		{
			var ventana = Window.GetWindow(this);
			ventana?.Close();
		}

		private async void Guardar_Click(object sender, RoutedEventArgs e)
		{
			var telefonoValido = long.TryParse(TelefonoTextBox.Text, out var telefono);

			var cliente = new Cliente
			{
				Nombre = NombreTextBox.Text,
				Dni = DniTextBox.Text,
				Apellidos = ApellidosTextBox.Text,
				Telefono = telefonoValido ? telefono : null,
				Email = EmailTextBox.Text,
				Direccion = DireccionTextBox.Text
			};

			try
			{
				var client = await SupabaseService.GetClientAsync();
				await client.From<Cliente>().Insert(cliente);
				var ventana = Window.GetWindow(this);
				ventana?.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al guardar el cliente: {ex.Message}", "Supabase", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
