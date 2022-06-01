using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        public IUserDialog UserDialog { get; }
        public IFile File { get; }

        public ICommand OpenFileCommand { get; }
        private bool CanOpeFile(object? arg)
        {
            if (arg == null || arg is not string path) return false;
            return File.CanOpen(path);
        }
        private void OpenFile(object? obj)
        {
            File.Open((string)obj);
        }

        public ICommand AboutProgramCommand { get; }
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

        public ICommand AboutDevCommand { get; }
        private void AboutDev(object? obj)
        {
            UserDialog.ShowMessage("О разработчике",
                "Город: Новосибирск\n" +
                "Организация: СГУПС\n" +
                "Кафедра: ИТТ\n\n" +
                "Разработчик: Чамзы К. Э.\n" +
                "Руководитель: Сарычев С. П.");
        }

        public MenuViewModel(IUserDialog userDialog, IFile file)
        {
            UserDialog = userDialog;
            File = file;

            OpenFileCommand = new RelayCommand(OpenFile, CanOpeFile);
            AboutProgramCommand = new RelayCommand(AboutProgram);
            AboutDevCommand = new RelayCommand(AboutDev);
        }
    }
}
