using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedruns.Backend.Migrations
{
    /// <inheritdoc />
    public partial class seriesimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_SeriesModel_SeriesId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeriesModel",
                table: "SeriesModel");

            migrationBuilder.RenameTable(
                name: "SeriesModel",
                newName: "Series");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Series",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Series_SeriesId",
                table: "Games",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Series_SeriesId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Series");

            migrationBuilder.RenameTable(
                name: "Series",
                newName: "SeriesModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeriesModel",
                table: "SeriesModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_SeriesModel_SeriesId",
                table: "Games",
                column: "SeriesId",
                principalTable: "SeriesModel",
                principalColumn: "Id");
        }
    }
}
