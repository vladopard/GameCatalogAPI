using GameCatalogAPI.DTOS;
using GameCatalogAPI.Helpers;
using GameCatalogAPI.QueryParameters;

namespace GameCatalogAPI.Repository
{
    public interface IGameService
    {
        Task<GameDTO?> CreateGameForDeveloper(CreateGameForDeveloperDTO newGameForDevDTO, int developerId);
        Task<MyPagedResult<GameDTO>> GetAllGames(GameQueryParameters query);
        Task<GameDTO?> GetSingleGame(int gameId);
        Task<bool> PartiallyUpdateGame(int id, GameUpdateDTO patchedGameDTO);
        Task<bool> UpdateGame(int id, GameUpdateDTO gameUpdateDTO);
        Task<bool> DeleteGame(int id);
    }
}