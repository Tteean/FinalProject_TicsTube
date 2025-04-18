using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TVShowTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TVShow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectorId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TVShow_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonNumber = table.Column<int>(type: "int", nullable: false),
                    TVShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Season_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TVShowActor",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    TVShowId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowActor", x => new { x.TVShowId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_TVShowActor_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TVShowActor_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TVShowGenre",
                columns: table => new
                {
                    TVShowId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowGenre", x => new { x.TVShowId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_TVShowGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TVShowGenre_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TVShowLanguage",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    TVShowId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowLanguage", x => new { x.TVShowId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_TVShowLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TVShowLanguage_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episode_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episode_SeasonId",
                table: "Episode",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Season_TVShowId",
                table: "Season",
                column: "TVShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShow_DirectorId",
                table: "TVShow",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShowActor_ActorId",
                table: "TVShowActor",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShowGenre_GenreId",
                table: "TVShowGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShowLanguage_LanguageId",
                table: "TVShowLanguage",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episode");

            migrationBuilder.DropTable(
                name: "TVShowActor");

            migrationBuilder.DropTable(
                name: "TVShowGenre");

            migrationBuilder.DropTable(
                name: "TVShowLanguage");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "TVShow");
        }
    }
}
