using Microsoft.EntityFrameworkCore.Migrations;

namespace NurseVolunteeringSystem.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Business");

            

            migrationBuilder.AlterColumn<int>(
                name: "SuburbID",
                table: "Business",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Business_SuburbID",
                table: "Business",
                column: "SuburbID");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_Suburb_SuburbID",
                table: "Business",
                column: "SuburbID",
                principalTable: "Suburb",
                principalColumn: "SuburbID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_Suburb_SuburbID",
                table: "Business");

            migrationBuilder.DropIndex(
                name: "IX_Business_SuburbID",
                table: "Business");

            migrationBuilder.AlterColumn<string>(
                name: "SuburbID",
                table: "Business",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Business",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SuburbID1",
                table: "Business",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Business_SuburbID1",
                table: "Business",
                column: "SuburbID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_Suburb_SuburbID1",
                table: "Business",
                column: "SuburbID1",
                principalTable: "Suburb",
                principalColumn: "SuburbID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
