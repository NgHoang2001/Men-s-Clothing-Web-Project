using BaseLibary.Entities;
using BaseLibary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibary.Context;
using ServerLibary.Model.Params;
using ServerLibary.Respositories.Contracts;

namespace ServerLibary.Respositories.Implementations
{
    public class ThanhToanServices : IThanhToanServices
    {
        private DuAnWebCanifaDbContext _context;

        public ThanhToanServices(DuAnWebCanifaDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralRespone> ThanhToanGioHang(ThanhToanParam param)
        {
            if (param == null || param.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Thanh toán thất bại");
            }
            var kh = await _context.KhachHangs.Where(x => x.Id == param.Id_KhachHang).FirstOrDefaultAsync();
            if (kh == null)
            {
                return new GeneralRespone(false, "Thanh toán thất bại");
            }
            var diaChi = await _context.DiaChis.Where(x => x.Id_KhachHang == kh.Id && x.Id == param.Id_DiaChi).FirstOrDefaultAsync();
            if (diaChi == null)
            {
                return new GeneralRespone(false, "Thanh toán thất bại");
            }
            // tạo mới đơn hàng khi có: id_kh và id_DiaChi
            var donHang = _context.DonHangs.Add(new BaseLibary.Entities.DonHang
            {
                Id_KhachHang = kh.Id,
                Id_DiaChi = diaChi.Id,
                ThoiGianTao = DateTime.Now,
            }).Entity;
            _context.SaveChanges();
            if (donHang == null)
            {
                return new GeneralRespone(false, "Thanh toán thất bại");
            }
            // ds sản phẩm giỏ hàng được chọn
            var lstSpGioHang = await _context.GioHangChiTiets.Where(x => x.Id_KhachHang == kh.Id && x.IsChon).ToListAsync();
            // ds id sp chi tiết trong lstSpGioHang
            var lstIdSpChiTiet = lstSpGioHang.Select(x => x.Id_SanPhamChiTiet).ToList();
            // ds sp chi tiết bao gồm sản phẩm, được lọc theo lstIdSpChiTiet
            var lstSpChiTiet = await _context.SanPhamChiTiets.Include(x => x.SanPham).Where(x => lstIdSpChiTiet.Contains(x.Id)).ToListAsync();
            // Tạo đơn hàng chi tiết từ danh sách sp giỏ hàng được chọn
            var lstSpDonHang = lstSpGioHang.Select(x => new DonHangChiTiet
            {
                Id_DonHang = donHang.Id,
                Id_SanPhamChiTiet = x?.Id_SanPhamChiTiet ?? 0,
                SoLuong = x?.SoLuong ?? 0,
                DonGia = lstSpChiTiet?.FirstOrDefault(a => a.Id == x?.Id_SanPhamChiTiet)?.SanPham?.DonGia ?? 0,
                ThoiGianTao = DateTime.Now
            }).ToArray();
            if (lstSpDonHang == null || lstSpDonHang.Length == 0)
            {
                return new GeneralRespone(false, "Thanh toán thất bại");
            }
            await _context.DonHangChiTiets.AddRangeAsync((DonHangChiTiet[])lstSpDonHang);
            //
            _context.SaveChanges();
            // Tạo mới trạng thái đơn hàng
            var trangThaiDH = await _context.TrangThaiDonHangs.FirstOrDefaultAsync(x => x.Ten.Trim().ToLower().Equals("Chờ xử lý".ToLower()));
            if (trangThaiDH == null)
            {
                trangThaiDH = _context.TrangThaiDonHangs.Add(new TrangThaiDonHang
                {
                    Ten = "Chờ xử lý"
                }).Entity;
                _context.SaveChanges();
            }
            _context.DonHang_TrangThaiDonHangs.Add(new DonHang_TrangThaiDonHang
            {
                Id_DonHang = donHang.Id,
                Id_TrangThaiDonHang = trangThaiDH.Id,
                ThoiGianTao = DateTime.Now,

            });
            _context.SaveChanges();
            // Ds đơn giá và id sp chi tiết
            var lstSpCTvsDonGia = lstSpChiTiet.Select(x => new
            {
                Id_SpChiTiet = x.Id,
                DonGia = x?.SanPham?.DonGia ?? 0
            }).ToList();
            //Tạo mới bảng thông tin thanh toán
            var tongTien = lstSpGioHang.Select(x => new
            {
                TongTienSp = (decimal)(x.SoLuong * (lstSpCTvsDonGia?.FirstOrDefault(a => a.Id_SpChiTiet == x.Id_SanPhamChiTiet)?.DonGia ?? 0))
            }).ToList().Sum(x => x.TongTienSp);
            // Thêm data thanh toán của đơn hàng
            _context.ThanhToans.Add(new ThanhToan
            {
                Id_DonHang = donHang.Id,
                Kieu = "Thanh toán khi nhận hàng",
                ThoiGianTao = DateTime.Now,
                TongTienChuaThanhToan = tongTien,
                TongTienDaThanhToan = 0,
                TrangThai = false,
            });
            _context.SaveChanges();

            //Sau tạo đơn hàng thành công -> xóa toàn bộ các sản phẩm đã được mua trong giỏ hàng
            _context.GioHangChiTiets.RemoveRange(lstSpGioHang);
            _context.SaveChanges();
            return new GeneralRespone(true, "Thanh toán thanh toán thành công");
        }
    }
}
