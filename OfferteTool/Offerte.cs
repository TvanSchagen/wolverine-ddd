namespace OfferteTool;

public class Offerte
{
    public Offerte(string offertenummer, string relatieEmail)
    {
        Offertenummer = offertenummer;
        RelatieEmail = relatieEmail;
    }

    public Offerte() { }
    
    public string Offertenummer { get; }
    
    public string RelatieEmail { get; }
}