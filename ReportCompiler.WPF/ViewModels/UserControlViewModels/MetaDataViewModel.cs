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

        public string[] Months { get; init; }

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
        public string? MainReportName
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
