using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Models.Directories;
using ReportCompiler.WPF.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class ReportsViewModel : ViewModelBase
    {
        private DirectoryItem? selecteItem;
        public DirectoryItem? SelecteItem
        {
            get => selecteItem; set
            {
                Set(ref selecteItem, value);
            }
        }

        public MetaDataViewModel MetaDataViewModel { get; }
        public ObservableCollection<DirectoryItem> ReportFiles { get; }

        public ICommand RefreshReportsCommand { get; }

        public ReportsViewModel(MetaDataViewModel metaDataViewModel)
        {
            MetaDataViewModel = metaDataViewModel;

            MetaDataViewModel.PropertyChanged += MetaDataViewModel_PropertyChanged;

            ReportFiles = new ObservableCollection<DirectoryItem>();
            RefreshReportsCommand = new RelayCommand(RefreshReports, CanRefreshReports);
        }

        private void MetaDataViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MetaDataViewModel.SelectedMonth))
            {
                if(MetaDataViewModel.SelectedMonth == null)
                {
                    ReportFiles.Clear();
                }
                else
                {
                    RefreshReports(sender);
                }
            }
        }

        private void RefreshReports(object? obj)
        {

        }

        private bool CanRefreshReports(object? arg) => ReportFiles != null;
    }
}
