using Solar.Heliac.Application.Common.Interfaces;
using Solar.Heliac.Domain.Heroes;

namespace Solar.Heliac.Application.Features.Heroes.Commands.CreateHero;

public sealed record CreateHeroCommand(
    string Name,
    string Alias,
    IEnumerable<CreateHeroPowerDto> Powers) : IRequest<Guid>;

// ReSharper disable once UnusedType.Global
public sealed class CreateHeroCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateHeroCommand, Guid>
{
    public async Task<Guid> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = Hero.Create(request.Name, request.Alias);
        var powers  = request.Powers.Select(p => new Power(p.Name, p.PowerLevel));
        hero.UpdatePowers(powers);

        await dbContext.Heroes.AddAsync(hero, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return hero.Id.Value;
    }
}