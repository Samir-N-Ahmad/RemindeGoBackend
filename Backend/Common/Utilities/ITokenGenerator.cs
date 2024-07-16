



using Backend.DataAccess.Entity;

namespace Backend.Common.Utilities;



public interface ITokenGenerator
{
    public string GenerateJWtToken(AppUser user);
}