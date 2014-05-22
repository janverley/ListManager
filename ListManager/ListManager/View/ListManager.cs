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
    }

    public List InternalItems
    {
      get { return (List)GetValue(InternalItemsProperty); }
      set { SetValue(InternalItemsProperty, value); }
    }

    public static readonly DependencyProperty InternalItemsProperty =
        DependencyProperty.Register("InternalItems", typeof(List), typeof(ListManager), new PropertyMetadata(null));

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

      if (lm.InternalItems != null)
      {
        lm.InternalItems.Dispose();
      }

      lm.InternalItems = new List(e.NewValue as ObservableCollection<Item>);

    }
  }
}
