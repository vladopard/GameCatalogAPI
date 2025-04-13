using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCatalogAPI.Entities
{
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public DateOnly Founded { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; } = string.Empty;
        public ICollection<Game> Games { get; set; }
            = new List<Game>();
    }
}
