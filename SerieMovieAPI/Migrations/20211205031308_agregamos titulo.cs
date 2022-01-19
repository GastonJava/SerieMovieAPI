using Microsoft.EntityFrameworkCore.Migrations;

namespace SerieMovieAPI.Migrations
{
    public partial class agregamostitulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "titulo_movie",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "titulo_movie",
                table: "Characters");
        }
    }
}
