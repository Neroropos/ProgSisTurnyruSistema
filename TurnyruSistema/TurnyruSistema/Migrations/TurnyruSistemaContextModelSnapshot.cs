﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TurnyruSistema.Models;

namespace TurnyruSistema.Migrations
{
    [DbContext(typeof(TurnyruSistemaContext))]
    partial class TurnyruSistemaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TurnyruSistema.Models.KomandaTurnyras", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Dalyvauja");

                    b.Property<int>("Ispejimai");

                    b.Property<int>("KomandaId");

                    b.Property<int>("TurnyrasId");

                    b.HasKey("Id");

                    b.HasIndex("KomandaId");

                    b.HasIndex("TurnyrasId");

                    b.ToTable("KomandaTurnyras");
                });

            modelBuilder.Entity("TurnyruSistema.Models.KompiuteriuZona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KompiuteriuSkaicius");

                    b.Property<string>("Pavadinimas");

                    b.Property<int>("TurnyrasId");

                    b.HasKey("Id");

                    b.HasIndex("TurnyrasId");

                    b.ToTable("KompiuteriuZona");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Naudotojas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ElPastas");

                    b.Property<string>("Prisijungimas");

                    b.Property<DateTime>("RegistracijosData");

                    b.Property<string>("Slaptazodis");

                    b.HasKey("Id");

                    b.ToTable("Naudotojas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Naudotojas");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Raundas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Numeris");

                    b.Property<int>("TurnyrasId");

                    b.HasKey("Id");

                    b.HasIndex("TurnyrasId");

                    b.ToTable("Raundas");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Turnyras", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrganizatoriusId");

                    b.Property<DateTime>("PabaigosData");

                    b.Property<string>("Pavadinimas");

                    b.Property<DateTime>("PradziosData");

                    b.Property<string>("Vieta");

                    b.HasKey("Id");

                    b.HasIndex("OrganizatoriusId");

                    b.ToTable("Turnyras");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Zaidejas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("komandaId");

                    b.Property<string>("slapyvardis");

                    b.Property<string>("vardas");

                    b.HasKey("Id");

                    b.HasIndex("komandaId");

                    b.ToTable("Zaidejas");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Zaidimas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Busena");

                    b.Property<int?>("Komanda1Id");

                    b.Property<int?>("Komanda2Id");

                    b.Property<int?>("KompiuteriuZonaId");

                    b.Property<DateTime>("Laikas");

                    b.Property<int>("LaimejusiKomanda");

                    b.Property<int?>("RaundasId");

                    b.HasKey("Id");

                    b.HasIndex("Komanda1Id");

                    b.HasIndex("Komanda2Id");

                    b.HasIndex("KompiuteriuZonaId");

                    b.HasIndex("RaundasId");

                    b.ToTable("Zaidimas");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Zinute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("IssiuntimoData");

                    b.Property<int>("NaudotojasId");

                    b.Property<string>("Tema");

                    b.Property<string>("Turinys");

                    b.HasKey("Id");

                    b.HasIndex("NaudotojasId");

                    b.ToTable("Zinute");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Komanda", b =>
                {
                    b.HasBaseType("TurnyruSistema.Models.Naudotojas");

                    b.Property<int>("Laimejimai");

                    b.Property<string>("Paveikslelis");

                    b.Property<int>("Pralaimejimai");

                    b.Property<string>("pavadinimas");

                    b.ToTable("Komanda");

                    b.HasDiscriminator().HasValue("Komanda");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Organizatorius", b =>
                {
                    b.HasBaseType("TurnyruSistema.Models.Naudotojas");

                    b.Property<string>("RodomasVardas");

                    b.ToTable("Organizatorius");

                    b.HasDiscriminator().HasValue("Organizatorius");
                });

            modelBuilder.Entity("TurnyruSistema.Models.KomandaTurnyras", b =>
                {
                    b.HasOne("TurnyruSistema.Models.Komanda", "Komanda")
                        .WithMany("Turnyrai")
                        .HasForeignKey("KomandaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TurnyruSistema.Models.Turnyras", "Turnyras")
                        .WithMany("Komandos")
                        .HasForeignKey("TurnyrasId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TurnyruSistema.Models.KompiuteriuZona", b =>
                {
                    b.HasOne("TurnyruSistema.Models.Turnyras", "Turnyras")
                        .WithMany("KompiuteriuZonos")
                        .HasForeignKey("TurnyrasId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TurnyruSistema.Models.Raundas", b =>
                {
                    b.HasOne("TurnyruSistema.Models.Turnyras", "Turnyras")
                        .WithMany()
                        .HasForeignKey("TurnyrasId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TurnyruSistema.Models.Turnyras", b =>
                {
                    b.HasOne("TurnyruSistema.Models.Organizatorius", "Organizatorius")
                        .WithMany()
                        .HasForeignKey("OrganizatoriusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TurnyruSistema.Models.Zaidejas", b =>
                {
                    b.HasOne("TurnyruSistema.Models.Komanda", "komanda")
                        .WithMany("zaidejai")
                        .HasForeignKey("komandaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TurnyruSistema.Models.Zaidimas", b =>
                {
                    b.HasOne("TurnyruSistema.Models.Komanda", "Komanda1")
                        .WithMany("Zaidimai1")
                        .HasForeignKey("Komanda1Id");

                    b.HasOne("TurnyruSistema.Models.Komanda", "Komanda2")
                        .WithMany("Zaidimai2")
                        .HasForeignKey("Komanda2Id");

                    b.HasOne("TurnyruSistema.Models.KompiuteriuZona", "KompiuteriuZona")
                        .WithMany("Zaidimai")
                        .HasForeignKey("KompiuteriuZonaId");

                    b.HasOne("TurnyruSistema.Models.Raundas", "Raundas")
                        .WithMany("Zaidimai")
                        .HasForeignKey("RaundasId");
                });

            modelBuilder.Entity("TurnyruSistema.Models.Zinute", b =>
                {
                    b.HasOne("TurnyruSistema.Models.Naudotojas", "Naudotojas")
                        .WithMany("Zinutes")
                        .HasForeignKey("NaudotojasId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
