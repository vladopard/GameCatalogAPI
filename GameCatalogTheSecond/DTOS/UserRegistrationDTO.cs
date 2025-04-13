using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.DTOS
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 50 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; } = string.Empty;

    }
}
