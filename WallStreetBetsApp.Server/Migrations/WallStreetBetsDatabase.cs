using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace WallStreetBetsApp.Server.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class WallStreetBetsDatabase : Migration
    {
        private const string favoriteTable = "favorites";
        private const string noteTable = "notes";
        private const string userTable = "users";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: favoriteTable,
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey($"PK_{favoriteTable}", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: noteTable,
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    favorite_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey($"PK_{noteTable}", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: userTable,
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey($"PK_{$"PK_{favoriteTable}"}", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: favoriteTable);

            migrationBuilder.DropTable(
                name: noteTable);

            migrationBuilder.DropTable(
                name: userTable);
        }
    }
}
