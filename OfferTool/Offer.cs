namespace OfferTool;

public class Offer : Entity
{
    public Offer(string offerNumber, string emailAddress)
    {
        OfferNumber = offerNumber;
        EmailAddress = emailAddress;
        
        AddDomainEvent(new OfferCreated(offerNumber, emailAddress));
    }

    public Offer() { }
    
    public string OfferNumber { get; }
    
    public string EmailAddress { get; }
}

public record OfferCreated(string OfferNumber, string EmailAddress);