using System;
using System.Globalization;
using System.Windows.Data;

namespace AplicacionClientesyReparaciones.Converters
{
    public sealed class DescripcionTrimmerConverter : IValueConverter
    {
        public int MaxLength { get; set; } = 60;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string text)
            {
                return value;
            }

            if (MaxLength <= 0 || text.Length <= MaxLength)
            {
                return text;
            }

            var sliceLength = Math.Max(0, MaxLength - 3);
            return string.Concat(text.AsSpan(0, sliceLength), "...");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
