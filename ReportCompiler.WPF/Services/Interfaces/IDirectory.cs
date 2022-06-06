using ReportCompiler.WPF.Models;
using System.Collections.Generic;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IDirectory
    {
        public ICollection<DirectoryItem> DirectoryContent { get; }
        public bool SelectDirectory(DirectoryItem browserItem);

        public bool Exists(string path);
        public void OpenDirectory(string path);
    }
}
