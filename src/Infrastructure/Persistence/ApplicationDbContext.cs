using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;
using Solar.Heliac.Application.Common.Interfaces;
using Solar.Heliac.Domain.Common.Interfaces;
using Solar.Heliac.Domain.Heroes;
using Solar.Heliac.Domain.Teams;
using System.Reflection;

namespace Solar.Heliac.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Hero> Heroes => AggregateRootSet<Hero>();

    public DbSet<Team> Teams => AggregateRootSet<Team>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ConfigureSmartEnum();

        base.OnModelCreating(modelBuilder);
    }

    private DbSet<T> AggregateRootSet<T>() where T : class, IAggregateRoot => Set<T>();
}