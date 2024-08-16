

using ErrorOr;

namespace Backend.Service.SmsService;

public interface ISmsService
{

    public Task<ErrorOr<bool>> SendSms(string reciever, string sender, string content, string subject);
}