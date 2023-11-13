namespace PersonReplations.Application.Features.PersonsFeatures.Models;

public class GetPersonsResponse : PaginationInfo
{
  public IEnumerable<PersonsListItem> Persons { get; set; } = new List<PersonsListItem>();
}
