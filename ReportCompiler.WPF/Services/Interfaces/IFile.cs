using ReportCompiler.WPF.Models;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IFile
    {
        bool Open(DirectoryItem directoryItem);
        bool CanOpen(DirectoryItem directoryItem);
    }
}
