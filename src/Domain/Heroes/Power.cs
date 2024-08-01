using Ardalis.GuardClauses;
using Solar.Heliac.Domain.Common.Interfaces;

namespace Solar.Heliac.Domain.Heroes;

public record Power : IValueObject
{
    // Private setters needed for EF
    public string Name { get; private set; }

    // Private setters needed for EF
    public int PowerLevel { get; private set; }

    public Power(string name, int powerLevel)
    {
        Name = name;
        PowerLevel = Guard.Against.OutOfRange(powerLevel, nameof(powerLevel), 1, 10);
    }
}