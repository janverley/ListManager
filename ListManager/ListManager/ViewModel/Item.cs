using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListManager.ViewModel
{
  class Item : INotifyPropertyChanged
  {
    private string name;

    public string Name
    {
      get { return name; }
      set
      {
        if (!Equals(name, value))
        {
          name = value;
          PropertyChanged(this, new PropertyChangedEventArgs("Name"));
        }
      }
    }

    private bool isFavorite;

    public bool IsFavorite
    {
      get { return isFavorite; }
      set
      {
        if (!Equals(isFavorite, value))
        {
          isFavorite = value;
          PropertyChanged(this, new PropertyChangedEventArgs("IsFavorite"));
        }
      }
    }

    private bool isCurrent;

    public bool IsCurrent
    {
      get { return isCurrent; }
      set
      {
        if (!Equals(isCurrent, value))
        {
          isCurrent = value;
          PropertyChanged(this, new PropertyChangedEventArgs("IsCurrent"));
        }
      }
    }

    public ICommand SaveCommand { get; private set; }
    public ICommand RenameCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
