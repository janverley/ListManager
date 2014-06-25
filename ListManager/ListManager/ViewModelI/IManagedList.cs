using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Lms.ModelI.Base.Constraint;

namespace Lms.ViewModelI.Infrastructure
{
  public interface IManagedList
  {
    ItemCollection Items { get; }
    IConstraint Constraint { get; }
    Func<string, IItem> Factory { get; }


    void OnAddItem(IItem item);
    void OnDeleteItem(IItem item);
  }
}
