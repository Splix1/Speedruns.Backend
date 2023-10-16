using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedruns.Backend.Migrations
{
    /// <inheritdoc />
    public partial class fixjunctiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameConsoles_Runs_RunId",
                table: "GameConsoles");

            migrationBuilder.DropIndex(
                name: "IX_GameConsoles_RunId",
                table: "GameConsoles");

            migrationBuilder.DropColumn(
                name: "RunId",
                table: "GameConsoles");

            migrationBuilder.AddColumn<long>(
                name: "ConsoleId",
                table: "Consoles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsoleId",
                table: "Consoles");

            migrationBuilder.AddColumn<long>(
                name: "RunId",
                table: "GameConsoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameConsoles_RunId",
                table: "GameConsoles",
                column: "RunId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameConsoles_Runs_RunId",
                table: "GameConsoles",
                column: "RunId",
                principalTable: "Runs",
                principalColumn: "Id");
        }
    }
}
