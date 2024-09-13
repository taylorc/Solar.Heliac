using Ardalis.SmartEnum;
using FluentAssertions;
using NetArchTest.Rules;
using Solar.Heliac.Architecture.UnitTests.Common;
using Solar.Heliac.Domain.Common.Base;
using Solar.Heliac.Domain.Common.Interfaces;
using Xunit.Abstractions;

namespace Solar.Heliac.Architecture.UnitTests;

public class DomainModelTests
{
    private readonly ITestOutputHelper _outputHelper;

    public DomainModelTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void DomainModel_ShouldInheritsBaseClasses()
    {
        // Arrange
        var domainModels = Types.InAssembly(typeof(AggregateRoot<>).Assembly)
            .That()
            .DoNotResideInNamespaceContaining("Common")
            .And().DoNotInherit(typeof(SmartEnum<>))
            .And().DoNotInherit(typeof(SmartEnum<,>))
            .And().DoNotHaveNameEndingWith("Id")
            .And().DoNotHaveNameEndingWith("Spec")
            .And().MeetCustomRule(new IsNotEnumRule());

        // Act
        var result = domainModels
            .Should()
            .Inherit(typeof(AggregateRoot<>))
            .Or().Inherit(typeof(Entity<>))
            .Or().Inherit(typeof(DomainEvent))
            .Or().ImplementInterface(typeof(IValueObject));

        // Assert
        result.GetResult().DumpFailingTypes(_outputHelper);
        result.GetResult().IsSuccessful.Should().BeTrue();
    }
}