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
            .AddSingleton<IDirectory, DirectoryService>()
            .AddSingleton<IFile, FileService>()
            .AddSingleton<ISummaryCompiler, SummaryCompiler>()
        ;
    }
}
