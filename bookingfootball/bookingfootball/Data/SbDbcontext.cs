using bookingfootball.Db_QL;
using Microsoft.EntityFrameworkCore;

namespace bookingfootball.Data
{
    public class SbDbcontext : DbContext
    {
        public SbDbcontext(DbContextOptions<SbDbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanCa>()
                .HasOne(sc => sc.SanBong)
                .WithMany(sb => sb.SanCas)
                .HasForeignKey(sc => sc.SanBongId);

            modelBuilder.Entity<SanCa>()
                .HasOne(sc => sc.Ca)
                .WithMany(c => c.SanCas)
                .HasForeignKey(sc => sc.CaId);

            modelBuilder.Entity<SanCa>()
                .HasOne(sc => sc.NgayTrongTuan)
                .WithMany(nt => nt.SanCas)
                .HasForeignKey(sc => sc.NgayTrongTuanId);

            modelBuilder.Entity<Sanbong>()
                .HasOne(sb => sb.LoaiSan)
                .WithMany(ls => ls.SanBongs)
                .HasForeignKey(sb => sb.LoaiSanId);

            modelBuilder.Entity<LichLamViec>()
                .HasOne(lv => lv.NhanVien)
                .WithMany(nv => nv.LichLamViecs)
                .HasForeignKey(lv => lv.NhanVienId);

            modelBuilder.Entity<DiemDanh>()
                .HasOne(dd => dd.NhanVien)
                .WithMany(nv => nv.DiemDanhs)
                .HasForeignKey(dd => dd.NhanVienId);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(tk => tk.KhachHang)
                .WithOne(kh => kh.TaiKhoan)
                .HasForeignKey<TaiKhoan>(tk => tk.KhachHangId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HoaDon>()
                .HasMany(h => h.HoaDonChiTiets)
                .WithOne(ct => ct.HoaDon)
                .HasForeignKey(ct => ct.HoaDonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HoaDon>()
                .HasMany(h => h.LichSuHoaDons)
                .WithOne(ls => ls.HoaDon)
                .HasForeignKey(ls => ls.HoaDonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.NhanVien)
                .WithMany(nv => nv.HoaDons)
                .HasForeignKey(h => h.NhanVienId);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.KhachHang)
                .WithMany(kh => kh.HoaDons)
                .HasForeignKey(h => h.KhachHangId);

            modelBuilder.Entity<DiaChiKhachHang>()
               .HasOne(d => d.KhachHang)
               .WithMany(k => k.DiaChiKhachHangs)
               .HasForeignKey(d => d.KhachHangId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhieuGiamGiaChiTiet>()
                .HasOne(p => p.KhachHang)
                .WithMany(k => k.PhieuGiamGiaChiTiets)
                .HasForeignKey(p => p.KhachHangId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhieuGiamGiaChiTiet>()
                .HasOne(p => p.PhieuGiamGia)
                .WithMany(g => g.PhieuGiamGiaChiTiets)
                .HasForeignKey(p => p.PhieuGiamGiaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Thue>()
               .Property(t => t.TenThue)
               .IsRequired();

            modelBuilder.Entity<NuocUong>()
                .Property(n => n.TenNuocUong)
                .IsRequired();

            modelBuilder.Entity<DichVuDatBong>()
                .HasOne(d => d.NuocUong)
                .WithMany()
                .HasForeignKey(d => d.NuocUongId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DichVuDatBong>()
                .HasOne(d => d.Thues)
                .WithMany()
                .HasForeignKey(d => d.ThueSanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HinhThucThanhToan>()
                .Property(h => h.PhuongThucThanhToan)
                .IsRequired();

            modelBuilder.Entity<CT_HinhThucThanhToan>()
                .HasOne(c => c.HoaDonChiTiet)
                .WithMany()
                .HasForeignKey(c => c.HoaDonChiTietId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CT_HinhThucThanhToan>()
                .HasOne(c => c.HinhThucThanhToan)
                .WithMany()
                .HasForeignKey(c => c.HinhThucThanhToanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DichVuDatBong>()
                .HasOne(d => d.HoaDonChiTiet)
                .WithMany(h => h.DichVuDatBongs)
                .HasForeignKey(d => d.HoaDonChiTietId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HoaDonChiTiet>()
                .HasOne(h => h.NhanVien)
                .WithMany(s => s.HoaDonChiTiets)
                .HasForeignKey(h => h.NhanVienId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Sanbong> Sanbongs { get; set; }
        public DbSet<LoaiSan> LoaiSans { get; set; }
        public DbSet<Ca> Cas { get; set; }
        public DbSet<SanCa> SanCas { get; set; }
        public DbSet<NgayTrongTuan> NgayTrongTuans { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<LichLamViec> LichLamViecs { get; set; }
        public DbSet<DiemDanh> DiemDanhs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public DbSet<LichSuHoaDon> LichSuHoaDons { get; set; }
        public DbSet<DiaChiKhachHang> DiaChiKhachHangs { get; set; }
        public DbSet<NuocUong> NuocUongs { get; set; }
    }
}
