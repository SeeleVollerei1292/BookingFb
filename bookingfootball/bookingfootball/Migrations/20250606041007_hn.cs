using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class hn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CT_HinhThucThanhToan_HinhThucThanhToan_HinhThucThanhToanId",
                table: "CT_HinhThucThanhToan");

            migrationBuilder.DropForeignKey(
                name: "FK_CT_HinhThucThanhToan_HoaDonChiTiet_HoaDonChiTietId",
                table: "CT_HinhThucThanhToan");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaChiKhachHang_KhachHang_KhachHangId",
                table: "DiaChiKhachHang");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBong_HoaDonChiTiet_HoaDonChiTietId",
                table: "DichVuDatBong");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBong_NuocUong_NuocUongId",
                table: "DichVuDatBong");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBong_Thue_ThueSanId",
                table: "DichVuDatBong");

            migrationBuilder.DropForeignKey(
                name: "FK_DiemDanh_NhanVien_NhanVienId",
                table: "DiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_KhachHang_KhachHangId",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_NhanVien_NhanVienId",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiet_HoaDon_HoaDonId",
                table: "HoaDonChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiet_NhanVien_NhanVienId",
                table: "HoaDonChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiet_PhieuGiamGiaChiTiet_PhieuGiamGiaId",
                table: "HoaDonChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiet_Sanbongs_SanBongId",
                table: "HoaDonChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_LichLamViec_NhanVien_NhanVienId",
                table: "LichLamViec");

            migrationBuilder.DropForeignKey(
                name: "FK_LichSuHoaDon_HoaDon_HoaDonId",
                table: "LichSuHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuGiamGiaChiTiet_KhachHang_KhachHangId",
                table: "PhieuGiamGiaChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuGiamGiaChiTiet_PhieuGiamGia_PhieuGiamGiaId",
                table: "PhieuGiamGiaChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_KhachHang_KhachHangId",
                table: "TaiKhoan");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_NhanVien_NhanVienId",
                table: "TaiKhoan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Thue",
                table: "Thue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaiKhoan",
                table: "TaiKhoan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhieuGiamGiaChiTiet",
                table: "PhieuGiamGiaChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NuocUong",
                table: "NuocUong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NhanVien",
                table: "NhanVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LichSuHoaDon",
                table: "LichSuHoaDon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LichLamViec",
                table: "LichLamViec");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KhachHang",
                table: "KhachHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoaDonChiTiet",
                table: "HoaDonChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HinhThucThanhToan",
                table: "HinhThucThanhToan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiemDanh",
                table: "DiemDanh");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DichVuDatBong",
                table: "DichVuDatBong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaChiKhachHang",
                table: "DiaChiKhachHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CT_HinhThucThanhToan",
                table: "CT_HinhThucThanhToan");

            migrationBuilder.RenameTable(
                name: "Thue",
                newName: "Thues");

            migrationBuilder.RenameTable(
                name: "TaiKhoan",
                newName: "TaiKhoans");

            migrationBuilder.RenameTable(
                name: "PhieuGiamGiaChiTiet",
                newName: "PhieuGiamGiaChiTiets");

            migrationBuilder.RenameTable(
                name: "NuocUong",
                newName: "NuocUongs");

            migrationBuilder.RenameTable(
                name: "NhanVien",
                newName: "NhanViens");

            migrationBuilder.RenameTable(
                name: "LichSuHoaDon",
                newName: "LichSuHoaDons");

            migrationBuilder.RenameTable(
                name: "LichLamViec",
                newName: "LichLamViecs");

            migrationBuilder.RenameTable(
                name: "KhachHang",
                newName: "KhachHangs");

            migrationBuilder.RenameTable(
                name: "HoaDonChiTiet",
                newName: "HoaDonChiTiets");

            migrationBuilder.RenameTable(
                name: "HinhThucThanhToan",
                newName: "HinhThucThanhToans");

            migrationBuilder.RenameTable(
                name: "DiemDanh",
                newName: "DiemDanhs");

            migrationBuilder.RenameTable(
                name: "DichVuDatBong",
                newName: "DichVuDatBongs");

            migrationBuilder.RenameTable(
                name: "DiaChiKhachHang",
                newName: "DiaChiKhachHangs");

            migrationBuilder.RenameTable(
                name: "CT_HinhThucThanhToan",
                newName: "CT_HinhThucThanhToans");

            migrationBuilder.RenameIndex(
                name: "IX_TaiKhoan_NhanVienId",
                table: "TaiKhoans",
                newName: "IX_TaiKhoans_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_TaiKhoan_KhachHangId",
                table: "TaiKhoans",
                newName: "IX_TaiKhoans_KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuGiamGiaChiTiet_PhieuGiamGiaId",
                table: "PhieuGiamGiaChiTiets",
                newName: "IX_PhieuGiamGiaChiTiets_PhieuGiamGiaId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuGiamGiaChiTiet_KhachHangId",
                table: "PhieuGiamGiaChiTiets",
                newName: "IX_PhieuGiamGiaChiTiets_KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_LichSuHoaDon_HoaDonId",
                table: "LichSuHoaDons",
                newName: "IX_LichSuHoaDons_HoaDonId");

            migrationBuilder.RenameIndex(
                name: "IX_LichLamViec_NhanVienId",
                table: "LichLamViecs",
                newName: "IX_LichLamViecs_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiet_SanBongId",
                table: "HoaDonChiTiets",
                newName: "IX_HoaDonChiTiets_SanBongId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiet_PhieuGiamGiaId",
                table: "HoaDonChiTiets",
                newName: "IX_HoaDonChiTiets_PhieuGiamGiaId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiet_NhanVienId",
                table: "HoaDonChiTiets",
                newName: "IX_HoaDonChiTiets_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiet_HoaDonId",
                table: "HoaDonChiTiets",
                newName: "IX_HoaDonChiTiets_HoaDonId");

            migrationBuilder.RenameIndex(
                name: "IX_DiemDanh_NhanVienId",
                table: "DiemDanhs",
                newName: "IX_DiemDanhs_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_DichVuDatBong_ThueSanId",
                table: "DichVuDatBongs",
                newName: "IX_DichVuDatBongs_ThueSanId");

            migrationBuilder.RenameIndex(
                name: "IX_DichVuDatBong_NuocUongId",
                table: "DichVuDatBongs",
                newName: "IX_DichVuDatBongs_NuocUongId");

            migrationBuilder.RenameIndex(
                name: "IX_DichVuDatBong_HoaDonChiTietId",
                table: "DichVuDatBongs",
                newName: "IX_DichVuDatBongs_HoaDonChiTietId");

            migrationBuilder.RenameIndex(
                name: "IX_DiaChiKhachHang_KhachHangId",
                table: "DiaChiKhachHangs",
                newName: "IX_DiaChiKhachHangs_KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_CT_HinhThucThanhToan_HoaDonChiTietId",
                table: "CT_HinhThucThanhToans",
                newName: "IX_CT_HinhThucThanhToans_HoaDonChiTietId");

            migrationBuilder.RenameIndex(
                name: "IX_CT_HinhThucThanhToan_HinhThucThanhToanId",
                table: "CT_HinhThucThanhToans",
                newName: "IX_CT_HinhThucThanhToans_HinhThucThanhToanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Thues",
                table: "Thues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaiKhoans",
                table: "TaiKhoans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhieuGiamGiaChiTiets",
                table: "PhieuGiamGiaChiTiets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NuocUongs",
                table: "NuocUongs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NhanViens",
                table: "NhanViens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LichSuHoaDons",
                table: "LichSuHoaDons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LichLamViecs",
                table: "LichLamViecs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhachHangs",
                table: "KhachHangs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoaDonChiTiets",
                table: "HoaDonChiTiets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HinhThucThanhToans",
                table: "HinhThucThanhToans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiemDanhs",
                table: "DiemDanhs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DichVuDatBongs",
                table: "DichVuDatBongs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaChiKhachHangs",
                table: "DiaChiKhachHangs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CT_HinhThucThanhToans",
                table: "CT_HinhThucThanhToans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CT_HinhThucThanhToans_HinhThucThanhToans_HinhThucThanhToanId",
                table: "CT_HinhThucThanhToans",
                column: "HinhThucThanhToanId",
                principalTable: "HinhThucThanhToans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CT_HinhThucThanhToans_HoaDonChiTiets_HoaDonChiTietId",
                table: "CT_HinhThucThanhToans",
                column: "HoaDonChiTietId",
                principalTable: "HoaDonChiTiets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiaChiKhachHangs_KhachHangs_KhachHangId",
                table: "DiaChiKhachHangs",
                column: "KhachHangId",
                principalTable: "KhachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBongs_HoaDonChiTiets_HoaDonChiTietId",
                table: "DichVuDatBongs",
                column: "HoaDonChiTietId",
                principalTable: "HoaDonChiTiets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBongs_NuocUongs_NuocUongId",
                table: "DichVuDatBongs",
                column: "NuocUongId",
                principalTable: "NuocUongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBongs_Thues_ThueSanId",
                table: "DichVuDatBongs",
                column: "ThueSanId",
                principalTable: "Thues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDanhs_NhanViens_NhanVienId",
                table: "DiemDanhs",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_KhachHangs_KhachHangId",
                table: "HoaDon",
                column: "KhachHangId",
                principalTable: "KhachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_NhanViens_NhanVienId",
                table: "HoaDon",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiets_HoaDon_HoaDonId",
                table: "HoaDonChiTiets",
                column: "HoaDonId",
                principalTable: "HoaDon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiets_NhanViens_NhanVienId",
                table: "HoaDonChiTiets",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiets_PhieuGiamGiaChiTiets_PhieuGiamGiaId",
                table: "HoaDonChiTiets",
                column: "PhieuGiamGiaId",
                principalTable: "PhieuGiamGiaChiTiets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiets_Sanbongs_SanBongId",
                table: "HoaDonChiTiets",
                column: "SanBongId",
                principalTable: "Sanbongs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LichLamViecs_NhanViens_NhanVienId",
                table: "LichLamViecs",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuHoaDons_HoaDon_HoaDonId",
                table: "LichSuHoaDons",
                column: "HoaDonId",
                principalTable: "HoaDon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuGiamGiaChiTiets_KhachHangs_KhachHangId",
                table: "PhieuGiamGiaChiTiets",
                column: "KhachHangId",
                principalTable: "KhachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuGiamGiaChiTiets_PhieuGiamGia_PhieuGiamGiaId",
                table: "PhieuGiamGiaChiTiets",
                column: "PhieuGiamGiaId",
                principalTable: "PhieuGiamGia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoans_KhachHangs_KhachHangId",
                table: "TaiKhoans",
                column: "KhachHangId",
                principalTable: "KhachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoans_NhanViens_NhanVienId",
                table: "TaiKhoans",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CT_HinhThucThanhToans_HinhThucThanhToans_HinhThucThanhToanId",
                table: "CT_HinhThucThanhToans");

            migrationBuilder.DropForeignKey(
                name: "FK_CT_HinhThucThanhToans_HoaDonChiTiets_HoaDonChiTietId",
                table: "CT_HinhThucThanhToans");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaChiKhachHangs_KhachHangs_KhachHangId",
                table: "DiaChiKhachHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBongs_HoaDonChiTiets_HoaDonChiTietId",
                table: "DichVuDatBongs");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBongs_NuocUongs_NuocUongId",
                table: "DichVuDatBongs");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuDatBongs_Thues_ThueSanId",
                table: "DichVuDatBongs");

            migrationBuilder.DropForeignKey(
                name: "FK_DiemDanhs_NhanViens_NhanVienId",
                table: "DiemDanhs");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_KhachHangs_KhachHangId",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_NhanViens_NhanVienId",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiets_HoaDon_HoaDonId",
                table: "HoaDonChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiets_NhanViens_NhanVienId",
                table: "HoaDonChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiets_PhieuGiamGiaChiTiets_PhieuGiamGiaId",
                table: "HoaDonChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDonChiTiets_Sanbongs_SanBongId",
                table: "HoaDonChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_LichLamViecs_NhanViens_NhanVienId",
                table: "LichLamViecs");

            migrationBuilder.DropForeignKey(
                name: "FK_LichSuHoaDons_HoaDon_HoaDonId",
                table: "LichSuHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuGiamGiaChiTiets_KhachHangs_KhachHangId",
                table: "PhieuGiamGiaChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuGiamGiaChiTiets_PhieuGiamGia_PhieuGiamGiaId",
                table: "PhieuGiamGiaChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoans_KhachHangs_KhachHangId",
                table: "TaiKhoans");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoans_NhanViens_NhanVienId",
                table: "TaiKhoans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Thues",
                table: "Thues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaiKhoans",
                table: "TaiKhoans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhieuGiamGiaChiTiets",
                table: "PhieuGiamGiaChiTiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NuocUongs",
                table: "NuocUongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NhanViens",
                table: "NhanViens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LichSuHoaDons",
                table: "LichSuHoaDons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LichLamViecs",
                table: "LichLamViecs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KhachHangs",
                table: "KhachHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoaDonChiTiets",
                table: "HoaDonChiTiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HinhThucThanhToans",
                table: "HinhThucThanhToans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiemDanhs",
                table: "DiemDanhs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DichVuDatBongs",
                table: "DichVuDatBongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaChiKhachHangs",
                table: "DiaChiKhachHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CT_HinhThucThanhToans",
                table: "CT_HinhThucThanhToans");

            migrationBuilder.RenameTable(
                name: "Thues",
                newName: "Thue");

            migrationBuilder.RenameTable(
                name: "TaiKhoans",
                newName: "TaiKhoan");

            migrationBuilder.RenameTable(
                name: "PhieuGiamGiaChiTiets",
                newName: "PhieuGiamGiaChiTiet");

            migrationBuilder.RenameTable(
                name: "NuocUongs",
                newName: "NuocUong");

            migrationBuilder.RenameTable(
                name: "NhanViens",
                newName: "NhanVien");

            migrationBuilder.RenameTable(
                name: "LichSuHoaDons",
                newName: "LichSuHoaDon");

            migrationBuilder.RenameTable(
                name: "LichLamViecs",
                newName: "LichLamViec");

            migrationBuilder.RenameTable(
                name: "KhachHangs",
                newName: "KhachHang");

            migrationBuilder.RenameTable(
                name: "HoaDonChiTiets",
                newName: "HoaDonChiTiet");

            migrationBuilder.RenameTable(
                name: "HinhThucThanhToans",
                newName: "HinhThucThanhToan");

            migrationBuilder.RenameTable(
                name: "DiemDanhs",
                newName: "DiemDanh");

            migrationBuilder.RenameTable(
                name: "DichVuDatBongs",
                newName: "DichVuDatBong");

            migrationBuilder.RenameTable(
                name: "DiaChiKhachHangs",
                newName: "DiaChiKhachHang");

            migrationBuilder.RenameTable(
                name: "CT_HinhThucThanhToans",
                newName: "CT_HinhThucThanhToan");

            migrationBuilder.RenameIndex(
                name: "IX_TaiKhoans_NhanVienId",
                table: "TaiKhoan",
                newName: "IX_TaiKhoan_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_TaiKhoans_KhachHangId",
                table: "TaiKhoan",
                newName: "IX_TaiKhoan_KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuGiamGiaChiTiets_PhieuGiamGiaId",
                table: "PhieuGiamGiaChiTiet",
                newName: "IX_PhieuGiamGiaChiTiet_PhieuGiamGiaId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuGiamGiaChiTiets_KhachHangId",
                table: "PhieuGiamGiaChiTiet",
                newName: "IX_PhieuGiamGiaChiTiet_KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_LichSuHoaDons_HoaDonId",
                table: "LichSuHoaDon",
                newName: "IX_LichSuHoaDon_HoaDonId");

            migrationBuilder.RenameIndex(
                name: "IX_LichLamViecs_NhanVienId",
                table: "LichLamViec",
                newName: "IX_LichLamViec_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiets_SanBongId",
                table: "HoaDonChiTiet",
                newName: "IX_HoaDonChiTiet_SanBongId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiets_PhieuGiamGiaId",
                table: "HoaDonChiTiet",
                newName: "IX_HoaDonChiTiet_PhieuGiamGiaId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiets_NhanVienId",
                table: "HoaDonChiTiet",
                newName: "IX_HoaDonChiTiet_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDonChiTiets_HoaDonId",
                table: "HoaDonChiTiet",
                newName: "IX_HoaDonChiTiet_HoaDonId");

            migrationBuilder.RenameIndex(
                name: "IX_DiemDanhs_NhanVienId",
                table: "DiemDanh",
                newName: "IX_DiemDanh_NhanVienId");

            migrationBuilder.RenameIndex(
                name: "IX_DichVuDatBongs_ThueSanId",
                table: "DichVuDatBong",
                newName: "IX_DichVuDatBong_ThueSanId");

            migrationBuilder.RenameIndex(
                name: "IX_DichVuDatBongs_NuocUongId",
                table: "DichVuDatBong",
                newName: "IX_DichVuDatBong_NuocUongId");

            migrationBuilder.RenameIndex(
                name: "IX_DichVuDatBongs_HoaDonChiTietId",
                table: "DichVuDatBong",
                newName: "IX_DichVuDatBong_HoaDonChiTietId");

            migrationBuilder.RenameIndex(
                name: "IX_DiaChiKhachHangs_KhachHangId",
                table: "DiaChiKhachHang",
                newName: "IX_DiaChiKhachHang_KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_CT_HinhThucThanhToans_HoaDonChiTietId",
                table: "CT_HinhThucThanhToan",
                newName: "IX_CT_HinhThucThanhToan_HoaDonChiTietId");

            migrationBuilder.RenameIndex(
                name: "IX_CT_HinhThucThanhToans_HinhThucThanhToanId",
                table: "CT_HinhThucThanhToan",
                newName: "IX_CT_HinhThucThanhToan_HinhThucThanhToanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Thue",
                table: "Thue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaiKhoan",
                table: "TaiKhoan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhieuGiamGiaChiTiet",
                table: "PhieuGiamGiaChiTiet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NuocUong",
                table: "NuocUong",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NhanVien",
                table: "NhanVien",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LichSuHoaDon",
                table: "LichSuHoaDon",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LichLamViec",
                table: "LichLamViec",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhachHang",
                table: "KhachHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoaDonChiTiet",
                table: "HoaDonChiTiet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HinhThucThanhToan",
                table: "HinhThucThanhToan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiemDanh",
                table: "DiemDanh",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DichVuDatBong",
                table: "DichVuDatBong",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaChiKhachHang",
                table: "DiaChiKhachHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CT_HinhThucThanhToan",
                table: "CT_HinhThucThanhToan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CT_HinhThucThanhToan_HinhThucThanhToan_HinhThucThanhToanId",
                table: "CT_HinhThucThanhToan",
                column: "HinhThucThanhToanId",
                principalTable: "HinhThucThanhToan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CT_HinhThucThanhToan_HoaDonChiTiet_HoaDonChiTietId",
                table: "CT_HinhThucThanhToan",
                column: "HoaDonChiTietId",
                principalTable: "HoaDonChiTiet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiaChiKhachHang_KhachHang_KhachHangId",
                table: "DiaChiKhachHang",
                column: "KhachHangId",
                principalTable: "KhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBong_HoaDonChiTiet_HoaDonChiTietId",
                table: "DichVuDatBong",
                column: "HoaDonChiTietId",
                principalTable: "HoaDonChiTiet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBong_NuocUong_NuocUongId",
                table: "DichVuDatBong",
                column: "NuocUongId",
                principalTable: "NuocUong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuDatBong_Thue_ThueSanId",
                table: "DichVuDatBong",
                column: "ThueSanId",
                principalTable: "Thue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDanh_NhanVien_NhanVienId",
                table: "DiemDanh",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_KhachHang_KhachHangId",
                table: "HoaDon",
                column: "KhachHangId",
                principalTable: "KhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_NhanVien_NhanVienId",
                table: "HoaDon",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiet_HoaDon_HoaDonId",
                table: "HoaDonChiTiet",
                column: "HoaDonId",
                principalTable: "HoaDon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiet_NhanVien_NhanVienId",
                table: "HoaDonChiTiet",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiet_PhieuGiamGiaChiTiet_PhieuGiamGiaId",
                table: "HoaDonChiTiet",
                column: "PhieuGiamGiaId",
                principalTable: "PhieuGiamGiaChiTiet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDonChiTiet_Sanbongs_SanBongId",
                table: "HoaDonChiTiet",
                column: "SanBongId",
                principalTable: "Sanbongs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LichLamViec_NhanVien_NhanVienId",
                table: "LichLamViec",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuHoaDon_HoaDon_HoaDonId",
                table: "LichSuHoaDon",
                column: "HoaDonId",
                principalTable: "HoaDon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuGiamGiaChiTiet_KhachHang_KhachHangId",
                table: "PhieuGiamGiaChiTiet",
                column: "KhachHangId",
                principalTable: "KhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuGiamGiaChiTiet_PhieuGiamGia_PhieuGiamGiaId",
                table: "PhieuGiamGiaChiTiet",
                column: "PhieuGiamGiaId",
                principalTable: "PhieuGiamGia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_KhachHang_KhachHangId",
                table: "TaiKhoan",
                column: "KhachHangId",
                principalTable: "KhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
