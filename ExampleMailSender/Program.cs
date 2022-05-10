using ExampleMailSender.Business;
using ExampleMailSender.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

await Host.CreateDefaultBuilder()
    .ConfigureServices(service =>
    {
        service.AddSingleton<IMailSendBusiness, MailSendBusiness>();
        service.AddMassTransit(mt =>
        {
            mt.AddConsumer<MessageMailConsumer>();
            mt.UsingRabbitMq((context, cfg) =>
            {
                //Hangi sunucuya baglanacıgının bilgileri bağlantı bilgileri
                cfg.Host(
                    "localhost",
                    5672,
                    "/",
                    "ExampleMailSender",
                    h =>
                    {
                        h.Username("admin");
                        h.Password("123456");
                    }
                );
                //endpoint ayarları hangi kuyurugu dinleyip nereye göndereyim
                cfg.ReceiveEndpoint("IMessageMail", endpoint =>
                {
                    endpoint.PrefetchCount = 8;//aynıanda  kaç mesaj alıcağı
                    endpoint.ConfigureConsumer<MessageMailConsumer>(context);
                });
            });

        });
    }).ConfigureLogging((hostContext, logging) => { logging.AddConsole(); })
    .Build()
    .RunAsync();