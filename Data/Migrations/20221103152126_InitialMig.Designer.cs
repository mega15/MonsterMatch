﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ContextBizagiMatch))]
    [Migration("20221103152126_InitialMig")]
    partial class InitialMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Model.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("CharacterType")
                        .HasColumnType("int");

                    b.Property<int>("ElementType")
                        .HasColumnType("int");

                    b.Property<int>("Life")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Model.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Model.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Character1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Character2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("WeaponCharacter1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WeaponCharacter2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WinnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Character1Id");

                    b.HasIndex("Character2Id");

                    b.HasIndex("WeaponCharacter1Id");

                    b.HasIndex("WeaponCharacter2Id");

                    b.HasIndex("WinnerId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Model.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Model.PlayerMatchBid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BidAmount")
                        .HasColumnType("int");

                    b.Property<int>("BidClass")
                        .HasColumnType("int");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MatchId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("MatchId");

                    b.ToTable("PlayersMatchBids");
                });

            modelBuilder.Entity("Model.PlayersByGame", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "GameId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("GameId");

                    b.ToTable("PlayersByGames");
                });

            modelBuilder.Entity("Model.Weapon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ElementType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("Model.Character", b =>
                {
                    b.HasOne("Model.Player", "Player")
                        .WithMany("Characters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Model.Match", b =>
                {
                    b.HasOne("Model.Character", "Character1")
                        .WithMany()
                        .HasForeignKey("Character1Id");

                    b.HasOne("Model.Character", "Character2")
                        .WithMany()
                        .HasForeignKey("Character2Id");

                    b.HasOne("Model.Weapon", "WeaponCharacter1")
                        .WithMany()
                        .HasForeignKey("WeaponCharacter1Id");

                    b.HasOne("Model.Weapon", "WeaponCharacter2")
                        .WithMany()
                        .HasForeignKey("WeaponCharacter2Id");

                    b.HasOne("Model.Character", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");

                    b.Navigation("Character1");

                    b.Navigation("Character2");

                    b.Navigation("WeaponCharacter1");

                    b.Navigation("WeaponCharacter2");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("Model.PlayerMatchBid", b =>
                {
                    b.HasOne("Model.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("Model.PlayersByGame", b =>
                {
                    b.HasOne("Model.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("Model.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Model.Player", "Player")
                        .WithMany("Games")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Model.Weapon", b =>
                {
                    b.HasOne("Model.Character", "Character")
                        .WithMany("Weapons")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("Model.Character", b =>
                {
                    b.Navigation("Weapons");
                });

            modelBuilder.Entity("Model.Game", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Model.Player", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
