using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Globalization;
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
			if (FindName("PrecioTextBox") is TextBox precioTextBox && _reparacion.Precio.HasValue)
			{
				precioTextBox.Text = _reparacion.Precio.Value.ToString("0.##", CultureInfo.CurrentCulture);
			}
        }
        private async void GuardarEstado_Click(object sender, RoutedEventArgs e)
        {
            var estadoSeleccionado = (EstadoComboBox.SelectedValue ?? EstadoComboBox.Text)?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(estadoSeleccionado))
                return;

            _reparacion.Estado = estadoSeleccionado;

			if (FindName("PrecioTextBox") is TextBox precioTextBox)
			{
				var precioTexto = precioTextBox.Text?.Trim();
				if (!string.IsNullOrWhiteSpace(precioTexto))
				{
					decimal precio;
					var estilos = NumberStyles.Number;
					var culturaActual = CultureInfo.CurrentCulture;
					var textoNormalizado = precioTexto.Replace(',', culturaActual.NumberFormat.NumberDecimalSeparator[0]);
					if (!decimal.TryParse(textoNormalizado, estilos, culturaActual, out precio))
					{
						var textoPunto = precioTexto.Replace(',', '.');
						if (!decimal.TryParse(textoPunto, estilos, CultureInfo.InvariantCulture, out precio))
						{
							MessageBox.Show("El campo 'Precio' debe ser numérico (use coma o punto como separador decimal).", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
							return;
						}
					}
					_reparacion.Precio = precio;
				}
			}

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

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() != true) return;

            var documento = CrearDocumentoImpresion();
            documento.PageWidth = 624;
            documento.ColumnWidth = 624;

            printDialog.PrintDocument(((IDocumentPaginatorSource)documento).DocumentPaginator, "Ticket reparación");
        }

        private FlowDocument CrearDocumentoImpresion()  
        {
            var documento = new FlowDocument
            {
                PagePadding = new Thickness(20),
                FontFamily = new FontFamily("Consolas"),
                FontSize = 10,
                PageWidth = 624,
                ColumnWidth = 624
            };

            var encabezado = new Paragraph
            {
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 6),
                TextAlignment = TextAlignment.Left
            };

            encabezado.Inlines.Add(new Run("GRUPO PCBYTE\n"));
            encabezado.Inlines.Add(new Run("NIF/CIF: 34103929D\n"));
            encabezado.Inlines.Add(new Run("PLAZA SAGASTIEDER N11 BAJO\n"));
            encabezado.Inlines.Add(new Run("DONOSTIA\n"));
            encabezado.Inlines.Add(new Run("20015 - GIPUZKOA\n"));
            encabezado.Inlines.Add(new Run("943321439\n"));
    
            documento.Blocks.Add(encabezado);

            documento.Blocks.Add(CrearSeparador());
            documento.Blocks.Add(CrearLineaInfo("CLIENTE", _reparacion.NombreCliente?.ToUpper()));
            documento.Blocks.Add(CrearLineaInfo("MOVIL", _reparacion.Telefono1?.ToString()));

            documento.Blocks.Add(CrearSeparador());

            documento.Blocks.Add(CrearLineaInfo("REPARACION", _reparacion.Id.ToString()));
            documento.Blocks.Add(CrearLineaInfo("FECHA", _reparacion.FechaDeEntrega?.ToString("dd/MM/yyyy")));

            documento.Blocks.Add(CrearSeparador());

            documento.Blocks.Add(CrearBloqueTexto("MATERIAL ENTREGADO", _reparacion.MaterialEntregado?.ToUpper()));
            documento.Blocks.Add(CrearBloqueTexto("DESCRIPCION AVERIA", _reparacion.Descripcion?.ToUpper()));

            documento.Blocks.Add(CrearSeparador());

            if (_reparacion.Precio.HasValue && _reparacion.Precio.Value != 0m)
            {
                var precioTexto = string.Format("{0:F2} €", _reparacion.Precio.Value);
                documento.Blocks.Add(CrearLineaPrecio("TOTAL", precioTexto));
                documento.Blocks.Add(CrearSeparador());
            }
           
            documento.Blocks.Add(CrearLineaInfo("", "LA REALIZACION DEL PRESUPUESTO EN CASO"));
            documento.Blocks.Add(CrearLineaInfo("", "DE NO REPARARSE COBRARAN 27.90€ + IVA"));

            documento.Blocks.Add(CrearSeparador());

            documento.Blocks.Add(CrearLineaInfo("FIRMA", ""));

            return documento;
        }
            
        private static Paragraph CrearLineaInfo(string etiqueta, string? valor)
        {
            var paragraph = new Paragraph { Margin = new Thickness(0, 2, 0, 2) };

            if (!string.IsNullOrWhiteSpace(etiqueta))
            {
                paragraph.Inlines.Add(new Run($"{etiqueta}: ") { FontWeight = FontWeights.Bold });
            }
            paragraph.Inlines.Add(new Run(valor ?? ""));
            paragraph.TextAlignment = TextAlignment.Left;

            return paragraph;
        }

        private static Paragraph CrearLineaPrecio(string etiqueta, string valor)
        {
            var paragraph = new Paragraph { Margin = new Thickness(0, 4, 0, 4) };
            paragraph.Inlines.Add(new Run($"{etiqueta}: {valor}") { FontWeight = FontWeights.Bold });
            paragraph.TextAlignment = TextAlignment.Left;
            return paragraph;
        }

        private static Paragraph CrearBloqueTexto(string etiqueta, string? valor)
        {
            var paragraph = new Paragraph { Margin = new Thickness(0, 4, 0, 4) };
            paragraph.Inlines.Add(new Run($"{etiqueta}:\n") { FontWeight = FontWeights.Bold });
            paragraph.Inlines.Add(new Run(valor ?? ""));
            paragraph.TextAlignment = TextAlignment.Left;
            return paragraph;
        }

        private static Paragraph CrearSeparador()
        {
            return new Paragraph(new Run(new string('-', 80))) { Margin = new Thickness(0, 4, 0, 4), TextAlignment = TextAlignment.Left };
        }
    }
}
