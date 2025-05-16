using AutoMapper;
using GameCatalogAPI.DTOS;
using GameCatalogAPI.Entities;
using GameCatalogAPI.QueryParameters;
using GameCatalogAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameCatalogAPI.Controllers
{
    [Route("api/games")]
    [ApiController]
    [Authorize(Policy = "AdminOver18")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        public GamesController(IGameService gameService, IMapper mapper) 
        {
            _gameService = gameService;
            _mapper = mapper; 
        }

        [HttpGet(Name = "GetAllGames")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames(
            [FromQuery] GameQueryParameters query)
        {
            var pagedResult = await _gameService.GetAllGames(query);

            var paginationMetadata = new
            {
                totalCount = pagedResult.TotalCount,
                pageSize = pagedResult.PageSize,
                currentPage = pagedResult.PageNumber,
                totalPages = pagedResult.TotalPages
            };

            Response.Headers.Append("X-Pagination",
                JsonConvert.SerializeObject(paginationMetadata));

            return Ok(pagedResult.Items);
        }

        [HttpGet("{gameId}", Name = "GetSingleGame")]
        public async Task<ActionResult<GameDTO>> GetSingleGame(int gameId)
        {
            var gameDTO = await _gameService.GetSingleGame(gameId);
            return gameDTO == null ? NotFound() : Ok(gameDTO);
        }

        [HttpPost("{developerId}")]
        public async Task<ActionResult<GameDTO>> CreateGameForDeveloper(
            CreateGameForDeveloperDTO newGameForDevDTO ,int developerId)
        {
            // REDUNDANT - If the newGameForDevDTO is null
            // (for example, if the request body is empty or malformed),
            // ASP.NET Core will automatically return due to model binding failure. 
            if (newGameForDevDTO == null) return BadRequest("Game information is missing.");

            var createdGameDTO = await
                _gameService.CreateGameForDeveloper(newGameForDevDTO, developerId);
            if(createdGameDTO == null)
                return NotFound($"Developer with ID {developerId} not found.");

            return CreatedAtRoute("GetSingleGame",
                new { gameId = createdGameDTO.Id },
                createdGameDTO);

        }
        //dodaj da user greska vraca 400 i nesto valjda
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame(int id,
            GameUpdateDTO gameUpdateDTO)
        {
            var success = await _gameService.UpdateGame(id, gameUpdateDTO);
            return success ? NoContent() : NotFound();
        }

        //Mapping DTO → DTO (like for PATCH)	✔️ Yes — totally fine
        //Mapping DTO ↔ Entity	❌ No — keep that in service
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateGame(int id,
            JsonPatchDocument<GameUpdateDTO> patchDocument)
        {
            if(patchDocument == null) return BadRequest();

            var gameDTO = await _gameService.GetSingleGame(id);
            if (gameDTO == null) return NotFound($"Game with ID {id} does not exist.");

            var gameForPatching = _mapper.Map<GameUpdateDTO>(gameDTO);

            patchDocument.ApplyTo(gameForPatching, ModelState);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var success = await _gameService.PartiallyUpdateGame(id, gameForPatching);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            var success = await _gameService.DeleteGame(id);
            return success ? NoContent() : NotFound();
        }
        

    }
}
