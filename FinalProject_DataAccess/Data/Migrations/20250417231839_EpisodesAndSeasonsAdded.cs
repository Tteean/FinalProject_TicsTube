using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EpisodesAndSeasonsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Season_SeasonId",
                table: "Episode");

            migrationBuilder.DropForeignKey(
                name: "FK_Season_TVShow_TVShowId",
                table: "Season");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShow_Directors_DirectorId",
                table: "TVShow");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowActor_Actors_ActorId",
                table: "TVShowActor");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowActor_TVShow_TVShowId",
                table: "TVShowActor");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowGenre_Genres_GenreId",
                table: "TVShowGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowGenre_TVShow_TVShowId",
                table: "TVShowGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowLanguage_Languages_LanguageId",
                table: "TVShowLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowLanguage_TVShow_TVShowId",
                table: "TVShowLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowLanguage",
                table: "TVShowLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowGenre",
                table: "TVShowGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowActor",
                table: "TVShowActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShow",
                table: "TVShow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Season",
                table: "Season");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode",
                table: "Episode");

            migrationBuilder.RenameTable(
                name: "TVShowLanguage",
                newName: "TVShowLanguages");

            migrationBuilder.RenameTable(
                name: "TVShowGenre",
                newName: "TVShowGenres");

            migrationBuilder.RenameTable(
                name: "TVShowActor",
                newName: "TVShowActors");

            migrationBuilder.RenameTable(
                name: "TVShow",
                newName: "TVShows");

            migrationBuilder.RenameTable(
                name: "Season",
                newName: "Seasons");

            migrationBuilder.RenameTable(
                name: "Episode",
                newName: "Episodes");

            migrationBuilder.RenameIndex(
                name: "IX_TVShowLanguage_LanguageId",
                table: "TVShowLanguages",
                newName: "IX_TVShowLanguages_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_TVShowGenre_GenreId",
                table: "TVShowGenres",
                newName: "IX_TVShowGenres_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_TVShowActor_ActorId",
                table: "TVShowActors",
                newName: "IX_TVShowActors_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_TVShow_DirectorId",
                table: "TVShows",
                newName: "IX_TVShows_DirectorId");

            migrationBuilder.RenameIndex(
                name: "IX_Season_TVShowId",
                table: "Seasons",
                newName: "IX_Seasons_TVShowId");

            migrationBuilder.RenameIndex(
                name: "IX_Episode_SeasonId",
                table: "Episodes",
                newName: "IX_Episodes_SeasonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowLanguages",
                table: "TVShowLanguages",
                columns: new[] { "TVShowId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowGenres",
                table: "TVShowGenres",
                columns: new[] { "TVShowId", "GenreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowActors",
                table: "TVShowActors",
                columns: new[] { "TVShowId", "ActorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShows",
                table: "TVShows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Seasons_SeasonId",
                table: "Episodes",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_TVShows_TVShowId",
                table: "Seasons",
                column: "TVShowId",
                principalTable: "TVShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowActors_Actors_ActorId",
                table: "TVShowActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowActors_TVShows_TVShowId",
                table: "TVShowActors",
                column: "TVShowId",
                principalTable: "TVShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowGenres_Genres_GenreId",
                table: "TVShowGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowGenres_TVShows_TVShowId",
                table: "TVShowGenres",
                column: "TVShowId",
                principalTable: "TVShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowLanguages_Languages_LanguageId",
                table: "TVShowLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowLanguages_TVShows_TVShowId",
                table: "TVShowLanguages",
                column: "TVShowId",
                principalTable: "TVShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShows_Directors_DirectorId",
                table: "TVShows",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Seasons_SeasonId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_TVShows_TVShowId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowActors_Actors_ActorId",
                table: "TVShowActors");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowActors_TVShows_TVShowId",
                table: "TVShowActors");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowGenres_Genres_GenreId",
                table: "TVShowGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowGenres_TVShows_TVShowId",
                table: "TVShowGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowLanguages_Languages_LanguageId",
                table: "TVShowLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShowLanguages_TVShows_TVShowId",
                table: "TVShowLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_TVShows_Directors_DirectorId",
                table: "TVShows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShows",
                table: "TVShows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowLanguages",
                table: "TVShowLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowGenres",
                table: "TVShowGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowActors",
                table: "TVShowActors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes");

            migrationBuilder.RenameTable(
                name: "TVShows",
                newName: "TVShow");

            migrationBuilder.RenameTable(
                name: "TVShowLanguages",
                newName: "TVShowLanguage");

            migrationBuilder.RenameTable(
                name: "TVShowGenres",
                newName: "TVShowGenre");

            migrationBuilder.RenameTable(
                name: "TVShowActors",
                newName: "TVShowActor");

            migrationBuilder.RenameTable(
                name: "Seasons",
                newName: "Season");

            migrationBuilder.RenameTable(
                name: "Episodes",
                newName: "Episode");

            migrationBuilder.RenameIndex(
                name: "IX_TVShows_DirectorId",
                table: "TVShow",
                newName: "IX_TVShow_DirectorId");

            migrationBuilder.RenameIndex(
                name: "IX_TVShowLanguages_LanguageId",
                table: "TVShowLanguage",
                newName: "IX_TVShowLanguage_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_TVShowGenres_GenreId",
                table: "TVShowGenre",
                newName: "IX_TVShowGenre_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_TVShowActors_ActorId",
                table: "TVShowActor",
                newName: "IX_TVShowActor_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Seasons_TVShowId",
                table: "Season",
                newName: "IX_Season_TVShowId");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episode",
                newName: "IX_Episode_SeasonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShow",
                table: "TVShow",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowLanguage",
                table: "TVShowLanguage",
                columns: new[] { "TVShowId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowGenre",
                table: "TVShowGenre",
                columns: new[] { "TVShowId", "GenreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowActor",
                table: "TVShowActor",
                columns: new[] { "TVShowId", "ActorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Season",
                table: "Season",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode",
                table: "Episode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Season_SeasonId",
                table: "Episode",
                column: "SeasonId",
                principalTable: "Season",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Season_TVShow_TVShowId",
                table: "Season",
                column: "TVShowId",
                principalTable: "TVShow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShow_Directors_DirectorId",
                table: "TVShow",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowActor_Actors_ActorId",
                table: "TVShowActor",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowActor_TVShow_TVShowId",
                table: "TVShowActor",
                column: "TVShowId",
                principalTable: "TVShow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowGenre_Genres_GenreId",
                table: "TVShowGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowGenre_TVShow_TVShowId",
                table: "TVShowGenre",
                column: "TVShowId",
                principalTable: "TVShow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowLanguage_Languages_LanguageId",
                table: "TVShowLanguage",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TVShowLanguage_TVShow_TVShowId",
                table: "TVShowLanguage",
                column: "TVShowId",
                principalTable: "TVShow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
