


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

    }
}