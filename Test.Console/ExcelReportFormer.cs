using OfficeOpenXml;

namespace Test.Console
{
    internal class ExcelReportFormer
    {
        public FileInfo TemplateFileInfo { get; }
        public FileInfo ReportFileInfo { get; set; }

        public ExcelReportFormer(FileInfo reportFileInfo, FileInfo templateFileInfo)
        {
            TemplateFileInfo = templateFileInfo;
            ReportFileInfo = reportFileInfo;
        }

        public string GenerateReport(string path)
        {
            var componentReportsList = ReadReportFiles(path);
            WriteData(componentReportsList);
            return ReportFileInfo.FullName;
        }

        private void WriteData(List<Report> componentReportsList)
        {
            var writePackage = new ExcelPackage(ReportFileInfo, TemplateFileInfo);

            var sheet = writePackage.Workbook.Worksheets["Сводный отчет"];

            if (IsOneMonth(componentReportsList))
            {
                sheet.Cells["P4"].Value = componentReportsList[0].Month.GetName();

                for (int i = 0; i < componentReportsList.Count; i++)
                {
                    var row = i + 9;
                    var component = componentReportsList[i];
                    sheet.Cells[$"A{row}"].Value = i;
                    sheet.Cells[$"B{row}"].Value = component.District;

                    sheet.Cells[$"D{row}"].Value = component.Declarations.Sum();
                    string text = ReadMonths(component);
                    sheet.Cells[$"F{row}"].Value = text;

                    sheet.Cells[$"H{row}"].Value = component.IssuedOrders.Sum();
                    sheet.Cells[$"K{row}"].Value = component.Agreements.Sum();
                    sheet.Cells[$"N{row}"].Value = component.RequestsSent.Sum();
                    sheet.Cells[$"Q{row}"].Value = component.RepliesReceviedNo.Sum();
                    sheet.Cells[$"S{row}"].Value = component.RepliesReceviedYes.Sum();
                    sheet.Cells[$"U{row}"].Value = component.Inspections.Sum();
                }
            }
            writePackage.SaveAs(ReportFileInfo);
        }

        private static string ReadMonths(Report component)
        {
            var text = string.Empty;
            var declarations = component.Declarations;
            for (int i = 0; i < declarations.Length; i++)
            {
                if (declarations[i] != 0)
                {
                    var monthName = ((Month)i).GetName();
                    var count = declarations[i];

                    text += $"{monthName}-{count}, ";
                }
            }

            return text;
        } //TODO: переделать метод

        private static bool IsOneMonth(List<Report> componentReportsList)
        {
            var month = componentReportsList[0].Month;
            foreach (var component in componentReportsList)
            {
                if (component.Month != month)
                {
                    return false;
                }
            }
            return true;
        }

        private static List<Report> ReadReportFiles(string path)
        {
            var componentReportsList = new List<Report>();

            var excelFilesPath = GetExcelFilesPath(path);

            foreach (var filePath in excelFilesPath)
            {
                if (TryReadFile(filePath, out Report readedReportData))
                {
                    componentReportsList.Add(readedReportData);
                }
            }

            return componentReportsList;
        }

        private static string[] GetExcelFilesPath(string path)
        {
            return Directory.GetFiles(path, "*.xlsx");
        }

        private static bool TryReadFile(string path, out Report componentReportData)
        {
            using var readPackage = new ExcelPackage(new FileInfo(path));

            var sheet = readPackage.Workbook.Worksheets["Услуга сопровождения"];
            if (sheet != null)
            {
                componentReportData = ReadReport(sheet);
                return true;
            }

            componentReportData = new Report();
            return false;
        }

        private static Report ReadReport(ExcelWorksheet sheet)
        {
            var componentReportData = new Report
            {
                Month = ConvertToMonth(sheet.Cells["P4"].GetValue<string>()),
                District = sheet.Cells["H4"].GetValue<string>(),

                Declarations = ConvertToIntArray(sheet.Cells["B11:M11"]),
                IssuedOrders = ConvertToIntArray(sheet.Cells["B15:M15"]),
                Agreements = ConvertToIntArray(sheet.Cells["B19:M19"]),
                RequestsSent = ConvertToIntArray(sheet.Cells["B23:M23"]),
                RepliesReceviedNo = ConvertToIntArray(sheet.Cells["B27:M27"]),
                RepliesReceviedYes = ConvertToIntArray(sheet.Cells["B28:M28"]),
                Inspections = ConvertToIntArray(sheet.Cells["B32:M32"]),

                Date = DateTime.TryParse(sheet.Cells["K38"].GetValue<string>(), out DateTime dateTime) ? dateTime : default,

                Director = sheet.Cells["J35"].GetValue<string>(),
                Executor = sheet.Cells["G42"].GetValue<string>(),
                Number = sheet.Cells["G43"].GetValue<string>()
            };

            return componentReportData;
        }

        private static Month ConvertToMonth(string monthName)
        {
            if (monthName == "Январь")
            {
                return Month.January;
            }
            else if (monthName == "Февраль")
            {
                return Month.February;
            }
            else if (monthName == "Март")
            {
                return Month.March;
            }
            else if (monthName == "Апрель")
            {
                return Month.April;
            }
            else if (monthName == "Май")
            {
                return Month.May;
            }
            else if (monthName == "Июнь")
            {
                return Month.June;
            }
            else if (monthName == "Июль")
            {
                return Month.July;
            }
            else if (monthName == "Август")
            {
                return Month.August;
            }
            else if (monthName == "Сентябрь")
            {
                return Month.September;
            }
            else if (monthName == "Октябрь")
            {
                return Month.October;
            }
            else if (monthName == "Ноябрь")
            {
                return Month.November;
            }
            else if (monthName == "Декабрь")
            {
                return Month.December;
            }

            throw new Exception("Некорректное название месяца.");
        }

        private static int[] ConvertToIntArray(ExcelRange excelRange)
        {
            var list = new List<int>(); //TODO: переопределить  array

            foreach (var item in excelRange)
            {
                list.Add(Convert.ToInt32(item.Value)); //TODO: unboxing
            }

            return list.ToArray();
        }
    }
}
