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

        private DirectoryItem? selectedDirectory;
        public DirectoryItem? SelectedDirectory
        {
            get => selectedDirectory; set
            {
                Set(ref selectedDirectory, value);
            }
        }

        public ICommand OpenDirectoryCommand => new RelayCommand(OpenDirectory, CanOpenDirectory);
        private bool CanOpenDirectory(object? arg) =>
            (SelectedDirectory != null && DirectoryService != null)
            && DirectoryService.CanSelectDirectory(SelectedDirectory);
        private void OpenDirectory(object? obj)
        {
            if (DirectoryService == null || SelectedDirectory == null) return;

            DirectoryService.SelectDirectory(SelectedDirectory);
        }

        public DirectoriesViewModel(IDirectory directoryService)
        {
            DirectoryService = directoryService;
        }
    }
}
