using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReportCompiler.WPF.Services.SummaryServices
{
    internal class SummaryCompiler : ISummaryCompiler
    {
        public ReportInfo GetReportInfo(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            using var excelPackage = new ExcelPackage(fileInfo);
            var sheet = excelPackage.Workbook.Worksheets[0];
            var data = ReportReader.ReadNotEmptyCells(sheet);

            var isCorrect = true;
            var comment = string.Empty;

            var (template, templateCell) = ReportReader.GetTemplate(data);
            var (row, column) = template.GetStartCell(templateCell);

            if (string.IsNullOrWhiteSpace(ReportReader.GetDistrict(excelPackage)))
            {
                isCorrect = false;
                comment += "В имени файла.\n";
            }

            var columnCount = template == TemplateType.Empty ? 5 : 6;

            for (int i = 0; i < columnCount; i++)
            {
                var cell = sheet.Cells[row, column + i];
                if (cell == null || cell.Value == null || !IsValidCell(cell.Value.ToString()))
                {
                    isCorrect = false;
                    comment += $"Ячейка {(char)('B' + i)}{row}.\n";
                }
            }


            return new ReportInfo
            {
                Name = fileInfo.Name,
                Path = fileInfo.FullName,
                IsCorrect = isCorrect,
                Comment = comment,
                Template = template,
                StartCellAddress = (row, column)
            };
        }

        private static bool IsValidCell(string value)
        {
            return value.Contains("нет") || value.Any(symbol => char.IsDigit(symbol));
        }


        public List<ReportInfo> GetReportInfos(string dirPath)
        {
            var excelFiles = GetExcelFiles(dirPath);
            var result = new List<ReportInfo>();
            foreach (var filePath in excelFiles)
            {
                result.Add(GetReportInfo(filePath));
            }
            return result;
        }
        private static List<string> GetExcelFiles(string path) => Directory.EnumerateFiles(path)
            .Where(path => !path.Contains("~$") && path.Contains(".xlsx"))
            .ToList()
        ;

        public Report ConvertToReport(ReportInfo reportInfo)
        {
            var fileInfo = new FileInfo(reportInfo.Path);
            using var readPackage = new ExcelPackage(fileInfo);
            var sheet = readPackage.Workbook.Worksheets[0];

            var (row, column) = reportInfo.StartCellAddress;

            var columnCount = reportInfo.Template == TemplateType.Empty ? 5 : 6;
            var array = new string[columnCount];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = sheet.Cells[row, column + i].Value.ToString();
            }

            return new Report
            {
                District = ReportReader.GetDistrict(readPackage),
                Declarations = Regex.Replace(array[0], @"\([^)]+\)", string.Empty),
                IssuedOrders = array[1],
                Agreements = array[2],
                RequestsSent = array[3],
                RepliesReceviedNo = reportInfo.Template == TemplateType.Empty && array[4].Contains("да") ? "0" : array[4],
                RepliesReceviedYes = reportInfo.Template == TemplateType.Empty ? (array[4].Contains("нет") ? "0" : array[4]) : array[5],
                Inspections = "NotImplemented"
            };
        }

        public string CompileSummary(List<Report> reports, MetaData metaData)
        {
            var reportsDirPath = Path.Combine(Environment.CurrentDirectory, "Отчеты");
            var monthDirPath = Path.Combine(reportsDirPath, metaData.Month);
            var summary = new FileInfo(Path.Combine(monthDirPath, $"{metaData.Name}.xlsx"));
            var summaryTemplate = new FileInfo("main_report_template.xltx");

            using var writePackage = new ExcelPackage(summary, summaryTemplate);

            var sheet = writePackage.Workbook.Worksheets["Сводный отчет"];

            var builder = new SummaryBuilder(reports, metaData);

            builder.FillHeaderInfo(sheet);
            builder.FillDistrictInfo(sheet);
            builder.FillBottomInfo(sheet);

            writePackage.SaveAs(summary);

            return summary.FullName;
        }
    }
}
