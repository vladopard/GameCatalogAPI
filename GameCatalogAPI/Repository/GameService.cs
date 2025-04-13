using AutoMapper;
using GameCatalogAPI.DTOS;
using GameCatalogAPI.Entities;
using GameCatalogAPI.Helpers;
using GameCatalogAPI.QueryParameters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace GameCatalogAPI.Repository
{
    public class GameService : IGameService
    {
        public IGameRepository _gameRepo;
        public IMapper _mapper;

        public GameService(IGameRepository gameRepo, IMapper mapper)
        {
            _gameRepo = gameRepo;
            _mapper = mapper;
        }

        public async Task<MyPagedResult<GameDTO>> GetAllGames(
            GameQueryParameters query)
        {
            var pagedEntities = await _gameRepo.GetAllGamesAsync(query);

            var pagedDtos = new MyPagedResult<GameDTO>(
                _mapper.Map<IEnumerable<GameDTO>>(pagedEntities.Items),
                pagedEntities.TotalCount,
                pagedEntities.PageNumber,
                pagedEntities.PageSize
            );

            return pagedDtos;
        }

        public async Task<GameDTO?> GetSingleGame(int gameId)
        {
            var gameEntity = await _gameRepo.GetGameAsync(gameId);
            return gameEntity == null ? null : _mapper.Map<GameDTO>(gameEntity);
        }

        public async Task<GameDTO?> CreateGameForDeveloper(
            CreateGameForDeveloperDTO newGameForDevDTO, int developerId)
        {
            var developer = await _gameRepo.GetDeveloperAsync(developerId);
            if (developer == null) return null;

            var newGameEntity = _mapper.Map<Game>(newGameForDevDTO);
            newGameEntity.DeveloperId = developerId;

            var addedGameEntity = await _gameRepo.AddGameAsync(newGameEntity);
            return _mapper.Map<GameDTO>(addedGameEntity);

        }

        public async Task<bool> UpdateGame(int id,
            GameUpdateDTO gameUpdateDTO)
        {
            var gameEntity = await _gameRepo.GetGameAsync(id);
            if (gameEntity == null) return false;

            var dev = await _gameRepo.GetDeveloperAsync(gameUpdateDTO.DeveloperId);
            if (dev == null) return false;

            _mapper.Map(gameUpdateDTO, gameEntity);
            return await _gameRepo.SaveChangesAsync();
        }

        public async Task<bool> PartiallyUpdateGame(int id,
            GameUpdateDTO patchedGameDTO)
        {
            var gameEntity = await _gameRepo.GetGameAsync(id);
            _mapper.Map(patchedGameDTO, gameEntity);
            return await _gameRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteGame(int id)
        {
            var game = await _gameRepo.GetGameAsync(id);
            if (game == null) return false;
            return await _gameRepo.DeleteGameAsync(id);
        }
    }
}
