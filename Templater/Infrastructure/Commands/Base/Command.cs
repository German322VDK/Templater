using System;
using System.Windows.Input;

namespace Templater.Infrastructure.Commands.Base
{
    public abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => CanExecute(parameter);

        void ICommand.Execute(object parameter) => Execute(parameter);

        public virtual bool CanExecute(object p) => true;

        public abstract void Execute(object p);
    }
}
