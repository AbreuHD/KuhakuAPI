using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changesinlistagainx2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_ApplicationUser_UserEntityID",
                table: "MovieList");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_Movie_MovieList_MovieListID",
                table: "MovieList_Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Recents_ApplicationUser_UserEntityID",
                table: "Recents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieList",
                table: "MovieList");

            migrationBuilder.RenameTable(
                name: "MovieList",
                newName: "ShareList");

            migrationBuilder.RenameColumn(
                name: "UserEntityID",
                table: "Recents",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Recents_UserEntityID",
                table: "Recents",
                newName: "IX_Recents_UserID");

            migrationBuilder.RenameColumn(
                name: "UserEntityID",
                table: "ShareList",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MovieList_UserEntityID",
                table: "ShareList",
                newName: "IX_ShareList_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShareList",
                table: "ShareList",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_Movie_ShareList_MovieListID",
                table: "MovieList_Movie",
                column: "MovieListID",
                principalTable: "ShareList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recents_ApplicationUser_UserID",
                table: "Recents",
                column: "UserID",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareList_ApplicationUser_UserID",
                table: "ShareList",
                column: "UserID",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_Movie_ShareList_MovieListID",
                table: "MovieList_Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Recents_ApplicationUser_UserID",
                table: "Recents");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareList_ApplicationUser_UserID",
                table: "ShareList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShareList",
                table: "ShareList");

            migrationBuilder.RenameTable(
                name: "ShareList",
                newName: "MovieList");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Recents",
                newName: "UserEntityID");

            migrationBuilder.RenameIndex(
                name: "IX_Recents_UserID",
                table: "Recents",
                newName: "IX_Recents_UserEntityID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "MovieList",
                newName: "UserEntityID");

            migrationBuilder.RenameIndex(
                name: "IX_ShareList_UserID",
                table: "MovieList",
                newName: "IX_MovieList_UserEntityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieList",
                table: "MovieList",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_ApplicationUser_UserEntityID",
                table: "MovieList",
                column: "UserEntityID",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_Movie_MovieList_MovieListID",
                table: "MovieList_Movie",
                column: "MovieListID",
                principalTable: "MovieList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recents_ApplicationUser_UserEntityID",
                table: "Recents",
                column: "UserEntityID",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
