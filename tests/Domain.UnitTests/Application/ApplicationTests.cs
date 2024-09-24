using Solar.Heliac.Domain.UnitTests.Testories;

namespace Solar.Heliac.Domain.UnitTests.Application;
public class ApplicationTests
{
    [Test]
    [Arguments("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "cc3431a8-4a31-4f76-af64-e8198279d7a4", false)]
    [Arguments("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "c8ad9974-ca93-44a5-9215-2f4d9e866c7a", true)]
    public async Task ApplicationId_ShouldBeComparable(string stringGuid1, string stringGuid2, bool isEqual)
    {
        // Arrange
        Guid guid1 = Guid.Parse(stringGuid1);
        Guid guid2 = Guid.Parse(stringGuid2);
        Domain.Application.ApplicationId id1 = new(guid1);

        // Act
        var areEqual = id1 == new Domain.Application.ApplicationId(guid2);

        // Assert
        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(areEqual).IsEqualTo(isEqual);
            await Assert.That(id1.Value).IsEqualTo(guid1);
            await Assert.That(new Domain.Application.ApplicationId(guid2).Value).IsEqualTo(guid2);
        }
    }

    [Test]
    public async Task Create_WithValidValues_ShouldSucceed()
    {
        // Act
        var fakeApplication = ApplicationTestory.Build();

        var application = Domain.Application.Application.Create(fakeApplication.Status, fakeApplication.Retailer, fakeApplication.Applicant, fakeApplication.AcceptedTermsAndConditions, fakeApplication.RebateAmount, fakeApplication.RebateType);

        // Assert
        await using (Assert.Multiple())
        {
            await Assert.That(application.AcceptedTermsAndConditions).IsEqualTo(fakeApplication.AcceptedTermsAndConditions);
            await Assert.That(application.Applicant).IsEqualTo(fakeApplication.Applicant);
            await Assert.That(application.Status).IsEqualTo(fakeApplication.Status);
            await Assert.That(application.Retailer).IsEqualTo(fakeApplication.Retailer);
            await Assert.That(application.RebateAmount).IsEqualTo(fakeApplication.RebateAmount);
        }
    }
}
