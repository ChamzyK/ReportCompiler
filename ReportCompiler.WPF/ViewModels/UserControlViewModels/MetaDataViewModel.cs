using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.ViewModels.Base;
using System;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class MetaDataViewModel : ViewModelBase
    {
        private string? selectedMonth;
        private DateTime? compileDate;
        private string? mainReportName;
        private bool isAnotherMonth;
        private bool isAnotherDate;

        public string[] Months { get; }

        public bool IsAnotherDate
        {
            get => isAnotherDate; set
            {
                Set(ref isAnotherDate, value);
            }
        }
        public bool IsAnotherMonth
        {
            get => isAnotherMonth; set
            {
                isAnotherMonth = value;
                Set(ref isAnotherMonth, value);
            }
        }
        public string? Name
        {
            get => mainReportName; set
            {
                Set(ref mainReportName, value);
            }
        }
        public string? SelectedMonth
        {
            get => selectedMonth; set
            {
                Set(ref selectedMonth, value);
            }
        }
        public DateTime? CompileDate
        {
            get => compileDate; set
            {
                Set(ref compileDate, value);
            }
        }

        public bool IsValidMetaData
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Name) &&
                    (!IsAnotherDate || CompileDate != null) &&
                    (!IsAnotherMonth || SelectedMonth != null);
            }
        }
        public MetaData MetaData
        {
            get
            {
                var month = IsAnotherMonth && SelectedMonth != null ? SelectedMonth : DateTime.Now.ToString("MMMM");
                var name = Name ?? $"Сводный отчет за {DateTime.Now:MMMM}";
                var date = IsAnotherDate && CompileDate != null ? (DateTime)CompileDate : DateTime.Now;
                return new MetaData(month, name, date);
            }
        }

        public MetaDataViewModel()
        {
            Months = new string[]
            {
                "Январь",
                "Февраль",
                "Март",
                "Апрель",
                "Май",
                "Июнь",
                "Июль",
                "Август",
                "Сентябрь",
                "Октябрь",
                "Ноябрь",
                "Декабрь"
            };
        }
    }
}
