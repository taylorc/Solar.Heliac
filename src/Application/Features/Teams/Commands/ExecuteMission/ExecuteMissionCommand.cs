using Solar.Heliac.Application.Common.Exceptions;
using Solar.Heliac.Application.Common.Interfaces;
using Solar.Heliac.Domain.Teams;

namespace Solar.Heliac.Application.Features.Teams.Commands.ExecuteMission;

public sealed record ExecuteMissionCommand(Guid TeamId, string Description) : IRequest;

// ReSharper disable once UnusedType.Global
public sealed class ExecuteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ExecuteMissionCommand>
{
    public async Task Handle(ExecuteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
        {
            throw new NotFoundException(nameof(Team), teamId);
        }
        
        team.ExecuteMission(request.Description);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

public class ExecuteMissionCommandValidator : AbstractValidator<ExecuteMissionCommand>
{
    public ExecuteMissionCommandValidator()
    {
        RuleFor(v => v.TeamId)
            .NotEmpty();

        RuleFor(v => v.Description)
            .NotEmpty();
    }
}