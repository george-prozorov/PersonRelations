using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Helpers
{
  public static class ApplicationExtensions
  {
    public static bool IsMature(this DateTime? birthDate)
    {
      if (birthDate == null) return false;
      var dateOfBirth = birthDate.Value;
      int age = 0;
      age = DateTime.Now.Year - dateOfBirth.Year;
      if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
        age = age - 1;

      return age >= 18;
    }
  }
}
