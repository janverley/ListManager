using System;
using System.ComponentModel;
using System.Windows.Input;
using Lms.ModelI.Base.Constraint;
using Lms.ViewModel.Infrastructure.RenamableControl;
using Lms.ViewModelI.Infrastructure;
using Microsoft.Practices.Prism.Commands;

namespace Lms.ViewModel.Infrastructure
{
  public class Item : IItem
  {
    public Item(string name, string defaultEditText, bool canRename, Func<Item, bool> onSave, Func<string, bool> acceptNewName, IConstraint constraint)
    {
      saveCommand = new DelegateCommand(() =>
        {
          IsDirty = !onSave(this);
        }, () => IsDirty);

      this.canRename = canRename;

      RenameObject = new RenamableNotificationObject(name, defaultEditText, false, acceptNewName, constraint);
    }

    public RenamableNotificationObject RenameObject { get; set; }

    public string Name
    {
      get { return RenameObject.Name; }
      //set { RenameObject.Name = value; }
    }

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

    private bool isCurrent = false;

    public bool IsCurrent
    {
      get { return isCurrent; }
      set
      {
        if (!Equals(isCurrent, value))
        {
          isCurrent = value;
          PropertyChanged(this, new PropertyChangedEventArgs("IsCurrent"));

          if (!IsCurrent)
          {
            IsDirty = false;
          }
          EvaluateRenamable();
        }
      }
    }

    private void EvaluateRenamable()
    {
      RenameObject.IsRenamable = IsCurrent && canRename && !IsDirty;
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

    private bool isDirty;

    public bool IsDirty
    {
      get { return isDirty; }
      set
      {
        if (!Equals(isDirty, value))
        {
          isDirty = value;
          PropertyChanged(this, new PropertyChangedEventArgs("IsDirty"));

          saveCommand.RaiseCanExecuteChanged();
          EvaluateRenamable();
        }
      }
    }

    public ICommand SaveCommand { get { return saveCommand; } }
    private DelegateCommand saveCommand;

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
