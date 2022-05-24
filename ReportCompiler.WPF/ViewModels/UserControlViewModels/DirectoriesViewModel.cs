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

        private DirectoryItem? selectedItem;
        public DirectoryItem? SelectedDirectory
        {
            get => selectedItem; set
            {
                Set(ref selectedItem, value);
            }
        }

        public ICommand OpenDirectoryCommand => new RelayCommand(SelectFolder, CanSelectFolder);
        private bool CanSelectFolder(object? arg)
        {
            return (SelectedDirectory != null && DirectoryService != null) &&
                (!DirectoryService.IsParent(SelectedDirectory) && (DirectoryService.CanChangeDir(SelectedDirectory)) ||
                (DirectoryService.IsParent(SelectedDirectory) && DirectoryService.CanChangeDirToParent()));
        }
        private void SelectFolder(object? obj)
        {
            if (DirectoryService != null && SelectedDirectory != null)
            {
                if (!DirectoryService.IsParent(SelectedDirectory))
                {
                    DirectoryService.ChangeDir(SelectedDirectory);
                }
                else
                {
                    DirectoryService.ChangeDirToParent();
                }
            }
        }

        public ICommand CreateReportCommand => new RelayCommand(CreateReport, CanCreateReport);
        private bool CanCreateReport(object? arg)
        {
            return true;
        }
        private void CreateReport(object? obj)
        {
            //var reporter = ReportFormerBuilder.GetReportFormer();

            //if (DirectoryService != null && SelectedItem != null)
            //{
            //    var fullName = DirectoryService.GetFullName(SelectedItem);
            //    reporter.GenerateReport(fullName);
            //}
        }

        public DirectoriesViewModel(IDirectory directoryService) //TODO: абстрагировать
        {
            DirectoryService = directoryService;
        }
    }
}
