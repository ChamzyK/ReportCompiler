using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Dialogues;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ReportCompiler.WPF.Services
{
    internal class UserDialogService : IUserDialog
    {
        public void ShowMessage(string header, string message)
        {
            var infoViewModel = App.Services.GetRequiredService<InfoViewModel>();
            infoViewModel.Header = header;
            infoViewModel.Information = message;
            DialogHost.Show(infoViewModel);
        }

        public void ShowSuccessResult(string filePath)
        {
            var successResultViewModel = App.Services.GetRequiredService<SuccessResultViewModel>();
            successResultViewModel.SummaryPath = filePath;
            DialogHost.Show(successResultViewModel);
        }

        public void ShowIncorrectReports(List<ReportInfo> reportInfos)
        {
            var incorrectReportsViewModel = App.Services.GetRequiredService<IncorrectReportsViewModel>();
            incorrectReportsViewModel.IncorrectReports = new ObservableCollection<ReportInfo>(reportInfos);
            DialogHost.Show(incorrectReportsViewModel);
        }
    }
}
