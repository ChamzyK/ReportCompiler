using MaterialDesignThemes.Wpf;
using ReportCompiler.WPF.Infrastructure.Converters.Base;
using ReportCompiler.WPF.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ReportCompiler.WPF.Infrastructure.Converters
{
    [ValueConversion(typeof(DirectoryItemType),typeof(PackIconKind))]
    internal class DirectoryItemTypeToKind : Converter
    {
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not DirectoryItemType type) return null;

            return GetPackIconKind(type);
        }

        private PackIconKind GetPackIconKind(DirectoryItemType type) => type switch
        {
            DirectoryItemType.Directory => PackIconKind.Folder,
            DirectoryItemType.ExcelFile => PackIconKind.MicrosoftExcel,
            DirectoryItemType.ParentDirectory => PackIconKind.FolderOpenOutline,
            DirectoryItemType.RootDirectory => PackIconKind.Harddisk,
            _ => throw new InvalidCastException("Не удалось выполнить преобразоввание.")
        };
    }
}
