using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "$2a$11$p3GsC2UFjJRChkxWiH1tJ.AZbG7LjJ9oEV0nlys3LbaGfraeGbL3K", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "$2a$11$2L.pBk0Q3W/D8pw58NUZqOPpTgtHYEBzhLiOyDbvrMuwuPgxpumXq", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Z0.dvtIXfKwvHfTaB/267uib7Jcj1oxEQdr/ArQdOFFxgUgG4PXYe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$AY0J3WlsOJCD8Da0n.r4Zu.sElNGY4DMDvN6deSwNDjQF4O6GJyGm");
        }
    }
}
