using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lms.ModelI.Base.Constraint;
using Lms.ViewModel.Infrastructure.RenamableControl;
using Lms.ViewModelI.Infrastructure;
using Microsoft.Practices.Prism.Commands;

namespace ListManager.ViewModel
{
  public class MyItem : IItem
  {
    public MyItem(string name, bool canRename)
      : this(name, name, canRename, (_) => true, (_) => true, NoConstraint.Instance)
    { }

    public MyItem(string name, string defaultEditText, bool canRename)
      : this(name, name, canRename, (_) => true, (_) => true, NoConstraint.Instance)
    { }

    public MyItem(string name, string defaultEditText, bool canRename, Func<IItem, bool> onSave)
      : this(name, defaultEditText, canRename, onSave, (_)=> true, NoConstraint.Instance)
    { }

    public MyItem(string name, string defaultEditText, bool canRename, Func<IItem, bool> onSave, Func<string, bool> acceptNewName, IConstraint constraint)
    {
      saveCommand = new DelegateCommand(() =>
      {
        IsDirty = !onSave(this);
      }, () => IsDirty);

      this.canRename = canRename;

      RenameObject = new RenamableNotificationObject(name, defaultEditText, false, acceptNewName, constraint);
    }

    private bool canRename;
    public ICommand SaveCommand { get { return saveCommand; } }
    private DelegateCommand saveCommand;

   
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

    public RenamableNotificationObject RenameObject { get; set; }

    public string Name
    {
      get { return RenameObject.Name; }
      set { RenameObject.Name = value; }
    }

    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
