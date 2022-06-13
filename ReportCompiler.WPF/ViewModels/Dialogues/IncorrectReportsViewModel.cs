using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels.Dialogues
{
    internal class IncorrectReportsViewModel : ViewModelBase
    {
        public IFile FileService { get; }
        private ObservableCollection<ReportInfo>? incorrectReports;
        public ObservableCollection<ReportInfo>? IncorrectReports
        {
            get => incorrectReports; set
            {
                Set(ref incorrectReports, value);
            }
        }

        public string? DirectoryPath => (IncorrectReports != null && IncorrectReports.Any()) 
            ? IncorrectReports.First().Path 
            : null; 

        public ISummaryCompiler Compiler { get; }
        public ICommand CheckReportCommand { get; }
        private bool CanCheckReport(object? arg) => arg != null && arg is string name && IncorrectReports.Any(info => info.Name == name);
        private void CheckReport(object? obj)
        {
            var checkedReport = IncorrectReports.FirstOrDefault(reportInfo => reportInfo.Name == (string)obj);
            var checkResult = Compiler.GetReportInfo(checkedReport.Path);
            if (checkResult == null || checkResult.IsCorrect)
            {
                IncorrectReports.Remove(checkedReport);
            }
            else
            {
                var index = IncorrectReports.IndexOf(checkedReport);
                IncorrectReports.RemoveAt(index);
                IncorrectReports.Insert(index, checkResult);
            }
        }

        public IncorrectReportsViewModel(ISummaryCompiler compiler, IFile fileService)
        {
            Compiler = compiler;

            CheckReportCommand = new RelayCommand(CheckReport, CanCheckReport);
            FileService = fileService;
        }
    }
}
