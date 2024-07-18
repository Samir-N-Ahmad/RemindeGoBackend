

using Backend.Common.Contracts;
using ErrorOr;
using Backend.Common.Contracts.Auth;
using Microsoft.AspNetCore.Identity;
using Backend.DataAccess.Entity;
using Backend.DataAccess;
using Backend.Common.Utilities;
using Backend.Service.MailService;

namespace Backend.Service;

public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
 DatabaseContext databaseContext, ITokenGenerator tokenGenerator, IMailService mailService) : IAuthService
{

    private readonly SignInManager<AppUser> _siginManager = signInManager;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly DatabaseContext _dbContext = databaseContext;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
    private readonly IMailService _mailService = mailService;
    public async Task<ErrorOr<LoginUserResult>> Login(LoginRequest request)
    {
        try
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is not null)
            {
                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    return new LoginUserResult(
                          _tokenGenerator.GenerateJWtToken(user), "");
                }
            }
            return Error.Conflict("Icorrect credentials");
        }
        catch (Exception)
        {
            return Error.Failure("Login Faild");

        }
    }

    public async Task<ErrorOr<bool>> OtpVerification(OtpVerificationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Error.NotFound("The Sent Email is not used");
        }
        try
        {

            var result = await _userManager.ConfirmEmailAsync(user, request.Otp);
            if (result.Succeeded)
            {
                return true;
            }
            return Error.Failure(result.Errors.Aggregate((acc, err) => new IdentityError() { Description = $"${acc.Description}\n${err.Description}" }).Description);
        }
        catch
        {
            return Error.Unexpected("Error while verfying you account, please try again later");
        }
    }

    public async Task<ErrorOr<bool>> Register(RegisterUserRequest request)
    {
        try
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is not null)
            {
                return Error.Conflict("Email already registered");
            }
            var newUser = new AppUser()
            {
                FirstName = request.FirstName,
                UserName = request.FirstName + request.LastName,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role
            };
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                await _dbContext.SaveChangesAsync();

                if (!newUser.EmailConfirmed)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    await _mailService.SendMail(newUser!.Email!, "remindego@remindego.developerdemos.site", token, "OTP");
                }
                return true;
            }
            else
            {
                return Error.Failure($"Error, could not add user, ${result.Errors.Aggregate((acc, err) => new IdentityError() { Description = acc.Description + err.Description }).Description}");
            }
        }
        catch (Exception)
        {
            return Error.Unexpected();
        }

    }
}