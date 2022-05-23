using Microsoft.Extensions.DependencyInjection;

namespace ReportCompiler.WPF.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainViewModel>()
        ;
    }
}
