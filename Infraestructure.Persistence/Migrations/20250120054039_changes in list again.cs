using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changesinlistagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_ApplicationUser_UserEntityID",
                table: "MovieList");

            migrationBuilder.DropForeignKey(
                name: "FK_Recents_ApplicationUser_UserEntityID",
                table: "Recents");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_ApplicationUser_UserEntityID",
                table: "MovieList",
                column: "UserEntityID",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recents_ApplicationUser_UserEntityID",
                table: "Recents",
                column: "UserEntityID",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_ApplicationUser_UserEntityID",
                table: "MovieList");

            migrationBuilder.DropForeignKey(
                name: "FK_Recents_ApplicationUser_UserEntityID",
                table: "Recents");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_ApplicationUser_UserEntityID",
                table: "MovieList",
                column: "UserEntityID",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recents_ApplicationUser_UserEntityID",
                table: "Recents",
                column: "UserEntityID",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
