namespace OfferteTool;

public class Offerte : Entity
{
    public Offerte(string offertenummer, string relatieEmail)
    {
        Offertenummer = offertenummer;
        RelatieEmail = relatieEmail;
        
        AddDomainEvent(new OfferteAangemaakt(offertenummer, relatieEmail));
    }

    public Offerte() { }
    
    public string Offertenummer { get; }
    
    public string RelatieEmail { get; }
}

public record OfferteAangemaakt(string Offertenummer, string RelatieEmail);