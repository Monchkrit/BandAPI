﻿// <auto-generated />
using System;
using BandAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BandAPI.Migrations
{
    [DbContext(typeof(BandAlbumContext))]
    [Migration("20200224015036_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BandAPI.Entities.Album", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BandID")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(400)")
                        .HasMaxLength(400);

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.HasIndex("BandID");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            ID = new Guid("dc4ccabe-29aa-42c4-9f80-18caea50adf5"),
                            BandID = new Guid("6b1eea43-5597-45a6-bdea-e68c60564247"),
                            Description = "One of the best heavy metal albums ever",
                            Title = "Master Of Puppets"
                        },
                        new
                        {
                            ID = new Guid("e5b6e8bf-5956-4329-a1b3-b1d48eea33ad"),
                            BandID = new Guid("a052a63d-fa53-44d5-a197-83089818a676"),
                            Description = "Amazing Rock album with raw sound",
                            Title = "Appetite for Destruction"
                        },
                        new
                        {
                            ID = new Guid("380c545c-9665-4043-baf2-34a3edefd373"),
                            BandID = new Guid("cb554ed6-8fa7-4b8d-8d90-55cc6a3e0074"),
                            Description = "Very groovy album",
                            Title = "Waterloo"
                        },
                        new
                        {
                            ID = new Guid("0e9a4ab5-4ae6-4ca3-ae7b-5f813e022527"),
                            BandID = new Guid("8e2f0a16-4c09-44c7-ba56-8dc62dfd792d"),
                            Description = "Arguably the best albums by Oasis",
                            Title = "Be Here Now"
                        },
                        new
                        {
                            ID = new Guid("8d2744ff-1134-4f36-a300-043febdc64b8"),
                            BandID = new Guid("cab51058-0996-4221-ba63-b841004e89dd"),
                            Description = "Awesome Debut album by A-Ha",
                            Title = "Hunting Hight and Low"
                        });
                });

            modelBuilder.Entity("BandAPI.Entities.Band", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Founded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MainGenre")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Bands");

                    b.HasData(
                        new
                        {
                            ID = new Guid("6b1eea43-5597-45a6-bdea-e68c60564247"),
                            MainGenre = "Heavy Metal",
                            Name = "Metallica"
                        },
                        new
                        {
                            ID = new Guid("a052a63d-fa53-44d5-a197-83089818a676"),
                            MainGenre = "Rock",
                            Name = "Guns N Roses"
                        },
                        new
                        {
                            ID = new Guid("cb554ed6-8fa7-4b8d-8d90-55cc6a3e0074"),
                            MainGenre = "Disco",
                            Name = "ABBA"
                        },
                        new
                        {
                            ID = new Guid("8e2f0a16-4c09-44c7-ba56-8dc62dfd792d"),
                            MainGenre = "Alternative",
                            Name = "Oasis"
                        },
                        new
                        {
                            ID = new Guid("cab51058-0996-4221-ba63-b841004e89dd"),
                            MainGenre = "Pop",
                            Name = "A-ha"
                        });
                });

            modelBuilder.Entity("BandAPI.Entities.Album", b =>
                {
                    b.HasOne("BandAPI.Entities.Band", "Band")
                        .WithMany("Albums")
                        .HasForeignKey("BandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
