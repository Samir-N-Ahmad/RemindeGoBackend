

using RemindeGo.Common.Contracts;
using ErrorOr;
using RemindeGo.Common.Contracts.Auth;
namespace RemindeGo.Service;

public interface IAuthService
{
    public Task<ErrorOr<bool>> Register(RegisterUserRequest request);
    public Task<ErrorOr<LoginUserResult>> Login(LoginRequest request);
    public Task<ErrorOr<bool>> OtpVerification(OtpVerificationRequest request);
    public Task<ErrorOr<string>> ResendVerificationEmail(string email);
}