using ReportCompiler.WPF.ViewModels.Base;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string title = "Summary report compiler";
        public string Title
        {
            get => title; set
            {
                Set(ref title, value);
            }
        }

        public MetaDataViewModel MetaDataViewModel { get; init; }
        public DirectoriesViewModel DirectoriesViewModel { get; init; }

        public MainViewModel(MetaDataViewModel metaDataViewModel, DirectoriesViewModel directoriesViewModel)
        {
            MetaDataViewModel = metaDataViewModel;
            DirectoriesViewModel = directoriesViewModel;
        }
    }
}
