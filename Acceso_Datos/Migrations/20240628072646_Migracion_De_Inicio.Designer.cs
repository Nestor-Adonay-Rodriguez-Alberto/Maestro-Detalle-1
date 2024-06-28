﻿// <auto-generated />
using System;
using Acceso_Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Acceso_Datos.Migrations
{
    [DbContext(typeof(MyDBcontext))]
    [Migration("20240628072646_Migracion_De_Inicio")]
    partial class Migracion_De_Inicio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entidades.Compra", b =>
                {
                    b.Property<int>("Id_Compra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Compra"), 1L, 1);

                    b.Property<int>("Correlativo")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRealizada")
                        .HasColumnType("datetime2");

                    b.HasKey("Id_Compra");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("Entidades.DetalleCompra", b =>
                {
                    b.Property<int>("Id_DetalleCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_DetalleCompra"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdCompraEnDetalle")
                        .HasColumnType("int");

                    b.Property<int>("IdZapatoEnDetalle")
                        .HasColumnType("int");

                    b.Property<double>("Precio_Producto")
                        .HasColumnType("float");

                    b.HasKey("Id_DetalleCompra");

                    b.HasIndex("IdCompraEnDetalle");

                    b.HasIndex("IdZapatoEnDetalle");

                    b.ToTable("DetalleCompras");
                });

            modelBuilder.Entity("Entidades.Zapato", b =>
                {
                    b.Property<int>("Id_Zapato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Zapato"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.HasKey("Id_Zapato");

                    b.ToTable("Zapatos");
                });

            modelBuilder.Entity("Entidades.DetalleCompra", b =>
                {
                    b.HasOne("Entidades.Compra", "Objeto_Compra")
                        .WithMany("Lista_DetalleCompra")
                        .HasForeignKey("IdCompraEnDetalle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entidades.Zapato", "Objeto_Zapato")
                        .WithMany("Lista_DetalleCompra")
                        .HasForeignKey("IdZapatoEnDetalle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Objeto_Compra");

                    b.Navigation("Objeto_Zapato");
                });

            modelBuilder.Entity("Entidades.Compra", b =>
                {
                    b.Navigation("Lista_DetalleCompra");
                });

            modelBuilder.Entity("Entidades.Zapato", b =>
                {
                    b.Navigation("Lista_DetalleCompra");
                });
#pragma warning restore 612, 618
        }
    }
}
