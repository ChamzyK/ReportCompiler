using ReportCompiler.WPF.Models;
using System.Collections.ObjectModel;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IDirectory //TODO: неудачный сервис (неудобен в использовании, но работающий)
    {
        public ObservableCollection<DirectoryItem> DirectoryContent { get; }

        public void ChangeDir(DirectoryItem browserItem);
        public bool CanChangeDir(DirectoryItem browserItem);

        public string GetFullName(DirectoryItem browserItem);
    }
}
