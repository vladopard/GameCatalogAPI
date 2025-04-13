namespace GameCatalogAPI.DTOS
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
