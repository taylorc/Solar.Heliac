namespace Solar.Heliac.Application.Features.Teams.Queries.GetAllTeams;

public class TeamDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}