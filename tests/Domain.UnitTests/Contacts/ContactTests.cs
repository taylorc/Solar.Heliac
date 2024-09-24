using Solar.Heliac.Domain.Contact;
using Solar.Heliac.Domain.UnitTests.Testories;
using System;
using TUnit.Assertions.Extensions;
using TUnit.Assertions.Extensions.Throws;

namespace Solar.Heliac.Domain.UnitTests.Contacts;
public class ContactTests
{
    private const string Phone = "0397821456";

    [Test]
    [Arguments("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "cc3431a8-4a31-4f76-af64-e8198279d7a4", false)]
    [Arguments("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "c8ad9974-ca93-44a5-9215-2f4d9e866c7a", true)]
    public async Task ContactId_ShouldBeComparable(string stringGuid1, string stringGuid2, bool isEqual)
    {
        // Arrange
        Guid guid1 = Guid.Parse(stringGuid1);
        Guid guid2 = Guid.Parse(stringGuid2);
        ContactId id1 = new(guid1);
        ContactId id2 = new(guid2);

        // Act
        var areEqual = id1 == id2;

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(areEqual).IsEqualTo(isEqual);
            await Assert.That(id1.Value).IsEqualTo(guid1);
            await Assert.That(id2.Value).IsEqualTo(guid2);
        }


        
        //areEqual.Should().Be(isEqual);
        //id1.Value.Should().Be(guid1);
        //id2.Value.Should().Be(guid2);
    }

    [Test]
    public async Task Create_WithValidValues_ShouldSucceed()
    {
        // Act
        //(01) 05195606 failed
        var fakeContact = ContactTestory.Build();

        Contact.Contact contact = Contact.Contact.Create(fakeContact.FirstName, fakeContact.LastName, fakeContact.Email, fakeContact.Phone, fakeContact.AddressLine1, fakeContact.AddressLine2, fakeContact.City, fakeContact.PostCode, fakeContact.State, fakeContact.ContactType);


        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(contact).IsNotNull();
            await Assert.That(contact.Email).IsEqualTo(fakeContact.Email);
            await Assert.That(contact.FirstName).IsEqualTo(fakeContact.FirstName);
            await Assert.That(contact.LastName).IsEqualTo(fakeContact.LastName);
            await Assert.That(contact.Phone).IsEqualTo(fakeContact.Phone);
            await Assert.That(contact.AddressLine1).IsEqualTo(fakeContact.AddressLine1);
            await Assert.That(contact.AddressLine2).IsEqualTo(fakeContact.AddressLine2);
            await Assert.That(contact.City).IsEqualTo(fakeContact.City);
            await Assert.That(contact.PostCode).IsEqualTo(fakeContact.PostCode);
            await Assert.That(contact.State).IsEqualTo(fakeContact.State);
            await Assert.That(contact.ContactType).IsEqualTo(fakeContact.ContactType);
            await Assert.That(contact.FullName).IsEqualTo($"{contact.FirstName} {contact.LastName}");


        }
        //contact.Should().NotBeNull();
        //contact.Email.Should().Be(fakeContact.Email);
        //contact.FirstName.Should().Be(fakeContact.FirstName);
        //contact.LastName.Should().Be(fakeContact.LastName);
        //contact.Phone.Should().Be(Phone);
        //contact.AddressLine1.Should().Be(fakeContact.AddressLine1);
        //contact.AddressLine2.Should().Be(fakeContact.AddressLine2);
        //contact.City.Should().Be(fakeContact.City);
        //contact.PostCode.Should().Be(fakeContact.PostCode);
        //contact.State.Should().Be(fakeContact.State);
        //contact.ContactType.Should().Be(fakeContact.ContactType);

        //contact.FullName.Should().Be($"{contact.FirstName} {contact.LastName}");
    }

    

    [Test]
    [Arguments("Mia_Heller71@yahoo.com")]
    [Arguments("Lincoln_Moore@yahoo.com")]
    [Arguments("Lincoln_Moore+deeca@yahoo.com")]
    public async Task Contact_IsValidEmail_ShouldSucceed(string email)
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        contact.AddEmail(email);

        // Assert
        await Assert.That(contact.Email).IsEqualTo(email);
    }

    [Test]
    [Arguments("Mia_Heller71yahoo.com")]
    [Arguments("Lincoln_Moore//yahoo.com")]
    public async Task Contact_IsNotValidEmail_ShouldNotSucceed(string email)
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddEmail(email);

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(() => act()).ThrowsException().OfType<ArgumentException>();
            await Assert.That(() => act()).ThrowsException().With.Message.EqualTo("Email is not valid (Parameter 'email')");
        }
        //    .WithMessage("Email is not valid (Parameter 'email')");
    }

    [Test]
    public async Task Contact_IsNullEmail_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddEmail("");

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(() => act()).ThrowsException().OfType<ArgumentException>();
            await Assert.That(() => act()).ThrowsException().With.Message.EqualTo("Email cannot be a null value (Parameter 'email')");
        }
    }

    [Test]
    public async Task Contact_IsNullFirstName_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddFirstName("");

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(() => act()).ThrowsException().OfType<ArgumentException>();
            await Assert.That(() => act()).ThrowsException().With.Message.EqualTo("First Name cannot be a null value (Parameter 'firstName')");
        }
    }
    
    [Test]
    public async Task Contact_IsNullLastName_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddLastName("");

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(() => act()).ThrowsException().OfType<ArgumentException>();
            await Assert.That(() => act()).ThrowsException().With.Message.EqualTo("Last Name cannot be a null value (Parameter 'lastName')");
        }
    }
    
    [Test]
    public async Task Contact_IsNullPhone_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddPhone("");

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(() => act()).ThrowsException().OfType<ArgumentException>();
            await Assert.That(() => act()).ThrowsException().With.Message.EqualTo("Phone cannot be a null value (Parameter 'phone')");
        }
    }

    [Test]
    public async Task Contact_IsNullAddress_ShouldNotSucceed()
    {
        // Arrange
        var contact = new Contact.Contact();

        // Act
        Action act = () => contact.AddAddress("", "", "", "", StateType.Victoria);

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(() => act()).ThrowsException().OfType<ArgumentException>();
            await Assert.That(() => act()).ThrowsException().With.Message.EqualTo("addressLine1 city postCode were empty.");
        }

    }


}
