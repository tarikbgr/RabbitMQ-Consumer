using ExampleMailSender.Business;
using Inbx;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ExampleMailSender.Consumers;

public class MessageMailConsumer : IConsumer<IMessageMail>
{
    private readonly ILogger<MessageMailConsumer> _logger;
    private readonly IMailSendBusiness _mailSendBusiness;

    public MessageMailConsumer(ILogger<MessageMailConsumer> logger, IMailSendBusiness mailSendBusiness)
    {
        _logger = logger;
        _mailSendBusiness = mailSendBusiness;
    }
    public async Task Consume(ConsumeContext<IMessageMail> context)
    {
        var msg = context.Message;

        await _mailSendBusiness.SendMail(msg.Id, msg.Email, msg.Host, msg.Port);

        //DO ANYTHING

        await Task.CompletedTask;
    }
}