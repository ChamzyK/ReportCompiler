using Microsoft.Extensions.DependencyInjection;

namespace ReportCompiler.WPF.ViewModels
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
    }
}
