using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainViewModel>()
            .AddTransient<InfoViewModel>()
            .AddSingleton<DirectoriesViewModel>()
            .AddSingleton<MetaDataViewModel>()
        ;
    }
}
