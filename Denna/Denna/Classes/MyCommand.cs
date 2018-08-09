using System;
using System.Windows.Input;

namespace Denna.Classes
{
    public class MyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Predicate<object> CanExecuteFunc
        {
            get;
            set;
        }

        public Action<object> ExecuteFunc
        {
            get;
            set;
        }

        public bool CanExecute(object parameter) => CanExecuteFunc(parameter);

        public void Execute(object parameter) => ExecuteFunc(parameter);
    }
}
