using ReportCompiler.WPF.ViewModels.Base;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string title = "ReportCompiler";
        public string Title
        {
            get => title; set
            {
                Set(ref title, value);
            }
        }

        public MetaDataViewModel MetaDataViewModel { get; }
        public DirectoriesViewModel DirectoriesViewModel { get; }

        public MainViewModel(MetaDataViewModel metaDataViewModel, DirectoriesViewModel directoriesViewModel)
        {
            MetaDataViewModel = metaDataViewModel;
            DirectoriesViewModel = directoriesViewModel;
        }
    }
}
