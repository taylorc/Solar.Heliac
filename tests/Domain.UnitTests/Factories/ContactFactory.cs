using Bogus;
using Solar.Heliac.Domain.Contact;

namespace Solar.Heliac.Domain.UnitTests.Factories;
internal class ContactFactory
{
    public static Contact.Contact Build()
    {
        var stateTypes = StateType.List;
        var contactTypes = ContactType.List;

        // Arrange
        var faker = new Faker<Contact.Contact>("en_AU")
            .RuleFor(x => x.Id, faker => new ContactId(Guid.NewGuid()))
            .RuleFor(x => x.Email, faker => faker.Person.Email)
            .RuleFor(x => x.FirstName, faker => faker.Person.FirstName)
            .RuleFor(x => x.LastName, faker => faker.Person.LastName)
            .RuleFor(x => x.AddressLine1, faker => faker.Address.StreetAddress())
            .RuleFor(x => x.AddressLine2, faker => string.Empty)
            .RuleFor(x => x.City, faker => faker.Address.City())
            .RuleFor(x => x.PostCode, faker => faker.Address.ZipCode("####"))
            .RuleFor(x => x.State, faker => faker.PickRandom<StateType>(stateTypes))
            .RuleFor(x => x.ContactType, faker => faker.PickRandom<ContactType>(contactTypes))
            .RuleFor(x => x.Phone, faker => faker.Phone.PhoneNumberFormat())
            .RuleFor(x => x.FullName, faker => string.Empty)
            .FinishWith((f, u) => Console.WriteLine("Contact Created! Email={0} Phone={1}", u.Email, u.Phone));

        var fakeContact = faker.Generate();


        return fakeContact;
    }
}
