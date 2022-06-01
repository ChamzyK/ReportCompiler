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
            return sheet.Cells[2, 12] != null ? sheet.Cells[2, 12].Value.ToString() : "";
        }
        public static string ReadRepliesReceviedYes(ExcelWorksheet sheet)
        {
            return sheet.Cells[7, 9] != null ? sheet.Cells[7, 9].Value.ToString() : "";
        }
        public static string ReadRepliesReceviedNo(ExcelWorksheet sheet)
        {
            return sheet.Cells[6, 9] != null ? sheet.Cells[6, 9].Value.ToString() : "";
        }
        public static string ReadRequestsSent(ExcelWorksheet sheet)
        {
            return sheet.Cells[5, 9] != null ? sheet.Cells[5, 9].Value.ToString() : "";
        }
        public static string ReadAgreements(ExcelWorksheet sheet)
        {
            return sheet.Cells[4, 9] != null ? sheet.Cells[4, 9].Value.ToString() : "";
        }
        public static string ReadIssuedOrders(ExcelWorksheet sheet)
        {
            return sheet.Cells[3, 9] != null ? sheet.Cells[3, 9].Value.ToString() : "";
        }
        public static string ReadDeclaration(ExcelWorksheet sheet)
        {
            return sheet.Cells[2, 9] != null ? sheet.Cells[2, 9].Value.ToString() : "";
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
