using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Models.Directories;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.Services.SummaryServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ReportCompiler.WPF.Services
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddSingleton<IUserDialog, UserDialogService>()
            .AddTransient<IDirectory, DirectoryService>()
            .AddTransient<IFile, FileService>()
            .AddSingleton<ISummaryCompiler, SummaryCompiler>()
            .AddTransient<ICollection<DirectoryItem>, ObservableCollection<DirectoryItem>>()
            .AddTransient(services =>
            {
                var currDir = Environment.CurrentDirectory;

                var reportsDir = Directory.CreateDirectory(Path.Combine(currDir, "Отчеты"));

                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Январь"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Февраль"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Март"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Апрель"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Май"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Июнь"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Июль"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Август"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Сентябрь"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Октябрь"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Ноябрь"));
                Directory.CreateDirectory(Path.Combine(reportsDir.FullName, "Декабрь"));

                return new DirectoryItem(reportsDir.Name, reportsDir.FullName, DirectoryItemType.Directory);
            })
        ;
    }
}
