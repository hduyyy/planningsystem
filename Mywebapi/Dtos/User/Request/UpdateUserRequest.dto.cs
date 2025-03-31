using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.User.Request
{
    public class UpdateUserRequest
    {
        public string? Username { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        public string? Fullname { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Role { get; set; }

        public int? DepartmentId { get; set; }
    }
}
