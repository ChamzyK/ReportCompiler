using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Infrastructure.Commands.Base;
using ReportCompiler.WPF.Services.Interfaces;

namespace ReportCompiler.WPF.Infrastructure.Commands
{
    internal class OpenDirectoryCommand : Command
    {
        public static IDirectory DirectoryService => App.Services.GetRequiredService<IDirectory>();
        public static IFile FileService => App.Services.GetRequiredService<IFile>();

        protected override bool CanExecute(object? parameter) => parameter != null && parameter is string path && (DirectoryService.Exists(path) || FileService.Exists(path));
        protected override void Execute(object? parameter)
        {
            DirectoryService.OpenDirectory((string)parameter);
        }
    }
}
