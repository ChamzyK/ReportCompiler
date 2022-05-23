using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Services.Interfaces;

namespace ReportCompiler.WPF.Services
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialogService>()
        ;
    }
}
