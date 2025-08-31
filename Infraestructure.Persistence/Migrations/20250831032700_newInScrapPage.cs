using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newInScrapPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastScrap",
                table: "ScrapPage");

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "ScrapPage",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastScrapEnd",
                table: "ScrapPage",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastScrapStart",
                table: "ScrapPage",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "ScrapPage");

            migrationBuilder.DropColumn(
                name: "LastScrapEnd",
                table: "ScrapPage");

            migrationBuilder.DropColumn(
                name: "LastScrapStart",
                table: "ScrapPage");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastScrap",
                table: "ScrapPage",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
