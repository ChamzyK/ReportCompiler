﻿using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Models;
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
                var currDirInfo = new DirectoryInfo(currDir);
                return new DirectoryItem(currDirInfo.Name, currDirInfo.FullName, DirectoryItemType.Directory);
            })
        ;
    }
}
