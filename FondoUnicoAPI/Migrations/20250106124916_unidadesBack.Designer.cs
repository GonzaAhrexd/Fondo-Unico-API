﻿// <auto-generated />
using System;
using FondoUnicoAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FondoUnicoAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20250106124916_unidadesBack")]
    partial class unidadesBack
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FondoUnicoAPI.Models.Bancos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Banco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bancos");
                });

            modelBuilder.Entity("FondoUnicoAPI.Models.Entregas", b =>
                {
                    b.Property<int>("NroEntrega")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NroEntrega"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Unidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NroEntrega");

                    b.ToTable("Entregas");
                });

            modelBuilder.Entity("FondoUnicoAPI.Models.Formularios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Formulario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Formularios");
                });

            modelBuilder.Entity("FondoUnicoAPI.Models.RenglonesEntrega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EntregasNroEntrega")
                        .HasColumnType("int");

                    b.Property<int>("NroRenglon")
                        .HasColumnType("int");

                    b.Property<string>("TipoFormulario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cantidad")
                        .HasColumnType("int");

                    b.Property<int>("desde")
                        .HasColumnType("int");

                    b.Property<int>("hasta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EntregasNroEntrega");

                    b.ToTable("RenglonesEntregas");
                });

            modelBuilder.Entity("FondoUnicoAPI.Models.Tipos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tipos");
                });

            modelBuilder.Entity("FondoUnicoAPI.Models.Valores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Valores");
                });

            modelBuilder.Entity("FondoUnicoAPI.Models.RenglonesEntrega", b =>
                {
                    b.HasOne("FondoUnicoAPI.Models.Entregas", null)
                        .WithMany("RenglonesEntregas")
                        .HasForeignKey("EntregasNroEntrega");
                });

            modelBuilder.Entity("FondoUnicoAPI.Models.Entregas", b =>
                {
                    b.Navigation("RenglonesEntregas");
                });
#pragma warning restore 612, 618
        }
    }
}
