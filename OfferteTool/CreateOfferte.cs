using System.Net.Mail;
using System.Security.Cryptography;
using Contracts;
using FluentValidation;
using Wolverine;
using Wolverine.Http;

namespace OfferteTool;

public record CreateOfferteCommand(string RelatieEmail);

public record OfferteCreated(string Offertenummer, string RelatieEmail);

public record OfferteReminder(string Offertenummer);

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

public static class OfferteCreatedHandler
{
    public static void Handle(OfferteAangemaakt message, MailService mailService, IMessageContext messageBus)
    {
        mailService.Send(new MailMessage(
            "wolverine@local",
            "t.van.schagen@surebusiness.nl",
            "Nieuwe offerte aangemaakt", 
            $"Er is een offerte aangemaakt met offertenummer {message.Offertenummer} voor relatie {message.RelatieEmail}."));
    }
}