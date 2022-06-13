using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class MetaDataViewModel : ViewModelBase
    {
        private string? selectedMonth;
        private DateTime? compileDate;
        private string? mainReportName;
        private bool isAnotherDate;

        public List<string> Months { get; }

        public bool IsAnotherDate
        {
            get => isAnotherDate; set
            {
                Set(ref isAnotherDate, value);
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
                    (SelectedMonth != null);
            }
        }
        public MetaData MetaData => new()
        {
            Month = SelectedMonth.GetMonth(),
            Name = Name,
            CompileDate = IsAnotherDate ? CompileDate : DateTime.Now
        };

        public MetaDataViewModel()
        {
            Months = new List<string>();

            foreach (Month month in Enum.GetValues(typeof(Month)))
            {
                Months.Add(month.GetName());
            }
        }
    }
}
