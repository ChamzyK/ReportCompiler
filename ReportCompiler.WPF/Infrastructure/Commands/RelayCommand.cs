using ReportCompiler.WPF.Infrastructure.Commands.Base;
using System;

namespace ReportCompiler.WPF.Infrastructure.Commands
{
    internal class RelayCommand : Command
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool>? canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        protected override bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;
        protected override void Execute(object? parameter) => execute(parameter);
    }
}
