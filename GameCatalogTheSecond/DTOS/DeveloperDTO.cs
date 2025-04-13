namespace GameCatalogAPI.DTOS
{
    public class DeveloperDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly Founded { get; set; }
        public string Country { get; set; } = string.Empty;
        public List<GameDTO> Games { get; set; } = new();
    }
}
