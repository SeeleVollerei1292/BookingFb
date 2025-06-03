using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class themmoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhanVienId",
                table: "HoaDonChiTiet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_NhanVienId",
                table: "HoaDonChiTiet",
                column: "NhanVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiet_NhanVien_NhanVienId",
                table: "HoaDonChiTiet",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiet_NhanVien_NhanVienId",
                table: "HoaDonChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_HoaDonChiTiet_NhanVienId",
                table: "HoaDonChiTiet");

            migrationBuilder.DropColumn(
                name: "NhanVienId",
                table: "HoaDonChiTiet");
        }
    }
}
