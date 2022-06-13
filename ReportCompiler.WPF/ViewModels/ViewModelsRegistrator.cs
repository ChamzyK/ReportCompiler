using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;
using ReportCompiler.WPF.ViewModels.Dialogues;

namespace ReportCompiler.WPF.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services

            .AddTransient<InfoViewModel>()
            .AddTransient<IncorrectReportsViewModel>()
            .AddTransient<SuccessResultViewModel>()

            .AddSingleton<ReportsViewModel>()
            .AddSingleton<MetaDataViewModel>()
            .AddSingleton<MenuViewModel>()
        ;
    }
}
