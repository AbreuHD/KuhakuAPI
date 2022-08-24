using Microsoft.EntityFrameworkCore.Migrations;

namespace K_haku.Infraestructure.Persistence.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuevanaMovies_MovieList_TMDBId",
                table: "CuevanaMovies");

            migrationBuilder.AddForeignKey(
                name: "FK_CuevanaMovies_MovieList_TMDBId",
                table: "CuevanaMovies",
                column: "TMDBId",
                principalTable: "MovieList",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuevanaMovies_MovieList_TMDBId",
                table: "CuevanaMovies");

            migrationBuilder.AddForeignKey(
                name: "FK_CuevanaMovies_MovieList_TMDBId",
                table: "CuevanaMovies",
                column: "TMDBId",
                principalTable: "MovieList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
