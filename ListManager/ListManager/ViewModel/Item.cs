using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lms.ViewModel.Infrastructure.RenamableControl;
using Microsoft.Practices.Prism.Commands;

namespace ListManager.ViewModel
{
  public class Item : INotifyPropertyChanged
  {
    public Item(string name, bool canRename)
    {
      this.canRename = canRename;
      Name = new RenamableNotificationObject(name, name, false);
    }

    public RenamableNotificationObject Name { get; set; }

    private bool canRename;

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

          Name.IsRenamable = IsCurrent && canRename;
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

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
