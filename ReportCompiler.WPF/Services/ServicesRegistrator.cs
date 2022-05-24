using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Models;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ReportCompiler.WPF.Services
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialogService>()
            .AddTransient<IDirectory, DirectoryService>()
            .AddTransient<IFile, FileService>()
            .AddTransient<ICollection<DirectoryItem>, ObservableCollection<DirectoryItem>>()
            .AddTransient(services =>
            {
                var currDir = Environment.CurrentDirectory;
                var currDirInfo = new DirectoryInfo(currDir);
                return new DirectoryItem(currDirInfo.Name, currDirInfo.FullName, DirectoryItemType.Directory);
            })
        ;
    }
}
