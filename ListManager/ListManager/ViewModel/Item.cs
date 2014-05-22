using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListManager.ViewModel
{
  public class Item : INotifyPropertyChanged
  {
    public Item()
    {
    }
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

    private bool canDelete = true;

    public bool CanDelete
    {
      get { return canDelete; }
      set
      {
        if (!Equals(canDelete, value))
        {
          canDelete = value;
          PropertyChanged(this, new PropertyChangedEventArgs("CanDelete"));
        }
      }
    }
    

    public ICommand SaveCommand { get { return saveCommand; } }
    public ICommand RenameCommand { get { return renameCommand; } }

    private DelegateCommand saveCommand = null;
    private DelegateCommand renameCommand = null;

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
