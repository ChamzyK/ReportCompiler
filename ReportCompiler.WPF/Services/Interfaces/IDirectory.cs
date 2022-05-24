using ReportCompiler.WPF.Models;
using System.Collections.ObjectModel;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IDirectory
    {
        public ObservableCollection<DirectoryItem> DirectoryContent { get; }

        public void ChangeDir(DirectoryItem browserItem);
        public bool CanChangeDir(DirectoryItem browserItem);

        public void ChangeDirToParent();
        public bool CanChangeDirToParent();

        public bool IsParent(DirectoryItem browserItem); //TODO: убрать 
        public string GetFullName(DirectoryItem browserItem);
    }
}
