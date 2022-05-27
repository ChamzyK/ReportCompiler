namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IReportFormer
    {
        bool CheckData(string path);
        void CreateMainReport(string path);
    }
}
