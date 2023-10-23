using Microsoft.EntityFrameworkCore.Migrations;

namespace NurseVolunteeringSystem.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Suburb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ChronicCondition",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Suburb");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ChronicCondition");
        }
    }
}
