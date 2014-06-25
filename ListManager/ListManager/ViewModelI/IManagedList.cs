namespace Lms.ViewModelI.Infrastructure
{
  using System;
  using Lms.ModelI.Base.Constraint;

  public interface IManagedList
  {
    ItemCollection Items { get; }
    IConstraint Constraint { get; }
    Func<string, IItem> Factory { get; }


    void OnAddItem(IItem item);
    void OnDeleteItem(IItem item);
  }
}
