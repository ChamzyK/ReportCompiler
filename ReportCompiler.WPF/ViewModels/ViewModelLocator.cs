using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.ViewModels
{
    internal class ViewModelLocator
    {
        public static MenuViewModel MenuViewModel => App.Services.GetRequiredService<MenuViewModel>();
        public static ReportsViewModel ReportsViewModel => App.Services.GetRequiredService<ReportsViewModel>();
        public static MetaDataViewModel MetaDataViewModel => App.Services.GetRequiredService<MetaDataViewModel>();
    }
}
