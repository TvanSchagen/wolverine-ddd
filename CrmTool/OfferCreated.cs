using Contracts;

namespace CrmTool;

public static class OfferCreatedHandler
{
    public static void Handle(OfferCreatedIntegrationEvent @event, ILogger logger)
    {
        logger.LogInformation("Recevied offer {Offernummer}", @event.Offernummer);
        
        // we could save the fact that the offer was made to our CRM system
    }
}