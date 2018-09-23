using System;
using System.Windows.Input;

namespace CsharpHelpers.Wpf
{
    public class Command : ICommand
    {
        private readonly Action _action;
        private bool _canExecute;

        public Command(Action action)
        {
            _action = action;
            _canExecute = true;
        }
        public Command(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            if (_canExecute)
            {
                _canExecute = false;
                _action.Invoke();
            }
            _canExecute = true;
        }

        public event EventHandler CanExecuteChanged;
    }

    public class Command<T> : ICommand where T : class
    {
        private readonly Action<T> _action;
        private bool _canExecute;

        public Command(Action<T> action)
        {
            _action = action;
            _canExecute = true;
        }

        public Command(Action<T> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            if (_canExecute)
            {
                _canExecute = false;
                if (parameter is T)
                {
                    _action.Invoke((T)parameter);
                }
                else
                {
                    _action.Invoke(null);
                }
            }
            _canExecute = true;
        }

        public event EventHandler CanExecuteChanged;
    }
}