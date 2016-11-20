using System;
using System.Threading;
using System.Windows.Input;

namespace MathEdit.Helpers
{
    public class AsyncRelayCommand<T> : ICommand
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Members

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public AsyncRelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public AsyncRelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }
        /// <summary>
        /// Executes the underlying action asynchronously by creating a new thread.
        /// The thread will be single-threaded apartment.
        /// </summary>
        /// <param name="parameter">generic parameter will be provided to the underlying action upon execution.</param>
        public void Execute(object parameter)
        {
            if (this._execute == null) throw new ArgumentNullException($"in {nameof(Execute)}() Action execute is null");
            var thread = new Thread(() => this._execute((T)parameter));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        #endregion
    }
}
