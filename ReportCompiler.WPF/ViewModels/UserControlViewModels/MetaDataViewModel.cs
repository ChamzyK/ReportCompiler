using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.ViewModels.Base;
using System;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class MetaDataViewModel : ViewModelBase
    {
        private string? mainReportName;
        public string? Name
        {
            get => mainReportName; set
            {
                Set(ref mainReportName, value);
            }
        }

        public bool IsValidMetaData
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Name);
            }
        }
        public MetaData MetaData
        {
            get
            {
                var month = DateTime.Now.ToString("MMMM");
                month = char.ToUpper(month[0]).ToString() + month.Substring(1);
                var name = Name ?? $"Сводный отчет за {DateTime.Now:MMMM}";
                var date = DateTime.Now;
                return new MetaData(month, name, date);
            }
        }
    }
}
