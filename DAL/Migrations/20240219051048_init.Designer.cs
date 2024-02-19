﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentAttendanceSystem.Data;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240219051048_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.AttendanceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ClassModelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("bit");

                    b.Property<int?>("SectionModelId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassModelId");

                    b.HasIndex("SectionModelId");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Models.ClassModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Classes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "10th Class"
                        },
                        new
                        {
                            Id = 2,
                            Name = "9th Class"
                        },
                        new
                        {
                            Id = 3,
                            Name = "7th Class"
                        },
                        new
                        {
                            Id = 4,
                            Name = "8th Class"
                        },
                        new
                        {
                            Id = 5,
                            Name = "6th Class"
                        },
                        new
                        {
                            Id = 6,
                            Name = "5th Class"
                        });
                });

            modelBuilder.Entity("Models.SectionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Models.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClassModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RollNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassModelId");

                    b.HasIndex("RollNumber")
                        .IsUnique();

                    b.HasIndex("SectionId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Models.AttendanceModel", b =>
                {
                    b.HasOne("Models.ClassModel", null)
                        .WithMany("Attendances")
                        .HasForeignKey("ClassModelId");

                    b.HasOne("Models.SectionModel", null)
                        .WithMany("Attendances")
                        .HasForeignKey("SectionModelId");

                    b.HasOne("Models.StudentModel", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Models.SectionModel", b =>
                {
                    b.HasOne("Models.ClassModel", "Class")
                        .WithMany("Sections")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("Models.StudentModel", b =>
                {
                    b.HasOne("Models.ClassModel", null)
                        .WithMany("Students")
                        .HasForeignKey("ClassModelId");

                    b.HasOne("Models.SectionModel", "Section")
                        .WithMany("Students")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Models.ClassModel", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Sections");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Models.SectionModel", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}