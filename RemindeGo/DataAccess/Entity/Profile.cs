

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RemindeGo.DataAccess.Entity;

public class UserProfile
{

    private UserProfile(Guid userId, string? image, string? bio, string? status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Image = image;
        Status = status;
        Bio = bio;
    }

    public static UserProfile New(Guid userId, string? image, string? bio, string? status)
    {

        return new(userId, image, bio, status);
    }

    public Guid Id { get; private set; }

    public string? Image { get; private set; }
    public string? Bio { get; private set; }
    public string? Status { get; private set; }

    public AppUser User { get; private set; }

    [Required]
    public Guid UserId { get; private set; }

    public List<Reminder>? Reminders { get; set; }
}