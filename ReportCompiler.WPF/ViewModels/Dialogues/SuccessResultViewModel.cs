using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System.Windows.Input;

namespace ReportCompiler.WPF.ViewModels.Dialogues
{
    internal class SuccessResultViewModel : ViewModelBase
    {
        public string? SummaryPath { get; set; }

        public IDirectory DirectoryService { get; init; }
        public IFile FileService { get; init; }

        public ICommand OpenSummaryCommand { get; init; }
        private bool CanOpenSummary(object? arg) => SummaryPath != null && FileService.CanOpen(SummaryPath);
        private void OpenSummary(object? obj)
        {
            FileService.Open(SummaryPath);
        }

        public ICommand OpenDirectoryCommand { get; init; }
        private bool CanOpenDirectory(object? arg) => SummaryPath != null;
        private void OpenDirectory(object? obj)
        {
            DirectoryService.OpenFileDirectory(SummaryPath);
        }

        public SuccessResultViewModel(IDirectory directoryService, IFile fileService)
        {
            DirectoryService = directoryService;
            FileService = fileService;

            OpenSummaryCommand = new RelayCommand(OpenSummary, CanOpenSummary);
            OpenDirectoryCommand = new RelayCommand(OpenDirectory, CanOpenDirectory);
        }
    }
}
