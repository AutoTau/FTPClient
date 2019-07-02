using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication1
{
    public class Command : ICommand
    {
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public Command(Predicate<object> canExecute, Action<object> execute)
        {
            CanExecuteDelegate = canExecute;
            ExecuteDelegate = execute;
        }

        public bool CanExecute(object parameter)
        {
            return (CanExecuteDelegate != null) ? CanExecuteDelegate(parameter) : true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null) ExecuteDelegate(parameter);
        }
    }
}
