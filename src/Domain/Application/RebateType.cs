using Ardalis.SmartEnum;
using System.Runtime.CompilerServices;

namespace Solar.Heliac.Domain.Application;

public class RebateType : SmartEnum<RebateType>
{
    public static readonly RebateType PVOwnerOccupier = new RebateType(1);
    public static readonly RebateType PVLandlord = new RebateType(2);
    public static readonly RebateType HotWaterUpgrade = new RebateType(3);
    public static readonly RebateType BatteryLoan = new RebateType(4);

    public RebateType(int value, [CallerMemberName] string? name = null) : base(name, value)
    {
    }
}