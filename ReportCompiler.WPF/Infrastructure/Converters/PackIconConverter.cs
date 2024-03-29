﻿using MaterialDesignThemes.Wpf;
using ReportCompiler.WPF.Infrastructure.Converters.Base;
using ReportCompiler.WPF.Models.Directories;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ReportCompiler.WPF.Infrastructure.Converters
{
    [ValueConversion(typeof(DirectoryItemType), typeof(PackIcon))]
    internal class PackIconConverter : Converter
    {
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not DirectoryItemType type) return null;

            return GetPackIconKind(type);
        }

        private static PackIcon GetPackIconKind(DirectoryItemType type) => type switch
        {
            DirectoryItemType.Directory => new PackIcon { Kind = PackIconKind.Folder, Margin = new Thickness(10, 0, 5, 0) },
            DirectoryItemType.ExcelFile => new PackIcon { Kind = PackIconKind.MicrosoftExcel, Margin = new Thickness(10, 0, 5, 0) },
            DirectoryItemType.ParentDirectory => new PackIcon { Kind = PackIconKind.FolderOpenOutline , Margin = new Thickness(0,0,5,0) },
            _ => throw new InvalidCastException("Не удалось выполнить преобразоввание.")
        };
    }
}
