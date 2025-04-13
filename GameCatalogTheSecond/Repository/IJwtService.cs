using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repository
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}