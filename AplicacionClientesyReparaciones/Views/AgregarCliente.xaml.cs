using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
			var nombre = NombreTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(nombre))
			{
				MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var apellidos = ApellidosTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(apellidos))
			{
				MessageBox.Show("Los apellidos son obligatorios.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var dni = DniTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(dni))
			{
				MessageBox.Show("El DNI es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (dni.Length != 9)
			{
				MessageBox.Show("El DNI debe tener 9 caracteres.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var telefono1Texto = Telefono1TextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(telefono1Texto))
			{
				MessageBox.Show("El movil 1 es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (!long.TryParse(telefono1Texto, out var telefono1))
			{
				MessageBox.Show("El movil 1 debe contener solo números.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var telefono2Texto = Telefono2TextBox.Text.Trim();
			long? telefono2 = null;
			if (!string.IsNullOrWhiteSpace(telefono2Texto))
			{
				if (!long.TryParse(telefono2Texto, out var telefono2Parseado))
				{
					MessageBox.Show("El movil 2 debe contener solo números.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				telefono2 = telefono2Parseado;
			}

			var email = EmailTextBox.Text.Trim();
			var patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, patronEmail))
			{
				MessageBox.Show("El email no es válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var direccion = DireccionTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(direccion))
			{
				MessageBox.Show("La dirección es obligatoria.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var poblacion = PoblacionTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(poblacion))
			{
				MessageBox.Show("La población es obligatoria.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var provincia = ProvinciaTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(provincia))
			{
				MessageBox.Show("La provincia es obligatoria.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var observaciones = ObservacionesTextBox.Text.Trim();

			var cliente = new Cliente
			{
				Nombre = nombre,
				Dni = dni,
				Apellidos = apellidos,
				Telefono1 = telefono1,
				Telefono2 = telefono2,
				Email = email,
				Direccion = direccion,
				Poblacion = poblacion,
				Provincia = provincia,
				Observaciones = observaciones
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
