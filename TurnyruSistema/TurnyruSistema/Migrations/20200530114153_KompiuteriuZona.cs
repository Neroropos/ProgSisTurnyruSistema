using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TurnyruSistema.Migrations
{
    public partial class KompiuteriuZona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KompiuteriuZona",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pavadinimas = table.Column<string>(nullable: true),
                    KompiuteriuSkaicius = table.Column<int>(nullable: false),
                    TurnyrasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KompiuteriuZona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KompiuteriuZona_Turnyras_TurnyrasId",
                        column: x => x.TurnyrasId,
                        principalTable: "Turnyras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KompiuteriuZona_TurnyrasId",
                table: "KompiuteriuZona",
                column: "TurnyrasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KompiuteriuZona");
        }
    }
}
