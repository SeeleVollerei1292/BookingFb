using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class akak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SanCaId",
                table: "HoaDonChiTiets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_SanCaId",
                table: "HoaDonChiTiets",
                column: "SanCaId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiets_SanCas_SanCaId",
                table: "HoaDonChiTiets",
                column: "SanCaId",
                principalTable: "SanCas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiets_SanCas_SanCaId",
                table: "HoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_HoaDonChiTiets_SanCaId",
                table: "HoaDonChiTiets");

            migrationBuilder.DropColumn(
                name: "SanCaId",
                table: "HoaDonChiTiets");
        }
    }
}
