using Lms.ModelI.Base;
using Lms.ModelI.Base.Constraint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManager.ViewModel
{
  class MyConstraint : IConstraint
  {
    public ValidationResult Validate(object value)
    {
      var s = value as string;
      if (s != null && s.Contains("s"))
      {
        return new ValidationResult(string.Format("{0} cant contain an S",value));
      }
      return ValidationResult.Success;
    }
  }
}
