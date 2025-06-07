using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class ks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanbongs_LoaiSans_LoaiSanId",
                table: "Sanbongs");

            migrationBuilder.AlterColumn<int>(
                name: "LoaiSanId",
                table: "Sanbongs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Gia",
                table: "Sanbongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "Sanbongs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "HinhAnh",
                table: "LoaiSans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanbongs_LoaiSans_LoaiSanId",
                table: "Sanbongs",
                column: "LoaiSanId",
                principalTable: "LoaiSans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanbongs_LoaiSans_LoaiSanId",
                table: "Sanbongs");

            migrationBuilder.DropColumn(
                name: "Gia",
                table: "Sanbongs");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "Sanbongs");

            migrationBuilder.AlterColumn<int>(
                name: "LoaiSanId",
                table: "Sanbongs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HinhAnh",
                table: "LoaiSans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanbongs_LoaiSans_LoaiSanId",
                table: "Sanbongs",
                column: "LoaiSanId",
                principalTable: "LoaiSans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
