using System;
using System.Windows.Input;

namespace ReportCompiler.WPF.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        private bool executable = true;
        public bool Executable
        {
            get => executable;
            set
            {
                if (Executable == value) return;
                executable = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object? parameter) => Executable && CanExecute(parameter);
        void ICommand.Execute(object? parameter)
        {
            if(CanExecute(parameter))
            {
                Execute(parameter);
            }
        }

        protected virtual bool CanExecute(object? parameter) => true;
        protected abstract void Execute(object? parameter);
    }
}
