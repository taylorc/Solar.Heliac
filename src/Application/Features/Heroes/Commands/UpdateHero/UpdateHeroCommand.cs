using Microsoft.EntityFrameworkCore;
using Solar.Heliac.Application.Common.Exceptions;
using Solar.Heliac.Application.Common.Interfaces;
using Solar.Heliac.Domain.Heroes;

namespace Solar.Heliac.Application.Features.Heroes.Commands.UpdateHero;

public sealed record UpdateHeroCommand(
    HeroId HeroId,
    string Name,
    string Alias,
    IEnumerable<UpdateHeroPowerDto> Powers) : IRequest<Guid>;

// ReSharper disable once UnusedType.Global
public sealed class UpdateHeroCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateHeroCommand, Guid>
{
    public async Task<Guid> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = await dbContext.Heroes
            .Include(h => h.Powers)
            .FirstOrDefaultAsync(h => h.Id == request.HeroId, cancellationToken);

        if (hero is null)
        {
            throw new NotFoundException(nameof(Hero), request.HeroId);
        }
        
        hero.UpdateName(request.Name);
        hero.UpdateAlias(request.Alias);
        var powers = request.Powers.Select(p => new Power(p.Name, p.PowerLevel));
        hero.UpdatePowers(powers);

        await dbContext.SaveChangesAsync(cancellationToken);

        return hero.Id.Value;
    }
}