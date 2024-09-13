using Ardalis.SmartEnum;
using System.Runtime.CompilerServices;

namespace Solar.Heliac.Domain.Contact;
public class ContactType : SmartEnum<ContactType>
{
    public static readonly ContactType Customer = new ContactType(1);
    public static readonly ContactType Retailer = new ContactType(2);
    public static readonly ContactType Installer = new ContactType(3);
    public ContactType(int value, [CallerMemberName] string? name = null) : base(name, value)
    {
    }
}
