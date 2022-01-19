using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SerieMovieAPI.Migrations
{
    public partial class configuramostodaslastablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_Genre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movieseries",
                columns: table => new
                {
                    MovieserieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_Movserie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Movserie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating_Movserie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movieseries", x => x.MovieserieId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image_Character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_Character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age_Character = table.Column<int>(type: "int", nullable: false),
                    Weight_Character = table.Column<double>(type: "float", nullable: false),
                    History_Character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieserieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Characters_Movieseries_MovieserieId",
                        column: x => x.MovieserieId,
                        principalTable: "Movieseries",
                        principalColumn: "MovieserieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreModelMovieserieModel",
                columns: table => new
                {
                    GenresGenreId = table.Column<int>(type: "int", nullable: false),
                    MovieseriesMovieserieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreModelMovieserieModel", x => new { x.GenresGenreId, x.MovieseriesMovieserieId });
                    table.ForeignKey(
                        name: "FK_GenreModelMovieserieModel_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreModelMovieserieModel_Movieseries_MovieseriesMovieserieId",
                        column: x => x.MovieseriesMovieserieId,
                        principalTable: "Movieseries",
                        principalColumn: "MovieserieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_MovieserieId",
                table: "Characters",
                column: "MovieserieId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreModelMovieserieModel_MovieseriesMovieserieId",
                table: "GenreModelMovieserieModel",
                column: "MovieseriesMovieserieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "GenreModelMovieserieModel");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movieseries");
        }
    }
}
