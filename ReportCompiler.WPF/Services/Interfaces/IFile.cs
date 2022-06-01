using ReportCompiler.WPF.Models.Reports;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IFile
    {
        bool Open(string path);
        bool CanOpen(string path);
    }
}
