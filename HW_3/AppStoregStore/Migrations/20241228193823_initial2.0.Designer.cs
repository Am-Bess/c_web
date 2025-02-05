﻿// <auto-generated />
using AppStoregStore.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WATaskStoreg.Migrations
{
    [DbContext(typeof(StoregeContext))]
    [Migration("20241228193823_initial2.0")]
    partial class initial20
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppStoregStore.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasMaxLength(255)
                        .HasColumnType("integer")
                        .HasColumnName("ProductCount");

                    b.Property<string>("Descript")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Descript");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("PositionName");

                    b.Property<int>("productId")
                        .HasMaxLength(255)
                        .HasColumnType("integer")
                        .HasColumnName("ProductID");

                    b.HasKey("Id")
                        .HasName("PositionID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("productId")
                        .IsUnique();

                    b.ToTable("Storage", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
