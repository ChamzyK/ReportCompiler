using System;
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
            Month.January => "1) Январь",
            Month.February => "2) Февраль",
            Month.March => "3) Март",
            Month.April => "4) Апрель",
            Month.May => "5) Май",
            Month.June => "6) Июнь",
            Month.July => "7) Июль",
            Month.August => "8) Август",
            Month.September => "9) Сентябрь",
            Month.October => "10) Октябрь",
            Month.November => "11) Ноябрь",
            Month.December => "12) Декабрь",

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


            "1) Январь" => Month.January,
            "2) Февраль" => Month.February,
            "3) Март" => Month.March,
            "4) Апрель" => Month.April,
            "5) Май" => Month.May,
            "6) Июнь" => Month.June,
            "7) Июль" => Month.July,
            "8) Август" => Month.August,
            "9) Сентябрь" => Month.September,
            "10) Октябрь" => Month.October,
            "11) Ноябрь" => Month.November,
            "12) Декабрь" => Month.December,

            _ => throw new Exception("Не удалось преобразовать название месяца в тип перечисления.")
        };

        public static string GetDirPath(this Month month) => Path.Combine(App.CurrentDir, Path.Combine("Отчеты", month.GetName()));

        public static string GetSummaryDirPath(this Month month) => Path.Combine(month.GetDirPath(), "Сводный отчет");
    }
}
