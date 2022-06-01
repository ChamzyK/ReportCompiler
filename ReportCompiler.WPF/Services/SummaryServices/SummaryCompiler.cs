using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            if (string.IsNullOrWhiteSpace(ReportReader.GetDistrict(excelPackage)))
            {
                isCorrect = false;
                comment += "Имя файла.\n";
            }

            for (int i = 0; i < 6; i++)
            {
                if (sheet.Cells[10, 2 + i].Value == null)
                {
                    isCorrect = false;
                    comment += $"Ячейка: {(char)('B' + i)}9.\n";
                    continue;
                }
                var ab = sheet.Cells[10, 2 + i].Value.ToString();
                var a = !sheet.Cells[10, 2 + i].Value.ToString().Any(symbol => char.IsDigit(symbol));
                if (a)
                {
                    isCorrect = false;
                    comment += $"Ячейка: {(char)('B' + i)}9.\n";
                }
            }

            return new ReportInfo(fileInfo.Name, isCorrect, comment);
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
            .Where(path => !path.Contains("~$") && path.Contains("xlsx"))
            .ToList()
        ;

        public Report ConvertToReport(ReportInfo reportInfo)
        {
            var fileInfo = new FileInfo(reportInfo.Path);
            using var readPackage = new ExcelPackage(fileInfo);
            var sheet = readPackage.Workbook.Worksheets[0];

            var data = ReportReader.ReadNotEmptyCells(sheet);

            return new Report
            {
                District = ReportReader.GetDistrict(readPackage),
                Declarations = ReportReader.ReadDeclaration(sheet),
                IssuedOrders = ReportReader.ReadIssuedOrders(sheet),
                Agreements = ReportReader.ReadAgreements(sheet),
                RequestsSent = ReportReader.ReadRequestsSent(sheet),
                RepliesReceviedNo = ReportReader.ReadRepliesReceviedNo(sheet),
                RepliesReceviedYes = ReportReader.ReadRepliesReceviedYes(sheet),
                Inspections = ReportReader.ReadInspections(sheet)
            };
        }

        public string CompileSummary(List<Report> reports, MetaData metaData)
        {
            var summary = new FileInfo($"{metaData.Name}.xlsx");
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
