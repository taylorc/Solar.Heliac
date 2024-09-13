using Ardalis.SmartEnum;
using System.Runtime.CompilerServices;

namespace Solar.Heliac.Domain.Application;

public class ApplicationStatus : SmartEnum<ApplicationStatus>
{
    public static readonly ApplicationStatus Applied = new ApplicationStatus(1);
    public static readonly ApplicationStatus ReadyForReview = new ApplicationStatus(2);
    public static readonly ApplicationStatus Approved = new ApplicationStatus(3);
    public static readonly ApplicationStatus Completed = new ApplicationStatus(4);

    public ApplicationStatus(int value, [CallerMemberName] string? name = null) : base(name, value)
    {
    }
}