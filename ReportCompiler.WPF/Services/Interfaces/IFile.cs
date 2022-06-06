namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IFile
    {
        bool Open(string path);
        bool Exists(string path);
    }
}
