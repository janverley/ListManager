namespace Lms.ViewModel.Infrastructure.RenamableControl
{
  using System;
  using System.Windows.Input;
  using Microsoft.Practices.Prism.ViewModel;
  using Microsoft.Practices.Prism.Commands;
  using Lms.ModelI.Base.Constraint;
  using System.Diagnostics;

  /// <summary>
  /// TODO: A generic ViewModel level object with a Property Name that can be bound to a View 
  /// Properties:
  /// - Name
  /// - IsRenameble
  /// - Default edit text
  /// </summary>
  public class RenamableNotificationObject : NotificationObject
  {
    public RenamableNotificationObject(string name, string defaultEditText, bool isRenamable)
      : this(name, defaultEditText, isRenamable, (_) => true, NoConstraint.Instance)
    { }

    public RenamableNotificationObject(string name, string defaultEditText, bool isRenamable, Func<string, bool> acceptNewName)
      : this(name, defaultEditText, isRenamable, acceptNewName, NoConstraint.Instance)
    { }

    public RenamableNotificationObject(string name, string defaultEditText, bool isRenamable, Func<string, bool> acceptNewName, IConstraint constraint)
    {
      Debug.Assert(constraint.Validate(defaultEditText).IsOk());

      EditName = defaultEditText;
      this.isRenamable = isRenamable;
      this.name = name;
      this.defaultEditText = defaultEditText;
      this.acceptNewName = acceptNewName;
      Constraint = constraint;
    }

    private string defaultEditText;

    public string DefaultEditText
    {
      get
      {
        return defaultEditText;
      }
      set
      {
        if (defaultEditText != value)
        {
          defaultEditText = value;
          RaisePropertyChanged(() => DefaultEditText);
        }
      }
    }

    private string name;

    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        if (value != name)
        {
          name = value;

          RaisePropertyChanged(() => Name);
        }
      }

    }

    private string editName = string.Empty;

    public string EditName
    {
      get { return editName; }
      set
      {
        if (!Equals(editName, value))
        {
          editName = value;
          RaisePropertyChanged(() => EditName);
          RaisePropertyChanged(() => EditIsValid);
          RaisePropertyChanged(() => EditValidationMessage);
        }
      }
    }

    private bool isRenamable;

    public bool IsRenamable
    {
      get
      {
        return isRenamable;
      }
      set
      {
        if (isRenamable != value)
        {
          isRenamable = value;
          RaisePropertyChanged(() => IsRenamable);
        }
      }
    }

    private Func<string, bool> acceptNewName;
    public void AcceptNewName(string newName)
    {
      if (acceptNewName(newName))
      {
        Name = newName;
        DefaultEditText = newName;
      }
      EditName = Name;
    }

    public ICommand StartRenameCmd
    {
      get;
      set;
    }

    public ModelI.Base.Constraint.IConstraint Constraint
    {
      get;
      private set;
    }


    public bool EditIsValid
    {
      get
      {
        return Constraint.Validate(EditName).IsOk();
      }
    }

    public string EditValidationMessage
    {
      get
      {
        return Constraint.Validate(EditName).Message;
      }
    }
  }
}
