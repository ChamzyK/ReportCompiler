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
            UserDialog.ShowMessage("О программе", "ReportCompiler\n\n" +
                "Автоматизированное формирование сводных отчетов в отделе трудоустройства управления занятости населения НСО\n\n" +
                "Технологии:\n" +
                "WPF (Windows Presentation Foundation)\n" +
                "Material Design in XAML\n" +
                "EPPlus 4.5.3.3\n\n" +
                "Год разработки: 2022");
        }

        public ICommand AboutDevCommand { get; init; }
        private void AboutDev(object? obj)
        {
            UserDialog.ShowMessage("О разработчике",
                "Город: Новосибирск\n" +
                "Организация: СГУПС\n" +
                "Кафедра: ИТТ\n\n" +
                "Разработчик: Чамзы К. Э.\n" +
                "Руководитель: Сарычев С. П.");
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
