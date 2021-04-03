using System;
using System.Windows.Input;

namespace HomeLibraryApplication.Service
{
    public class CommandHandler : ICommand
    {
        private Action<object> _action;
        private bool _canExecute = false;
        public event EventHandler CanExecuteChanged;

        public CommandHandler(Action<object> action, bool canExecute = true)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => _canExecute;


        public void Execute(object parameter) => _action(parameter);

    }
}
