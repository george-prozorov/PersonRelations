﻿using PersonReplations.Application.Interfaces;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace PersonReplations.Persistence;

public class UnitOfWork : IUnitOfWork
{
  private readonly PersonRelationsDbContext _db;

  public IPersonRepository PersonRepository { get; }

  IPersonRepository IUnitOfWork.personRepository => throw new NotImplementedException();

  public UnitOfWork(PersonRelationsDbContext db, IPersonRepository personRepository)
  {
    _db = db;
    PersonRepository = personRepository;
  }

  public Task SaveChangesAsync()
  {
    return _db.SaveChangesAsync();
  }

  public void Dispose()
  {
    _db.Dispose();
  }
}
