using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReportCompiler.WPF.Services.SummaryServices
{
    internal static class ReportReader
    {
        public static string GetDistrict(ExcelPackage readPackage)
        {
            var fileName = readPackage.File.Name;

            return ConvertToFullName(fileName.Replace(fileName.Contains(".xlsx") ? ".xlsx" : ".xls", "").Split('_')[0]);
        }
        public static string ConvertToFullName(string district) => district switch
        {
            "Барабинск" => "г. Барабинск",
            "Бердск" => "г. Бердск",
            "Баган" => "Баганский",
            "Кыштовка" => "Кыштовский",
            "Маслянино" => "Маслянинский",
            "Мошково" => "Мошковский",
            "Новосибирск" => "г. Новосибирск",
            "Новосибирский район" => "Новосибирский",
            "Ордынск" => "Ордынский",
            "Ордынка" => "Ордынский",
            "Северный" => "Северный",
            "Северное" => "Северный",
            "Сузун" => "Сузунский",
            "Татарск" => "г. Татарск",
            "Тогучин" => "Тогучинский",
            "Убинка" => "Убинский",
            "Усть-Тарка" => "Усть-Таркский",
            "Усть Тарка" => "Усть-Таркский",
            "Чаны" => "Чановский",
            "Черепаново" => "Черепановский",
            "Чистоозерный" => "Чистоозерный",
            "Чистоозерное" => "Чистоозерный",
            "Чистоозерка" => "Чистоозерный",
            "Чулым" => "Чулымский",
            "Болотное" => "Болотнинский",
            "Венгерово" => "Венгеровский",
            "Довольное" => "Доволенский",
            "Здвинск" => "Здвинский",
            "Искитим" => "г. Искитим",
            "Карасук" => "Карасукский",
            "Каргат" => "Каргатский",
            "Колывань" => "Колыванский",
            "Коченево" => "Коченевский",
            "Кочки" => "Кочковский",
            "Краснозерка" => "Краснозерский",
            "Куйбышев" => "г. Куйбышев",
            "Купино" => "Купинский",
            _ => string.Empty
        };

        public static List<ExcelRangeBase> ReadNotEmptyCells(ExcelWorksheet sheet) => sheet.Cells
            .Where(cell => HasContent(cell))
            .ToList()
        ;
        private static bool HasContent(ExcelRangeBase cell)
        {
            return cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString());
        }

        public static (TemplateType template, ExcelRangeBase cell) GetTemplate(List<ExcelRangeBase> data)
        {
            var firstTemplateCell = data.FirstOrDefault(cell => cell.Value.ToString().ToLower().Replace(" ", "").Replace("   ", "") == "кол-во(месяц)");

            var secondTemplateCell = data.FirstOrDefault(cell => cell.Value.ToString().ToLower().Replace(" ", "").Replace("   ", "") == "х");

            var thirdTemplateCellNo = data.FirstOrDefault(cell => cell.Value.ToString().ToLower().Contains("(нет)"));
            var thirdTemplateCellYes = data.FirstOrDefault(cell => cell.Value.ToString().ToLower().Contains("(да)"));

            var forthTemplateCell = data.FirstOrDefault(cell => cell.Value.ToString().ToLower().Contains("получено"));

            var fifthTemplateCell = data.FirstOrDefault(cell => cell.Value.ToString().ToLower().Contains("поступило"));


            if (firstTemplateCell != null) return (TemplateType.WithMonths, firstTemplateCell);
            else if (secondTemplateCell != null) return (TemplateType.WithX, secondTemplateCell);
            else if (thirdTemplateCellNo != null && thirdTemplateCellYes != null) return (TemplateType.WithNoYes, thirdTemplateCellNo);
            else if (forthTemplateCell != null && !forthTemplateCell.Merge) return (TemplateType.Empty, forthTemplateCell);
            else if (fifthTemplateCell != null) return (TemplateType.WithoutX, fifthTemplateCell);

            throw new System.Exception("Не удалось определить шаблон");
        }

        internal static string GetInspections(ExcelRange cell)
        {
            var value = cell.Value;
            if (value is string @string && !string.IsNullOrWhiteSpace(@string))
            {
                var matches = Regex.Matches(@string, @"(\d+)");
                var lastMatch = matches.LastOrDefault();
                if (lastMatch != null && !string.IsNullOrWhiteSpace(lastMatch.Value) && int.TryParse(lastMatch.Value, out int result))
                {
                    if(result > 2000)
                    {
                        return "0";
                    }
                    return lastMatch.Value;
                }
            }
            return "ОШИБКА ЧТЕНИЯ";
        }
    }
}
