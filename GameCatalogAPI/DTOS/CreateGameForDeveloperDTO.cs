using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.DTOS
{
    public class CreateGameForDeveloperDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        public int Rating { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }
    }
}
