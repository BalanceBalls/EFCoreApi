﻿// <auto-generated />
using System;
using EFCoreApi.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20200330135232_AddPairs")]
    partial class AddPairs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCoreApi.Models.ViewModel.PairData", b =>
                {
                    b.Property<long>("PairId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("BuyPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("HighPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LastTradePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LowPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PairName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("SellPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Volume")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PairId");

                    b.ToTable("Pairs");
                });
#pragma warning restore 612, 618
        }
    }
}
