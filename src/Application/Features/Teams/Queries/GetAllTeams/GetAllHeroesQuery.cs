using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Solar.Heliac.Application.Common.Interfaces;

namespace Solar.Heliac.Application.Features.Teams.Queries.GetAllTeams;

public record GetAllTeamsQuery : IRequest<IReadOnlyList<TeamDto>>;

public sealed class GetAllTeamsQueryHandler(
    IMapper mapper,
    IApplicationDbContext dbContext) : IRequestHandler<GetAllTeamsQuery, IReadOnlyList<TeamDto>>
{
    public async Task<IReadOnlyList<TeamDto>> Handle(
        GetAllTeamsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .ProjectTo<TeamDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}