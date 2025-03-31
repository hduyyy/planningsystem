using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.User.Request
{
    public class CreateUserRequest
    {
       [Required]
    public string Username { get; set; } = null!;

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }

    public string? Fullname { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Role { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
}
}
