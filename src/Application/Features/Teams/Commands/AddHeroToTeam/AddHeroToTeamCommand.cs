﻿using Solar.Heliac.Application.Common.Exceptions;
using Solar.Heliac.Application.Common.Interfaces;
using Solar.Heliac.Domain.Heroes;
using Solar.Heliac.Domain.Teams;

namespace Solar.Heliac.Application.Features.Teams.Commands.AddHeroToTeam;

public sealed record AddHeroToTeamCommand(Guid TeamId, Guid HeroId) : IRequest;

// ReSharper disable once UnusedType.Global
public sealed class AddHeroToTeamCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<AddHeroToTeamCommand>
{
    public async Task Handle(AddHeroToTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var heroId = new HeroId(request.HeroId);

        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
        {
            throw new NotFoundException(nameof(Team), teamId);
        }

        var hero = dbContext.Heroes
            .WithSpecification(new HeroByIdSpec(heroId))
            .FirstOrDefault();

        if (hero is null)
        {
            throw new NotFoundException(nameof(Hero), heroId);
        }

        team.AddHero(hero);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

public class AddHeroToTeamCommandValidator : AbstractValidator<AddHeroToTeamCommand>
{
    public AddHeroToTeamCommandValidator()
    {
        RuleFor(v => v.HeroId)
            .NotEmpty();

        RuleFor(v => v.TeamId)
            .NotEmpty();
    }
}