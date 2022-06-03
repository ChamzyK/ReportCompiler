using ReportCompiler.WPF.ViewModels.Base;

namespace ReportCompiler.WPF.ViewModels.Dialogues
{
    internal class SuccessResultViewModel : ViewModelBase
    {
        private string? summaryPath;
        public string? SummaryPath
        {
            get => summaryPath; set
            {
                Set(ref summaryPath, value);
            }
        }
    }
}
