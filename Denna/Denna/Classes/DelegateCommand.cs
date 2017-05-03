
using System;

using System.Diagnostics;

using System.Diagnostics.CodeAnalysis;

using System.Windows.Input;

using Windows.UI.Xaml.Input;
namespace Denna.Classes
{




    public class DelegateCommand : DelegateCommand<object>

    {

        public DelegateCommand(Action execute, Func<bool> canExecute)

            : base(_ => execute(), _ => canExecute())

        {



            if (execute == null)

                throw new ArgumentNullException("execute");



            if (canExecute == null)

                throw new ArgumentNullException("canExecute");

        }



        public DelegateCommand(Action execute)

            : this(execute, () => true)

        {

        }

    }



    public class DelegateCommand<T> : ICommand

    {



        private Action<T> execute;

        private Func<T, bool> canExecute;



        public event EventHandler CanExecuteChanged;



        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)

        {



            if (execute == null)

                throw new ArgumentNullException("execute");



            if (canExecute == null)

                throw new ArgumentNullException("canExecute");





            this.execute = execute;

            this.canExecute = canExecute;

        }



        public DelegateCommand(Action<T> execute)

            : this(execute, _ => true)

        {

        }



        public bool CanExecute(object parameter)

        {

            if (!(parameter is T) && parameter != (object)default(T))

                return false;



            return canExecute((T)parameter);

        }



        public void Execute(object parameter)

        {

            execute((T)parameter);

        }



        public void NotifyCanExecuteChanged()

        {

            EventHandler eventHandler = CanExecuteChanged;



            if (eventHandler != null)

                eventHandler(this, EventArgs.Empty);

        }

    }
}
