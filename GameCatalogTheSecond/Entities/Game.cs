using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCatalogAPI.Entities
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateOnly ReleaseDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Platform { get; set; } = string.Empty;
        [Required]
        [Range(1,100)]
        public int Rating { get; set; }
        public int DeveloperId { get; set; }

        [ForeignKey("DeveloperId")]
        public Developer Developer { get; set; } = null!;

    }
}
