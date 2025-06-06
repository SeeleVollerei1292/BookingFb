using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class l : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan");

            migrationBuilder.AlterColumn<int>(
                name: "NhanVienId",
                table: "TaiKhoan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan",
                column: "NhanVienId",
                unique: true,
                filter: "[NhanVienId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan");

            migrationBuilder.AlterColumn<int>(
                name: "NhanVienId",
                table: "TaiKhoan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoan",
                column: "NhanVienId",
                unique: true);
        }
    }
}
