using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.DTOS
{
    public class GameUpdateDTO
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
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DeveloperId must be a positive number.")]
        public int DeveloperId { get; set; }
    }
}
