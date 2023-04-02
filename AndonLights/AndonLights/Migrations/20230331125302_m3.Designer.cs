﻿// <auto-generated />
using System;
using AndonLights.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AndonLights.Migrations
{
    [DbContext(typeof(AndonLightsDbContext))]
    [Migration("20230331125302_m3")]
    partial class m3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AndonLights.Model.AndonLight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AndonLights", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.DailyStateStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DayOfStats")
                        .HasColumnType("datetime2");

                    b.Property<double>("MinutesSpentInState")
                        .HasColumnType("float");

                    b.Property<int>("NumberOfEntries")
                        .HasColumnType("int");

                    b.Property<int?>("StateID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateID");

                    b.ToTable("DailyStateStats", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.MonthlyStateStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("MinutesSpentInState")
                        .HasColumnType("float");

                    b.Property<DateTime>("MonthOfStats")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfEntries")
                        .HasColumnType("int");

                    b.Property<int?>("StateID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateID");

                    b.ToTable("MonthlyStateStats", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("InTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("LenghtOfSessionInMinutes")
                        .HasColumnType("float");

                    b.Property<DateTime>("OutTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StateID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateID");

                    b.ToTable("Sessions", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.State", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("LightID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("LightID")
                        .IsUnique();

                    b.ToTable("States", (string)null);
                });

            modelBuilder.Entity("AndonLights.Model.DailyStateStats", b =>
                {
                    b.HasOne("AndonLights.Model.State", null)
                        .WithMany("DailyStats")
                        .HasForeignKey("StateID");
                });

            modelBuilder.Entity("AndonLights.Model.MonthlyStateStats", b =>
                {
                    b.HasOne("AndonLights.Model.State", null)
                        .WithMany("MonthlyStats")
                        .HasForeignKey("StateID");
                });

            modelBuilder.Entity("AndonLights.Model.Session", b =>
                {
                    b.HasOne("AndonLights.Model.State", null)
                        .WithMany("ClosedSessions")
                        .HasForeignKey("StateID");
                });

            modelBuilder.Entity("AndonLights.Model.State", b =>
                {
                    b.HasOne("AndonLights.Model.AndonLight", null)
                        .WithOne("YellowState")
                        .HasForeignKey("AndonLights.Model.State", "LightID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AndonLights.Model.AndonLight", b =>
                {
                    b.Navigation("YellowState")
                        .IsRequired();
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