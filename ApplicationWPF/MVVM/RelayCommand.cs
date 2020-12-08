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

        public RelayCommand(Action<object> Action)
        {
            this.action = Action;
        }

        #region ICommand members

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
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
