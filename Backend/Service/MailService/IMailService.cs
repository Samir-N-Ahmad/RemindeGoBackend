using ErrorOr;
namespace Backend.Service.MailService;



public interface IMailService
{
    public Task<ErrorOr<bool>> SendMail(string reciever, string sender, string content, string subject);
}