using Microsoft.AspNetCore.Http;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Helpers;

public static class ApplicationExtensions
{
  public static bool IsMature(this DateTime? birthDate)
  {
    if (birthDate is null) return false;
    var dateOfBirth = birthDate.Value;
    int age;
    age = DateTime.Now.Year - dateOfBirth.Year;
    if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
      age--;

    return age >= 18;
  }
  public static bool IsImage(this IFormFile file)
  {
    try
    {
      using var image = Image.Load<Rgba32>(file.OpenReadStream());
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }
  public static string GetPersonName(this Person person)
  {
    return string.Concat(person.FirstName, " ", person.LastName);
  }
}
