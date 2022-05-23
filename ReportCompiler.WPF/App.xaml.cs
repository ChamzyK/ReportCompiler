using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace ReportCompiler.WPF
{
    public partial class App : Application
    {
        private static IHost? host;
        public static IHost Host => 
            host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs())
            .Build();

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {

        }

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
