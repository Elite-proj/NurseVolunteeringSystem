using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NurseVolunteeringSystem.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChronicCondition",
                columns: table => new
                {
                    ChronicConditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChronicCondition", x => x.ChronicConditionID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    GenderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Suburb",
                columns: table => new
                {
                    SuburbID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuburbName = table.Column<string>(nullable: false),
                    CityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suburb", x => x.SuburbID);
                    table.ForeignKey(
                        name: "FK_Suburb_City_CityID",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    BusinessID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(nullable: false),
                    NPONumber = table.Column<string>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: false),
                    AddressLine2 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: false),
                    ContactNo = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    OperatingHours = table.Column<string>(nullable: false),
                    SuburbID = table.Column<string>(nullable: true),
                    SuburbID1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.BusinessID);
                    table.ForeignKey(
                        name: "FK_Business_Suburb_SuburbID1",
                        column: x => x.SuburbID1,
                        principalTable: "Suburb",
                        principalColumn: "SuburbID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    IDNumber = table.Column<string>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: false),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    ConfirmEmail = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: false),
                    ContactNo = table.Column<string>(nullable: false),
                    UserType = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    GenderID = table.Column<int>(nullable: false),
                    SuburbID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Gender_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Gender",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Suburb_SuburbID",
                        column: x => x.SuburbID,
                        principalTable: "Suburb",
                        principalColumn: "SuburbID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminID);
                    table.ForeignKey(
                        name: "FK_Admin_Users_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ManagerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerID);
                    table.ForeignKey(
                        name: "FK_Manager_Users_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nurse",
                columns: table => new
                {
                    NurseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurse", x => x.NurseID);
                    table.ForeignKey(
                        name: "FK_Nurse_Users_NurseID",
                        column: x => x.NurseID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientID = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    EmergencyContactPerson = table.Column<string>(nullable: false),
                    EmergencyContactNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_Patient_Users_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrefferedSuburb",
                columns: table => new
                {
                    PrefferedSuburbID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuburbID = table.Column<int>(nullable: false),
                    NurseID = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrefferedSuburb", x => x.PrefferedSuburbID);
                    table.ForeignKey(
                        name: "FK_PrefferedSuburb_Nurse_NurseID",
                        column: x => x.NurseID,
                        principalTable: "Nurse",
                        principalColumn: "NurseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrefferedSuburb_Suburb_SuburbID",
                        column: x => x.SuburbID,
                        principalTable: "Suburb",
                        principalColumn: "SuburbID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CareContract",
                columns: table => new
                {
                    CareContractID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractDate = table.Column<DateTime>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    WoundDescription = table.Column<string>(nullable: true),
                    StartCareDate = table.Column<DateTime>(nullable: false),
                    EndCareDate = table.Column<DateTime>(nullable: false),
                    ContractStatus = table.Column<string>(nullable: true),
                    DeleteStatus = table.Column<string>(nullable: true),
                    SuburbID = table.Column<int>(nullable: false),
                    PatientID = table.Column<int>(nullable: true),
                    NurseID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareContract", x => x.CareContractID);
                    table.ForeignKey(
                        name: "FK_CareContract_Nurse_NurseID",
                        column: x => x.NurseID,
                        principalTable: "Nurse",
                        principalColumn: "NurseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareContract_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareContract_Suburb_SuburbID",
                        column: x => x.SuburbID,
                        principalTable: "Suburb",
                        principalColumn: "SuburbID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient_ChronicConditions",
                columns: table => new
                {
                    PatientChronicConditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChronicConditionID = table.Column<int>(nullable: false),
                    PatientID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_ChronicConditions", x => x.PatientChronicConditionID);
                    table.ForeignKey(
                        name: "FK_Patient_ChronicConditions_ChronicCondition_ChronicConditionID",
                        column: x => x.ChronicConditionID,
                        principalTable: "ChronicCondition",
                        principalColumn: "ChronicConditionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_ChronicConditions_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CareVisit",
                columns: table => new
                {
                    CareVisitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    ApproximateArriveTime = table.Column<DateTime>(nullable: false),
                    VisistArriveTime = table.Column<DateTime>(nullable: false),
                    DepartTime = table.Column<DateTime>(nullable: false),
                    WoundProgress = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CareContractID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareVisit", x => x.CareVisitID);
                    table.ForeignKey(
                        name: "FK_CareVisit_CareContract_CareContractID",
                        column: x => x.CareContractID,
                        principalTable: "CareContract",
                        principalColumn: "CareContractID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "GenderID", "GenderName" },
                values: new object[] { 1, "Male" });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "GenderID", "GenderName" },
                values: new object[] { 2, "Female" });

            migrationBuilder.CreateIndex(
                name: "IX_Business_SuburbID1",
                table: "Business",
                column: "SuburbID1");

            migrationBuilder.CreateIndex(
                name: "IX_CareContract_NurseID",
                table: "CareContract",
                column: "NurseID");

            migrationBuilder.CreateIndex(
                name: "IX_CareContract_PatientID",
                table: "CareContract",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_CareContract_SuburbID",
                table: "CareContract",
                column: "SuburbID");

            migrationBuilder.CreateIndex(
                name: "IX_CareVisit_CareContractID",
                table: "CareVisit",
                column: "CareContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_ChronicConditions_ChronicConditionID",
                table: "Patient_ChronicConditions",
                column: "ChronicConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_ChronicConditions_PatientID",
                table: "Patient_ChronicConditions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PrefferedSuburb_NurseID",
                table: "PrefferedSuburb",
                column: "NurseID");

            migrationBuilder.CreateIndex(
                name: "IX_PrefferedSuburb_SuburbID",
                table: "PrefferedSuburb",
                column: "SuburbID");

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_CityID",
                table: "Suburb",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderID",
                table: "Users",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SuburbID",
                table: "Users",
                column: "SuburbID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "CareVisit");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Patient_ChronicConditions");

            migrationBuilder.DropTable(
                name: "PrefferedSuburb");

            migrationBuilder.DropTable(
                name: "CareContract");

            migrationBuilder.DropTable(
                name: "ChronicCondition");

            migrationBuilder.DropTable(
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Suburb");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
