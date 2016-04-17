using System;
using System.Windows.Input;

namespace meteosat.viewModel.CommandHandler
{
    public class ParameterizedCommandHandler<T> : ICommand
    {
        private Action<T> TargetMethod { get; }

        public ParameterizedCommandHandler(Action<T> targetMethod)
        {
            TargetMethod = targetMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            TargetMethod((T)parameter);
        }
    }
}
