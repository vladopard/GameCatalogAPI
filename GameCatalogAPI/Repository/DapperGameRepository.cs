using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using Dapper;
using GameCatalogAPI.Entities;
using GameCatalogAPI.Helpers;
using GameCatalogAPI.QueryParameters;

namespace GameCatalogAPI.Repository
{
    public class DapperGameRepository : IGameRepository
    {
        private readonly IDbConnection _db;

        public DapperGameRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Developer?> AddDeveloperAsync(Developer? developer)
        {
            if (developer == null) return null;

            var sql = @"INSERT INTO Developers (Name, Founded, Country)
                        VALUES (@Name, @Founded, @Country);
                        SELECT last_insert_rowid();";

            var id = await _db.ExecuteScalarAsync<int>(sql, developer);
            developer.Id = id;
            return developer;
        }

        public async Task<Game?> AddGameAsync(Game? game)
        {
            if (game == null) return null;

            //last_insert_rowid()  - gets the ID of the newly inserted row - specific to SQLite
            var sql = @"INSERT INTO Games (Name, Genre, Platform, Rating, ReleaseDate, DeveloperId) 
                        VALUES (@Name, @Genre, @Platform, @Rating, @ReleaseDate, @DeveloperId);
                        SELECT last_insert_rowid();";

            var id = await _db.ExecuteScalarAsync<int>(sql, game);
            game.Id = id;
            return game;
        }

        public async Task<bool> DeleteDeveloperAsync(int id)
        {
            var sql = "DELETE FROM Developers WHERE Id = @Id";
            var affected = await _db.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var sql = "DELETE FROM Games " +
                "WHERE Id = @Id";
            var affected = await _db.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<Developer>> GetAllDevelopersAsync()
        {
            var sql = "SELECT * FROM Developers";
            return await _db.QueryAsync<Developer>(sql);
        }

        public async Task<MyPagedResult<Game>> GetAllGamesAsync(GameQueryParameters query)
        {
            var baseSql = @"SELECT g.*, d.Id as DevId, d.Name as DevName, d.Country, d.Founded
            FROM Games g
            INNER JOIN Developers d on g.DeveloperId = d.Id";

            var countSql = @"SELECT COUNT(*) FROM Games g
                             INNER JOIN Developers d ON g.DeveloperId = d.Id";

            var whereClause = "";
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                whereClause += " WHERE LOWER(g.Name) LIKE @Search OR LOWER(g.Genre) LIKE @Search";
                parameters.Add("@Search", $"%{query.Search.Trim().ToLower()}%");
            }

            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                var clause = "LOWER(g.Genre) = @Genre";
                whereClause += string.IsNullOrWhiteSpace(whereClause) ?
                    $" WHERE {clause}" : $" AND {clause}";
                parameters.Add("@Genre", query.Genre.Trim().ToLower());
            }

            var orderClause = query.OrderBy.ApplyDapperSort();

            // 🧮 Pagination
            var pagedSql = $"{baseSql}{whereClause}{orderClause} LIMIT @PageSize OFFSET @Offset";
            parameters.Add("@PageSize", query.PageSize);
            parameters.Add("@Offset", (query.PageNumber - 1) * query.PageSize);

            var items = await _db.QueryAsync<Game, Developer, Game>(
                pagedSql,
                (g,d) =>
                {
                    g.Developer = d;
                    return g;
                },
                parameters,
                splitOn: "DevId");

            var totalCount = await _db.ExecuteScalarAsync<int>($"{countSql}{whereClause}",
                parameters);

            return new MyPagedResult<Game>(items, totalCount, query.PageNumber, 
                query.PageSize);
        }

        public async Task<IEnumerable<Game>> GetAllGamesWithDevsAsync(GameQueryParameters query)
        {
            //Game → "Fortnite" with Developer → "Epic Games"
            var sql = "SELECT g.*, d.Id as DevId, d.Name as DevName, d.Country, d.Founded" +
                "FROM Games g " +
                "INNER JOIN Developers d on g.DeveloperId = d.id" +
                "WHERE LOWER(g.Name) LIKE @Search or LOWER(G.Genre) LIKE @Search";

            //First part of the row maps to a Game,Second part maps to a Developer
            //The return type is still a Game(after you manually attach the developer)
            
            return await _db.QueryAsync<Game, Developer, Game>(
                sql,
                //This is the mapping lambda
                //Dapper gives you both Game and Developer from each row
                //you set game.Developer = dev manually
                //Then return the game object with its developer info attached
                (game, dev) =>
                {
                    game.Developer = dev;
                    return game;
                },
                //Dapper uses this to know where to “split” the row
                //It looks at all columns in the result:
                //First it maps to Game until it hits "DevId"
                //From "DevId" onward, it maps to Developer
                //So if you don’t set splitOn correctly, Dapper will fail or map incorrectly.
                splitOn: "DevId"
                );


            //a helper class provided by Dapper to pass parameters into SQL queries easily and safely.
            //var parameters = new DynamicParameters();
            //parameters.Add("@Search", $"%{query.Search?.Trim().ToLower()}%");

        }

        public async Task<Developer?> GetDeveloperAsync(int id)
        {
            var sql = "SELECT * FROM Developers WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<Game?> GetGameAsync(int id)
        {
            var sql = @"SELECT g.*, d.Id as DevId, d.Name as DevName, d.Country, d.Founded
                        FROM Games g
                        INNER JOIN Developers d on g.DeveloperId = d.Id
                        WHERE g.Id = @Id";

            var game = await _db.QueryAsync<Game, Developer, Game>(
                sql,
                (g, d) =>
                {
                    g.Developer = d;
                    return g;
                },
                new { Id = id },
                splitOn: "DevId"
                );
            return game.FirstOrDefault();
        }

        public Task<bool> SaveChangesAsync()
        {
            return Task.FromResult(true);
            //When you execute an INSERT, UPDATE, or DELETE with ExecuteAsync()
            //→ the change happens immediately in the database.
            //There’s no context that’s keeping track of what needs to be saved later.
        }
    }
}
