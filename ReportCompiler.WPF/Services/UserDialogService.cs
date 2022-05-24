using MaterialDesignThemes.Wpf;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.Services
{
    internal class UserDialogService : IUserDialog
    {
        public InfoViewModel InfoViewModel { get; init; }
        public void ShowMessage(string header, string message)
        {
            InfoViewModel.Header = header;
            InfoViewModel.Information = message;
            DialogHost.Show(InfoViewModel);
        }

        public UserDialogService(InfoViewModel infoViewModel)
        {
            InfoViewModel = infoViewModel;
        }
    }
}
