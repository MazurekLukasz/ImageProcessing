using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApplicationWPF
{
    class RelayCommand : ICommand
    {

        private Action<object> action;
        Func<bool> canExecFunc;

        public RelayCommand(Action<object> Action)
        {
            this.action = Action;
        }
        public RelayCommand(Action<object> Action,Func<bool> canExec)
        {
            this.action = Action;
            this.canExecFunc = canExec;
        }

        #region ICommand members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecFunc == null) return true;

            return canExecFunc.Invoke();
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                action(parameter);
            }
        }
        #endregion
    }
}
