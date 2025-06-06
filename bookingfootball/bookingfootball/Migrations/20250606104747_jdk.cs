using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class jdk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaiKhoans_NhanVienId",
                table: "TaiKhoans");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_NhanVienId",
                table: "TaiKhoans",
                column: "NhanVienId",
                unique: true,
                filter: "[NhanVienId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaiKhoans_NhanVienId",
                table: "TaiKhoans");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_NhanVienId",
                table: "TaiKhoans",
                column: "NhanVienId");
        }
    }
}
