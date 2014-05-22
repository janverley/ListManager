using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace ListManager.ViewModel
{
  public class List : ObservableCollection<Item>, IDisposable
  {
    public List(ObservableCollection<Item> externalItems)
    {
      this.externalItems = externalItems;
      deleteCommand = new DelegateCommand<Item>(Delete, CanDelete);

      BuildInternalItems();
      externalItems.CollectionChanged += externalItems_CollectionChanged;
    }

    void externalItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      BuildInternalItems();
    }

    private void BuildInternalItems()
    {
      var currentSelectedItem = SelectedItem;
      Clear();

      foreach (var item in externalItems)
      {
        Add(item);
      }

      Add(new PlaceHolder(newName => OnAdd(newName)));

      SelectedItem = currentSelectedItem;
    }

    public ICommand DeleteCommand { get { return deleteCommand; } }
    private DelegateCommand<Item> deleteCommand;
    private ObservableCollection<Item> externalItems;

    private void Delete(Item item)
    {
      externalItems.Remove(item);
    }

    private bool CanDelete(Item item)
    {
      return externalItems.Contains(item) && item.CanDelete;
    }

    private void OnAdd(string newName)
    {
      externalItems.Add(new Item(newName, true));
    }

    public void Dispose()
    {
      externalItems.CollectionChanged -= externalItems_CollectionChanged;
      SelectedItem = null;
    }

    private Item selectedItem;

    public Item SelectedItem
    {
      get { return selectedItem; }
      set
      {
        if (!Equals(selectedItem, value))
        {
          if (selectedItem != null)
          {
            selectedItem.PropertyChanged -= selectedItem_PropertyChanged;
            selectedItem.IsCurrent = false;
          }

          selectedItem = value;

          if (selectedItem != null)
          {
            selectedItem.PropertyChanged += selectedItem_PropertyChanged;
            selectedItem.IsCurrent = true;
          }

          OnPropertyChanged(new PropertyChangedEventArgs("SelectedItem"));
        }
      }
    }

    void selectedItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (Equals(e.PropertyName, "IsCurrent") && !SelectedItem.IsCurrent)
      {
        SelectedItem = null;
      }
    }
  }
}
