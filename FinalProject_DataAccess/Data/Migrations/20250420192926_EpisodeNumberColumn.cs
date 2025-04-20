using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EpisodeNumberColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Video",
                table: "TVShows");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeNumber",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeNumber",
                table: "Episodes");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "TVShows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
