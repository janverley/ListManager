using System;

namespace Lms.ModelI.Base
{
  public enum ValidationState
  {
    Ok,
    Info,
    Warning,
    Failed
  }

  public sealed class ValidationResult
  {
    private const String DefaultDelimiter = ", ";

    public static readonly ValidationResult Success = new ValidationResult(ValidationState.Ok
                                                                           , String.Empty);

    private readonly String _message;
    private readonly ValidationState _state;

    internal ValidationResult(ValidationState state, String message)
    {
      _state = state;
      _message = message;
    }

    public ValidationResult(String message)
      : this(ValidationState.Failed, message)
    { }

    public static ValidationResult Info(String message)
    {
      return new ValidationResult(ValidationState.Info, message);
    }

    public static ValidationResult Warning(String message)
    {
      return new ValidationResult(ValidationState.Warning, message);
    }

    public static ValidationResult Failure(String message)
    {
      return new ValidationResult(ValidationState.Failed, message);
    }

    public ValidationState State
    {
      get { return _state; }
    }

    public String Message
    {
      get { return _message; }
    }

    public bool IsBelow(ValidationState state)
    {
      return (_state < state);
    }

    public bool IsOk()
    {
      return IsBelow(ValidationState.Failed);
    }

    public ValidationResult Merge(ValidationResult other)
    {
      return Merge(other, DefaultDelimiter);
    }

    public ValidationResult Merge(ValidationResult other, String delimiter)
    {
      if (ValidationState.Ok == _state)
      {
        return other; // if this one is ok, return the other
      }
      else if (ValidationState.Ok == other._state)
      {
        return this; // if the other is ok, return this one
      }
      else
      {
        // if both are not OK, return the concatenated messages
        return new ValidationResult(MergedState(State, other.State)
                                    , String.Format("{0}{1}{2}"
                                                    , _message
                                                    , delimiter
                                                    , other._message));
      }
    }

    private static ValidationState MergedState(ValidationState first, ValidationState second)
    {
      return (first > second ? first : second);
    }

    // override object.Equals
    public override string ToString()
    {
      return _message;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as ValidationResult);
    }

    public bool Equals(ValidationResult rhs)
    {
      return (System.Object.ReferenceEquals(this, rhs) ||
              ((null != (object)rhs) &&
               _state.Equals(rhs._state) &&
               _message.Equals(rhs._message)));
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
      return _state.GetHashCode() ^ _message.GetHashCode();
    }

    // override == and !=
    public static bool operator ==(ValidationResult lhs, ValidationResult rhs)
    {
      if (null != (object)lhs)
      {
        return lhs.Equals(rhs);
      }
      else
      {
        return (null == (object)rhs);
      }
    }

    public static bool operator !=(ValidationResult lhs, ValidationResult rhs)
    {
      return !(lhs == rhs);
    }
  }
}
