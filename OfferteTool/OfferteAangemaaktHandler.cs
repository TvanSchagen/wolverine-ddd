using System.Net.Mail;

namespace OfferteTool;

public static class OfferteAangemaaktHandler
{
    public static void Handle(OfferteAangemaakt message, MailService mailService)
    {
        mailService.Send(new MailMessage(
            "wolverine@local",
            "t.van.schagen@surebusiness.nl",
            "Nieuwe offerte aangemaakt", 
            $"Er is een offerte aangemaakt met offertenummer {message.Offertenummer} voor relatie {message.RelatieEmail}."));
    }
}