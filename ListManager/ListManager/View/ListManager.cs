using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using ListManager.ViewModel;

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
  }
}
