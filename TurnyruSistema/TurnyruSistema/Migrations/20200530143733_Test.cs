using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TurnyruSistema.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnyras_Organizatorius_OrganizatoriusId",
                table: "Turnyras");

            migrationBuilder.DropForeignKey(
                name: "FK_Zaidejas_Komanda_komandaId",
                table: "Zaidejas");

            migrationBuilder.DropTable(
                name: "Komanda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizatorius",
                table: "Organizatorius");

            migrationBuilder.RenameTable(
                name: "Organizatorius",
                newName: "Naudotojas");

            migrationBuilder.AddColumn<int>(
                name: "Laimejimai",
                table: "Naudotojas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Paveikslelis",
                table: "Naudotojas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pralaimejimai",
                table: "Naudotojas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pavadinimas",
                table: "Naudotojas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Naudotojas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Naudotojas",
                table: "Naudotojas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "KomandaTurnyras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dalyvauja = table.Column<bool>(nullable: false),
                    Ispejimai = table.Column<int>(nullable: false),
                    KomandaId = table.Column<int>(nullable: false),
                    TurnyrasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomandaTurnyras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KomandaTurnyras_Naudotojas_KomandaId",
                        column: x => x.KomandaId,
                        principalTable: "Naudotojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KomandaTurnyras_Turnyras_TurnyrasId",
                        column: x => x.TurnyrasId,
                        principalTable: "Turnyras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Raundas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Numeris = table.Column<int>(nullable: false),
                    TurnyrasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raundas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raundas_Turnyras_TurnyrasId",
                        column: x => x.TurnyrasId,
                        principalTable: "Turnyras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zinute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tema = table.Column<string>(nullable: true),
                    Turinys = table.Column<string>(nullable: true),
                    IssiuntimoData = table.Column<DateTime>(nullable: false),
                    NaudotojasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zinute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zinute_Naudotojas_NaudotojasId",
                        column: x => x.NaudotojasId,
                        principalTable: "Naudotojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zaidimas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Laikas = table.Column<DateTime>(nullable: false),
                    Busena = table.Column<int>(nullable: false),
                    Komanda1Id = table.Column<int>(nullable: true),
                    Komanda2Id = table.Column<int>(nullable: true),
                    LaimejusiKomanda = table.Column<int>(nullable: false),
                    KompiuteriuZonaId = table.Column<int>(nullable: true),
                    RaundasId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaidimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zaidimas_Naudotojas_Komanda1Id",
                        column: x => x.Komanda1Id,
                        principalTable: "Naudotojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaidimas_Naudotojas_Komanda2Id",
                        column: x => x.Komanda2Id,
                        principalTable: "Naudotojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaidimas_KompiuteriuZona_KompiuteriuZonaId",
                        column: x => x.KompiuteriuZonaId,
                        principalTable: "KompiuteriuZona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaidimas_Raundas_RaundasId",
                        column: x => x.RaundasId,
                        principalTable: "Raundas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomandaTurnyras_KomandaId",
                table: "KomandaTurnyras",
                column: "KomandaId");

            migrationBuilder.CreateIndex(
                name: "IX_KomandaTurnyras_TurnyrasId",
                table: "KomandaTurnyras",
                column: "TurnyrasId");

            migrationBuilder.CreateIndex(
                name: "IX_Raundas_TurnyrasId",
                table: "Raundas",
                column: "TurnyrasId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaidimas_Komanda1Id",
                table: "Zaidimas",
                column: "Komanda1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zaidimas_Komanda2Id",
                table: "Zaidimas",
                column: "Komanda2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zaidimas_KompiuteriuZonaId",
                table: "Zaidimas",
                column: "KompiuteriuZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaidimas_RaundasId",
                table: "Zaidimas",
                column: "RaundasId");

            migrationBuilder.CreateIndex(
                name: "IX_Zinute_NaudotojasId",
                table: "Zinute",
                column: "NaudotojasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnyras_Naudotojas_OrganizatoriusId",
                table: "Turnyras",
                column: "OrganizatoriusId",
                principalTable: "Naudotojas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zaidejas_Naudotojas_komandaId",
                table: "Zaidejas",
                column: "komandaId",
                principalTable: "Naudotojas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnyras_Naudotojas_OrganizatoriusId",
                table: "Turnyras");

            migrationBuilder.DropForeignKey(
                name: "FK_Zaidejas_Naudotojas_komandaId",
                table: "Zaidejas");

            migrationBuilder.DropTable(
                name: "KomandaTurnyras");

            migrationBuilder.DropTable(
                name: "Zaidimas");

            migrationBuilder.DropTable(
                name: "Zinute");

            migrationBuilder.DropTable(
                name: "Raundas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Naudotojas",
                table: "Naudotojas");

            migrationBuilder.DropColumn(
                name: "Laimejimai",
                table: "Naudotojas");

            migrationBuilder.DropColumn(
                name: "Paveikslelis",
                table: "Naudotojas");

            migrationBuilder.DropColumn(
                name: "Pralaimejimai",
                table: "Naudotojas");

            migrationBuilder.DropColumn(
                name: "pavadinimas",
                table: "Naudotojas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Naudotojas");

            migrationBuilder.RenameTable(
                name: "Naudotojas",
                newName: "Organizatorius");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizatorius",
                table: "Organizatorius",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Komanda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElPastas = table.Column<string>(nullable: true),
                    Laimejimai = table.Column<int>(nullable: false),
                    Paveikslelis = table.Column<string>(nullable: true),
                    Pralaimejimai = table.Column<int>(nullable: false),
                    Prisijungimas = table.Column<string>(nullable: true),
                    RegistracijosData = table.Column<DateTime>(nullable: false),
                    Slaptazodis = table.Column<string>(nullable: true),
                    pavadinimas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komanda", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Turnyras_Organizatorius_OrganizatoriusId",
                table: "Turnyras",
                column: "OrganizatoriusId",
                principalTable: "Organizatorius",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zaidejas_Komanda_komandaId",
                table: "Zaidejas",
                column: "komandaId",
                principalTable: "Komanda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
