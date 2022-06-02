using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;

namespace ReportCompiler.WPF.Services.SummaryServices
{
    internal static class ReportReader
    {
        public static string GetDistrict(ExcelPackage readPackage)
        {
            var fileName = readPackage.File.Name;

            return ConvertToFullName(fileName.Split('_')[0]);
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
            "Северный" => "Северный",
            "Сузун" => "Сузунский",
            "Татарск" => "г. Татарск",
            "Тогучин" => "Тогучинский",
            "Убинка" => "Убинский",
            "Усть-Тарка" => "Усть-Таркский",
            "Чаны" => "Чановский",
            "Черепаново" => "Черепановский",
            "Чистоозерный" => "Чистоозерный",
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

        public static string ReadInspections(ExcelWorksheet sheet)
        {
            return sheet.Cells[13, 2] != null ? sheet.Cells[13, 2].Value.ToString() : "";
        }
        public static string ReadRepliesReceviedYes(ExcelWorksheet sheet)
        {
            return sheet.Cells[10, 7] != null ? sheet.Cells[10, 7].Value.ToString() : "";
        }
        public static string ReadRepliesReceviedNo(ExcelWorksheet sheet)
        {
            return sheet.Cells[10, 6] != null ? sheet.Cells[10, 6].Value.ToString() : "";
        }
        public static string ReadRequestsSent(ExcelWorksheet sheet)
        {
            return sheet.Cells[10, 5] != null ? sheet.Cells[10, 5].Value.ToString() : "";
        }
        public static string ReadAgreements(ExcelWorksheet sheet)
        {
            return sheet.Cells[10, 4] != null ? sheet.Cells[10, 4].Value.ToString() : "";
        }
        public static string ReadIssuedOrders(ExcelWorksheet sheet)
        {
            return sheet.Cells[10, 3] != null ? sheet.Cells[10, 3].Value.ToString() : "";
        }
        public static string ReadDeclaration(ExcelWorksheet sheet)
        {
            return sheet.Cells[10, 2] != null ? sheet.Cells[10, 2].Value.ToString() : "";
        }

        public static List<ExcelRangeBase> ReadNotEmptyCells(ExcelWorksheet sheet) => sheet.Cells
            .Where(cell => HasContent(cell))
            .ToList()
        ;
        private static bool HasContent(ExcelRangeBase cell)
        {
            return cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString());
        }
    }
}
