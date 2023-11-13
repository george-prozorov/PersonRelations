using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Features.PersonsFeatures.Models
{
  public class StatisticsListItem
  {
    public string Person { get; set; } = string.Empty;
    public string RelationType { get; set; } = string.Empty;
    public int Count { get; set; }

  }
}
