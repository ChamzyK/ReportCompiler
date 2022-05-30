using ReportCompiler.WPF.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ReportCompiler.WPF.Infrastructure.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal class BooleanToVisibility : Converter
    {
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool type) return null;

            return type ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
