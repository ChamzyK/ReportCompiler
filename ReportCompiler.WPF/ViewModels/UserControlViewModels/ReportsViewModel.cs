using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Models.Directories;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class ReportsViewModel : ViewModelBase
    {
        private DirectoryItem? selectedItem;
        private ObservableCollection<DirectoryItem>? reportFiles;

        public DirectoryItem? SelectedItem
        {
            get => selectedItem; set
            {
                Set(ref selectedItem, value);
            }
        }
        public ObservableCollection<DirectoryItem>? ReportFiles
        {
            get => reportFiles; private set
            {
                Set(ref reportFiles, value);
            }
        }

        public MetaDataViewModel MetaDataViewModel { get; }
        public IDirectory DirectoryService { get; }

        public ICommand RefreshReportsCommand { get; }
        private void RefreshReports(object? obj)
        {
            var monthPath = ((string)obj).GetMonth().GetDirPath();
            ReportFiles = new ObservableCollection<DirectoryItem>(DirectoryService.GetExcelFiles(monthPath));
        }
        private bool CanRefreshReports(object? arg) => 
            arg != null 
            && arg is string monthName
            && DirectoryService.Exists(monthName.GetMonth().GetDirPath());

        public ReportsViewModel(MetaDataViewModel metaDataViewModel, IDirectory directoryService)
        {
            MetaDataViewModel = metaDataViewModel;
            DirectoryService = directoryService;

            MetaDataViewModel.PropertyChanged += MetaDataViewModel_PropertyChanged;

            RefreshReportsCommand = new RelayCommand(RefreshReports, CanRefreshReports);
        }

        private void MetaDataViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MetaDataViewModel.SelectedMonth))
            {
                var month = MetaDataViewModel.SelectedMonth;
                if(CanRefreshReports(month))
                {
                    RefreshReports(month);
                }
            }
        }
    }
}
