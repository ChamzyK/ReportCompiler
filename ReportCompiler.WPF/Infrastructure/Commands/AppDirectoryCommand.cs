using ReportCompiler.WPF.Infrastructure.Commands.Base;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.Infrastructure.Commands
{
    internal class AppDirectoryCommand : Command
    {
        protected override void Execute(object? parameter)
        {
            if (App.Services.GetService(typeof(DirectoriesViewModel)) == null
                || App.Services.GetService(typeof(IDirectory)) == null) return;

            if (App.Services.GetService(typeof(DirectoriesViewModel)) is not DirectoriesViewModel dvm 
                || dvm.DirectoryService == null) return;


            var directoryService = App.Services.GetService(typeof(IDirectory)) as IDirectory;
            dvm.DirectoryService = directoryService;
        }
    }
}
