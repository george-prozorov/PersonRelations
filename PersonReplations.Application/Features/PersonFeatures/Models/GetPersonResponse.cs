namespace PersonReplations.Application.Features.PersonFeatures.Models;

public class GetPersonResponse
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public int GenderId { get; set; }
  public string Gender { get; set; } = string.Empty;
  public string PersonalId { get; set; } = string.Empty;
  public DateTime BirthDate { get; set; }
  public int CityId { get; set; }
  public string City { get; set; } = string.Empty;
  public string? Image { get; set; }

  public IEnumerable<ContactResponse> Contacts { get; set; } = new List<ContactResponse>();
  public IEnumerable<Relative> Relatives { get; set; } = new List<Relative>();

}

public class ContactResponse
{
  public int ContactTypeId { get; set; }
  public string ContactType { get; set; } = string.Empty;
  public string Value { get; set; } = string.Empty;
}

public class Relative
{
  public int RelationTypeId { get; set; }
  public string RelationType { get; set; } = string.Empty;
  public int RelativePersonId { get; set; }
  public string RelativePerson { get; set; } = string.Empty;
}
