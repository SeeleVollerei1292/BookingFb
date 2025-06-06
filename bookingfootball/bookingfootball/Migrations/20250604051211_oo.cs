using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class oo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_NhanVien_NhanVienId",
                table: "TaiKhoan");

            migrationBuilder.DropIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan",
                column: "NhanVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_NhanVien_NhanVienId",
                table: "TaiKhoan",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_NhanVien_NhanVienId",
                table: "TaiKhoan");

            migrationBuilder.DropIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan",
                column: "NhanVienId",
                unique: true,
                filter: "[NhanVienId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_NhanVien_NhanVienId",
                table: "TaiKhoan",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
