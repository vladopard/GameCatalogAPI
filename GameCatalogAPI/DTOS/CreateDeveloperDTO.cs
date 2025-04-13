using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.DTOS
{
    public class CreateDeveloperDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateOnly Founded { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; } = string.Empty;
    }
}
