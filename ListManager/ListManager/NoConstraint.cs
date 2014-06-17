
namespace Lms.ModelI.Base.Constraint
{

  public sealed class NoConstraint : IConstraint
  {

    private NoConstraint()
    {
    }

    public static IConstraint Instance
    {
      get { return Nested.instance; }
    }

    class Nested
    {
      // Explicit static constructor to tell C# compiler
      // not to mark type as beforefieldinit
      static Nested() { }

      internal static readonly NoConstraint instance = new NoConstraint();
    }

    public ValidationResult Validate(object _)
    {
      return ValidationResult.Success;
    }

  }

}
