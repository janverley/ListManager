using System;
using System.Windows.Input;

namespace ListManager.ViewModel
{
  class DelegateCommand : ICommand
  {
    public DelegateCommand(Action<object> execute)
    : this(execute, _ => true)
    {
    }

    public DelegateCommand(Action<object> execute,Func<object, bool> canExecute)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
      CanExecuteChanged(this,EventArgs.Empty);
    }

    private Action<object> execute;
    private Func<object, bool> canExecute;
  }
}
