using System.Security.Cryptography;
using FluentValidation;
using Wolverine.Http;

namespace OfferTool;

public record CreateOfferCommand(string RelatieEmail);

public class CreateOfferValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferValidator() => RuleFor(c => c.RelatieEmail).NotEmpty().NotNull().EmailAddress();
}

public static class CreateOfferEndpoint
{
    [WolverinePost("/offer")]
    public static CreationResponse CreateOfferHandler(
        CreateOfferCommand command, 
        OfferContext context)
    {
        var rand = RandomNumberGenerator.GetInt32(1_000_000);
        var offernummer = rand.ToString().PadLeft(6, '0');
        var offer = new Offer(offernummer, command.RelatieEmail);
        
        context.Offers.Add(offer);
        
        return new CreationResponse($"/offer/{offernummer}");
    }
}