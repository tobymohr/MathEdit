﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MathEdit.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    namespace MathEdit.Services
    {
        public class RelayCommand<T> : ICommand
        {
            #region Fields

            readonly Action<object> _execute;
            readonly Predicate<object> _canExecute;

            #endregion // Fields

            #region Constructors

            public RelayCommand(Action<object> execute)
                : this(execute, null)
            {
            }

            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            {
                if (execute == null)
                    throw new ArgumentNullException("execute");

                _execute = execute;
                _canExecute = canExecute;
            }

            #endregion // Constructors

            #region ICommand Members

            [DebuggerStepThrough]
            public bool CanExecute(object parameters)
            {
                return _canExecute == null ? true : _canExecute(parameters);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameters)
            {
                _execute(parameters);
            }
            #endregion // ICommand Members
        }
    }
}
