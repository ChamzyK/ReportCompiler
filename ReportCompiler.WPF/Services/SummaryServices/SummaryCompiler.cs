using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReportCompiler.WPF.Services.SummaryServices
{
    internal class SummaryCompiler : ISummaryCompiler
    {
        public IDirectory DirectoryService { get; }
        public IUserDialog DialogService { get; }
        public List<Report>? Reports { get; set; }

        public SummaryCompiler(IDirectory directoryService, IUserDialog dialogService)
        {
            DirectoryService = directoryService;
            DialogService = dialogService;
        }

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
            var excelFiles = DirectoryService.GetExcelFiles(dirPath);
            var result = new List<ReportInfo>();
            foreach (var filePath in excelFiles)
            {
                result.Add(GetReportInfo(filePath.Path));
            }
            return result;
        }

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

        public void CompileSummary(MetaData metaData)
        {
            var month = ((Month)metaData.Month);
            var reportInfos = GetReportInfos(month.GetDirPath());
            var incorrectReports = reportInfos.Where(reportInfo => !reportInfo.IsCorrect);

            if (incorrectReports.Any())
            {
                DialogService.ShowIncorrectReports(incorrectReports.ToList());
            }
            else
            {
                Reports = reportInfos.Select(info => ConvertToReport(info)).ToList();
                CreateSummary(metaData);
                DialogService.ShowSuccessResult(Path.Combine(month.GetSummaryDirPath(), $"{metaData.Name}.xlsx"));
            }
        }

        private void CreateSummary(MetaData metaData)
        {
            var month = (Month)metaData.Month;
            var summary = new FileInfo(Path.Combine(month.GetSummaryDirPath(), $"{metaData.Name}.xlsx"));
            var summaryTemplate = new FileInfo("main_report_template.xltx");

            using var writePackage = new ExcelPackage(summary, summaryTemplate);

            var sheet = writePackage.Workbook.Worksheets["Сводный отчет"];

            var builder = new SummaryBuilder(Reports, metaData);

            builder.FillHeaderInfo(sheet);
            builder.FillDistrictInfo(sheet);
            builder.FillBottomInfo(sheet);

            var prevMonthFile = GetPrevMonthSheet((Month)metaData.Month);
            if (prevMonthFile != null)
            {
                builder.FillPrevMonthInfo(sheet, prevMonthFile);
            }

            writePackage.SaveAs(summary);
        }

        private FileInfo? GetPrevMonthSheet(Month month)
        {
            var summaryDirPath = ((Month)(int)(month != Month.January ? month - 1 : Month.December)).GetSummaryDirPath();
            var prevSummaryFiles = DirectoryService.GetExcelFiles(summaryDirPath);

            if(prevSummaryFiles != null && prevSummaryFiles.Any())
            {
               return new FileInfo(prevSummaryFiles.FirstOrDefault().Path);
            }
            return null;
        }
    }
}
