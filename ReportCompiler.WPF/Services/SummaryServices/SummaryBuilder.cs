using OfficeOpenXml;
using OfficeOpenXml.Style;
using ReportCompiler.WPF.Models.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
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


                    FillNumberCell(sheet.Cells[$"C{row}"], report.Declarations);
                    FillNumberCell(sheet.Cells[$"E{row}"], report.IssuedOrders);
                    FillNumberCell(sheet.Cells[$"F{row}"], report.Agreements);
                    FillNumberCell(sheet.Cells[$"G{row}"], report.RequestsSent);
                    FillNumberCell(sheet.Cells[$"H{row}"], report.RepliesReceviedNo);
                    FillNumberCell(sheet.Cells[$"I{row}"], report.RepliesReceviedYes);
                    FillNumberCell(sheet.Cells[$"J{row}"], report.Inspections);

                    SetCellBackground(sheet.Cells[$"C{row}"]);
                    SetCellBackground(sheet.Cells[$"E{row}"]);
                    SetCellBackground(sheet.Cells[$"F{row}"]);
                    SetCellBackground(sheet.Cells[$"G{row}"]);
                    SetCellBackground(sheet.Cells[$"H{row}"]);
                    SetCellBackground(sheet.Cells[$"I{row}"]);
                    SetCellBackground(sheet.Cells[$"J{row}"]);

                    if (sheet.Cells[$"C{row}"].Value != null && (int)sheet.Cells[$"C{row}"].Value != 0)
                    {
                        FillCell(sheet.Cells[$"D{row}"], report.Declarations);
                    }

                    sheet.Row(row).CustomHeight = false;
                }
            }
        }

        private static void SetCellBackground(ExcelRange cell)
        {
            if (cell.Value != null && (int)cell.Value != 0)
            {
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            }
        }


        private static void FillCell(ExcelRange cell, string value)
        {
            var noSpaces = Regex.Replace(value, @"(\s+)", " ");
            cell.Value = noSpaces;
        }

        private static void FillNumberCell(ExcelRange cell, string value)
        {
            var allSum = GetSum(value);

            var withoutBrackets = Regex.Replace(value, @"\((.*?)\)", "");
            var outSum = GetSum(withoutBrackets);

            var insideBrackets = Regex.Match(value, @"\((.*?)\)").Value;
            var inSum = GetSum(insideBrackets);


            if (outSum == allSum || allSum == outSum * 2)
            {
                cell.Style.Numberformat.Format = "0";
                cell.Value = outSum;
            }
            else if (inSum == allSum)
            {
                cell.Style.Numberformat.Format = "0";
                cell.Value = inSum;
            }
            else
            {
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.Red);
            }
        }

        private static int GetSum(string value)
        {
            var regex = new Regex(@"(\d*)");
            var sum = 0;
            foreach (Match match in regex.Matches(value))
            {
                if (int.TryParse(match.Groups[1].Value, out int temp))
                {
                    sum += temp;
                }
            }

            return sum;
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
            if (prevMonthFile != null)
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
            else
            {
                currentSummarySheet.Cells["C41"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentSummarySheet.Cells["C41"].Style.Fill.BackgroundColor.SetColor(Color.Red);

                currentSummarySheet.Cells["E41"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentSummarySheet.Cells["C41"].Style.Fill.BackgroundColor.SetColor(Color.Red);

                currentSummarySheet.Cells["F41"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentSummarySheet.Cells["C41"].Style.Fill.BackgroundColor.SetColor(Color.Red);

                currentSummarySheet.Cells["G41"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentSummarySheet.Cells["C41"].Style.Fill.BackgroundColor.SetColor(Color.Red);

                currentSummarySheet.Cells["H41"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentSummarySheet.Cells["C41"].Style.Fill.BackgroundColor.SetColor(Color.Red);

                currentSummarySheet.Cells["I41"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentSummarySheet.Cells["C41"].Style.Fill.BackgroundColor.SetColor(Color.Red);

                currentSummarySheet.Cells["J41"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentSummarySheet.Cells["C41"].Style.Fill.BackgroundColor.SetColor(Color.Red);

            }
        }

        public void FillHeaderInfo(ExcelWorksheet sheet)
        {
            var headRichText = sheet.Cells["A1"].RichText.Add("Информация о предоставлении ");
            headRichText.Bold = false;
            var boldRichText = sheet.Cells["A1"].RichText.Add("государственной услуги сопровождения");
            boldRichText.Bold = true;
            var tailRichText = sheet.Cells["A1"].RichText.Add($"\nпри содействии занятости инвалидов ГКУ ЦЗН НСО в {Year} году");
            tailRichText.Bold = false;


            sheet.Cells["G2"].Value = $"Ежемесячно за {Month.Split(' ')[1]}" +
                "\nПредоставляется: до 5 числа месяца, следующего за отчетным";


            var bold = sheet.Cells["G4"].RichText.Add($"{CompileDate:d}"); ;
            bold.Bold = true;

            var normal = sheet.Cells["G4"].RichText.Add(" (нарастающим итогом)");
            normal.Bold = false;
        }
    }
}
