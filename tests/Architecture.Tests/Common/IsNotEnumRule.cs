using Mono.Cecil;
using NetArchTest.Rules;

namespace Solar.Heliac.Architecture.UnitTests.Common;

public class IsNotEnumRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => !type.IsEnum;
}