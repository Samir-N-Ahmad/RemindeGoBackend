using RemindeGo.Service.MailService;
using Mailjet.Client;

namespace Tests;

public class MailUnitTests()
{
  [Fact]
  public async void SendMail()
  {
    IMailjetClient client = new MailjetClient("f302f2e1a1d2ad3166fbed14d2fd4995", "b7b3a6ed0e6150fa58ee792034a385bf");
    MailService mailService = new(client);
    var result = await mailService.SendMail(sender: "remindego@remindego.developerdemos.site",
      reciever: "sameer.ahmad.st@gmail.com", subject: "test", content: "Test");
    Assert.False(result.IsError);
  }

}