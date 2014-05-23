using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ListManager.ViewModel;
using Microsoft.Practices.Prism.Commands;

namespace ListManager.View
{
  public class ListManager : Selector
  {
    static ListManager()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ListManager), new FrameworkPropertyMetadata(typeof(ListManager)));
      ItemsSourceProperty.OverrideMetadata(typeof(ListManager), new FrameworkPropertyMetadata(OnItemsSourceChanged));
    }

    public List InternalItems
    {
      get { return (List)GetValue(InternalItemsProperty); }
      set { SetValue(InternalItemsProperty, value); }
    }

    public static readonly DependencyProperty InternalItemsProperty =
        DependencyProperty.Register("InternalItems", typeof(List), typeof(ListManager), new PropertyMetadata(null));

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var lm = d as ListManager;

      if (lm.InternalItems != null)
      {
        lm.InternalItems.Dispose();
      }

      lm.InternalItems = new List(e.NewValue as ObservableCollection<Item>);

    }

    public class List : ObservableCollection<Item>, IDisposable
    {
      protected override void RemoveItem(int index)
      {
        if (buildingInternalItems)
        {
          base.RemoveItem(index);
        }
        else
        {
          externalItems.RemoveAt(index);
        }
      }

      protected override void InsertItem(int index, Item item)
      {
        if (buildingInternalItems)
        {
          base.InsertItem(index, item);
        }
        else
        {
          SelectedItem = item; // Selection was lost when the item was removed
          externalItems.Insert(index, item);
        }
      }

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

      private bool buildingInternalItems = false;

      private void BuildInternalItems()
      {
        buildingInternalItems = true;
        var currentSelectedItem = SelectedItem;
        Clear();

        foreach (var item in externalItems)
        {
          Add(item);
        }

        Add(new PlaceHolder(newName => OnAdd(newName)));

        if (Contains(currentSelectedItem))
        {
          SelectedItem = currentSelectedItem;
        }
        buildingInternalItems = false;
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
}
