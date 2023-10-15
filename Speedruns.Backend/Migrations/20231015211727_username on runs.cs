using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedruns.Backend.Migrations
{
    /// <inheritdoc />
    public partial class usernameonruns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Runs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Runs");
        }
    }
}
