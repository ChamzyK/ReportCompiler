namespace ReportCompiler.WPF.Services.Interfaces
{
    internal interface IUserDialog
    {
        void ShowMessage(string header, string message);
        void ShowSuccessResult();
        void ShowIncorrectReports();
    }
}
