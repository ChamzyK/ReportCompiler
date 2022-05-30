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

        public IReportFormer ReportFormer { get; init; }
        public MetaDataViewModel MetaDataViewModel { get; init; }
        public MenuViewModel MenuViewModel { get; init; }

        public ICommand CheckDataCommand => new RelayCommand(CheckData, CanCheckData);
        private bool CanCheckData(object? obj) =>
            obj != null
            && (obj is DirectoriesViewModel dvm)
            && dvm.SelectedItem != null && false; //TODO: временно отключил возможность проверки данных
        private void CheckData(object? obj)
        {
            var dvm = obj as DirectoriesViewModel;
            ReportFormer.CheckData(dvm.SelectedItem.Path);
        }

        public ICommand CreateReportCommand => new RelayCommand(CreateReport, CanCreateReport);
        private bool CanCreateReport(object? obj) => 
            obj != null 
            && (obj is DirectoriesViewModel dvm) 
            && dvm.SelectedItem != null;
        private void CreateReport(object? obj)
        {
            var dvm = obj as DirectoriesViewModel;
            ReportFormer.CreateMainReport(dvm.SelectedItem.Path);
        }

        public MainViewModel(IReportFormer reportFormer, MetaDataViewModel metaDataViewModel, MenuViewModel menuViewModel)
        {
            ReportFormer = reportFormer;
            MetaDataViewModel = metaDataViewModel;
            MenuViewModel = menuViewModel;
        }
    }
}
