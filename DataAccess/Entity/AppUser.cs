


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ReminderApp.DataAccess.types;

namespace ReminderApp.DataAccess.Entity;

public class AppUser : IdentityUser
{
    [Required()]
    public required string FirstName { get; set; }
    [Required()]
    public required string LastName { get; set; }

    [EnumDataType(typeof(UserRole))]
    [Required()]
    public required int Role { get; set; }
}