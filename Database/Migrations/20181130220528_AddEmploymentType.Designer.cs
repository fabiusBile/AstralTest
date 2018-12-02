﻿// <auto-generated />
using System;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(VacanciesContext))]
    [Migration("20181130220528_AddEmploymentType")]
    partial class AddEmploymentType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.Models.Employer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("Database.Models.EmploymentType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(63);

                    b.Property<string>("Name")
                        .HasMaxLength(63);

                    b.HasKey("Id");

                    b.ToTable("EmploymentTypes");
                });

            modelBuilder.Entity("Database.Models.Vacancy", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactPerson")
                        .HasMaxLength(63);

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("EmployerId");

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(12);

                    b.Property<int?>("Salary");

                    b.Property<string>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("Database.Models.Vacancy", b =>
                {
                    b.HasOne("Database.Models.Employer", "Employer")
                        .WithMany("Vacancies")
                        .HasForeignKey("EmployerId");

                    b.HasOne("Database.Models.EmploymentType", "Type")
                        .WithMany("Vacancies")
                        .HasForeignKey("TypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
