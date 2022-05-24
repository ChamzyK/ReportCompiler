using ReportCompiler.WPF.Models;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ReportCompiler.WPF.Services
{
    internal class DirectoryService : IDirectory
    {
        public IUserDialog UserDialog { get; set; }
        private DirectoryInfo? currentDirectoryInfo;
        private string? currentDirFullName;

        public ObservableCollection<DirectoryItem> DirectoryContent { get; }
        public DirectoryInfo? CurrentDirInfo
        {
            get => currentDirectoryInfo; private set
            {
                currentDirectoryInfo = value;
                UpdateContent();
            }
        }
        public string CurrentDirFullName
        {
            get
            {
                return currentDirFullName;
            }
            set
            {
                var dirInfo = new DirectoryInfo(value);
                currentDirFullName = value;
                CurrentDirInfo = dirInfo;
            }
        }
        public DirectoryInfo ParentInfo
        {
            get
            {
                return CurrentDirInfo.Parent;
            }
        }

        public DirectoryService(IUserDialog userDialog)
        {
            DirectoryContent = new ObservableCollection<DirectoryItem>();
            CurrentDirFullName = Directory.GetCurrentDirectory();
            UserDialog = userDialog;
        }

        private void UpdateContent()
        {
            DirectoryContent.Clear();

            var parent = new DirectoryItem(ParentInfo.Name, ParentInfo.FullName, DirectoryItemType.ParentDirectory);
            DirectoryContent.Add(parent);

            try
            {
                AddFiles(Directories.Concat(ExcelFiles));

            }
            catch (UnauthorizedAccessException e)
            {
                UserDialog.ShowMessage("Ошибка", $"{e.Message}");
            }
        }

        private IEnumerable<DirectoryItem> Directories => Directory
            .GetDirectories(CurrentDirFullName)
            .Select(name => new DirectoryItem(
                Path.GetFileName(name),
                name,
                DirectoryItemType.Directory))
            ;

        private IEnumerable<DirectoryItem> ExcelFiles => Directory
            .GetFiles(CurrentDirFullName, "*.xls?")
            .Select(name => new DirectoryItem(
                Path.GetFileName(name),
                name,
                DirectoryItemType.ExcelFile))
            ;

        private void AddFiles(IEnumerable<DirectoryItem> files)
        {
            foreach (var file in files)
            {
                DirectoryContent.Add(file);
            }
        }

        public void ChangeDir(DirectoryItem browserItem)
        {
            CurrentDirFullName = Path.Combine(CurrentDirFullName, browserItem.Name);
        }
        public bool CanChangeDir(DirectoryItem browserItem)
        {
            var dirName = browserItem.Name;
            var dirFullName = Path.Combine(CurrentDirFullName, dirName);
            var exists = Directory.Exists(dirFullName);
            var contain = Directory.GetDirectories(CurrentDirFullName).Contains(dirFullName);
            return exists && contain;
        }
        public void ChangeDirToParent()
        {
            CurrentDirFullName = ParentInfo.FullName;
        }
        public bool CanChangeDirToParent()
        {
            return ParentInfo != null;
        }

        public bool IsParent(DirectoryItem browserItem)
        {
            return browserItem.Name == "...";
        }

        public string GetFullName(DirectoryItem browserItem) =>
            (browserItem != null && CurrentDirFullName != null)
            ? Path.Combine(CurrentDirFullName, browserItem.Name)
            : throw new ArgumentNullException(browserItem == null
                ? nameof(browserItem)
                : nameof(CurrentDirFullName));

    }
}
