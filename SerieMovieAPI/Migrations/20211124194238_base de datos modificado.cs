using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SerieMovieAPI.Migrations
{
    public partial class basededatosmodificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image_Character = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name_Character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age_Character = table.Column<int>(type: "int", nullable: false),
                    Weight_Character = table.Column<double>(type: "float", nullable: false),
                    History_Character = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "Movieseries",
                columns: table => new
                {
                    MovieserieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_Movserie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_Movserie = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Date_Movserie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating_Movserie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movieseries", x => x.MovieserieId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterModelMovieserieModel",
                columns: table => new
                {
                    CharactersCharacterId = table.Column<int>(type: "int", nullable: false),
                    MovieseriesMovieserieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterModelMovieserieModel", x => new { x.CharactersCharacterId, x.MovieseriesMovieserieId });
                    table.ForeignKey(
                        name: "FK_CharacterModelMovieserieModel_Characters_CharactersCharacterId",
                        column: x => x.CharactersCharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterModelMovieserieModel_Movieseries_MovieseriesMovieserieId",
                        column: x => x.MovieseriesMovieserieId,
                        principalTable: "Movieseries",
                        principalColumn: "MovieserieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_Genre = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MovieserieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                    table.ForeignKey(
                        name: "FK_Genres_Movieseries_MovieserieId",
                        column: x => x.MovieserieId,
                        principalTable: "Movieseries",
                        principalColumn: "MovieserieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModelMovieserieModel_MovieseriesMovieserieId",
                table: "CharacterModelMovieserieModel",
                column: "MovieseriesMovieserieId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieserieId",
                table: "Genres",
                column: "MovieserieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterModelMovieserieModel");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Movieseries");
        }
    }
}
