



using RemindeGo.DataAccess.Entity;

namespace RemindeGo.Common.Utilities;



public interface ITokenGenerator
{
    public string GenerateJWtToken(AppUser user);
}