using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReportCompiler.WPF.Services
{
    internal class ExcelReportFormer : IReportFormer
    {
        public IUserDialog UserDialog { get; init; }

        public ExcelReportFormer(IUserDialog userDialog)
        {
            UserDialog = userDialog;
        }

        public bool CheckData(string path)
        {
            return false;
        }

        public void CreateMainReport(string path, MetaData metaData)
        {
            var excelFiles = GetExcelFiles(path);

            var reports = GetReports(excelFiles);

            CreateMainReport(reports, path);
        }

        private static List<FileInfo> GetExcelFiles(string path) => Directory.EnumerateFiles(Path
        .Combine(Environment.CurrentDirectory, path)) //для этого проекта созданы специальные тестовые файлы в директории ReportsDir
        .Where(path => !path.Contains("~$") && path.Contains("xlsx")) //выбор всех файлов в формате .xlsx кроме временных (~$)
        .Select(path => new FileInfo(path))
        .ToList()
        ;

        private List<Report> GetReports(List<FileInfo> excelFiles)
        {
            var result = new List<Report>();
            foreach (var fileInfo in excelFiles)
            {
                result.Add(ConvertToReport(fileInfo));
            }
            return result;
        }

        private static Report ConvertToReport(FileInfo fileInfo) //TODO: без проверок
        {
            using var readPackage = new ExcelPackage(fileInfo); //todo: нужно как-то определять что данный файл является файлом-отчетом
            var district = GetDistrict(readPackage);
            var sheet = readPackage.Workbook.Worksheets[0]; //todo: точно ли первый лист всегда является листом с данными?
            var data = GetNotEmptyCells(sheet);

            return new Report
            {
                Month = GetMonth(data),
                District = district,
                TableData = GetTable(data)
            };
        }


        #region Заполнение таблицы
        private static Table GetTable(List<ExcelRangeBase> data)
        {
            var table = new Table
            {
                Declarations = GetDeclarationInTable(data),
                IssuedOrders = GetIssuedOrdersInTable(data),
                Agreements = GetAgreementsInTable(data),
                RequestsSent = GetRequestsSentInTable(data),
                RepliesReceviedNo = GetRepliesReceviedNoInTable(data),
                RepliesReceviedYes = GetRepliesReceviedYesInTable(data),
                Inspections = GetInspectionsInTable(data)
            };

            return table;
        }

        private static string GetInspectionsInTable(List<ExcelRangeBase> data)
        {
            return "Количество проведенных проверок соблюдения...";
        }
        private static string GetRepliesReceviedYesInTable(List<ExcelRangeBase> data)
        {
            return "Получено ответов из учреждений (да)...";
        }
        private static string GetRepliesReceviedNoInTable(List<ExcelRangeBase> data)
        {
            return "Получено ответов из учреждений (нет)...";
        }
        private static string GetRequestsSentInTable(List<ExcelRangeBase> data)
        {
            return "Направлено запросов в учреждения...";
        }
        private static string GetAgreementsInTable(List<ExcelRangeBase> data)
        {
            return "Заключено соглашений...";
        }
        private static string GetIssuedOrdersInTable(List<ExcelRangeBase> data)
        {
            return "Издано приказов ...";
        }
        private static string GetDeclarationInTable(List<ExcelRangeBase> data)
        {
            return "Поступило заявлений о предоставлении...";
        } 
        #endregion

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
        private static string ConvertToFullName(string district) => district switch
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
            _ => district
        };

        static List<ExcelRangeBase> GetNotEmptyCells(ExcelWorksheet sheet) => sheet.Cells
        .Where(cell => HasContent(cell))
        .ToList()
        ;
        static bool HasContent(ExcelRangeBase cell) =>
            cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString());


        private static string GetMonth(List<ExcelRangeBase> data)
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
        private static string? DefineMonth(string content)
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

        private static void CreateMainReport(List<Report> reports, string path)
        {
            var mainReportTemplate = new FileInfo("main_report_template.xltx");

            var year = "2022";
            var month = "декабрь";
            var today = DateTime.Now; //данные которые надо будет доставать откуда-то

            var mainReport = new FileInfo($"Свод_отчет_{month} {year} усл_Сопровождения_сверка со Штайгер.xlsx");

            using var writePackage = new ExcelPackage(mainReport, mainReportTemplate);

            var sheet = writePackage.Workbook.Worksheets["Сводный отчет"];

            FillGeneralInfo(year, month, today, sheet);

            FillDistrictInformation(reports, sheet);

            writePackage.SaveAs(mainReport);
        }

        private static void FillDistrictInformation(List<Report> reports, ExcelWorksheet sheet)
        {
            var range = sheet.Cells["B7:B38"];

            foreach (var cell in range)
            {
                var report = reports.FirstOrDefault(report => report.District.Equals(cell.Value.ToString()));
                if (report != null)
                {
                    var row = cell.Start.Row;
                    sheet.Cells[$"D{row}"].Value = report.TableData.Declarations;
                    sheet.Cells[$"E{row}"].Value = report.TableData.IssuedOrders;
                    sheet.Cells[$"F{row}"].Value = report.TableData.Agreements;
                    sheet.Cells[$"G{row}"].Value = report.TableData.RequestsSent;
                    sheet.Cells[$"H{row}"].Value = report.TableData.RepliesReceviedNo;
                    sheet.Cells[$"I{row}"].Value = report.TableData.RepliesReceviedYes;
                    sheet.Cells[$"J{row}"].Value = report.TableData.Inspections;
                }
            }
        }

        private static void FillGeneralInfo(string year, string month, DateTime today, ExcelWorksheet sheet)
        {
            FillHeaderGeneral(year, month, today, sheet);
            FillBottomGeneral(year, today, sheet);
        }
        private static void FillBottomGeneral(string year, DateTime today, ExcelWorksheet sheet)
        {
            sheet.Cells["A40"].Value = $"за {year} год";
            sheet.Cells["A40"].Style.Font.Bold = true;

            sheet.Cells["A41"].Value = $"{today:Y}";
            sheet.Cells["A41"].Style.Font.Bold = true;

            sheet.Cells["J40"].Value = $"на {today:d} проведено проверок";
            sheet.Cells["J40"].Style.Font.Bold = true;

            sheet.Cells["J42"].Value = $"на {today:d} из МСЭ не пришли ответы  на запросы ЦЗН";
            sheet.Cells["J42"].Style.Font.Bold = true;
        }
        private static void FillHeaderGeneral(string year, string month, DateTime today, ExcelWorksheet sheet)
        {
            var headRichText = sheet.Cells["A1"].RichText.Add("Информация о предоставлении ");
            headRichText.Bold = false;
            var boldRichText = sheet.Cells["A1"].RichText.Add("государственной услуги сопровождения");
            boldRichText.Bold = true;
            var tailRichText = sheet.Cells["A1"].RichText.Add($"\nпри содействии занятости инвалидов ГКУ ЦЗН НСО в {year} году");
            tailRichText.Bold = false;


            sheet.Cells["G2"].Value = $"Ежемесячно за {month}" +
                "\nПредоставляется: до 5 числа месяца, следующего за отчетным";


            var bold = sheet.Cells["G4"].RichText.Add($"{today:d}"); ;
            bold.Bold = true;

            var normal = sheet.Cells["G4"].RichText.Add(" (нарастающим итогом)");
            normal.Bold = false;
        }
    }
}
