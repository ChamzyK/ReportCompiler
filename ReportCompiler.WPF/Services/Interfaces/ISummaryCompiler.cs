using ReportCompiler.WPF.Models.Reports;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface ISummaryCompiler
    {
        void CompileSummary(MetaData metaData);
        ReportInfo GetReportInfo(string filePath);
    }
}
