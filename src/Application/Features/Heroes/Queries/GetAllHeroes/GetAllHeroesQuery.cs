using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Solar.Heliac.Application.Common.Interfaces;

namespace Solar.Heliac.Application.Features.Heroes.Queries.GetAllHeroes;

public record GetAllHeroesQuery : IRequest<IReadOnlyList<HeroDto>>;

public sealed class GetAllHeroesQueryHandler(
    IMapper mapper,
    IApplicationDbContext dbContext) : IRequestHandler<GetAllHeroesQuery, IReadOnlyList<HeroDto>>
{
    public async Task<IReadOnlyList<HeroDto>> Handle(
        GetAllHeroesQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Heroes
            .ProjectTo<HeroDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}