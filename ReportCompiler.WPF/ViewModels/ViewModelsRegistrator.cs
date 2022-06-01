using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;
using ReportCompiler.WPF.ViewModels.Dialogues;

namespace ReportCompiler.WPF.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainViewModel>()

            .AddTransient<InfoViewModel>()
            .AddTransient<IncorrectReportsViewModel>()
            .AddTransient<SuccessResultViewModel>()

            .AddSingleton<DirectoriesViewModel>()
            .AddSingleton<MetaDataViewModel>()
            .AddSingleton<MenuViewModel>()

            .AddSingleton<ReportFormerViewModel>()
        ;
    }
}
