﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TesteDevDbm.Context;

#nullable disable

namespace TesteDevDbm.Migrations
{
    [DbContext(typeof(ProtocoloContext))]
    partial class ProtocoloContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TesteDevDbm.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("IdCliente");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("TesteDevDbm.Models.Protocolo", b =>
                {
                    b.Property<int>("IdProtocolo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProtocolo"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DataAbertura")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DataFechamento")
                        .HasColumnType("date");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ProtocoloStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdProtocolo");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ProtocoloStatusId");

                    b.ToTable("Protocolos", (string)null);
                });

            modelBuilder.Entity("TesteDevDbm.Models.ProtocoloFollow", b =>
                {
                    b.Property<int>("IdFollow")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFollow"));

                    b.Property<DateOnly>("DataAcao")
                        .HasColumnType("date");

                    b.Property<string>("DescricaoAcao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ProtocoloId")
                        .HasColumnType("int");

                    b.HasKey("IdFollow");

                    b.HasIndex("ProtocoloId");

                    b.ToTable("ProtocolosFollow", (string)null);
                });

            modelBuilder.Entity("TesteDevDbm.Models.StatusProtocolo", b =>
                {
                    b.Property<int>("IdStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStatus"));

                    b.Property<string>("NomeStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdStatus");

                    b.ToTable("StatusProtocolos", (string)null);

                    b.HasData(
                        new
                        {
                            IdStatus = 1,
                            NomeStatus = "Aberto"
                        },
                        new
                        {
                            IdStatus = 2,
                            NomeStatus = "Em Andamento"
                        },
                        new
                        {
                            IdStatus = 3,
                            NomeStatus = "Fechado"
                        });
                });

            modelBuilder.Entity("TesteDevDbm.Models.Protocolo", b =>
                {
                    b.HasOne("TesteDevDbm.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TesteDevDbm.Models.StatusProtocolo", "ProtocoloStatus")
                        .WithMany()
                        .HasForeignKey("ProtocoloStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("ProtocoloStatus");
                });

            modelBuilder.Entity("TesteDevDbm.Models.ProtocoloFollow", b =>
                {
                    b.HasOne("TesteDevDbm.Models.Protocolo", "Protocolo")
                        .WithMany()
                        .HasForeignKey("ProtocoloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Protocolo");
                });
#pragma warning restore 612, 618
        }
    }
}
