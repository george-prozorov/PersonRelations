﻿using PersonReplations.Application.Interfaces;

namespace PersonReplations.Persistence;

public class UnitOfWork : IUnitOfWork
{
  private readonly PersonRelationsDbContext _db;

  public IPersonRepository PersonRepository { get; }

  public UnitOfWork(PersonRelationsDbContext db, IPersonRepository personRepository)
  {
    _db = db;
    PersonRepository = personRepository;
  }

  public Task SaveChangesAsync()
  {
    return _db.SaveChangesAsync();
  }
}
