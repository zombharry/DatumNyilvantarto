using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Osztalyok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Betu = table.Column<string>(type: "TEXT", nullable: false),
                    EvFolyam = table.Column<int>(type: "INTEGER", nullable: false),
                    OsztalyFonok = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osztalyok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tanulok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nev = table.Column<string>(type: "TEXT", nullable: false),
                    OsztalyId = table.Column<int>(type: "INTEGER", nullable: false),
                    GyermekVedelmi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Hatranyos = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HalmozottanHatranyos = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanulok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tanulok_Osztalyok_OsztalyId",
                        column: x => x.OsztalyId,
                        principalTable: "Osztalyok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tanulok_OsztalyId",
                table: "Tanulok",
                column: "OsztalyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tanulok");

            migrationBuilder.DropTable(
                name: "Osztalyok");
        }
    }
}
