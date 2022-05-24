using ReportCompiler.WPF.Models;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ReportCompiler.WPF.Services
{
    internal class DirectoryService : IDirectory
    {
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

        public DirectoryService()
        {
            DirectoryContent = new ObservableCollection<DirectoryItem>();
            CurrentDirFullName = Directory.GetCurrentDirectory();
        }

        private void UpdateContent()
        {
            DirectoryContent.Clear();

            var parent = new DirectoryItem
            {
                Name = "...",
                IsDirectory = true
            };
            DirectoryContent.Add(parent);

            var directories = Directory.GetDirectories(CurrentDirFullName);
            AddFiles(directories, true);

            var excelFiles = Directory.GetFiles(CurrentDirFullName, "*.xls?");
            AddFiles(excelFiles, false);
        }

        private void AddFiles(string[] filesName, bool isDirectory) //TODO:появились костыли и флаги
        {
            foreach (var fileName in filesName)
            {
                var folderBrowserItem = new DirectoryItem
                {
                    Name = Path.GetFileName(fileName),
                    IsDirectory = isDirectory
                };
                DirectoryContent.Add(folderBrowserItem);
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

        public string GetFullName(DirectoryItem browserItem)
        {
            if (browserItem == null)
            {
                throw new Exception("А здесь null почему то");
            }
            else if (!browserItem.IsDirectory)
            {
                throw new Exception("А здесь не директория, а файл");
            }
            else
            {
                return Path.Combine(CurrentDirFullName, browserItem.Name);
            }
        }
    }
}
