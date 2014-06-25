using System;
using System.Collections.ObjectModel;
using Lms.ModelI.Base.Constraint;

namespace Lms.ViewModelI.Infrastructure
{
  public interface IManagedList
  {
    ObservableCollection<IItem> Items { get; }
    IConstraint Constraint { get; }
    Func<string, IItem> Factory { get; }


    void OnAddItem(IItem item);
    void OnDeleteItem(IItem item);
  }
}
