using ReportCompiler.WPF.Models;
using System.Collections.Generic;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IDirectory
    {
        public ICollection<DirectoryItem> DirectoryContent { get; }

        public bool SelectDirectory(DirectoryItem browserItem);
        public bool CanSelectDirectory(DirectoryItem browserItem);

        public void OpenFileDirectory(string filePath);
    }
}
