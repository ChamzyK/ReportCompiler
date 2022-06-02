using ReportCompiler.WPF.Infrastructure.Commands;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.Base;
using System.Windows.Input;

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
