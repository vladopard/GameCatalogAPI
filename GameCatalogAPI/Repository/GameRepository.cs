using System.Linq.Dynamic.Core;
using GameCatalogAPI.DbContexts;
using GameCatalogAPI.Entities;
using GameCatalogAPI.Helpers;
using GameCatalogAPI.QueryParameters;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogAPI.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly GameCatalogContext _context;

        public GameRepository(GameCatalogContext context)
        {
            _context = context;
        }

        public async Task<Developer?> AddDeveloperAsync(Developer? developer)
        {
            if (developer == null)
                return null;

            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();
            return developer;
        }

        public async Task<Game?> AddGameAsync(Game? game)
        {
            if (game == null) return null;

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> DeleteDeveloperAsync(int id)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null)
                return false;

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var game = await _context.Games.FirstAsync(g => g.Id == id);

            if (game == null)
                return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Developer>> GetAllDevelopersAsync()
        {
            return await _context
                .Developers
                .Include(d => d.Games)
                .ToListAsync();
        }

        public async Task<MyPagedResult<Game>> GetAllGamesAsync(GameQueryParameters query)
        {
            var collection = _context.Games
                .Include(g => g.Developer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var searchTrimmed = query.Search.Trim().ToLower();
                collection = collection.Where(g => g.Name.ToLower().Contains(searchTrimmed) ||
                    g.Genre.ToLower().Contains(searchTrimmed));
            }

            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                var genreTrimmed = query.Genre.Trim().ToLower();
                collection = collection.Where(g => g.Genre.ToLower() == genreTrimmed);
            }

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                try
                {
                    collection = collection.ApplyOrdering(query.OrderBy);
                }
                catch
                {
                    collection = collection.OrderBy(g => g.Name);
                }
            }
            
            var totalCount = await collection.CountAsync();

            var items = await collection
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

            return new MyPagedResult<Game>(items, totalCount, query.PageNumber, query.PageSize);
        }

        public async Task<Developer?> GetDeveloperAsync(int id)
        {
            return await _context.Developers
                .Include(d => d.Games)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Game?> GetGameAsync(int id)
        {
            return await _context.Games
            .Include(g => g.Developer)
            .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }


    }
}
