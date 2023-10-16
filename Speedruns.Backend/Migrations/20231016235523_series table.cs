using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Speedruns.Backend.Migrations
{
    /// <inheritdoc />
    public partial class seriestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Series",
                table: "Games");

            migrationBuilder.AddColumn<long>(
                name: "SeriesId",
                table: "Games",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SeriesModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Players = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_SeriesId",
                table: "Games",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_SeriesModel_SeriesId",
                table: "Games",
                column: "SeriesId",
                principalTable: "SeriesModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_SeriesModel_SeriesId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "SeriesModel");

            migrationBuilder.DropIndex(
                name: "IX_Games_SeriesId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Games",
                type: "text",
                nullable: true);
        }
    }
}
