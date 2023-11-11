namespace PersonReplations.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
  IPersonRepository PersonRepository { get; }
  Task SaveChangesAsync();
}
