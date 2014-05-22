using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ListManager.ViewModel
{
  class MainWindow : INotifyPropertyChanged
  {
    public MainWindow()
    {
      items = new ObservableCollection<Item>{
        new Item{Name="Default" ,CanDelete = false},
        new Item{Name="Item1"},
        new Item{Name="Item1"},
      };
    }

    private ObservableCollection<Item> items;

    public ObservableCollection<Item> Items
    {
      get { return items; }
      private set { items = value; }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
