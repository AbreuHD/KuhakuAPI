using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShareItemmodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_Movie_ShareList_MovieListID",
                table: "MovieList_Movie");

            migrationBuilder.RenameColumn(
                name: "MovieListID",
                table: "MovieList_Movie",
                newName: "ShareListID");

            migrationBuilder.RenameIndex(
                name: "IX_MovieList_Movie_MovieListID",
                table: "MovieList_Movie",
                newName: "IX_MovieList_Movie_ShareListID");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_Movie_ShareList_ShareListID",
                table: "MovieList_Movie",
                column: "ShareListID",
                principalTable: "ShareList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_Movie_ShareList_ShareListID",
                table: "MovieList_Movie");

            migrationBuilder.RenameColumn(
                name: "ShareListID",
                table: "MovieList_Movie",
                newName: "MovieListID");

            migrationBuilder.RenameIndex(
                name: "IX_MovieList_Movie_ShareListID",
                table: "MovieList_Movie",
                newName: "IX_MovieList_Movie_MovieListID");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_Movie_ShareList_MovieListID",
                table: "MovieList_Movie",
                column: "MovieListID",
                principalTable: "ShareList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
