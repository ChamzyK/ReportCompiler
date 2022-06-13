using ReportCompiler.WPF.Models.Directories;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ReportCompiler.WPF.Services
{
    internal class DirectoryService : IDirectory
    {
        public bool Exists(string path) => Directory.Exists(path);

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

        public IList<DirectoryItem> GetExcelFiles(string dirPath) => Directory.EnumerateFiles(dirPath)
            .Where(path => !path.Contains("~$") && path.Contains(".xlsx"))
            .Select(filePath => new DirectoryItem(Path.GetFileName(filePath), filePath, DirectoryItemType.ExcelFile))
            .ToList();

        public void CreateReportsDirectories()
        {
            var currDir = App.CurrentDir;
            var reportsDir = Directory.CreateDirectory(Path.Combine(currDir, "Отчеты"));

            foreach (Month month in Enum.GetValues(typeof(Month)))
            {
                var monthName = month.GetName();
                CreateReportsDirectory(reportsDir, monthName);
            }
        }

        private static void CreateReportsDirectory(DirectoryInfo reportsDir, string month)
        {
            var monthDir = Directory.CreateDirectory(Path.Combine(reportsDir.FullName, month)).FullName;

            Directory.CreateDirectory(Path.Combine(monthDir, "Сводный отчет"));
        }

        public DirectoryService()
        {
            CreateReportsDirectories();
        }
    }
}
