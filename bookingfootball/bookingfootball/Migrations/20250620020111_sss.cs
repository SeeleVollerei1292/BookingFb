using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class sss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoThueId",
                table: "DichVuDatBong",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DichVuDatBong_DoThueId",
                table: "DichVuDatBong",
                column: "DoThueId");

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBong_DoThues_DoThueId",
                table: "DichVuDatBong",
                column: "DoThueId",
                principalTable: "DoThues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBong_DoThues_DoThueId",
                table: "DichVuDatBong");

            migrationBuilder.DropIndex(
                name: "IX_DichVuDatBong_DoThueId",
                table: "DichVuDatBong");

            migrationBuilder.DropColumn(
                name: "DoThueId",
                table: "DichVuDatBong");
        }
    }
}
