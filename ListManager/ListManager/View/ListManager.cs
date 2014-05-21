using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ListManager.ViewModel;
using System.Windows.Controls.Primitives;

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


    public List TheItems
    {
      get { return (List)GetValue(TheItemsProperty); }
      set { SetValue(TheItemsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TheItemsProperty =
        DependencyProperty.Register("TheItems", typeof(List), typeof(ListManager), new PropertyMetadata(new List()));



    //public Item MyItem
    //{
    //  get { return (Item)GetValue(MyItemProperty); }
    //  set { SetValue(MyItemProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty MyItemProperty =
    //    DependencyProperty.Register("MyItem", typeof(Item), typeof(ListManager), new FrameworkPropertyMetadata(OnSelectedItemChanged));

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
      var o = e.OldValue as List;
      if (o != null)
      {
        o.CollectionChanged -= lm.OnItemsChanged;
      }

      var i = e.NewValue as List;
      if (i != null)
      {
        i.CollectionChanged += lm.OnItemsChanged;
        lm.BuildTheItems();
      }
    }

    private void BuildTheItems()
    {
      var currentSelectedItem = SelectedItem as Item;
      TheItems.Clear();
      foreach (var item in Items)
      {
        TheItems.Add(item as Item);
      }
      TheItems.Add(new Item { Name = "Click to add..." });

      if (currentSelectedItem != null && TheItems.Contains(currentSelectedItem))
      {
        SelectedItem = currentSelectedItem;
      }
    }

    private void OnItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      BuildTheItems();
    }
  }
}
