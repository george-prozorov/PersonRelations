using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Features.PersonsFeatures.Models
{
  public class GetPersonsResponse
  {
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PersonalId { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
  }
}
