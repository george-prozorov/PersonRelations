using PersonReplations.Application.Features.PersonsFeatures.Models;

namespace PersonReplations.Application.Features.PersonFeatures.Models;

public class GetStatisticsResponse : PaginationInfo
{
  public IEnumerable<StatisticsListItem> Stats { get; set; } = new List<StatisticsListItem>();
}
