using Microsoft.Extensions.Logging;

namespace ExampleMailSender.Business;

public interface IMailSendBusiness
{
    Task<bool> SendMail(int id, string email, string host, int port);
}
public class MailSendBusiness : IMailSendBusiness
{
    private readonly ILogger<MailSendBusiness> _logger;

    public MailSendBusiness(ILogger<MailSendBusiness> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendMail(int id, string email, string host, int port)
    {
        _logger.LogInformation("{id} : {email} > {host}:{port} accepted.", id, email, host, port);
        await Task.Delay(200);
        _logger.LogInformation("{id} : {email} > {host}:{port} done.", id, email, host, port);
        return true;
    }
}