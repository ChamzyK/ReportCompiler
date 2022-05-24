using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string title = "Составитель отчётов";
        public string Title
        {
            get => title; set
            {
                Set(ref title, value);
            }
        }

        public IUserDialog UserDialog { get; init; }

        public ICommand AboutProgramCommand => new RelayCommand(AboutProgram);
        private void AboutProgram(object? obj)
        {
            UserDialog.ShowMessage("О программе", "<Информация о программе>");
        }

        public ICommand AboutDevCommand => new RelayCommand(AboutDev);
        private void AboutDev(object? obj)
        {
            UserDialog.ShowMessage("О разработчике", "<Информация о разработчике>");
        }

        private DirectoriesViewModel? directoriesViewModel;
        public DirectoriesViewModel? DirectoriesViewModel
        {
            get => directoriesViewModel;
            set
            {
                Set(ref directoriesViewModel, value);
            }
        }

        public MainViewModel(IUserDialog userDialog, DirectoriesViewModel? directoriesViewModel)
        {
            UserDialog = userDialog;
            DirectoriesViewModel = directoriesViewModel;
        }
    }
}
