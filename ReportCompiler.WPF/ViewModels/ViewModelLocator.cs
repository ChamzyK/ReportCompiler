using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.ViewModels.Base;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.ViewModels
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
        public ViewModelBase InfoViewModel => App.Services.GetRequiredService<InfoViewModel>();
    }
}
