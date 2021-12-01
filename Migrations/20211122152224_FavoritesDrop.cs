using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfChef.Migrations
{
    public partial class FavoritesDrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    RecipeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Favorites_PK", x => new { x.AuthorID, x.RecipeID });
                });
        }
    }
}
