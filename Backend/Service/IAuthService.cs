

using Backend.Common.Contracts;
using ErrorOr;
using Backend.Common.Contracts.Auth;
namespace Backend.Service;

public interface IAuthService
{
    public Task<ErrorOr<bool>> Register(RegisterUserRequest request);
    public Task<ErrorOr<LoginUserResult>> Login(LoginRequest request);
    public Task<ErrorOr<bool>> OtpVerification(OtpVerificationRequest request);
    public Task<ErrorOr<string>> ResendVerificationEmail(string email);
}