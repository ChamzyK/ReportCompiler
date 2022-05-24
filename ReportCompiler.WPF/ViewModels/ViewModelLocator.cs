using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.ViewModels
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
        public InfoViewModel InfoViewModel => App.Services.GetRequiredService<InfoViewModel>();
        public DirectoriesViewModel DirectoriesViewModel => App.Services.GetRequiredService<DirectoriesViewModel>();
    }
}
