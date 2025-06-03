using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class ThemHoadon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoaDonChiTietId",
                table: "DichVuDatBong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DichVuDatBong_HoaDonChiTietId",
                table: "DichVuDatBong",
                column: "HoaDonChiTietId");

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBong_HoaDonChiTiet_HoaDonChiTietId",
                table: "DichVuDatBong",
                column: "HoaDonChiTietId",
                principalTable: "HoaDonChiTiet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBong_HoaDonChiTiet_HoaDonChiTietId",
                table: "DichVuDatBong");

            migrationBuilder.DropIndex(
                name: "IX_DichVuDatBong_HoaDonChiTietId",
                table: "DichVuDatBong");

            migrationBuilder.DropColumn(
                name: "HoaDonChiTietId",
                table: "DichVuDatBong");
        }
    }
}
