using System;
using System.ComponentModel;
using System.IO;

namespace ReportCompiler.WPF.Models.Reports
{
    internal enum Month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    internal static class MonthExtensions
    {
        public static string GetName(this Month month) => month switch
        {
            Month.January => "Январь",
            Month.February => "Февраль",
            Month.March => "Март",
            Month.April => "Апрель",
            Month.May => "Май",
            Month.June => "Июнь",
            Month.July => "Июль",
            Month.August => "Август",
            Month.September => "Сентябрь",
            Month.October => "Октябрь",
            Month.November => "Ноябрь",
            Month.December => "Декабрь",

            _ => throw new Exception("Не удалось преобразовать месяц в строковое значение.")
        };

        public static Month GetMonth(this string name) => name switch
        {
            "Январь" => Month.January,
            "Февраль" => Month.February,
            "Март" => Month.March,
            "Апрель" => Month.April,
            "Май" => Month.May,
            "Июнь" => Month.June,
            "Июль" => Month.July,
            "Август" => Month.August,
            "Сентябрь" => Month.September,
            "Октябрь" => Month.October,
            "Ноябрь" => Month.November,
            "Декабрь" => Month.December,

            _ => throw new Exception("Не удалось преобразовать месяц в строковое значение.")
        };

        public static string GetDirPath(this Month month) => Path.Combine(App.CurrentDir, Path.Combine("Отчеты", month.GetName()));

        public static string GetSummaryDirPath(this Month month) => Path.Combine(month.GetDirPath(), "Сводный отчет");
    }
}
