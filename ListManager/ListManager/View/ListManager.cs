using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ListManager.ViewModel;

namespace ListManager.View
{
  /// <summary>
  /// Interaction logic for ListManager.xaml
  /// </summary>
  public partial class ListManager : Control
  {
    static ListManager()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ListManager), new FrameworkPropertyMetadata(typeof(ListManager)));
    }



    public List ItemsSource
    {
      get { return (List)GetValue(ItemsSourceProperty); }
      set { SetValue(ItemsSourceProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(List), typeof(ListManager), new PropertyMetadata(null));

  }
}
