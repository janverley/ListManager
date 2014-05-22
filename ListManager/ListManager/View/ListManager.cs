using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ListManager.ViewModel;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using System;
using System.Collections.Specialized;

namespace ListManager.View
{
  /// <summary>
  /// Interaction logic for ListManager.xaml
  /// </summary>
  public class ListManager : Selector
  {
    static ListManager()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ListManager), new FrameworkPropertyMetadata(typeof(ListManager)));
      SelectedItemProperty.OverrideMetadata(typeof(ListManager), new FrameworkPropertyMetadata(OnSelectedItemChanged));
      ItemsSourceProperty.OverrideMetadata(typeof(ListManager), new FrameworkPropertyMetadata(OnItemsSourceChanged));
    }

    public ListManager()
    {
      InternalItems.CollectionChanged += InternalItems_CollectionChanged;
    }

    void InternalItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      FromUI = true;

      var o = ItemsSource as Collection<Item>;
      if (o != null)
      {
        switch (e.Action)
        {
          case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
            foreach (var item in e.NewItems)
            {
              o.Add(item as Item);              
            }
            break;
          case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
            throw new NotImplementedException();
            break;
          case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
            foreach (var item in e.OldItems)
            {
              o.Remove(item as Item);
            }
            break;
          case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
            throw new NotImplementedException();
            break;
          case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
            o.Clear();
            foreach (var item in InternalItems)
            {
              o.Add(item);
            }
            break;
          default:
            break;
        }
      }

      FromUI = false;
    }

    private bool fromModel;
    private bool FromUI;

    public List InternalItems
    {
      get { return (List)GetValue(InternalItemsProperty); }
      set { SetValue(InternalItemsProperty, value); }
    }

    public static readonly DependencyProperty InternalItemsProperty =
        DependencyProperty.Register("InternalItems", typeof(List), typeof(ListManager), new PropertyMetadata(new List()));
    
    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var o = e.OldValue as Item;
      if (o != null)
      {
        o.IsCurrent = false;
      }

      var i = e.NewValue as Item;
      if (i != null)
      {
        i.IsCurrent = true;
      }
    }

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var lm = d as ListManager;
      var o = e.OldValue as INotifyCollectionChanged;
      if (o != null)
      {
        o.CollectionChanged -= lm.OnItemsChanged;
      }

      var i = e.NewValue as INotifyCollectionChanged;
      if (i != null)
      {
        i.CollectionChanged += lm.OnItemsChanged;
        lm.BuildInternalItems();
      }
    }

    private void BuildInternalItems()
    {
      var currentSelectedItem = SelectedItem as Item;
      InternalItems.Clear();
      foreach (var item in Items)
      {
        InternalItems.Add(item as Item);
      }
      InternalItems.Add(new PlaceHolder());

      if (currentSelectedItem != null && InternalItems.Contains(currentSelectedItem))
      {
        SelectedItem = currentSelectedItem;
      }
    }

    private void OnItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      if (FromUI) return;
      BuildInternalItems();
    }
  }
}
