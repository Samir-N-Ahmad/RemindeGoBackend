



using ReminderApp.DataAccess.Entity;

namespace ReminderApp.Common.Utilities;



public interface ITokenGenerator
{
    public string GenerateJWtToken(AppUser user);
}