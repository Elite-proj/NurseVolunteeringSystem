﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NurseVolunteeringSystem;

namespace NurseVolunteeringSystem.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20231023111836_third")]
    partial class third
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .HasColumnType("int");

                    b.HasKey("AdminID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Business", b =>
                {
                    b.Property<int>("BusinessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NPONumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatingHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SuburbID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuburbID1")
                        .HasColumnType("int");

                    b.HasKey("BusinessID");

                    b.HasIndex("SuburbID1");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.CareContract", b =>
                {
                    b.Property<int>("CareContractID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ContractDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ContractStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeleteStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndCareDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NurseID")
                        .HasColumnType("int");

                    b.Property<int?>("PatientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartCareDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SuburbID")
                        .HasColumnType("int");

                    b.Property<string>("WoundDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CareContractID");

                    b.HasIndex("NurseID");

                    b.HasIndex("PatientID");

                    b.HasIndex("SuburbID");

                    b.ToTable("CareContract");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.CareVisit", b =>
                {
                    b.Property<int>("CareVisitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApproximateArriveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CareContractID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VisistArriveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WoundProgress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CareVisitID");

                    b.HasIndex("CareContractID");

                    b.ToTable("CareVisit");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.ChronicCondition", b =>
                {
                    b.Property<int>("ChronicConditionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConditionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChronicConditionID");

                    b.ToTable("ChronicCondition");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Gender", b =>
                {
                    b.Property<int>("GenderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenderName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenderID");

                    b.ToTable("Gender");

                    b.HasData(
                        new
                        {
                            GenderID = 1,
                            GenderName = "Male"
                        },
                        new
                        {
                            GenderID = 2,
                            GenderName = "Female"
                        });
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Manager", b =>
                {
                    b.Property<int>("ManagerID")
                        .HasColumnType("int");

                    b.HasKey("ManagerID");

                    b.ToTable("Manager");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Nurse", b =>
                {
                    b.Property<int>("NurseID")
                        .HasColumnType("int");

                    b.HasKey("NurseID");

                    b.ToTable("Nurse");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Patient", b =>
                {
                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmergencyContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientID");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.PatientChronicCondition", b =>
                {
                    b.Property<int>("PatientChronicConditionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChronicConditionID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientChronicConditionID");

                    b.HasIndex("ChronicConditionID");

                    b.HasIndex("PatientID");

                    b.ToTable("Patient_ChronicConditions");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.PrefferedSuburb", b =>
                {
                    b.Property<int>("PrefferedSuburbID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NurseID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuburbID")
                        .HasColumnType("int");

                    b.HasKey("PrefferedSuburbID");

                    b.HasIndex("NurseID");

                    b.HasIndex("SuburbID");

                    b.ToTable("PrefferedSuburb");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Suburb", b =>
                {
                    b.Property<int>("SuburbID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SuburbName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SuburbID");

                    b.HasIndex("CityID");

                    b.ToTable("Suburb");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderID")
                        .HasColumnType("int");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuburbID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("GenderID");

                    b.HasIndex("SuburbID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Admin", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("NurseVolunteeringSystem.Models.Admin", "AdminID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Business", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.Suburb", "Suburb")
                        .WithMany()
                        .HasForeignKey("SuburbID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.CareContract", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseID");

                    b.HasOne("NurseVolunteeringSystem.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID");

                    b.HasOne("NurseVolunteeringSystem.Models.Suburb", "Suburb")
                        .WithMany()
                        .HasForeignKey("SuburbID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.CareVisit", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.CareContract", "CareContract")
                        .WithMany()
                        .HasForeignKey("CareContractID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Manager", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.User", "User")
                        .WithOne("Manager")
                        .HasForeignKey("NurseVolunteeringSystem.Models.Manager", "ManagerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Nurse", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.User", "User")
                        .WithOne("Nurse")
                        .HasForeignKey("NurseVolunteeringSystem.Models.Nurse", "NurseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Patient", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.User", "User")
                        .WithOne("Patient")
                        .HasForeignKey("NurseVolunteeringSystem.Models.Patient", "PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.PatientChronicCondition", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.ChronicCondition", "ChronicCondition")
                        .WithMany()
                        .HasForeignKey("ChronicConditionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NurseVolunteeringSystem.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.PrefferedSuburb", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseID");

                    b.HasOne("NurseVolunteeringSystem.Models.Suburb", "Suburb")
                        .WithMany()
                        .HasForeignKey("SuburbID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.Suburb", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NurseVolunteeringSystem.Models.User", b =>
                {
                    b.HasOne("NurseVolunteeringSystem.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NurseVolunteeringSystem.Models.Suburb", "Suburb")
                        .WithMany()
                        .HasForeignKey("SuburbID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
