using System.Net.Mail;

namespace OfferTool;

public class MailService
{
    private readonly ILogger _logger;
    private readonly SmtpClient _client;
    private readonly string _path;
    
    public MailService(ILogger<MailService> logger)
    {
        _logger = logger;
        _path = Path.Combine(Path.GetTempPath(), "OfferTool", "Emails");
        
        Directory.CreateDirectory(_path);
        
        _client = new SmtpClient
        {
            DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
            PickupDirectoryLocation = _path
        };
    }

    public void Send(MailMessage message)
    {
        _logger.LogInformation("Writing mailmessage to {Path}", _path);
        
        _client.Send(message);
    }
}