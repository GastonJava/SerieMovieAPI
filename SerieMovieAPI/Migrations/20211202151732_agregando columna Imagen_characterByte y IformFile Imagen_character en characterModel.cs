using Microsoft.EntityFrameworkCore.Migrations;

namespace SerieMovieAPI.Migrations
{
    public partial class agregandocolumnaImagen_characterByteyIformFileImagen_characterencharacterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image_Character",
                table: "Characters",
                newName: "Image_Characterbyte");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image_Characterbyte",
                table: "Characters",
                newName: "Image_Character");
        }
    }
}
