using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSQLMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Founded = table.Column<DateOnly>(type: "date", nullable: false),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Genre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Platform = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    DeveloperId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Country", "Founded", "Name" },
                values: new object[,]
                {
                    { 1, "USA", new DateOnly(1991, 1, 1), "Epic Games" },
                    { 2, "Japan", new DateOnly(1889, 9, 23), "Nintendo" },
                    { 3, "Poland", new DateOnly(2002, 5, 1), "CD Projekt Red" },
                    { 4, "USA", new DateOnly(1998, 12, 1), "Rockstar Games" },
                    { 5, "USA", new DateOnly(1996, 8, 24), "Valve" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, 25, "$2a$11$c4FBkI2HLSzuyGLHQ0H98.KQejkPcR9FbKO25Wv9FdfDBQwLhtOCu", 1, "admin" },
                    { 2, 30, "$2a$11$yaPqa1JJSHTuxbP6Jdgw/Oc7aVNT/OLuXGO/yXbnT8m.jBDdq.u82", 0, "covek" },
                    { 3, 16, "$2a$11$c4FBkI2HLSzuyGLHQ0H98.KQejkPcR9FbKO25Wv9FdfDBQwLhtOCu", 1, "keba" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "DeveloperId", "Genre", "Name", "Platform", "Rating", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, 1, "Battle Royale", "Fortnite", "PC", 85, new DateOnly(2017, 7, 25) },
                    { 2, 2, "Action-Adventure", "The Legend of Zelda: Breath of the Wild", "Nintendo Switch", 97, new DateOnly(2017, 3, 3) },
                    { 3, 3, "RPG", "The Witcher 3: Wild Hunt", "PC", 93, new DateOnly(2015, 5, 19) },
                    { 4, 3, "Action RPG", "Cyberpunk 2077", "PC", 75, new DateOnly(2020, 12, 10) },
                    { 5, 4, "Open World", "Grand Theft Auto V", "PC", 96, new DateOnly(2013, 9, 17) },
                    { 6, 4, "Action-Adventure", "Red Dead Redemption 2", "PS4", 97, new DateOnly(2018, 10, 26) },
                    { 7, 5, "FPS", "Half-Life 2", "PC", 96, new DateOnly(2004, 11, 16) },
                    { 8, 5, "Puzzle", "Portal 2", "PC", 95, new DateOnly(2011, 4, 19) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Developers");
        }
    }
}
