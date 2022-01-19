using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SerieMovieAPI.Migrations
{
    public partial class agregandolatabladeimagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Genre",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "Image_Character",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "ImageFK",
                table: "Movieseries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageFK",
                table: "Genres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movieseries_ImageFK",
                table: "Movieseries",
                column: "ImageFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ImageFK",
                table: "Genres",
                column: "ImageFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_CharacterId",
                table: "Images",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Images_ImageFK",
                table: "Genres",
                column: "ImageFK",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movieseries_Images_ImageFK",
                table: "Movieseries",
                column: "ImageFK",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Images_ImageFK",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Movieseries_Images_ImageFK",
                table: "Movieseries");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Movieseries_ImageFK",
                table: "Movieseries");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ImageFK",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ImageFK",
                table: "Movieseries");

            migrationBuilder.DropColumn(
                name: "ImageFK",
                table: "Genres");

            migrationBuilder.AddColumn<string>(
                name: "Image_Genre",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image_Character",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
