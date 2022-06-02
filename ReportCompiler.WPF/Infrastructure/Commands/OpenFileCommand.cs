using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Infrastructure.Commands.Base;
using ReportCompiler.WPF.Services.Interfaces;

namespace ReportCompiler.WPF.Infrastructure.Commands
{
    internal class OpenFileCommand : Command
    {
        public static IFile FileService => App.Services.GetRequiredService<IFile>();

        protected override bool CanExecute(object? parameter)
        {
            if (parameter == null || parameter is not string path) return false;
            return FileService.CanOpen(path);
        }

        protected override void Execute(object? parameter)
        {
            FileService.Open((string)parameter);
        }
    }
}
