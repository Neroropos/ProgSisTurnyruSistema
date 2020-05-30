using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TurnyruSistema.Migrations
{
    public partial class Komanda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Komanda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Prisijungimas = table.Column<string>(nullable: true),
                    Slaptazodis = table.Column<string>(nullable: true),
                    ElPastas = table.Column<string>(nullable: true),
                    RegistracijosData = table.Column<DateTime>(nullable: false),
                    Laimejimai = table.Column<int>(nullable: false),
                    Pralaimejimai = table.Column<int>(nullable: false),
                    Paveikslelis = table.Column<string>(nullable: true),
                    pavadinimas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komanda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zaidejas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vardas = table.Column<string>(nullable: true),
                    slapyvardis = table.Column<string>(nullable: true),
                    komandaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaidejas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zaidejas_Komanda_komandaId",
                        column: x => x.komandaId,
                        principalTable: "Komanda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zaidejas_komandaId",
                table: "Zaidejas",
                column: "komandaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zaidejas");

            migrationBuilder.DropTable(
                name: "Komanda");
        }
    }
}
