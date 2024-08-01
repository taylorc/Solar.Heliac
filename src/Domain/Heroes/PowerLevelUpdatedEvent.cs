﻿using Ardalis.GuardClauses;
using Solar.Heliac.Domain.Common.Base;
using Solar.Heliac.Domain.Teams;

namespace Solar.Heliac.Domain.Heroes;

public record PowerLevelUpdatedEvent : DomainEvent
{
    public HeroId Id { get; }
    public TeamId? TeamId { get; }
    public string HeroName { get; }
    public int HeroPowerLevel { get; }

    public PowerLevelUpdatedEvent(Hero hero)
    {
        Guard.Against.Null(hero);

        Id = hero.Id;
        TeamId = hero.TeamId;
        HeroName = hero.Name;
        HeroPowerLevel = hero.PowerLevel;
    }
}