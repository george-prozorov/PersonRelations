using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Domain.Entities;

public class Person : EntityBase
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public int GenderId { get; set; }
  public Gender Gender { get; set; } = new();
  public string PersonalId { get; set; } = string.Empty;
  public DateTime BirthDate { get; set; }
  public int CityId { get; set; }
  public City City { get; set; } = new();
  public string ImagePath { get; set; } = string.Empty;

  public IEnumerable<Contact> Contacts { get; set; } = Enumerable.Empty<Contact>();
  public IEnumerable<PersonRelation> PersonRelations { get; set; } = Enumerable.Empty<PersonRelation>();
}
