


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RemindeGo.DataAccess.types;

namespace RemindeGo.DataAccess.Entity;

public class AppUser : IdentityUser<Guid>
{
    public override Guid Id { get => base.Id; set => base.Id = value; }

    [Required()]
    public required string FirstName { get; set; }
    [Required()]
    public required string LastName { get; set; }

    [EnumDataType(typeof(UserRole))]
    [Required()]
    public required int Role { get; set; }
    public UserProfile UserProfile { get; set; }
}