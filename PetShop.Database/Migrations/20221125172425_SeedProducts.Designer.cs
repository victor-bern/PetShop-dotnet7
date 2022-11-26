﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetShop.Database.Context;

#nullable disable

namespace PetShop.Database.Migrations
{
    [DbContext(typeof(PetShopContext))]
    [Migration("20221125172425_SeedProducts")]
    partial class SeedProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PetShop.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Price")
                        .HasPrecision(2)
                        .HasColumnType("decimal(2,2)");

                    b.Property<string>("ProductImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Price = 50.37m,
                            ProductImageUrl = "Url",
                            Title = "Product 1"
                        },
                        new
                        {
                            Id = 2,
                            Price = 40.09m,
                            ProductImageUrl = "Url",
                            Title = "Product 2"
                        },
                        new
                        {
                            Id = 3,
                            Price = 61.03m,
                            ProductImageUrl = "Url",
                            Title = "Product 3"
                        },
                        new
                        {
                            Id = 4,
                            Price = 33.86m,
                            ProductImageUrl = "Url",
                            Title = "Product 4"
                        },
                        new
                        {
                            Id = 5,
                            Price = 76.70m,
                            ProductImageUrl = "Url",
                            Title = "Product 5"
                        },
                        new
                        {
                            Id = 6,
                            Price = 60.96m,
                            ProductImageUrl = "Url",
                            Title = "Product 6"
                        },
                        new
                        {
                            Id = 7,
                            Price = 57.34m,
                            ProductImageUrl = "Url",
                            Title = "Product 7"
                        },
                        new
                        {
                            Id = 8,
                            Price = 57.02m,
                            ProductImageUrl = "Url",
                            Title = "Product 8"
                        },
                        new
                        {
                            Id = 9,
                            Price = 68.27m,
                            ProductImageUrl = "Url",
                            Title = "Product 9"
                        },
                        new
                        {
                            Id = 10,
                            Price = 65.05m,
                            ProductImageUrl = "Url",
                            Title = "Product 10"
                        });
                });

            modelBuilder.Entity("PetShop.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Role")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}