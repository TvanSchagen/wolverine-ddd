using System.Security.Cryptography;
using FluentValidation;
using Wolverine.Http;

namespace OfferteTool;

public record CreateOfferteCommand(string RelatieEmail);

public class CreateOfferteValidator : AbstractValidator<CreateOfferteCommand>
{
    public CreateOfferteValidator() => RuleFor(c => c.RelatieEmail).NotEmpty().NotNull().EmailAddress();
}

public static class CreateOfferteEndpoint
{
    [WolverinePost("/offerte")]
    public static CreationResponse CreateOfferteHandler(
        CreateOfferteCommand command, 
        OfferteContext context)
    {
        var rand = RandomNumberGenerator.GetInt32(1_000_000);
        var offertenummer = rand.ToString().PadLeft(6, '0');
        var offerte = new Offerte(offertenummer, command.RelatieEmail);
        
        context.Offertes.Add(offerte);
        
        return new CreationResponse($"/offerte/{offertenummer}");
    }
}