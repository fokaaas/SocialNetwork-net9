using System.ComponentModel.DataAnnotations;

namespace Business.Models.Auth;

public class SignUpModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one number")]
    public string Password { get; set; }

    [Url(ErrorMessage = "Invalid URL format for avatar link")]
    public string? AvatarLink { get; set; }
}