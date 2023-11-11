using Microsoft.EntityFrameworkCore;
using PersonReplations.Domain.Entities;
using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Persistence;

public class PersonRelationsDbContext : DbContext
{
  public DbSet<Person> Persons { get; set; }
  public DbSet<Contact> Contacts { get; set; }
  public DbSet<PersonRelation> PersonRelations { get; set; }
  public DbSet<Relation> Relations { get; set; }
  public DbSet<Gender> Genders { get; set; }
  public DbSet<City> Cities { get; set; }
  public DbSet<ContactType> ContactTypes { get; set; }
  public DbSet<RelationType> RelationTypes { get; set; }


  public PersonRelationsDbContext(DbContextOptions<PersonRelationsDbContext> options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonRelationsDbContext).Assembly);
  }
}
