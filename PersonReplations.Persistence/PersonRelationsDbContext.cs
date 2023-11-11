using Microsoft.EntityFrameworkCore;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence;

public class PersonRelationsDbContext : DbContext
{
  public DbSet<Person> Persons { get; set; }
  public DbSet<Contact> Contacts { get; set; }
  public DbSet<PersonRelation> PersonRelations { get; set; }
  public DbSet<Relation> Relations { get; set; }
  public DbSet<Reference> References { get; set; }

  public PersonRelationsDbContext(DbContextOptions<PersonRelationsDbContext> options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonRelationsDbContext).Assembly);
  }
}
