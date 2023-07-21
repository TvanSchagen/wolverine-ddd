using Contracts;
using Wolverine;

namespace OfferTool;

public static class PublishIntegrationEventWhenOfferCreatedHandler
{
    public static async Task Handle(OfferCreated message, IMessageContext messaging)
    {
        await messaging.PublishAsync(
            new OfferCreatedIntegrationEvent(message.OfferNumber, message.EmailAddress));
    }
}