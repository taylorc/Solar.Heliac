using Solar.Heliac.Domain.UnitTests.Factories;

namespace Solar.Heliac.Domain.UnitTests.Application;
public class ApplicationTests
{
    [Theory]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "cc3431a8-4a31-4f76-af64-e8198279d7a4", false)]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "c8ad9974-ca93-44a5-9215-2f4d9e866c7a", true)]
    public void ApplicationId_ShouldBeComparable(string stringGuid1, string stringGuid2, bool isEqual)
    {
        // Arrange
        Guid guid1 = Guid.Parse(stringGuid1);
        Guid guid2 = Guid.Parse(stringGuid2);
        Domain.Application.ApplicationId id1 = new(guid1);

        // Act
        var areEqual = id1 == new Domain.Application.ApplicationId(guid2);

        // Assert
        areEqual.Should().Be(isEqual);
        id1.Value.Should().Be(guid1);
        new Domain.Application.ApplicationId(guid2).Value.Should().Be(guid2);
    }

    [Fact]
    public void Create_WithValidValues_ShouldSucceed()
    {
        // Act
        var fakeApplication = ApplicationFactory.Build();

        var application = Domain.Application.Application.Create(fakeApplication.Status, fakeApplication.Retailer, fakeApplication.Applicant, fakeApplication.AcceptedTermsAndConditions, fakeApplication.RebateAmount, fakeApplication.RebateType);

        // Assert
        application.AcceptedTermsAndConditions.Should().Be(fakeApplication.AcceptedTermsAndConditions);
        application.Applicant.Should().Be(fakeApplication.Applicant);
        application.Status.Should().Be(fakeApplication.Status);
        application.Retailer.Should().Be(fakeApplication.Retailer);
        application.RebateAmount.Should().Be(fakeApplication.RebateAmount);
        application.RebateType.Should().Be(fakeApplication.RebateType);
    }
}
