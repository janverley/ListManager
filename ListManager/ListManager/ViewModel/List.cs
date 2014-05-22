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
using Microsoft.Practices.Prism.Commands;

namespace ListManager.ViewModel
{
  public class List : ObservableCollection<Item>, IDisposable
  {

    public List(ObservableCollection<Item> externalItems)
    {
      this.externalItems = externalItems;
      BuildInternalItems();
      externalItems.CollectionChanged += externalItems_CollectionChanged;
      deleteCommand = new DelegateCommand<Item>(DoDelete, CanDelete);
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
      Add(new PlaceHolder(newName => OnAdd(newName)));
    }

    public ICommand DeleteCommand { get { return deleteCommand; } }
    private DelegateCommand<Item> deleteCommand;
    private ObservableCollection<Item> externalItems;

    private void DoDelete(Item item)
    {
      if (item != null)
      {
        externalItems.Remove(item);
      }
    }

    private bool CanDelete(Item item)
    {
      return item != null && Contains(item) && item.CanDelete;
    }

    private void OnAdd(string newName)
    {
      var newItem = new Item(newName, true);
      externalItems.Add(newItem);
    }

    public void Dispose()
    {
      externalItems.CollectionChanged -= externalItems_CollectionChanged;
    }
  }
}
