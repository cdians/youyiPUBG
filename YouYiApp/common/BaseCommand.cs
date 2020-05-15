using System;
using System.Windows.Input;

namespace YouYiApp.common
{
    /// <summary>
    /// 命令基类
    /// </summary>
    public class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                _execute(parameter);
            }
        }

        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        public BaseCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public BaseCommand(Action<object> execute) :
            this(execute, null)
        {
        }
    }
    
}