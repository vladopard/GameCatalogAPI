using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCatalogAPI.Entities
{
    public enum Role
    {
        User,
        Admin
    }
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public Role Role { get; set; } = Role.User;
        [Required]
        public int Age { get; set; }
    }
}
