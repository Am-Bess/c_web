﻿// <auto-generated />
using AppProduct.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppProduct.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20250105085631_initial1.0")]
    partial class initial10
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

            modelBuilder.Entity("AppProduct.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descript")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("ProductName");

                    b.HasKey("Id")
                        .HasName("CategoryID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CategoryProduct", (string)null);
                });

            modelBuilder.Entity("AppProduct.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriId")
                        .HasColumnType("integer");

                    b.Property<string>("Descript")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Descript");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("ProductName");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("Price");

                    b.HasKey("Id")
                        .HasName("ProductID");

                    b.HasIndex("CategoriId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("AppProduct.Models.Storage", b =>
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
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("StorageName");

                    b.HasKey("Id")
                        .HasName("StorageID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Storage", (string)null);
                });

            modelBuilder.Entity("ProductStorage", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.Property<int>("StoresId")
                        .HasColumnType("integer");

                    b.HasKey("ProductsId", "StoresId");

                    b.HasIndex("StoresId");

                    b.ToTable("StorageToProduct", (string)null);
                });

            modelBuilder.Entity("AppProduct.Models.Product", b =>
                {
                    b.HasOne("AppProduct.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("CategoryProduct");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProductStorage", b =>
                {
                    b.HasOne("AppProduct.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppProduct.Models.Storage", null)
                        .WithMany()
                        .HasForeignKey("StoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppProduct.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
