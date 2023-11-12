using Microsoft.AspNetCore.Http;

namespace PersonReplations.Application.Helpers;

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
  public static bool IsImage(this IFormFile file)
  {
    try
    {
      using (var image = Image.Load<Rgba32>(file.OpenReadStream()))
      {
        return true;
      }
    }
    catch (Exception)
    {
      return false;
    }
  }
}
