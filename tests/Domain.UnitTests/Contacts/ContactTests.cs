using Solar.Heliac.Domain.Contact;
using Solar.Heliac.Domain.UnitTests.Factories;

namespace Solar.Heliac.Domain.UnitTests.Contacts;
public class ContactTests
{
    private const string Phone = "0397821456";

    [Theory]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "cc3431a8-4a31-4f76-af64-e8198279d7a4", false)]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "c8ad9974-ca93-44a5-9215-2f4d9e866c7a", true)]
    public void ContactId_ShouldBeComparable(string stringGuid1, string stringGuid2, bool isEqual)
    {
        // Arrange
        Guid guid1 = Guid.Parse(stringGuid1);
        Guid guid2 = Guid.Parse(stringGuid2);
        ContactId id1 = new(guid1);
        ContactId id2 = new(guid2);

        // Act
        var areEqual = id1 == id2;

        // Assert
        areEqual.Should().Be(isEqual);
        id1.Value.Should().Be(guid1);
        id2.Value.Should().Be(guid2);
    }

    [Fact]
    public void Create_WithValidValues_ShouldSucceed()
    {
        // Act
        //(01) 05195606 failed
        var fakeContact = ContactFactory.Build();

        Contact.Contact contact = Contact.Contact.Create(fakeContact.FirstName, fakeContact.LastName, fakeContact.Email, Phone, fakeContact.AddressLine1, fakeContact.AddressLine2, fakeContact.City, fakeContact.PostCode, fakeContact.State, fakeContact.ContactType);


        // Assert
        contact.Should().NotBeNull();
        contact.Email.Should().Be(fakeContact.Email);
        contact.FirstName.Should().Be(fakeContact.FirstName);
        contact.LastName.Should().Be(fakeContact.LastName);
        contact.Phone.Should().Be(Phone);
        contact.AddressLine1.Should().Be(fakeContact.AddressLine1);
        contact.AddressLine2.Should().Be(fakeContact.AddressLine2);
        contact.City.Should().Be(fakeContact.City);
        contact.PostCode.Should().Be(fakeContact.PostCode);
        contact.State.Should().Be(fakeContact.State);
        contact.ContactType.Should().Be(fakeContact.ContactType);

        contact.FullName.Should().Be($"{contact.FirstName} {contact.LastName}");
    }

    

    [Theory]
    [InlineData("Mia_Heller71@yahoo.com")]
    [InlineData("Lincoln_Moore@yahoo.com")]
    [InlineData("Lincoln_Moore+deeca@yahoo.com")]
    public void Contact_IsValidEmail_ShouldSucceed(string email)
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddEmail(email);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData("Mia_Heller71yahoo.com")]
    [InlineData("Lincoln_Moore//yahoo.com")]
    public void Contact_IsNotValidEmail_ShouldNotSucceed(string email)
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddEmail(email);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Email is not valid (Parameter 'email')");
    }

    [Fact]
    public void Contact_IsNullEmail_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddEmail("");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Required input email was empty. (Parameter 'email')");
    }

    [Fact]
    public void Contact_IsNullFirstName_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddFirstName("");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Required input FirstName was empty. (Parameter 'firstName')");
    }
    
    [Fact]
    public void Contact_IsNullLastName_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddLastName("");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Required input LastName was empty. (Parameter 'lastName')");
    }
    
    [Fact]
    public void Contact_IsNullPhone_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddPhone("");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Required input Phone was empty. (Parameter 'Phone')");
    }

    [Fact]
    public void Contact_IsNullAddress_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddAddress("", "", "", "", StateType.Victoria);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Required input addressLine1 city postCode were empty.");

    }


}
