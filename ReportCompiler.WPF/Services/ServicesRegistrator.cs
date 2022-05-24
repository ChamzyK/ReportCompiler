using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Models;
using ReportCompiler.WPF.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ReportCompiler.WPF.Services
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialogService>()
            .AddTransient<IDirectory, DirectoryService>()
            .AddSingleton<IExcelReportFormer, ExcelReportFormerService>()
            .AddTransient<ICollection<DirectoryItem>, ObservableCollection<DirectoryItem>>()
        ;
    }
}
