﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SzafyNaLeki.Entities;

#nullable disable

namespace SzafyNaLeki.Migrations
{
    [DbContext(typeof(SzafaDbContext))]
    [Migration("20240131113741_alarm")]
    partial class alarm
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SzafyNaLeki.Entities.Alarm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aktywny")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Alarm");
                });

            modelBuilder.Entity("SzafyNaLeki.Entities.Szafa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Alarm")
                        .HasColumnType("bit");

                    b.Property<bool>("CzyZepsuta")
                        .HasColumnType("bit");

                    b.Property<float>("Temperatura1")
                        .HasColumnType("real");

                    b.Property<float>("Temperatura2")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Szafy");
                });
#pragma warning restore 612, 618
        }
    }
}
