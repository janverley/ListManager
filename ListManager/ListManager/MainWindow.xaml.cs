using ListManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lms.ViewModel.Infrastructure;
using Lms.ViewModelI.Infrastructure;

namespace ListManager
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      DataContext = new ViewModel.MainWindow();
    }

    private ViewModel.MainWindow ViewModel
    {
      get { return DataContext as ViewModel.MainWindow; }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      ViewModel.ManagedList.Items.Add(new MyItem("added", true));
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      ViewModel.ManagedList.Items.RemoveAt(1);
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
      //ViewModel.Items[1].IsFavorite = !ViewModel.Items[1].IsFavorite;

    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
      foreach (var item in ViewModel.ManagedList.Items)
      {
        item.IsCurrent = false;
      }
    }

    private void Button_Click_4(object sender, RoutedEventArgs e)
    {
      if (ViewModel.ManagedList.Items.Any(i => i.IsCurrent))
      {
       // ViewModel.Items.First(i => i.IsCurrent).IsDirty = true;        
      }
    }
    private void Button_Click_5(object sender, RoutedEventArgs e)
    {
      ViewModel.ManagedList = new MyManagedList();

        //new System.Collections.ObjectModel.ObservableCollection<IItem>{
        //new MyItem("Item1", true)};

    }
  }
}
