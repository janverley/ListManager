using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Lms.ViewModel.Infrastructure;
using Lms.ViewModelI.Infrastructure;
using Lms.ViewModel.Infrastructure.RenamableControl;
using Lms.ModelI.Base.Constraint;

namespace Lms.View.Infrastructure
{
  public class ListManager : Selector
  {
    static ListManager()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ListManager), new FrameworkPropertyMetadata(typeof(ListManager)));
    }



    public IManagedList ManagedList
    {
      get { return (IManagedList)GetValue(ManagedListProperty); }
      set { SetValue(ManagedListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ManagedList.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ManagedListProperty =
        DependencyProperty.Register("ManagedList", typeof(IManagedList), typeof(ListManager), new PropertyMetadata(OnManagedListChanged));


    public InternalItemList InternalItems
    {
      get { return (InternalItemList)GetValue(InternalItemsProperty); }
      set { SetValue(InternalItemsProperty, value); }
    }

    public static readonly DependencyProperty InternalItemsProperty =
        DependencyProperty.Register("InternalItems", typeof(InternalItemList), typeof(ListManager), new PropertyMetadata(null));

    private static void OnManagedListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var managedList = e.NewValue as IManagedList;
      if (managedList != null)
      {

        var lm = d as ListManager;

        if (lm.InternalItems != null)
        {
          lm.InternalItems.Dispose();
        }

        lm.InternalItems = new InternalItemList(managedList);
      }
    }

    public class InternalItemList : ObservableCollection<IItem>, IDisposable
    {
      //protected override void RemoveItem(int index)
      //{
      //  if (buildingInternalItems)
      //  {
      //    base.RemoveItem(index);
      //  }
      //  else
      //  {
      //    externalItems.RemoveAt(index);
      //  }
      //}

      //protected override void InsertItem(int index, Item item)
      //{
      //  if (buildingInternalItems)
      //  {
      //    base.InsertItem(index, item);
      //  }
      //  else
      //  {
      //    SelectedItem = item; // Selection was lost when the item was removed
      //    externalItems.Insert(index, item);
      //  }
      //}

      public InternalItemList(IManagedList managedList)
      {
        this.managedList = managedList;
        deleteCommand = new DelegateCommand<IItem>(Delete, CanDelete);

        BuildInternalItems();
        this.managedList.Items.CollectionChanged += externalItems_CollectionChanged;
      }

      void externalItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
        BuildInternalItems();
      }

      private void BuildInternalItems()
      {
        var currentSelectedItem = SelectedItem;
        Clear();

        foreach (var item in managedList.Items)
        {
          Add(item);
        }

        Add(new Placeholder((newName) =>
          {
            var newItem = managedList.Factory(newName);

            if (newItem != null)
            {
              if (newItem.IsCurrent)
              {
                foreach (var item in managedList.Items)
                {
                  item.IsCurrent = false;
                }
                currentSelectedItem = null;
                SelectedItem = newItem;
              }
              managedList.Items.Add(newItem);
            }
            return newItem != null;
          }, managedList.Constraint));

        if (Contains(currentSelectedItem))
        {
          SelectedItem = currentSelectedItem;
        }
      }

      public ICommand DeleteCommand { get { return deleteCommand; } }
      private DelegateCommand<IItem> deleteCommand;
      private IManagedList managedList;

      private void Delete(IItem item)
      {
        managedList.Items.Remove(item);
      }

      private bool CanDelete(IItem item)
      {
        return managedList.Items.Contains(item) && item.CanDelete;
      }

      //private bool OnAdd(string newName)
      //{
      //  externalItems.Add(new Item(newName, true));
      //  return true;
      //}

      public void Dispose()
      {
        managedList.Items.CollectionChanged -= externalItems_CollectionChanged;
        SelectedItem = null;
      }

      private IItem selectedItem;

      public IItem SelectedItem
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
