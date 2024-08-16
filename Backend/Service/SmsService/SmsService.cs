
using ErrorOr;
using Mailjet.Client;
using Mailjet.Client.Resources.SMS;
using Microsoft.Extensions.Options;
using Backend.Common.Configs;

namespace Backend.Service.SmsService;

public class SmsService(IMailjetClient mailjetClient, IOptions<MailJetConfigs> mailJetConfigs) : ISmsService
{

    private readonly IMailjetClient _mailjetClient = mailjetClient;
    private readonly IOptions<MailJetConfigs> _mailJetConfigs = mailJetConfigs;
    /// <summary>
    /// Sends an SMS message to the specified recipient.
    /// </summary>
    /// <param name="reciever">The recipient's phone number.</param>
    /// <param name="sender">The sender's phone number.</param>
    /// <param name="content">The content of the SMS message.</param>
    /// <param name="subject">The subject of the SMS message.</param>
    /// <returns>A task that represents the asynchronous operation, containing a boolean indicating whether the SMS was sent successfully.</returns>
    public async Task<ErrorOr<bool>> SendSms(string reciever, string sender, string content, string subject)
    {

        var request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
            .Property(Send.From, sender)
            .Property(Send.To, reciever)
            .Property(Send.Text, content);

        try
        {
            MailjetResponse response = await _mailjetClient.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return Error.Failure(response.GetData().ToString());
            }
        }
        catch (Exception)
        {
            return Error.Failure("SMS Sending Failed");
        }

    }
}