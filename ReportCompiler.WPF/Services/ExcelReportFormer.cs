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

        public void CretateMainReport(string path)
        {
            var excelFiles = GetExcelFiles(path);

            var reports = GetReports(excelFiles);

            CreateReport(reports, path);
        }

        private void CreateReport(List<Report> reports, string path)
        {
            throw new NotImplementedException();
        }

        private List<Report> GetReports(List<FileInfo> excelFiles)
        {
            var result = new List<Report>();
            foreach (var fileInfo in excelFiles)
            {
                result.Add(ConvertToReport(fileInfo));
            }
            return result;
        }
        private Report ConvertToReport(FileInfo fileInfo) //TODO: без проверок
        {
            using var readPackage = new ExcelPackage(fileInfo);

            var sheet = readPackage.Workbook.Worksheets[1];
            var data = GetNotEmptyCells(sheet);

            return new Report
            {
                Month = GetMonth(data),
                District = GetDistrict(data),
                TableData = GetTable(data)
            };
        }

        private string GetMonth(IEnumerable<ExcelRangeBase> data)
        {
            throw new NotImplementedException();
        }

        private string GetDistrict(IEnumerable<ExcelRangeBase> data)
        {
            throw new NotImplementedException();
        }

        private Table GetTable(IEnumerable<ExcelRangeBase> data)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<ExcelRangeBase> GetNotEmptyCells(ExcelWorksheet sheet) => sheet.Cells
            .Where(cell => cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()));

        private static List<FileInfo> GetExcelFiles(string path)
        {
            return new DirectoryInfo(path).EnumerateFiles("*.xlsx").ToList();//TODO: дореализовать возможность работы с файлами *.xls
        }
    }
}
