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
    }


    //public List Items
    //{
    //  get { return (List)GetValue(ItemsProperty); }
    //  set { SetValue(ItemsProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty ItemsProperty =
    //    DependencyProperty.Register("Items", typeof(List), typeof(ListManager), new PropertyMetadata(null));



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
      i.IsCurrent = true;
    }

  }
}
