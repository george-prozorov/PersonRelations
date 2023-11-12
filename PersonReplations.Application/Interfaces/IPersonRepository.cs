﻿using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Interfaces;

public interface IPersonRepository
{
  Task AddAsync<T>(T entity) where T : class;
  Task<T?> GetByIdAsync<T>(int id) where T : class;
  Task<Person> GetUserForUpdate(int id);
}
