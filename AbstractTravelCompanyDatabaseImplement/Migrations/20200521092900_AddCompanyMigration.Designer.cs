﻿// <auto-generated />
using System;
using AbstractTravelCompanyDatabaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AbstractTravelCompanyDatabaseImplement.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20200521092900_AddCompanyMigration")]
    partial class AddCompanyMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateImplement")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<int>("TourId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.StoreComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreComponents");
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("TourName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.TourComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("TourId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("TourId");

                    b.ToTable("TourComponents");
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.Order", b =>
                {
                    b.HasOne("AbstractTravelCompanyDatabaseImplement.Models.Tour", "Tour")
                        .WithMany("Orders")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.StoreComponent", b =>
                {
                    b.HasOne("AbstractTravelCompanyDatabaseImplement.Models.Component", "Component")
                        .WithMany("StoreComponents")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AbstractTravelCompanyDatabaseImplement.Models.Store", "Store")
                        .WithMany("StoreComponents")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AbstractTravelCompanyDatabaseImplement.Models.TourComponent", b =>
                {
                    b.HasOne("AbstractTravelCompanyDatabaseImplement.Models.Component", "Component")
                        .WithMany("TourComponents")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AbstractTravelCompanyDatabaseImplement.Models.Tour", "Tour")
                        .WithMany("TourComponents")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
