﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SpecRandomizer.Server.Models;

#nullable disable

namespace SpecRandomizer.Server.Migrations
{
    [DbContext(typeof(SpecRandomizerDbContext))]
    [Migration("20250303220446_AddedTimeStamp")]
    partial class AddedTimeStamp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SpecRandomizer.Server.Model.Configuration", b =>
                {
                    b.Property<int>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ConfigurationId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ConfigurationId");

                    b.HasIndex("UserId");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("SpecRandomizer.Server.Model.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlayerId"));

                    b.Property<int?>("ConfigurationId")
                        .HasColumnType("integer");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("SpecList")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.HasKey("PlayerId");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SpecRandomizer.Server.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SpecRandomizer.Server.Model.Configuration", b =>
                {
                    b.HasOne("SpecRandomizer.Server.Model.User", "User")
                        .WithMany("Configurations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpecRandomizer.Server.Model.Player", b =>
                {
                    b.HasOne("SpecRandomizer.Server.Model.Configuration", "Configuration")
                        .WithMany("Players")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("SpecRandomizer.Server.Model.Configuration", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("SpecRandomizer.Server.Model.User", b =>
                {
                    b.Navigation("Configurations");
                });
#pragma warning restore 612, 618
        }
    }
}
