using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManager.ViewModel
{
  class MainWindow : INotifyPropertyChanged
  {
    public MainWindow()
    {
      items = new List{
        new Item{Name="Default"},
        new Item{Name="Item1"},
      };
    }

    private List items;

    public List Items
    {
      get { return items; }
      private set { items = value; }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
