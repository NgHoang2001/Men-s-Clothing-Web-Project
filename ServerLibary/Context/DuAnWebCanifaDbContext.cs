using BaseLibary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibary.Context
{
    public class DuAnWebCanifaDbContext : DbContext
    {
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<DiaChi> DiaChis { get; set; }
        public DbSet<DoiTuong> DoiTuongs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHang_TrangThaiDonHang> DonHang_TrangThaiDonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public DbSet<HinhAnh> HinhAnhs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<KichThuoc> KichThuocs { get; set; }
        public DbSet<MauSac> MauSacs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<SanPhamChiTiet> SanPhamChiTiets { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<TrangThaiDonHang> TrangThaiDonHangs { get; set; }
        public DbSet<TrangThaiGioHang> TrangThaiGioHangs { get; set; }
        //public DbSet<TrangThaiSanPham> TrangThaiSanPhams { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<SuKien> SuKiens { get; set; }
        public DbSet<SanPham_SuKien> SanPham_SuKiens { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<administrative_regions> Administrative_Regions { get; set; }
        public DbSet<administrative_units> Administrative_Units { get; set; }
        public DbSet<provinces> Provinces { get; set; }
        public DbSet<districts> Districts { get; set; }
        public DbSet<wards> Wards { get; set; }
        public DuAnWebCanifaDbContext()
        {

        }

        public DuAnWebCanifaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=HOANGNN\\SQLEXPRESS;Initial Catalog=DuAnWebCanifa;Integrated Security=True;Encrypt=False;Trusted_Connection = true;");
        }
    }
}
