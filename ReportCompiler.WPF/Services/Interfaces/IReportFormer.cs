namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IReportFormer
    {
        bool CheckData(string path);
        void CretateMainReport(string path);
    }
}
