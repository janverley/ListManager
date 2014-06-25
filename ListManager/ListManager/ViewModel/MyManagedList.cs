using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lms.ModelI.Base.Constraint;
using Lms.ViewModelI.Infrastructure;

namespace ListManager.ViewModel
{
  public class MyManagedList : IManagedList
  {
    public MyManagedList()
    {
      var acceptNewName = new Func<string, bool>((newName) =>
      MessageBox
          .Show(string.Format("Rename requested!\n Accept?\nNew Name: {0}", newName), "Confirm", MessageBoxButton.YesNo)
          .Equals(MessageBoxResult.Yes)
        );
      var renameItem = new MyItem("Rename Me!", "This will need confirmation", true, null, acceptNewName, new MyConstraint());

      Items = new ItemCollection( new[]{
        new MyItem("Default", false){CanDelete = false},
        new MyItem("Item1", true),
        new MyItem("Save Me!", "Save Me!", true, (item) => 
          {
            return MessageBox
              .Show(string.Format("Save requested!\nItem: {0} - {1}\n Accept?",item.Name, item.IsFavorite),"Confirm",MessageBoxButton.YesNo)
              .Equals(MessageBoxResult.Yes);
          }),
        new MyItem("UnRenamable", false),
        renameItem,

        new MyItem("Item1", true),
      });
    }

    public ItemCollection Items
    {
      get;
      private set;
    }

    public Lms.ModelI.Base.Constraint.IConstraint Constraint
    {
      get { return NoConstraint.Instance; }
    }

    public Func<string, IItem> Factory
    {
      get { return (newname) => new MyItem(newname, true) { IsCurrent = newname.Contains('n')}; }
    }


    public void OnAddItem(IItem item)
    {

    }

    public void OnDeleteItem(IItem item)
    {

    }
  }
}
