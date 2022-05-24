using ReportCompiler.WPF.ViewModels.Base;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

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

        private readonly InfoViewModel? develeporInfoViewModel;
        public InfoViewModel? DeveleporInfoViewModel
        {
            get => develeporInfoViewModel; init
            {
                develeporInfoViewModel = value;
                if (develeporInfoViewModel != null)
                {
                    develeporInfoViewModel.Header = "О разработчике";
                    develeporInfoViewModel.Information = "<Информация о разработчике>"; //TODO: добавить информацию о разработчике
                }
            }
        }

        private readonly InfoViewModel? appInfoViewModel;
        public InfoViewModel? AppInfoViewModel
        {
            get => appInfoViewModel;
            init
            {
                appInfoViewModel = value;
                if (appInfoViewModel != null)
                {
                    appInfoViewModel.Header = "О программе";
                    appInfoViewModel.Information = "<Информация о программе>"; //TODO: добавить информацию о программе
                }
            }
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

        public MainViewModel(InfoViewModel appInfoViewModel, InfoViewModel develeporInfoViewModel, DirectoriesViewModel? directoriesViewModel)
        {
            AppInfoViewModel = appInfoViewModel;
            DeveleporInfoViewModel = develeporInfoViewModel;
            DirectoriesViewModel = directoriesViewModel;
        }
    }
}
