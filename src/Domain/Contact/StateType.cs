using Ardalis.SmartEnum;
using System.Runtime.CompilerServices;

namespace Solar.Heliac.Domain.Contact;
public class StateType : SmartEnum<StateType, string>

{
    public static readonly StateType Victoria = new StateType("VIC", nameof(Victoria));
    public static readonly StateType Queensland = new StateType("QLD", nameof(Queensland));
    public static readonly StateType NewSouthWales = new StateType("NSW", nameof(NewSouthWales));
    public static readonly StateType Tasmania = new StateType("TAS", nameof(Tasmania));
    public static readonly StateType SouthAustralia = new StateType("SA", nameof(SouthAustralia));
    public static readonly StateType WesternAustralia = new StateType("WA", nameof(WesternAustralia));
    public static readonly StateType NorthernTerritory = new StateType("NT", nameof(NorthernTerritory));
    public static readonly StateType AustralianCapitalTerritory = new StateType("AustralianCapitalTerritory", nameof(AustralianCapitalTerritory));

    public StateType(string value, [CallerMemberName] string? name = null) : base(name, value)
    {
    }
}
