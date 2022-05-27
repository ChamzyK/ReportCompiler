using OfficeOpenXml;
using Test.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var reports = GetFiles().CastToReport(); //Получили файлы report

        //Отображение полученных файлов
        foreach (var item in reports)
        {
            Console.WriteLine($"{item.District}, {item.Month}\n");
        }
    }

    static List<FileInfo> GetFiles() => Directory.EnumerateFiles(Path
        .Combine(Environment.CurrentDirectory, "ReportsDir")) //для этого проекта созданы специальные тестовые файлы в директории ReportsDir
        .Where(path => !path.Contains("~$") && path.Contains("xlsx")) //выбор всех файлов в формате .xlsx кроме временных (~$)
        .Select(path => new FileInfo(path))
        .ToList()
        ;

    static List<Report> CastToReport(this List<FileInfo> excelFiles) => excelFiles
        .Select(file => ConvertToReport(file))
        .ToList()
        ;

    static List<ExcelRangeBase> GetNotEmptyCells(ExcelWorksheet sheet) => sheet.Cells
        .Where(cell => HasContent(cell))
        .ToList()
        ;

    static bool HasContent(ExcelRangeBase cell)
    {
        return cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString());
    }

    static Report ConvertToReport(FileInfo fileInfo)
    {
        using var readPackage = new ExcelPackage(fileInfo); //todo: нужно как-то определять что данный файл является файлом-отчетом
        var district = GetDistrict(readPackage);
        var sheet = readPackage.Workbook.Worksheets[0]; //todo: точно ли первый лист всегда является листом с данными?
        var data = GetNotEmptyCells(sheet);

        return new Report
        {
            Month = GetMonth(data),
            District = district//Пока только определение месяца составления отчета и название района (города)
        };
    }

    private static string GetDistrict(ExcelPackage readPackage)
    {
        var fileName = readPackage.File.Name;
        if (!fileName.Contains('_'))
        {
            return "(Файл: " + fileName + ")";
        }
        else if (fileName.Split('_')[0] != "свод")
        {
            return ConvertToFullName(fileName.Split('_')[0]);
        }
        return "Не удалось определить название района (города)";
    }

    static string GetMonth(List<ExcelRangeBase> data)
    {
        foreach (var cell in data)
        {
            var value = cell.Value.ToString();
            if (value.Contains("ГКУ"))
            {
                var month = DefineMonth(value);
                if (month != null)
                {
                    return month;
                }
                else
                {
                    break;
                }
            }
        }
        return "месяц отсутствует в названии";
    }

    static string? DefineMonth(string content)
    {
        if (content.Contains("Январь", StringComparison.OrdinalIgnoreCase)) return "Январь";
        else if (content.Contains("Февраль", StringComparison.OrdinalIgnoreCase)) return "Февраль";
        else if (content.Contains("Март", StringComparison.OrdinalIgnoreCase)) return "Март";
        else if (content.Contains("Апрель", StringComparison.OrdinalIgnoreCase)) return "Апрель";
        else if (content.Contains("Май", StringComparison.OrdinalIgnoreCase)) return "Май";
        else if (content.Contains("Июнь", StringComparison.OrdinalIgnoreCase)) return "Июнь";
        else if (content.Contains("Июль", StringComparison.OrdinalIgnoreCase)) return "Июль";
        else if (content.Contains("Август", StringComparison.OrdinalIgnoreCase)) return "Август";
        else if (content.Contains("Сентябрь", StringComparison.OrdinalIgnoreCase)) return "Сентябрь";
        else if (content.Contains("Ноябрь", StringComparison.OrdinalIgnoreCase)) return "Ноябрь";
        else if (content.Contains("Октябрь", StringComparison.OrdinalIgnoreCase)) return "Октябрь";
        else if (content.Contains("Декабрь", StringComparison.OrdinalIgnoreCase)) return "Декабрь";
        return null;
    }

    static string ConvertToFullName(string district) => district switch
    {
        "Барабинск" => "г. Барабинск",
        "Бердск" => "г. Бердск",
        "Баган" => "Баганский",
        "Кыштовка" => "Кыштовский",
        "Маслянино" => "Маслянинский",
        "Мошково" => "Мошковский",
        "Новосибирск" => "г.Новосибирск",
        "Новосибирский районx" => "Новосибирский",
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
        _ => district
    };


}
