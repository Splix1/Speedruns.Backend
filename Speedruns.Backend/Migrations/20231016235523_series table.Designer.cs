﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Speedruns.Backend.Models;

#nullable disable

namespace Speedruns.Backend.Migrations
{
    [DbContext(typeof(SpeedrunsContext))]
    [Migration("20231016235523_series table")]
    partial class seriestable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Speedruns.Backend.Models.CommentModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("RunId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RunId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.ConsoleModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ConsoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Consoles");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.GameConsoleModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ConsoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ConsoleId");

                    b.HasIndex("GameId");

                    b.ToTable("GameConsoles");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.GameModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Players")
                        .HasColumnType("integer");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("integer");

                    b.Property<int>("RunsPublished")
                        .HasColumnType("integer");

                    b.Property<long?>("SeriesId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.RunModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ConsoleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ConsoleId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Runs");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.SeriesModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Players")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SeriesModel");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.UserModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("TwitchLink")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<string>("YoutubeLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.CommentModel", b =>
                {
                    b.HasOne("Speedruns.Backend.Models.RunModel", "Run")
                        .WithMany("Comments")
                        .HasForeignKey("RunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Speedruns.Backend.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Run");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.GameConsoleModel", b =>
                {
                    b.HasOne("Speedruns.Backend.Models.ConsoleModel", "Console")
                        .WithMany("GameConsoles")
                        .HasForeignKey("ConsoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Speedruns.Backend.Models.GameModel", "Game")
                        .WithMany("GameConsoles")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Console");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.GameModel", b =>
                {
                    b.HasOne("Speedruns.Backend.Models.SeriesModel", "Series")
                        .WithMany("Games")
                        .HasForeignKey("SeriesId");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.RunModel", b =>
                {
                    b.HasOne("Speedruns.Backend.Models.ConsoleModel", "Console")
                        .WithMany("Runs")
                        .HasForeignKey("ConsoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Speedruns.Backend.Models.GameModel", "Game")
                        .WithMany("Runs")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Speedruns.Backend.Models.UserModel", "User")
                        .WithMany("Runs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Console");

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.ConsoleModel", b =>
                {
                    b.Navigation("GameConsoles");

                    b.Navigation("Runs");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.GameModel", b =>
                {
                    b.Navigation("GameConsoles");

                    b.Navigation("Runs");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.RunModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.SeriesModel", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Speedruns.Backend.Models.UserModel", b =>
                {
                    b.Navigation("Runs");
                });
#pragma warning restore 612, 618
        }
    }
}