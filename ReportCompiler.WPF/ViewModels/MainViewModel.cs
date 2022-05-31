using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;
using System.Windows.Input;

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
        public IReportFormer ReportFormer { get; init; }

        public ICommand CreateReportCommand { get; init; }
        private bool CanCreateReport(object? arg)
        {
            return DirectoriesViewModel.IsDirectorySelected && MetaDataViewModel.IsValidMetaData;
        }
        private void CreateReport(object? obj)
        {
            ReportFormer.CreateMainReport(DirectoriesViewModel.SelectedItem.Path, MetaDataViewModel.MetaData);
        }

        public MainViewModel(MetaDataViewModel metaDataViewModel, DirectoriesViewModel directoriesViewModel, IReportFormer reportFormer)
        {
            MetaDataViewModel = metaDataViewModel;
            DirectoriesViewModel = directoriesViewModel;
            ReportFormer = reportFormer;

            CreateReportCommand = new RelayCommand(CreateReport, CanCreateReport);
        }
    }
}
