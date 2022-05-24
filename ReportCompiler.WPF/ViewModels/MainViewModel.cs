using ReportCompiler.WPF.ViewModels.Base;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;
using System;

namespace ReportCompiler.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region Свойство Title
        private string title = "Составитель отчётов";

        public string Title
        {
            get => title; set
            {
                Set(ref title, value);
            }
        }
        #endregion

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

        public MainViewModel(InfoViewModel appInfoViewModel, InfoViewModel develeporInfoViewModel)
        {
            AppInfoViewModel = appInfoViewModel;
            DeveleporInfoViewModel = develeporInfoViewModel;
        }
    }
}
