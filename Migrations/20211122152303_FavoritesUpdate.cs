using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfChef.Migrations
{
    public partial class FavoritesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    AuthorID = table.Column<string>(nullable: false),
                    RecipeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Favorites_PK", x => new { x.AuthorID, x.RecipeID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");
        }
    }
}
