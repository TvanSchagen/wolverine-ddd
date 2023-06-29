using Microsoft.EntityFrameworkCore;
using Wolverine.Http;

namespace OfferteTool;

public record OfferteDto(string Offertenummer, string RelatieEmail);

public class GetOfferteEndpoint
{
    [WolverineGet("/offerte/{offertenummer}")]
    public static async ValueTask<OfferteDto?> Handle(
        string offertenumer,
        OfferteContext context, 
        CancellationToken ct)
    {
        var offerte = await context.Offertes
            .FirstOrDefaultAsync(o => o.Offertenummer == offertenumer, ct);

        return offerte is not null 
            ? new OfferteDto(offerte.Offertenummer, offerte.RelatieEmail) 
            : null;
    }
}