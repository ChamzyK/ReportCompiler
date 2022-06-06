using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReportCompiler.WPF.Services.SummaryServices
{
    internal class SummaryBuilder
    {
        public List<Report> Reports { get; init; }
        public MetaData MetaData { get; init; }

        public string Year => MetaData.CompileDate.Year.ToString();
        public DateTime CompileDate => MetaData.CompileDate;
        public string Month => MetaData.Month;

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
                    sheet.Cells[$"D{row}"].Value = Regex.Replace(report.Declarations, @"\s+", " ");
                    sheet.Cells[$"E{row}"].Value = Regex.Replace(report.IssuedOrders, @"\s+", " ");
                    sheet.Cells[$"F{row}"].Value = Regex.Replace(report.Agreements, @"\s+", " ");
                    sheet.Cells[$"G{row}"].Value = Regex.Replace(report.RequestsSent, @"\s+", " ");
                    sheet.Cells[$"H{row}"].Value = Regex.Replace(report.RepliesReceviedNo, @"\s+", " ");
                    sheet.Cells[$"I{row}"].Value = Regex.Replace(report.RepliesReceviedYes, @"\s+", " ");
                    sheet.Cells[$"J{row}"].Value = Regex.Replace(report.Inspections, @"\s+", " ");

                    sheet.Cells[$"D{row}"].Style.WrapText = true;
                    sheet.Cells[$"E{row}"].Style.WrapText = true;
                    sheet.Cells[$"F{row}"].Style.WrapText = true;
                    sheet.Cells[$"G{row}"].Style.WrapText = true;
                    sheet.Cells[$"H{row}"].Style.WrapText = true;
                    sheet.Cells[$"I{row}"].Style.WrapText = true;
                    sheet.Cells[$"J{row}"].Style.WrapText = true;

                    sheet.Row(row).CustomHeight = false;
                }
            }
        }

        public void FillBottomInfo(ExcelWorksheet sheet)
        {
            sheet.Cells["A40"].Value = $"за {Year} год";
            sheet.Cells["A40"].Style.Font.Bold = true;

            sheet.Cells["A41"].Value = $"{CompileDate.AddMonths(-1):Y}";
            sheet.Cells["A41"].Style.Font.Bold = true;

            sheet.Cells["J40"].Value = $"на {CompileDate:d} проведено проверок";
            sheet.Cells["J40"].Style.Font.Bold = true;

            sheet.Cells["J42"].Value = $"на {CompileDate:d} из МСЭ не пришли ответы  на запросы ЦЗН";
            sheet.Cells["J42"].Style.Font.Bold = true;
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
