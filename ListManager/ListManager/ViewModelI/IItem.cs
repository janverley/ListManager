using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Lms.ViewModelI.Infrastructure
{
  public interface IItem: INotifyPropertyChanged
  {
    bool CanDelete { get;  }
    bool IsCurrent { get; set; }
    bool IsDirty { get; }
    bool IsFavorite { get; }
    string Name { get; }
    ICommand SaveCommand { get; }

  }
}
