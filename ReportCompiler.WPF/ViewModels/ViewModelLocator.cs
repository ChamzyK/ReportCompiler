using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.ViewModels
{
    internal class ViewModelLocator
    {
        public static MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
        public static MenuViewModel MenuViewModel => App.Services.GetRequiredService<MenuViewModel>();
    }
}
