using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TurnyruSistema.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizatorius",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Prisijungimas = table.Column<string>(nullable: true),
                    Slaptazodis = table.Column<string>(nullable: true),
                    ElPastas = table.Column<string>(nullable: true),
                    RegistracijosData = table.Column<DateTime>(nullable: false),
                    RodomasVardas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizatorius", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turnyras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pavadinimas = table.Column<string>(nullable: true),
                    Vieta = table.Column<string>(nullable: true),
                    PradziosData = table.Column<DateTime>(nullable: false),
                    PabaigosData = table.Column<DateTime>(nullable: false),
                    OrganizatoriusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnyras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnyras_Organizatorius_OrganizatoriusId",
                        column: x => x.OrganizatoriusId,
                        principalTable: "Organizatorius",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turnyras_OrganizatoriusId",
                table: "Turnyras",
                column: "OrganizatoriusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turnyras");

            migrationBuilder.DropTable(
                name: "Organizatorius");
        }
    }
}
