﻿// <auto-generated />
using System;
using GameCatalogAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameCatalogAPI.Migrations
{
    [DbContext(typeof(GameCatalogContext))]
    partial class GameCatalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.14");

            modelBuilder.Entity("GameCatalogAPI.Entities.Developer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Founded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Developers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "USA",
                            Founded = new DateOnly(1991, 1, 1),
                            Name = "Epic Games"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Japan",
                            Founded = new DateOnly(1889, 9, 23),
                            Name = "Nintendo"
                        },
                        new
                        {
                            Id = 3,
                            Country = "Poland",
                            Founded = new DateOnly(2002, 5, 1),
                            Name = "CD Projekt Red"
                        },
                        new
                        {
                            Id = 4,
                            Country = "USA",
                            Founded = new DateOnly(1998, 12, 1),
                            Name = "Rockstar Games"
                        },
                        new
                        {
                            Id = 5,
                            Country = "USA",
                            Founded = new DateOnly(1996, 8, 24),
                            Name = "Valve"
                        });
                });

            modelBuilder.Entity("GameCatalogAPI.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeveloperId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DeveloperId = 1,
                            Genre = "Battle Royale",
                            Name = "Fortnite",
                            Platform = "PC",
                            Rating = 85,
                            ReleaseDate = new DateOnly(2017, 7, 25)
                        },
                        new
                        {
                            Id = 2,
                            DeveloperId = 2,
                            Genre = "Action-Adventure",
                            Name = "The Legend of Zelda: Breath of the Wild",
                            Platform = "Nintendo Switch",
                            Rating = 97,
                            ReleaseDate = new DateOnly(2017, 3, 3)
                        },
                        new
                        {
                            Id = 3,
                            DeveloperId = 3,
                            Genre = "RPG",
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = "PC",
                            Rating = 93,
                            ReleaseDate = new DateOnly(2015, 5, 19)
                        },
                        new
                        {
                            Id = 4,
                            DeveloperId = 3,
                            Genre = "Action RPG",
                            Name = "Cyberpunk 2077",
                            Platform = "PC",
                            Rating = 75,
                            ReleaseDate = new DateOnly(2020, 12, 10)
                        },
                        new
                        {
                            Id = 5,
                            DeveloperId = 4,
                            Genre = "Open World",
                            Name = "Grand Theft Auto V",
                            Platform = "PC",
                            Rating = 96,
                            ReleaseDate = new DateOnly(2013, 9, 17)
                        },
                        new
                        {
                            Id = 6,
                            DeveloperId = 4,
                            Genre = "Action-Adventure",
                            Name = "Red Dead Redemption 2",
                            Platform = "PS4",
                            Rating = 97,
                            ReleaseDate = new DateOnly(2018, 10, 26)
                        },
                        new
                        {
                            Id = 7,
                            DeveloperId = 5,
                            Genre = "FPS",
                            Name = "Half-Life 2",
                            Platform = "PC",
                            Rating = 96,
                            ReleaseDate = new DateOnly(2004, 11, 16)
                        },
                        new
                        {
                            Id = 8,
                            DeveloperId = 5,
                            Genre = "Puzzle",
                            Name = "Portal 2",
                            Platform = "PC",
                            Rating = 95,
                            ReleaseDate = new DateOnly(2011, 4, 19)
                        });
                });

            modelBuilder.Entity("GameCatalogAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 25,
                            PasswordHash = "$2a$11$LyZxtrjPfmnZRr41E.SIEuJS0kpymeaFFaqOudqVWdSJRB.hRICdi",
                            Role = 1,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 3,
                            Age = 16,
                            PasswordHash = "$2a$11$LyZxtrjPfmnZRr41E.SIEuJS0kpymeaFFaqOudqVWdSJRB.hRICdi",
                            Role = 1,
                            Username = "keba"
                        },
                        new
                        {
                            Id = 2,
                            Age = 30,
                            PasswordHash = "$2a$11$59p1npv2XBeCR56oyoFqeeqb8Dqf5TXgEmY/3W3VRQF.urC2f/kB6",
                            Role = 0,
                            Username = "covek"
                        });
                });

            modelBuilder.Entity("GameCatalogAPI.Entities.Game", b =>
                {
                    b.HasOne("GameCatalogAPI.Entities.Developer", "Developer")
                        .WithMany("Games")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("GameCatalogAPI.Entities.Developer", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
