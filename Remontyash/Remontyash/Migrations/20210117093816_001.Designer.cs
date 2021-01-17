﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Remontyash.Models;

namespace Remontyash.Migrations
{
    [DbContext(typeof(RemontDBContext))]
    [Migration("20210117093816_001")]
    partial class _001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Remontyash.Models.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Remontyash.Models.Emp", b =>
                {
                    b.Property<Guid>("Empid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Empid");

                    b.ToTable("Emps");
                });

            modelBuilder.Entity("Remontyash.Models.MigrationHistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ContextKey")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<byte[]>("Model")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("MigrationId", "ContextKey")
                        .HasName("PK_dbo.__MigrationHistory");

                    b.ToTable("__MigrationHistory");
                });

            modelBuilder.Entity("Remontyash.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Accepted")
                        .HasColumnType("datetime");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Completed")
                        .HasColumnType("datetime");

                    b.Property<Guid>("Empid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("TypeJobId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex(new[] { "ClientId" }, "IX_ClientId");

                    b.HasIndex(new[] { "Empid" }, "IX_Empid");

                    b.HasIndex(new[] { "TypeJobId" }, "IX_TypeJobId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Remontyash.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Remontyash.Models.TypeJob", b =>
                {
                    b.Property<Guid>("TypeJobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TypeTechnicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TypeJobId");

                    b.HasIndex(new[] { "TypeTechnicId" }, "IX_TypeTechnicId");

                    b.ToTable("TypeJobs");
                });

            modelBuilder.Entity("Remontyash.Models.TypeTechnic", b =>
                {
                    b.Property<Guid>("TypeTechnicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeTechnicId");

                    b.ToTable("TypeTechnics");
                });

            modelBuilder.Entity("Remontyash.Models.User", b =>
                {
                    b.Property<Guid>("Userid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("USERID");

                    b.Property<Guid>("Empid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EMPID");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LOGIN");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PASSWORD");

                    b.Property<Guid>("Roleid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ROLEID");

                    b.HasKey("Userid");

                    b.HasIndex("Empid");

                    b.HasIndex("Roleid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Remontyash.Models.Order", b =>
                {
                    b.HasOne("Remontyash.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_dbo.Orders_dbo.Clients_ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Remontyash.Models.Emp", "Emp")
                        .WithMany("Orders")
                        .HasForeignKey("Empid")
                        .HasConstraintName("FK_dbo.Orders_dbo.Emps_Empid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Remontyash.Models.TypeJob", "TypeJob")
                        .WithMany("Orders")
                        .HasForeignKey("TypeJobId")
                        .HasConstraintName("FK_dbo.Orders_dbo.TypeJobs_TypeJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Emp");

                    b.Navigation("TypeJob");
                });

            modelBuilder.Entity("Remontyash.Models.TypeJob", b =>
                {
                    b.HasOne("Remontyash.Models.TypeTechnic", "TypeTechnic")
                        .WithMany("TypeJobs")
                        .HasForeignKey("TypeTechnicId")
                        .HasConstraintName("FK_dbo.TypeJobs_dbo.TypeTechnics_TypeTechnicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeTechnic");
                });

            modelBuilder.Entity("Remontyash.Models.User", b =>
                {
                    b.HasOne("Remontyash.Models.Emp", "Emp")
                        .WithMany("Users")
                        .HasForeignKey("Empid")
                        .HasConstraintName("FK_Users_Emps")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Remontyash.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("Roleid")
                        .HasConstraintName("FK_Users_Roles")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emp");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Remontyash.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Remontyash.Models.Emp", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Remontyash.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Remontyash.Models.TypeJob", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Remontyash.Models.TypeTechnic", b =>
                {
                    b.Navigation("TypeJobs");
                });
#pragma warning restore 612, 618
        }
    }
}
