using Microsoft.EntityFrameworkCore.Migrations;

namespace NurseVolunteeringSystem.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Suburb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ChronicCondition",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Suburb");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ChronicCondition");
        }
    }
}
