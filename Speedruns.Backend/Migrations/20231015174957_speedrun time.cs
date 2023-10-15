using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedruns.Backend.Migrations
{
    /// <inheritdoc />
    public partial class speedruntime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Runs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Runs");
        }
    }
}
