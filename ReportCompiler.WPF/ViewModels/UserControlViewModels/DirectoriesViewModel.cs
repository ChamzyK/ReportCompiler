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

        private IFile? fileService;
        public IFile? FileService
        {
            get => fileService; set
            {
                Set(ref fileService, value);
            }
        }

        private DirectoryItem? selectedItem;

        public DirectoryItem? SelectedItem
        {
            get => selectedItem; set
            {
                Set(ref selectedItem, value);
            }
        }

        public ICommand OpenCommand => new RelayCommand(Open, CanOpen);
        private bool CanOpen(object? arg)
        {
            if (SelectedItem == null) return false;

            if (SelectedItem.Type == DirectoryItemType.ExcelFile && FileService != null)
            {
                return FileService.CanOpen(SelectedItem);
            }
            else if(DirectoryService != null)
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
                FileService.Open(SelectedItem);
            }
            else
            {
                DirectoryService.SelectDirectory(SelectedItem);
            }
        }

        public DirectoriesViewModel(IDirectory directoryService, IFile? fileService)
        {
            DirectoryService = directoryService;
            FileService = fileService;
        }
    }
}
