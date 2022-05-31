using ReportCompiler.WPF.Models.Reports;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IReportFormer
    {
        bool CheckData(string path);
        void CreateMainReport(string path, MetaData metaData);
    }
}
