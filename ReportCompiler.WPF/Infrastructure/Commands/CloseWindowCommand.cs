using ReportCompiler.WPF.Infrastructure.Commands.Base;
using System.Windows;

namespace ReportCompiler.WPF.Infrastructure.Commands
{
    internal class CloseWindowCommand : Command
    {
        protected override bool CanExecute(object? parameter) => 
            (parameter as Window ?? App.FocusedWindow ?? App.ActiveWindow) != null;

        protected override void Execute(object? parameter) => 
            (parameter as Window ?? App.FocusedWindow ?? App.ActiveWindow)?.Close();
    }
}
