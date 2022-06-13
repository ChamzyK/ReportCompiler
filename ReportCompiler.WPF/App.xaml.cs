using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReportCompiler.WPF.Services;
using ReportCompiler.WPF.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace ReportCompiler.WPF
{
    public partial class App : Application
    {
        public static string CurrentDir => Environment.CurrentDirectory;

        public static Window? FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);
        public static Window? ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        private static IHost? host;
        public static IHost Host => host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddServices()
            .AddViewModels()
        ;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            base.OnStartup(e);

            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            using (Host) await Host.StopAsync();
        }
    }
}
