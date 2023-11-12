using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Persistence.Helpers
{
  internal static class Extensions
  {
    internal static IQueryable<T> Pagination<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
      int toSkip = (pageNumber - 1) * pageSize;
      return query.Skip(toSkip).Take(pageSize);
    }
  }
}
