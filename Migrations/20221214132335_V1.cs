using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prvenstvo.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stadioni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false),
                    Otvaranje = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadioni", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Timovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rezultat = table.Column<int>(type: "int", nullable: false),
                    Faza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StadionID = table.Column<int>(type: "int", nullable: true),
                    VremeIgranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Timovi_Stadioni_StadionID",
                        column: x => x.StadionID,
                        principalTable: "Stadioni",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Utakmice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Posecenost = table.Column<int>(type: "int", nullable: false),
                    BrojPosetioca = table.Column<int>(type: "int", nullable: false),
                    StadionID = table.Column<int>(type: "int", nullable: true),
                    TimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utakmice_Stadioni_StadionID",
                        column: x => x.StadionID,
                        principalTable: "Stadioni",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Utakmice_Timovi_TimID",
                        column: x => x.TimID,
                        principalTable: "Timovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timovi_StadionID",
                table: "Timovi",
                column: "StadionID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_StadionID",
                table: "Utakmice",
                column: "StadionID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_TimID",
                table: "Utakmice",
                column: "TimID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utakmice");

            migrationBuilder.DropTable(
                name: "Timovi");

            migrationBuilder.DropTable(
                name: "Stadioni");
        }
    }
}
