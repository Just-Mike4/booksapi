using System.ComponentModel.DataAnnotations;

namespace BooksApi.Api.Models
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "username must be between 3 to 50 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email ddress is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 characters long.")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must have at least one uppercase , one lowercase, one digit, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Psswords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}