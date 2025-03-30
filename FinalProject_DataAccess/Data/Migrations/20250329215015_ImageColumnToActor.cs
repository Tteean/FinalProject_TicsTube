using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ImageColumnToActor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Actors");
        }
    }
}
