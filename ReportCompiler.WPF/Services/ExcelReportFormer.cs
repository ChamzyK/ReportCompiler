using ReportCompiler.WPF.Services.Interfaces;

namespace ReportCompiler.WPF.Services
{
    internal class ExcelReportFormer : IReportFormer
    {
        public IUserDialog UserDialog { get; init; }

        public ExcelReportFormer(IUserDialog userDialog)
        {
            UserDialog = userDialog;
        }

        public bool CheckData(string path)
        {
            return false;
        }

        public void CretateMainReport(string path)
        {

        }
    }
}
