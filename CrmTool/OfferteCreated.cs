using Contracts;

namespace CrmTool;

public static class OfferteCreatedHandler
{
    public static void Handle(OfferteCreatedIntegrationEvent @event, ILogger logger)
    {
        // doen alsof we de offerte opslaan achter de relatie ofzo
        logger.LogInformation("Recevied offerte {Offertenummer}", @event.Offertenummer);
    }
}