using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ListManager.View;

namespace ListManager.ViewModel
{
  public class List : ObservableCollection<Item>, IDisposable
  {

    public List(ObservableCollection<Item> externalItems)
    {
      this.externalItems = externalItems;
      BuildInternalItems();
      externalItems.CollectionChanged += externalItems_CollectionChanged;
      deleteCommand = new DelegateCommand(DoDelete, CanDelete);
    }

    void externalItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      BuildInternalItems();
    }

    private void BuildInternalItems()
    {
      // todo : retain selected item
      //var currentSelectedItem = SelectedItem as Item;
      Clear();
      foreach (var item in externalItems)
      {
        Add(item);
      }
      Add(new PlaceHolder());
    }

    public ICommand DeleteCommand { get { return deleteCommand; } }
    private DelegateCommand deleteCommand;
    private ObservableCollection<Item> externalItems;

    private void DoDelete(object parameter)
    {
      var item = parameter as Item;
      if (item != null)
      {
        externalItems.Remove(item);
        Remove(item);        
      }
    }

    private bool CanDelete(object parameter)
    {
      var item = parameter as Item;
      return item != null && Contains(item) && item.CanDelete;
    }

    public void Dispose()
    {
      externalItems.CollectionChanged -= externalItems_CollectionChanged;
    }
  }
}
