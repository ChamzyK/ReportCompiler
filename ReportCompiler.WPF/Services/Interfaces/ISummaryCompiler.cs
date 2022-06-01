using ReportCompiler.WPF.Models.Reports;
using System.Collections.Generic;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface ISummaryCompiler
    {
        ReportInfo GetReportInfo(string filePath);
        List<ReportInfo> GetReportInfos(string dirPath);
        Report ConvertToReport(ReportInfo reportInfo);
        string CompileSummary(List<Report> reports, MetaData metaData);
    }
}
