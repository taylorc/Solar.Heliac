using Ardalis.Specification;

namespace Solar.Heliac.Domain.Heroes;

public sealed class HeroByIdSpec : SingleResultSpecification<Hero>
{
    public HeroByIdSpec(HeroId heroId)
    {
        Query.Where(t => t.Id == heroId);
    }
}