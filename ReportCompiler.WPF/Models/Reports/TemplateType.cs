using OfficeOpenXml;
using System.Collections.Generic;

namespace ReportCompiler.WPF.Models.Reports
{
    /// <summary>
    /// Перечисление шаблонов заполнения отчетов
    /// </summary>
    internal enum TemplateType
    {
        WithMonths, //шаблон отчета в котором есть ячейка с: кол-во (месяц). Данные в таком шаблоне в нижней строке
        WithX, //шаблон отчета в котором есть ячейка с: x. Данные в таком шаблоне в верхней строке
        WithNoYes, //шаблон отчета в котором есть подряд идущие ячейки с: нет да. Данные в таком шаблоне в нижней строке
        WithoutX, //шаблон отчета в котором должна быть ячейка с: x. Данные в таком шаблоне в верхней строке
        Empty //шаблон отчета в котором нет никаких особых отметок
    }

    internal static class TemplateTypeExtentions
    {
        public static (int row, int column) GetStartCell(this TemplateType template, ExcelRangeBase cell)
        {
            int column;
            int row;
            if (template == TemplateType.WithMonths)
            {
                row = cell.End.Row + 1;
                column = cell.End.Column;
            }
            else if (template == TemplateType.WithX || template == TemplateType.WithoutX)
            {
                row = cell.End.Row - 1;
                column = cell.End.Column;
            }
            else if (template == TemplateType.WithNoYes)
            {
                row = cell.End.Row + 1;
                column = cell.End.Column - 4;
            }
            else if(template == TemplateType.Empty)
            {
                row = cell.End.Row + 1;
                column = cell.Start.Column - 4;
            }
            else
            {
                throw new System.Exception("Не удалось определить шаблон");
            }
            return (row, column);
        }
    }
}
