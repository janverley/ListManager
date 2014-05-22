using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListManager.ViewModel
{
  public class List : ObservableCollection<Item>
  {

    public List()
    {
      deleteCommand = new DelegateCommand(DoDelete, CanDelete);
    }

    public ICommand DeleteCommand { get { return deleteCommand; } }
    private DelegateCommand deleteCommand;

    private void DoDelete(object parameter)
    {
      Remove(parameter as Item);
    }

    private bool CanDelete(object parameter)
    {
      var i = parameter as Item;
      return Contains(i) && i.CanDelete;
    }


  }
}
