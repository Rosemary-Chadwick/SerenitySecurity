namespace Serenity_Security.Models.DTOs;

public class UserProfileDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool? IsAdmin { get; set; }
}
