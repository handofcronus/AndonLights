﻿// <auto-generated />
using System;
using AndonLights.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AndonLights.Migrations
{
    [DbContext(typeof(AndonLightsDbContext))]
    partial class AndonLightsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AndonLights.Model.AndonLight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.ToTable("AndonLights", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.DailyStateStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfStats")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("MinutesSpentInState")
                        .HasColumnType("double precision");

                    b.Property<int>("NumberOfEntries")
                        .HasColumnType("integer");

                    b.Property<int>("StateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("DailyStateStats", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.MonthlyStateStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfStats")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("MinutesSpentInState")
                        .HasColumnType("double precision");

                    b.Property<int>("NumberOfEntries")
                        .HasColumnType("integer");

                    b.Property<int>("StateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("MonthlyStateStats", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime>("InTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("LenghtOfSessionInMinutes")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("OutTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Sessions", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.State", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("LightID")
                        .HasColumnType("integer");

                    b.Property<string>("StateColour")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("LightID");

                    b.ToTable("States", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.DailyStateStats", b =>
                {
                    b.HasOne("AndonLights.Model.State", null)
                        .WithMany("DailyStats")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AndonLights.Model.MonthlyStateStats", b =>
                {
                    b.HasOne("AndonLights.Model.State", null)
                        .WithMany("MonthlyStats")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AndonLights.Model.Session", b =>
                {
                    b.HasOne("AndonLights.Model.State", null)
                        .WithMany("ClosedSessions")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AndonLights.Model.State", b =>
                {
                    b.HasOne("AndonLights.Model.AndonLight", null)
                        .WithMany("States")
                        .HasForeignKey("LightID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AndonLights.Model.AndonLight", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("AndonLights.Model.State", b =>
                {
                    b.Navigation("ClosedSessions");

                    b.Navigation("DailyStats");

                    b.Navigation("MonthlyStats");
                });
#pragma warning restore 612, 618
        }
    }
}
