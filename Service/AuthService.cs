

using ReminderApp.Common.Contracts;
using ErrorOr;
using ReminderApp.Common.Contracts.Auth;
using Microsoft.AspNetCore.Identity;
using ReminderApp.DataAccess.Entity;
using ReminderApp.DataAccess;
using ReminderApp.Common.Utilities;

namespace ReminderApp.Service;

public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
 DatabaseContext databaseContext, ITokenGenerator tokenGenerator) : IAuthService
{

    private readonly SignInManager<AppUser> _siginManager = signInManager;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly DatabaseContext _dbContext = databaseContext;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
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

    public async Task<ErrorOr<bool>> Register(RegisterUserRequest request)
    {
        try
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is not null)
            {
                return Error.Conflict("Email already registered");
            }

            var result = await _userManager.CreateAsync(new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role
            }, request.Password);

            if (result.Succeeded)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return Error.Failure("Error, could not add user");
            }
        }
        catch (Exception)
        {
            return Error.Unexpected();
        }

    }
}