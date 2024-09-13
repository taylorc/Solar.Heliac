using Ardalis.GuardClauses;
using Solar.Heliac.Domain.Common.Base;
using Solar.Heliac.Domain.Contact;

namespace Solar.Heliac.Domain.Application;
public readonly record struct ApplicationId(Guid Value);
public class Application : AggregateRoot<ApplicationId>
{
    public Application()
    {

    }

    public ApplicationStatus Status { get; set; }
    public Contact.Contact Retailer { get; set; }
    public Contact.Contact Applicant { get; set; }
    public bool AcceptedTermsAndConditions { get; set; }
    public decimal RebateAmount { get; set; }
    public RebateType RebateType { get; set; }

    public static Application Create(ApplicationStatus applicationStatus, Contact.Contact retailer, Contact.Contact applicant, bool acceptedTermsAndConditions, decimal rebateAmount, RebateType rebateType)
    {
        var application = new Application { Id = new ApplicationId(Guid.NewGuid()) };

        application.ChangeStatus(applicationStatus);
        application.AddRetailer(retailer);
        application.AddApplication(applicant);
        applicant.AddAcceptedTermsAndConditions(acceptedTermsAndConditions);
        application.AddRebateAmount(rebateAmount);
        application.AddRebateType(rebateType);

        return application;
    }

    public void AddRebateType(RebateType rebateType)
    {
        RebateType = rebateType;
    }

    public void AddRebateAmount(decimal rebateAmount)
    {
        Guard.Against.Null(rebateAmount, nameof(RebateAmount));
        Guard.Against.Negative(rebateAmount, nameof(RebateAmount));

        RebateAmount = rebateAmount;

    }

    public void AddApplication(Contact.Contact applicant)
    {
        Guard.Against.InvalidInput(applicant, nameof(Contact.Contact), x => x.ContactType == ContactType.Customer, "Applicant must be of type Customer");

        Applicant = applicant;
    }

    public void AddRetailer(Contact.Contact retailer)
    {
        Guard.Against.InvalidInput(retailer, nameof(Contact.Contact), x => x.ContactType == ContactType.Retailer, "Retailer must be of type Retailer");

        Retailer = retailer;
    }

    private void ChangeStatus(ApplicationStatus applicationStatus)
    {
        Guard.Against.Null(applicationStatus, nameof(ApplicationStatus));

        if (Status != null)
            Guard.Against.InvalidInput(applicationStatus, nameof(ApplicationStatus), x => x.Value == Status.Value, "Application Status is the same as the current status");

        Status = applicationStatus;
    }
}
