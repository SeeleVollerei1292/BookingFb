using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class ops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThoiGianDatSans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdHoaDon = table.Column<int>(type: "int", nullable: false),
                    IdSanCa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoiGianDatSans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThoiGianDatSans_HoaDons_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThoiGianDatSans_SanCas_IdSanCa",
                        column: x => x.IdSanCa,
                        principalTable: "SanCas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThoiGianDatSans_IdHoaDon",
                table: "ThoiGianDatSans",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ThoiGianDatSans_IdSanCa",
                table: "ThoiGianDatSans",
                column: "IdSanCa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThoiGianDatSans");
        }
    }
}
