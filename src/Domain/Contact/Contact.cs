using Ardalis.GuardClauses;
using Solar.Heliac.Domain.Common.Base;
using Solar.Heliac.Domain.Common.Guards;

namespace Solar.Heliac.Domain.Contact;

public readonly record struct ContactId(Guid Value);
public class Contact : AggregateRoot<ContactId>
{
    public Contact() { }

    public string FirstName { get; private set; } = null!;

    public string Email { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string FullName { get; private set; }
    public string Phone { get; private set; } = null!;
    public string AddressLine1 { get; private set; } = null!;
    public string AddressLine2 { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public StateType State { get; private set; } = null!;
    public string PostCode { get; private set; } = null!;
    public ContactType ContactType { get; private set; }
    public bool AcceptedTermsAndConditions { get; set; }

    public static Contact Create(string firstName, string lastName, string email, string phone, string addressLine1, string addressLine2, string city, string postCode, StateType state, ContactType contactType)
    {
        var contact = new Contact { Id = new ContactId(Guid.NewGuid()) };
        contact.AddPhone(phone);
        contact.AddEmail(email);
        contact.AddFirstName(firstName);
        contact.AddLastName(lastName);
        contact.AddAddress(addressLine1, addressLine2, city, postCode, state);
        contact.AddContactType(contactType);

        return contact;
    }

    public void AddFirstName(string firstName)
    {
        Guard.Against.NullOrEmpty(firstName, nameof(firstName));

        FirstName = firstName;

        if (!string.IsNullOrEmpty(LastName))
        {
            SetFirstName();
        }
    }

    private void SetFirstName()
    {
        FullName = $"{FirstName} {LastName}";
    }

    public void AddLastName(string lastName)
    {
        Guard.Against.NullOrEmpty(lastName, nameof(lastName));
        LastName = lastName;

        if (!string.IsNullOrEmpty(FirstName))
        {
            SetFirstName();
        }
    }

    public void AddEmail(string email)
    {
        Guard.Against.NullOrEmpty(email, nameof(email));
        Guard.Against.InvalidFormat(email, nameof(email), "(?:[a-zA-Z0-9!#$%&'*+\\/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+\\/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])", "Email is not valid");

        Email = email;
    }

    public void AddPhone(string phone)
    {
        //(01) 05195606
        Guard.Against.NullOrEmpty(phone, nameof(phone));
        Guard.Against.InvalidFormat(phone, nameof(phone), "^(?:\\+?(61))? ?(?:\\((?=.*\\)))?(0?[2-57-8])\\)? ?(\\d\\d(?:[- ](?=\\d{3})|(?!\\d\\d[- ]?\\d[- ]))\\d\\d[- ]?\\d[- ]?\\d{3})$", "Phone is not valid");

        Phone = phone;
    }
    public void AddAddress(string addressLine1, string addressLine2, string city, string postCode, StateType state)
    {
        Guard.Against.Address(new Dictionary<string, string> { { nameof(addressLine1), addressLine1 }, { nameof(city), city }, { nameof(postCode), postCode } });

        //Guard.Against.NullOrEmpty(city, nameof(city));
        //Guard.Against.NullOrEmpty(postCode, nameof(postCode));

        AddressLine1 = addressLine1.Trim();
        AddressLine2 = addressLine2.Trim();
        City = city.Trim();
        PostCode = postCode.Trim();
        State = state;

    }

    public void AddContactType(ContactType contactType)
    {
        ContactType = contactType;
    }

    internal void AddAcceptedTermsAndConditions(bool acceptedTermsAndConditions)
    {
        AcceptedTermsAndConditions = acceptedTermsAndConditions;
    }
}
