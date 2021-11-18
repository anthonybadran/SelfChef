using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfChef.Migrations
{
    public partial class UpdateReviewVotesAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review_Votes",
                columns: table => new
                {
                    ReviewID = table.Column<int>(nullable: false),
                    AuthorID = table.Column<string>(nullable: false),
                    Vote = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Review_Votes_PK", x => new { x.ReviewID, x.AuthorID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review_Votes");
        }
    }
}
