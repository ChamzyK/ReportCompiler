using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Models;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels.UserControlViewModels
{
    internal class DirectoriesViewModel : ViewModelBase
    {
        private IDirectory? directoryService;
        public IDirectory? DirectoryService
        {
            get => directoryService; set
            {
                Set(ref directoryService, value);
            }
        }

        public IFile FileService { get; init; }

        private DirectoryItem? selectedItem;
        public DirectoryItem? SelectedItem
        {
            get => selectedItem; set
            {
                Set(ref selectedItem, value);
            }
        }

        public ICommand OpenCommand { get; init; }
        private bool CanOpen(object? arg)
        {
            if (SelectedItem == null) return false;

            if (SelectedItem.Type == DirectoryItemType.ExcelFile && FileService != null)
            {
                return FileService.CanOpen(SelectedItem.Path);
            }
            else if (DirectoryService != null)
            {
                return DirectoryService.CanSelectDirectory(SelectedItem);
            }

            return false;
        }
        private void Open(object? obj)
        {
            if (DirectoryService == null || SelectedItem == null || FileService == null) return;

            if (SelectedItem.Type == DirectoryItemType.ExcelFile)
            {
                FileService.Open(SelectedItem.Path);
            }
            else
            {
                DirectoryService.SelectDirectory(SelectedItem);
            }
        }

        public bool IsDirectorySelected => SelectedItem != null && (SelectedItem.Type == DirectoryItemType.Directory);

        public DirectoriesViewModel(IDirectory directoryService, IFile fileService)
        {
            DirectoryService = directoryService;
            FileService = fileService;

            OpenCommand = new RelayCommand(Open, CanOpen);
        }
    }
}
