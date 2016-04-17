using System;
using System.Windows.Input;

namespace meteosat.viewModel.CommandHandler
{
    public class EmptyCommandHandler : ICommand
    {
        private Action TargetMethod { get; set; }

        public EmptyCommandHandler(Action targetMethod)
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
            TargetMethod();
        }
    }
}
