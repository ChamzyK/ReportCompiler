using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Infrastructure.Commands.Base;
using ReportCompiler.WPF.Services.Interfaces;

namespace ReportCompiler.WPF.Infrastructure.Commands
{
    internal class OpenDirectoryCommand : Command
    {
        public static IDirectory DirectoryService => App.Services.GetRequiredService<IDirectory>();

        protected override bool CanExecute(object? parameter) => parameter != null && parameter is string;
        protected override void Execute(object? parameter)
        {
            DirectoryService.OpenDirectory((string)parameter);
        }
    }
}
