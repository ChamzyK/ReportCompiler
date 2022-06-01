using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.ViewModels.Base;
using System.Collections.ObjectModel;

namespace ReportCompiler.WPF.ViewModels.Dialogues
{
    internal class IncorrectReportsViewModel : ViewModelBase
    {
        private ObservableCollection<ReportInfo> incorrectReports;
        public ObservableCollection<ReportInfo> IncorrectReports
        {
            get => incorrectReports; set
            {
                Set(ref incorrectReports, value);
            }
        }
    }
}
