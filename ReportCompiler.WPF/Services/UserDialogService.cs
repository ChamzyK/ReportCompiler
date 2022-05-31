using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Dialogues;

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

        public void ShowSuccessResult()
        {
            var successResultViewModel = App.Services.GetRequiredService<SuccessResultViewModel>();
            DialogHost.Show(successResultViewModel);
        }

        public void ShowIncorrectReports()
        {
            var incorrectReportsViewModel = App.Services.GetRequiredService<IncorrectReportsViewModel>();
            DialogHost.Show(incorrectReportsViewModel);
        }
    }
}
