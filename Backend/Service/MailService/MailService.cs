


using ErrorOr;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;
using MySql.Data;
using Newtonsoft.Json.Linq;

namespace Backend.Service.MailService;

public class MailService(IMailjetClient mailjetClient) : IMailService
{
    private readonly IMailjetClient _mailjetClient = mailjetClient;

    public async Task<ErrorOr<bool>> SendMail(string reciever, string sender, string content, string subject)
    {

        MailjetRequest request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
           .Property(Send.FromEmail, sender)
           .Property(Send.FromName, "RemindeGo")
           .Property(Send.Subject, subject)
           .Property(Send.TextPart, "<h3>Dear passenger, welcome to RemindeGo, May the delivery force be with you!")
           .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", reciever}
                 }
                });
        try
        {
            var response = await _mailjetClient.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return Error.Failure($"Could not send email, Error : ${response.GetData()}");

            }
        }
        catch
        {
            return Error.Failure("Unkown error, Could not send email");
        }
        // await _mailjetClient.PostAsync(request)

        // MailjetClient client = new(
        //    "f302f2e1a1d2ad3166fbed14d2fd4995",
        //    "b7b3a6ed0e6150fa58ee792034a385bf");


        //     MailjetRequest request = new()
        //     {
        //         Resource = Send.Resource
        //     };

        //     var email = new TransactionalEmailBuilder()
        //            .WithFrom(new SendContact(sender))
        //            .WithSubject(subject)
        //            .WithHtmlPart(content)
        //            .WithTo(new SendContact(reciever))
        //            .Build();
        //     try
        //     {

        //         var response = await client.SendTransactionalEmailAsync(email);
        //         if (response.Messages.Length >= 1)
        //         {
        //             Console.WriteLine(response.Messages.Aggregate((acc, message) => new MessageResult { Status = acc.Status + message.Status }));
        //         }
        //     }
        //     catch
        //     {

        //     }
        // }
    }
}