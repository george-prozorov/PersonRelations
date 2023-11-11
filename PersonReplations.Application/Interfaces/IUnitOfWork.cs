namespace PersonReplations.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
  IPersonRepository personRepository { get; }
  Task SaveChangesAsync();
}
