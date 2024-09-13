using Ardalis.SmartEnum;
using System.Runtime.CompilerServices;

namespace Solar.Heliac.Domain.Installation;
public class InstallationStatus : SmartEnum<InstallationStatus>
{
    public static readonly InstallationStatus Applied = new InstallationStatus(1);
    public static readonly InstallationStatus ReadyForReview = new InstallationStatus(2);
    public static readonly InstallationStatus ReadyForInstallation = new InstallationStatus(3);
    public static readonly InstallationStatus Installed = new InstallationStatus(4);

    public InstallationStatus(int value, [CallerMemberName] string? name = null) : base(name, value)
    {
    }
}
