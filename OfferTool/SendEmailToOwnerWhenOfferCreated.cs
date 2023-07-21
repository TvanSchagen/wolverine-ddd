using System.Net.Mail;

namespace OfferTool;

public static class SendEmailToOwnerWhenOfferCreatedHandler
{
    public static void Handle(OfferCreated message, MailService mailService)
    {
        mailService.Send(new MailMessage(
            "wolverine@local",
            "owner@example.org",
            "New offer made", 
            $"An offer with number {message.OfferNumber} was made for {message.EmailAddress}."));
    }
}