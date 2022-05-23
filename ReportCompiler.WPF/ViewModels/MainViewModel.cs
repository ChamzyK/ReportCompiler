using ReportCompiler.WPF.ViewModels.Base;

namespace ReportCompiler.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region Свойство Title
        private string title = "Составитель отчётов";

        public string Title
        {
            get => title; set
            {
                Set(ref title, value);
            }
        } 
        #endregion
    }
}
