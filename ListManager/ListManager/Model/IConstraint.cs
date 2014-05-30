
namespace Lms.ModelI.Base.Constraint
{
  public interface IConstraint
  {
    ValidationResult Validate(object value);
  }
}
