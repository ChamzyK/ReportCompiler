using ReportCompiler.WPF.ViewModels.Base;

namespace ReportCompiler.WPF.ViewModels.Dialogues
{
    internal class InfoViewModel : ViewModelBase
    {
        private string? header;
        private string? information;

        public string? Header
        {
            get => header; set
            {
                Set(ref header, value);
            }
        }
        public string? Information
        {
            get => information; set
            {
                Set(ref information, value);
            }
        }
    }
}
