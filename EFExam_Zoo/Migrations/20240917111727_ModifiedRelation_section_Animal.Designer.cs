﻿// <auto-generated />
using System;
using EFExam_Zoo.EFPersistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFExam_Zoo.Migrations
{
    [DbContext(typeof(EFDataContext))]
    [Migration("20240917111727_ModifiedRelation_section_Animal")]
    partial class ModifiedRelation_section_Animal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFExam_Zoo.Models.Animals.Animal", b =>
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

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("EFExam_Zoo.Models.Sections.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("AnimalsCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.Property<int>("ZooId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("TicketId")
                        .IsUnique()
                        .HasFilter("[TicketId] IS NOT NULL");

                    b.HasIndex("ZooId");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("EFExam_Zoo.Models.TIckets.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("EFExam_Zoo.Models.TicketsSolds.TicketsSold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TicketsSold");
                });

            modelBuilder.Entity("EFExam_Zoo.Models.Zoos.Zoo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Zoo");
                });

            modelBuilder.Entity("EFExam_Zoo.Models.Sections.Section", b =>
                {
                    b.HasOne("EFExam_Zoo.Models.Animals.Animal", "Animal")
                        .WithMany("Sections")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EFExam_Zoo.Models.TIckets.Ticket", "Ticket")
                        .WithOne("Section")
                        .HasForeignKey("EFExam_Zoo.Models.Sections.Section", "TicketId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EFExam_Zoo.Models.Zoos.Zoo", "Zoo")
                        .WithMany("Sections")
                        .HasForeignKey("ZooId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Ticket");

                    b.Navigation("Zoo");
                });

            modelBuilder.Entity("EFExam_Zoo.Models.Animals.Animal", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("EFExam_Zoo.Models.TIckets.Ticket", b =>
                {
                    b.Navigation("Section")
                        .IsRequired();
                });

            modelBuilder.Entity("EFExam_Zoo.Models.Zoos.Zoo", b =>
                {
                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}