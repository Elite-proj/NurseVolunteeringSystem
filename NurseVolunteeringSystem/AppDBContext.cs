using Microsoft.EntityFrameworkCore;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem
{
    public class AppDBContext :DbContext 
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Gender> Gender { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Suburb> Suburb { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Nurse> Nurse { get; set; }
        public DbSet<ChronicCondition> ChronicCondition { get; set; }
        public DbSet<PatientChronicCondition> Patient_ChronicConditions { get; set; }
        public DbSet<PrefferedSuburb> PrefferedSuburb { get; set; }
        public DbSet<CareContract> CareContract { get; set; }
        public DbSet<CareVisit> CareVisit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Gender>().HasData(
                new Gender { GenderID = 1, GenderName = "Male" },
                 new Gender { GenderID = 2, GenderName = "Female" }
               );

        }

    }
}
