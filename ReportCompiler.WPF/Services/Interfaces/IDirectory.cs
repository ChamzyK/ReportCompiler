using ReportCompiler.WPF.Models.Directories;
using System.Collections.Generic;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IDirectory
    {
        public bool Exists(string path);
        public void OpenDirectory(string path);
        public IList<DirectoryItem> GetExcelFiles(string path);
        public void CreateReportsDirectories();
    }
}
