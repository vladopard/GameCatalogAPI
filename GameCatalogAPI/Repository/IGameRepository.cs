using System.Linq.Dynamic.Core;
using GameCatalogAPI.Entities;
using GameCatalogAPI.Helpers;
using GameCatalogAPI.QueryParameters;
using Microsoft.AspNetCore.JsonPatch;

namespace GameCatalogAPI.Repository
{
    public interface IGameRepository
    {
        Task<MyPagedResult<Game>> GetAllGamesAsync(GameQueryParameters query);
        Task<Game?> GetGameAsync(int id);
        Task<Game?> AddGameAsync(Game? game);
        Task<bool> DeleteGameAsync(int id);
        Task<IEnumerable<Developer>> GetAllDevelopersAsync();
        Task<Developer?> GetDeveloperAsync(int id);
        Task<Developer?> AddDeveloperAsync(Developer? developer);
        Task<bool> DeleteDeveloperAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
