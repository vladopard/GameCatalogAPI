namespace GameCatalogAPI.DTOS
{
    //GET DOESNT NEED VALIDATION
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; } = string.Empty;

    }
}
