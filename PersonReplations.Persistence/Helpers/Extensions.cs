namespace PersonReplations.Persistence.Helpers;

internal static class Extensions
{
  internal static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
  {
    int toSkip = (pageNumber - 1) * pageSize;
    return query.Skip(toSkip).Take(pageSize);
  }
}
