using Solar.Heliac.Domain.Teams;

namespace Solar.Heliac.Application.Features.Teams.Queries.GetAllTeams;

public class GetAllTeamsMapping : Profile
{
    public GetAllTeamsMapping()
    {
        CreateMap<Team, TeamDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}