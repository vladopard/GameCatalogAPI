using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserAgeAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "PasswordHash" },
                values: new object[] { 25, "$2a$11$LyZxtrjPfmnZRr41E.SIEuJS0kpymeaFFaqOudqVWdSJRB.hRICdi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "PasswordHash" },
                values: new object[] { 30, "$2a$11$59p1npv2XBeCR56oyoFqeeqb8Dqf5TXgEmY/3W3VRQF.urC2f/kB6" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "PasswordHash", "Role", "Username" },
                values: new object[] { 3, 16, "$2a$11$LyZxtrjPfmnZRr41E.SIEuJS0kpymeaFFaqOudqVWdSJRB.hRICdi", 1, "keba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$p3GsC2UFjJRChkxWiH1tJ.AZbG7LjJ9oEV0nlys3LbaGfraeGbL3K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$2L.pBk0Q3W/D8pw58NUZqOPpTgtHYEBzhLiOyDbvrMuwuPgxpumXq");
        }
    }
}
