using Microsoft.EntityFrameworkCore;
using Solar.Heliac.Domain.Heroes;
using Solar.Heliac.Domain.Teams;

namespace Solar.Heliac.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Hero> Heroes { get; }
    DbSet<Team> Teams { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}