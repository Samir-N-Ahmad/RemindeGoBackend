using ErrorOr;
namespace RemindeGo.Service.MailService;



public interface IMailService
{
    public Task<ErrorOr<bool>> SendMail(string reciever, string sender, string content, string subject);
}