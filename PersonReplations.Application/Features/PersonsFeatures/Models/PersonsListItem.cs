namespace PersonReplations.Application.Features.PersonsFeatures.Models;

public class PersonsListItem
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string PersonalId { get; set; } = string.Empty;
  public string Gender { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
}
