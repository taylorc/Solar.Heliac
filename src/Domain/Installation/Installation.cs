using Solar.Heliac.Domain.Application;
using Solar.Heliac.Domain.Common.Base;

namespace Solar.Heliac.Domain.Installation;

public readonly record struct InstallationId(Guid Value);

public class Installation : Entity<InstallationId>
{

    public Installation()
    {

    }

    public Property? Property { get; set; }
    public Contact.Contact Installer { get; set; } = null!;
    public Contact.Contact Owner { get; set; } = null!;
    public InstallationStatus? Status { get; set; }
}
