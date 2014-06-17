using System;
using Lms.ModelI.Base.Constraint;
using Lms.ViewModel.Infrastructure;

namespace Lms.View.Infrastructure
{
  public class Placeholder : Item
  {
    public Placeholder(Func<string, bool> acceptNewName, IConstraint constraint)
      : base("Click to add...", "NewName", true, (_) => true, acceptNewName, constraint)
    {
      CanDelete = false;
      RenameObject.IsRenamable = true;
    }
  }
}
