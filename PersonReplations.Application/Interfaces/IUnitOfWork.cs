namespace PersonReplations.Application.Interfaces;

public interface IUnitOfWork
{
  IPersonRepository PersonRepository { get; }
  Task SaveChangesAsync();
}
