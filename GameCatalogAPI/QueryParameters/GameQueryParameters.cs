namespace GameCatalogAPI.QueryParameters
{
    public class GameQueryParameters
    {
        private const int _maxPageSize = 10;
        public string? Genre { get; set; }
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";
    }
}
