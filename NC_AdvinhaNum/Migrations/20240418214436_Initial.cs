using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NC_AdvinhaNum.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvinhaNC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    COD_JOGADOR = table.Column<int>(type: "INTEGER", nullable: false),
                    NUM_TENTATIVA = table.Column<int>(type: "INTEGER", nullable: false),
                    HorarioTentativa = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Resultado = table.Column<string>(type: "TEXT", nullable: true),
                    Porcentagem = table.Column<double>(type: "REAL", nullable: true),
                    DataFormatada = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvinhaNC", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvinhaNC");
        }
    }
}
