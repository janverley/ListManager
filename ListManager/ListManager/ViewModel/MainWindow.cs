using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ListManager.ViewModel
{
  class MainWindow : INotifyPropertyChanged
  {
    public MainWindow()
    {
      items = new ObservableCollection<Item>{
        new Item("Default", false){CanDelete = false},
        new Item("Item1", true),
        new Item("Item1", true),
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
