using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ImageColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieImages");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "Movies",
                newName: "Video");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Movies",
                newName: "VideoUrl");

            migrationBuilder.CreateTable(
                name: "MovieImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieImages_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieImages_MovieId",
                table: "MovieImages",
                column: "MovieId");
        }
    }
}
