using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System.Linq;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels
{
    internal class ReportFormerViewModel : ViewModelBase
    {

        public MainViewModel MainViewModel { get; }
        public IUserDialog UserDialog { get; }
        public ISummaryCompiler SummaryCompiler { get; }

        public ICommand CreateReportCommand { get; }
        private bool CanCreateReport(object? arg)
        {
            var isDirSelected = MainViewModel.DirectoriesViewModel.IsDirectorySelected;
            var isValidMetaData = MainViewModel.MetaDataViewModel.IsValidMetaData;

            return isDirSelected && isValidMetaData;
        }
        private void CreateReport(object? obj)
        {
            var metaData = MainViewModel.MetaDataViewModel.MetaData;
            var dirPath = MainViewModel.DirectoriesViewModel.SelectedItem.Path;

            var reportInfos = SummaryCompiler.GetReportInfos(dirPath);
            var incorrectReports = reportInfos.Where(reportInfo => !reportInfo.IsCorrect);
            if(incorrectReports.Any())
            {
                UserDialog.ShowIncorrectReports(incorrectReports.ToList());
            }
            else
            {
                var reports = reportInfos.Select(info => SummaryCompiler.ConvertToReport(info)).ToList();
                var summaryPath = SummaryCompiler.CompileSummary(reports, metaData);
                UserDialog.ShowSuccessResult(summaryPath);
            }
        }

        public ReportFormerViewModel(MainViewModel mainViewModel, IUserDialog userDialog, ISummaryCompiler summaryCompiler)
        {
            MainViewModel = mainViewModel;
            UserDialog = userDialog;
            SummaryCompiler = summaryCompiler;

            CreateReportCommand = new RelayCommand(CreateReport, CanCreateReport);
        }
    }
}
