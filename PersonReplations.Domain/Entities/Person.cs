using PersonReplations.Domain.Entities.Abstraction;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PersonReplations.Domain.Entities;

public class Person : EntityBase
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public int GenderId { get; set; }
  public Gender? Gender { get; set; }
  public string PersonalId { get; set; } = string.Empty;
  public DateTime BirthDate { get; set; }
  public int CityId { get; set; }
  public City? City { get; set; }
  public string? ImagePath { get; set; }

  public IEnumerable<Contact> Contacts { get; set; } = new List<Contact>();
  public IEnumerable<PersonRelation> PersonRelations { get; set; } = new List<PersonRelation>();

  public override void Deactivate()
  {
    IsActive = false;
    foreach (var contact in Contacts)
      contact.Deactivate();
    var relations = PersonRelations.Select(x => x.Relation);
    foreach (var relation in relations)
      relation!.Deactivate();
  }

  public void Update(string? firstName, string? lastName, int? genderid, string? personalId, DateTime? birthDate, int? cityId)
  {
    if (!string.IsNullOrEmpty(firstName))
      FirstName = firstName;
    if (!string.IsNullOrEmpty(lastName))
      LastName = lastName;
    if (!string.IsNullOrEmpty(personalId))
      PersonalId = personalId;
    if (genderid.HasValue)
      GenderId = genderid.Value;
    if (cityId.HasValue)
      CityId = cityId.Value;
    if (birthDate.HasValue)
      BirthDate = birthDate.Value;
  }

  public void UpdateContacts(IEnumerable<Contact> contacts)
  {
    var newContacts = Contacts.ToList();
    foreach (var contact in newContacts)
    {
      if (!contacts.Contains(contact, new ContactComparer()))
        contact.IsActive = false;
    }
    foreach (var contact in contacts)
    {
      if (!Contacts.Contains(contact, new ContactComparer()))
        newContacts.Add(contact);
    }
    Contacts = newContacts;
  }

  public void UpdateImagePath(string fileName)
  {
    ImagePath = fileName;
  }

  public class ContactComparer : IEqualityComparer<Contact>
  {
    public bool Equals(Contact? x, Contact? y)
    {
      if (x is null || y is null)
        return false;
      return x.ContactTypeId == y.ContactTypeId && x.Value == y.Value;
    }

    public int GetHashCode([DisallowNull] Contact obj)
    {
      throw new NotImplementedException();
    }
  }
}
