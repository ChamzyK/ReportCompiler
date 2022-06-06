using ReportCompiler.WPF.Models;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;

namespace ReportCompiler.WPF.Services
{
    internal class DirectoryService : IDirectory
    {
        public IUserDialog UserDialog { get; set; }
        public ICollection<DirectoryItem> DirectoryContent { get; }
        public DirectoryInfo? DirectoryInfo { get; private set; }

        public DirectoryService(DirectoryItem directoryItem, IUserDialog userDialog, ICollection<DirectoryItem> directoryContent)
        {
            UserDialog = userDialog;
            DirectoryContent = directoryContent;

            SelectDirectory(directoryItem);
        }

        public bool SelectDirectory(DirectoryItem browserItem)
        {
            try
            {
                if (!Directory.Exists(browserItem.Path)) return false;
                DirectoryInfo = new DirectoryInfo(browserItem.Path);

                DirectoryContent.Clear();

                AddCurrent();
                AddItems(DirectoryNames, DirectoryItemType.Directory);
                AddItems(ExcelFileNames, DirectoryItemType.ExcelFile);

                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                UserDialog.ShowMessage("Ошибка доступа", e.Message);
                return false;
            }
            catch (SecurityException e)
            {
                UserDialog.ShowMessage("Ошибка безопасности", e.Message);
                return false;
            }
        }
        public bool Exists(string path) => Directory.Exists(path);

        private string[] DirectoryNames => DirectoryInfo == null 
            ? Array.Empty<string>() 
            : Directory.GetDirectories(DirectoryInfo.FullName);

        private string[] ExcelFileNames => DirectoryInfo == null 
            ? Array.Empty<string>() 
            : Directory.GetFiles(DirectoryInfo.FullName, "*.xls?");

        private void AddCurrent()
        {
            if (DirectoryInfo == null || DirectoryInfo.Parent == null) return;

            var name = DirectoryInfo.Name;
            var path = DirectoryInfo.Parent.FullName;
            var type = DirectoryItemType.ParentDirectory;

            var parent = new DirectoryItem(name, path, type);
            DirectoryContent.Add(parent);
        }
        private void AddItems(string[] itemNames, DirectoryItemType itemsType)
        {
            var directoryItems = itemNames
                .Select(name => new DirectoryItem(Path.GetFileName(name), name, itemsType));

            foreach (var item in directoryItems)
            {
                DirectoryContent.Add(item);
            }
        }

        public void OpenDirectory(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            var psInfo = new ProcessStartInfo
            {
                FileName = dirPath,
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }
    }
}
