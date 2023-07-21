using Microsoft.EntityFrameworkCore;
using Wolverine.Http;

namespace OfferTool;

public record OfferDto(string OfferNumber, string EmailAddress);

public class GetOfferEndpoint
{
    [WolverineGet("/offer/{offernummer}")]
    public static async ValueTask<OfferDto?> Handle(
        string offerNumber,
        OfferContext context,
        CancellationToken ct)
    {
        var offer = await context.Offers
            .FirstOrDefaultAsync(o => o.OfferNumber == offerNumber, ct);

        return offer is not null 
            ? new OfferDto(offer.OfferNumber, offer.EmailAddress) 
            : null;
    }
}