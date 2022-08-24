using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K_haku.Infraestructure.Persistence.Migrations
{
    public partial class gg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieList",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    adult = table.Column<bool>(type: "bit", nullable: false),
                    backdrop_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    original_language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    original_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    popularity = table.Column<double>(type: "float", nullable: false),
                    poster_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    release_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vote_average = table.Column<double>(type: "float", nullable: false),
                    vote_count = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ScrapPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isOn = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    LastScrap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapPages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CuevanaMovies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    TMDBId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuevanaMovies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CuevanaMovies_MovieList_TMDBId",
                        column: x => x.TMDBId,
                        principalTable: "MovieList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieListID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.id);
                    table.ForeignKey(
                        name: "FK_Genre_MovieList_MovieListID",
                        column: x => x.MovieListID,
                        principalTable: "MovieList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuevanaMovies_TMDBId",
                table: "CuevanaMovies",
                column: "TMDBId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MovieListID",
                table: "Genre",
                column: "MovieListID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuevanaMovies");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "ScrapPages");

            migrationBuilder.DropTable(
                name: "MovieList");
        }
    }
}
