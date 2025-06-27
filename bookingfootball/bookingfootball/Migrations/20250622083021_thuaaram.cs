using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingfootball.Migrations
{
    /// <inheritdoc />
    public partial class thuaaram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HinhThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiSan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NgayTrongTuans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThuTu = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgayTrongTuans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NuocUongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNuocUong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuocUongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhieuGiamGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenPhieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    SoLuongDaSuDung = table.Column<int>(type: "int", nullable: false),
                    GiaTriGiamToiDa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DieuKien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTriGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGiamGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Thue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaChiKhachHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    TenDiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChiTietDiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhPho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChiKhachHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaChiKhachHangs_KhachHangs_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sanbongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiSanId = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanbongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sanbongs_LoaiSans_LoaiSanId",
                        column: x => x.LoaiSanId,
                        principalTable: "LoaiSans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiemDanhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDanhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiemDanhs_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: true),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienCoc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TongTienThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDons_KhachHangs_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDons_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LichLamViecs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThoiGianKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichLamViecs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichLamViecs_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienId = table.Column<int>(type: "int", nullable: true),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhatCuoi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_KhachHangs_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhieuGiamGiaChiTiet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuGiamGiaId = table.Column<int>(type: "int", nullable: false),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    SoTienGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGiamGiaChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuGiamGiaChiTiet_KhachHangs_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuGiamGiaChiTiet_PhieuGiamGia_PhieuGiamGiaId",
                        column: x => x.PhieuGiamGiaId,
                        principalTable: "PhieuGiamGia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanCas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanBongId = table.Column<int>(type: "int", nullable: false),
                    CaId = table.Column<int>(type: "int", nullable: false),
                    NgayTrongTuanId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanCas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanCas_Cas_CaId",
                        column: x => x.CaId,
                        principalTable: "Cas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanCas_NgayTrongTuans_NgayTrongTuanId",
                        column: x => x.NgayTrongTuanId,
                        principalTable: "NgayTrongTuans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanCas_Sanbongs_SanBongId",
                        column: x => x.SanBongId,
                        principalTable: "Sanbongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuHoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    NgayThucHien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgươiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuHoaDons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSuHoaDons_HoaDons_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    SanBongId = table.Column<int>(type: "int", nullable: true),
                    PhieuGiamGiaId = table.Column<int>(type: "int", nullable: true),
                    NhanVienId = table.Column<int>(type: "int", nullable: true),
                    MaChiTietHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongTienDuocGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienThueSan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NgayDenSan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiets_HoaDons_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiets_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiets_PhieuGiamGiaChiTiet_PhieuGiamGiaId",
                        column: x => x.PhieuGiamGiaId,
                        principalTable: "PhieuGiamGiaChiTiet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiets_Sanbongs_SanBongId",
                        column: x => x.SanBongId,
                        principalTable: "Sanbongs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CT_HinhThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonChiTietId = table.Column<int>(type: "int", nullable: false),
                    HinhThucThanhToanId = table.Column<int>(type: "int", nullable: false),
                    TenHinhThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_HinhThucThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CT_HinhThucThanhToan_HinhThucThanhToan_HinhThucThanhToanId",
                        column: x => x.HinhThucThanhToanId,
                        principalTable: "HinhThucThanhToan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_HinhThucThanhToan_HoaDonChiTiets_HoaDonChiTietId",
                        column: x => x.HoaDonChiTietId,
                        principalTable: "HoaDonChiTiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoThues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDoThue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    HoaDonChiTietId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoThues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoThues_HoaDonChiTiets_HoaDonChiTietId",
                        column: x => x.HoaDonChiTietId,
                        principalTable: "HoaDonChiTiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DichVuDatBong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NuocUongId = table.Column<int>(type: "int", nullable: true),
                    DoThueId = table.Column<int>(type: "int", nullable: true),
                    ThueSanId = table.Column<int>(type: "int", nullable: true),
                    SoLuongDoThue = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoaDonChiTietId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVuDatBong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DichVuDatBong_DoThues_DoThueId",
                        column: x => x.DoThueId,
                        principalTable: "DoThues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DichVuDatBong_HoaDonChiTiets_HoaDonChiTietId",
                        column: x => x.HoaDonChiTietId,
                        principalTable: "HoaDonChiTiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DichVuDatBong_NuocUongs_NuocUongId",
                        column: x => x.NuocUongId,
                        principalTable: "NuocUongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DichVuDatBong_Thue_ThueSanId",
                        column: x => x.ThueSanId,
                        principalTable: "Thue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CT_HinhThucThanhToan_HinhThucThanhToanId",
                table: "CT_HinhThucThanhToan",
                column: "HinhThucThanhToanId");

            migrationBuilder.CreateIndex(
                name: "IX_CT_HinhThucThanhToan_HoaDonChiTietId",
                table: "CT_HinhThucThanhToan",
                column: "HoaDonChiTietId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaChiKhachHangs_KhachHangId",
                table: "DiaChiKhachHangs",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuDatBong_DoThueId",
                table: "DichVuDatBong",
                column: "DoThueId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuDatBong_HoaDonChiTietId",
                table: "DichVuDatBong",
                column: "HoaDonChiTietId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuDatBong_NuocUongId",
                table: "DichVuDatBong",
                column: "NuocUongId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuDatBong_ThueSanId",
                table: "DichVuDatBong",
                column: "ThueSanId");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanhs_NhanVienId",
                table: "DiemDanhs",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_DoThues_HoaDonChiTietId",
                table: "DoThues",
                column: "HoaDonChiTietId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_HoaDonId",
                table: "HoaDonChiTiets",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_NhanVienId",
                table: "HoaDonChiTiets",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_PhieuGiamGiaId",
                table: "HoaDonChiTiets",
                column: "PhieuGiamGiaId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_SanBongId",
                table: "HoaDonChiTiets",
                column: "SanBongId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_KhachHangId",
                table: "HoaDons",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_NhanVienId",
                table: "HoaDons",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_LichLamViecs_NhanVienId",
                table: "LichLamViecs",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuHoaDons_HoaDonId",
                table: "LichSuHoaDons",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGiamGiaChiTiet_KhachHangId",
                table: "PhieuGiamGiaChiTiet",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGiamGiaChiTiet_PhieuGiamGiaId",
                table: "PhieuGiamGiaChiTiet",
                column: "PhieuGiamGiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanbongs_LoaiSanId",
                table: "Sanbongs",
                column: "LoaiSanId");

            migrationBuilder.CreateIndex(
                name: "IX_SanCas_CaId",
                table: "SanCas",
                column: "CaId");

            migrationBuilder.CreateIndex(
                name: "IX_SanCas_NgayTrongTuanId",
                table: "SanCas",
                column: "NgayTrongTuanId");

            migrationBuilder.CreateIndex(
                name: "IX_SanCas_SanBongId",
                table: "SanCas",
                column: "SanBongId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_KhachHangId",
                table: "TaiKhoans",
                column: "KhachHangId",
                unique: true);

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
            migrationBuilder.DropTable(
                name: "CT_HinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "DiaChiKhachHangs");

            migrationBuilder.DropTable(
                name: "DichVuDatBong");

            migrationBuilder.DropTable(
                name: "DiemDanhs");

            migrationBuilder.DropTable(
                name: "LichLamViecs");

            migrationBuilder.DropTable(
                name: "LichSuHoaDons");

            migrationBuilder.DropTable(
                name: "SanCas");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "HinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "DoThues");

            migrationBuilder.DropTable(
                name: "NuocUongs");

            migrationBuilder.DropTable(
                name: "Thue");

            migrationBuilder.DropTable(
                name: "Cas");

            migrationBuilder.DropTable(
                name: "NgayTrongTuans");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiets");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "PhieuGiamGiaChiTiet");

            migrationBuilder.DropTable(
                name: "Sanbongs");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "PhieuGiamGia");

            migrationBuilder.DropTable(
                name: "LoaiSans");
        }
    }
}
