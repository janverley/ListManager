namespace Lms.ViewModel.Infrastructure.RenamableControl
{
  using Microsoft.Practices.Prism.Commands;
  using Microsoft.Practices.Prism.ViewModel;
  using System.Windows.Input;

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
    {
      this.isRenamable = isRenamable;
      this.name = name;
      this.defaultEditText = defaultEditText;
      AcceptNewNameCmd = new DelegateCommand<string>((newName) => 
      { 
        Name = newName; 
        DefaultEditText = newName; 
      });
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

    private string editingName;

    public string EditName
    {
      get { return editingName; }
      set
      {
        if (!Equals(editingName, value))
        {
          editingName = value;
          RaisePropertyChanged(()=>EditName);
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

    public ICommand AcceptNewNameCmd
    {
      get;
      set;
    }

    public ICommand StartRenameCmd
    {
      get; 
      set;
    }

    public ModelI.Base.Constraint.IConstraint Constraint
    {
      get; 
      set;
    }

    public bool EditIsValid
    {
      get { return Constraint != null ? Constraint.Validate(EditName).IsOk() : true; }
    }

    public string EditValidationMessage
    {
      get { return Constraint != null ? Constraint.Validate(EditName).Message : string.Empty; }
    }
    
    
  }
}
