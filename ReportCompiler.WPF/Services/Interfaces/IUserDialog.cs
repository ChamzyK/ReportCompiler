using ReportCompiler.WPF.Models.Reports;
using System.Collections.Generic;

namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IUserDialog
    {
        void ShowMessage(string header, string message);
        void ShowSuccessResult(string filePath);
        void ShowIncorrectReports(List<ReportInfo> reportInfos);
    }
}
