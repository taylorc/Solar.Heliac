
using Bogus;

namespace Solar.Heliac.Domain.UnitTests.Factories;
internal class ApplicationFactory
{
    internal static Domain.Application.Application Build()
    {
        var rebateTypes = Domain.Application.RebateType.List;
        var applicationStatuses = Domain.Application.ApplicationStatus.List;

        var applicant = ContactFactory.Build();
        applicant.AddContactType(Contact.ContactType.Customer);

        var retailer = ContactFactory.Build();
        retailer.AddContactType(Contact.ContactType.Retailer);

        // Arrange
        var faker = new Faker<Domain.Application.Application>("en_AU")
            .RuleFor(x => x.Id, faker => new Domain.Application.ApplicationId(Guid.NewGuid()))
            .RuleFor(x => x.Status, faker => faker.PickRandom<Domain.Application.ApplicationStatus>(applicationStatuses))
            .RuleFor(x => x.AcceptedTermsAndConditions, faker => faker.Random.Bool())
            .RuleFor(x => x.Applicant, applicant)
            .RuleFor(x => x.Retailer, retailer)
            .RuleFor(x => x.RebateAmount, faker => faker.Random.Decimal(0.01m, 1000))
            .RuleFor(x => x.RebateType, faker => faker.PickRandom<Domain.Application.RebateType>(rebateTypes));


        var fakeApplication = faker.Generate();


        return fakeApplication;
    }
}
