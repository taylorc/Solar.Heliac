using Ardalis.Specification;

namespace Solar.Heliac.Domain.Teams;

public sealed class TeamByIdSpec : SingleResultSpecification<Team>
{
    public TeamByIdSpec(TeamId teamId)
    {
        Query.Where(t => t.Id == teamId)
            .Include(t => t.Missions)
            .Include(t => t.Heroes);
    }
}