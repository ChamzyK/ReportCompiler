using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReportCompiler.WPF.Services.SummaryServices
{
    internal class SummaryBuilder
    {
        public List<Report> Reports { get; }
        public MetaData MetaData { get; }

        public string Year => ((DateTime)MetaData.CompileDate).Year.ToString();
        public DateTime CompileDate => ((DateTime)MetaData.CompileDate);
        public string Month => ((Month)MetaData.Month).GetName();

        public SummaryBuilder(List<Report> reports, MetaData metaData)
        {
            Reports = reports;
            MetaData = metaData;
        }

        public void FillDistrictInfo(ExcelWorksheet sheet)
        {
            var range = sheet.Cells["B7:B38"];

            foreach (var cell in range)
            {
                var report = Reports.FirstOrDefault(report => report.District.Equals(cell.Value.ToString()));
                if (report != null)
                {
                    var row = cell.Start.Row;


                    FillCell(sheet.Cells[$"C{row}"], report.Declarations);
                    FillCell(sheet.Cells[$"E{row}"], report.IssuedOrders);
                    FillCell(sheet.Cells[$"F{row}"], report.Agreements);
                    FillCell(sheet.Cells[$"G{row}"], report.RequestsSent);
                    FillCell(sheet.Cells[$"H{row}"], report.RepliesReceviedNo);
                    FillCell(sheet.Cells[$"I{row}"], report.RepliesReceviedYes);
                    FillCell(sheet.Cells[$"J{row}"], report.Inspections);


                    sheet.Row(row).CustomHeight = false; 
                }
            }
        }

        private void FillCell(ExcelRange cell, string value)
        {
            string pattern = @"(\d*)";
            var regex = new Regex(pattern);

            var sum = 0;
            foreach (Match match in regex.Matches(Regex.Replace(value, @"\s+", " ")))
            {
                if (int.TryParse(match.Groups[1].Value, out int temp))
                {
                    sum += temp;
                }
            }
            cell.Style.Numberformat.Format = "0";
            cell.Value = sum;
        }

        public void FillBottomInfo(ExcelWorksheet sheet)
        {
            sheet.Cells["A40"].Value = $"за {Year} год";
            sheet.Cells["A40"].Style.Font.Bold = true;

            sheet.Cells["A41"].Value = $"{CompileDate.AddMonths(-1):Y}";
            sheet.Cells["A41"].Style.Font.Bold = true;

            sheet.Cells["J40"].Value = $"на {CompileDate.AddMonths(-1):d} проведено проверок";
            sheet.Cells["J40"].Style.Font.Bold = true;

            sheet.Cells["J42"].Value = $"на {CompileDate.AddMonths(-1):d} из МСЭ не пришли ответы  на запросы ЦЗН";
            sheet.Cells["J42"].Style.Font.Bold = true;
        }

        public void FillPrevMonthInfo(ExcelWorksheet currentSummarySheet, FileInfo prevMonthFile)
        {
            using var prevSummarySheet = new ExcelPackage(prevMonthFile).Workbook.Worksheets[0];
            currentSummarySheet.Cells["C41"].Value = prevSummarySheet.Cells["C39"].Value;
            currentSummarySheet.Cells["E41"].Value = prevSummarySheet.Cells["E39"].Value;
            currentSummarySheet.Cells["F41"].Value = prevSummarySheet.Cells["F39"].Value;
            currentSummarySheet.Cells["G41"].Value = prevSummarySheet.Cells["G39"].Value;
            currentSummarySheet.Cells["H41"].Value = prevSummarySheet.Cells["H39"].Value;
            currentSummarySheet.Cells["I41"].Value = prevSummarySheet.Cells["I39"].Value;
            currentSummarySheet.Cells["J41"].Value = prevSummarySheet.Cells["J39"].Value;
        }

        public void FillHeaderInfo(ExcelWorksheet sheet)
        {
            var headRichText = sheet.Cells["A1"].RichText.Add("Информация о предоставлении ");
            headRichText.Bold = false;
            var boldRichText = sheet.Cells["A1"].RichText.Add("государственной услуги сопровождения");
            boldRichText.Bold = true;
            var tailRichText = sheet.Cells["A1"].RichText.Add($"\nпри содействии занятости инвалидов ГКУ ЦЗН НСО в {Year} году");
            tailRichText.Bold = false;


            sheet.Cells["G2"].Value = $"Ежемесячно за {Month}" +
                "\nПредоставляется: до 5 числа месяца, следующего за отчетным";


            var bold = sheet.Cells["G4"].RichText.Add($"{CompileDate:d}"); ;
            bold.Bold = true;

            var normal = sheet.Cells["G4"].RichText.Add(" (нарастающим итогом)");
            normal.Bold = false;
        }
    }
}
