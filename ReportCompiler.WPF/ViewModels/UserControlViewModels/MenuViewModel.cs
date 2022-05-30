using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        public IUserDialog UserDialog { get; init; }

        public ICommand OpenReportTemplateCommand { get; init; }
        private void OpenReportTemplate(object? obj)
        {
            UserDialog.ShowMessage("<Открытие шаблона отчета>", "<Шаблон отчета>");
        }

        public ICommand OpenManualCommand { get; init; }
        private void OpenManual(object? obj)
        {
            UserDialog.ShowMessage("<Открытие руководства пользователя>", "<Руководство пользователя>");
        }

        public ICommand AboutProgramCommand { get; init; }
        private void AboutProgram(object? obj)
        {
            UserDialog.ShowMessage("О программе", "<Информация о программе>");
        }

        public ICommand AboutDevCommand { get; init; }
        private void AboutDev(object? obj)
        {
            UserDialog.ShowMessage("О разработчике", "<Информация о разработчике>");
        }

        public MenuViewModel(IUserDialog userDialog)
        {
            UserDialog = userDialog;

            OpenReportTemplateCommand = new RelayCommand(OpenReportTemplate);
            OpenManualCommand = new RelayCommand(OpenManual);
            AboutProgramCommand = new RelayCommand(AboutProgram);
            AboutDevCommand = new RelayCommand(AboutDev);
        }
    }
}
