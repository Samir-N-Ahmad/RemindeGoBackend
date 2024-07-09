

using ReminderApp.Common.Contracts;
using ErrorOr;
using ReminderApp.Common.Contracts.Auth;
namespace ReminderApp.Service;

public interface IAuthService
{
    public Task<ErrorOr<bool>> Register(RegisterUserRequest request);
    public Task<ErrorOr<LoginUserResult>> Login(LoginRequest request);
}