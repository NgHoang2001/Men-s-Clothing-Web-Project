using Client.Models;
using ClientLibary.Services.Client.Constracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    //[Route("TrangChu/[action]")]
    public class TrangChuController : Controller
    {
        private IUserAuthentication _servicesAuthen;
        private ISanPhamApiServices _servicesSanPham;
        private IGioHangClientServices _servicesGioHang;
        public TrangChuController(IUserAuthentication services, ISanPhamApiServices servicesSanPham, IGioHangClientServices servicesGioHang)
        {
            this._servicesAuthen = services;
            this._servicesSanPham = servicesSanPham;
            _servicesGioHang = servicesGioHang;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["listHinhAnhBanner"] = await _servicesSanPham.DanhSachBanner();
            ViewData["listDanhMuc"] = await _servicesSanPham.DanhSachDanhMuc(null);
            ViewData["listSanPham"] = await _servicesSanPham.DanhSachSanPham(null);
            return View("TrangChu");
        }
        [HttpGet("ChiTietSanPham/{id:int}")]
        public async Task<IActionResult> ChiTietSanPham(int id)
        {
            var lstSp = await _servicesSanPham.SanPhamChiTiet(id);
            ViewData["sanPhamChiTiet"] = lstSp;
            ViewData["danhMucCha"] = await _servicesSanPham.DanhSachDanhMuc(new() { id_danhmuc = lstSp.DanhMuc?.Id_DanhMuc_Cha });
            ViewData["user"] = await _servicesAuthen.GetUserInfoClient();

            return View("ChiTietSanPham");
        }

        [HttpPost("ThemSpVaoGioHang")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemSpVaoGioHang(ThemSpVaoGioHangParam param)
        {
            //var resp = await _servicesSanPham.
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user?.Id_TaiKhoan == null)
            {
                return RedirectToAction("dangnhap", "Authen");
            }
            var sp = await _servicesSanPham.SanPhamChiTiet(param.Id_SP);
            if (sp == null)
            {
                return RedirectToAction("ChiTietSanPham", "TrangChu", new { id = param.Id_SP });
            }
            var spct = sp.SanPhamChiTiet?.FirstOrDefault(x => x.MauSac?.Id == param.Id_MS && x.KichThuoc?.Id == param.Id_KT);
            var resp = await _servicesGioHang.ThemSanPhamVaoGioHang(new()
            {
                Id_KhachHang = user.Id_KhachHang,
                Id_SanPhamChiTiet = spct?.Id,
                SoLuong = 1
            });
            if (!resp.Flag)
            {
                return RedirectToAction("ChiTietSanPham", "TrangChu", new { id = param.Id_SP });
            }
            return RedirectToAction("ChiTietSanPham", "TrangChu", new { id = param.Id_SP });
        }

        [HttpPost("MuaNgaySp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MuaNgaySp(ThemSpVaoGioHangParam param)
        {
            //var resp = await _servicesSanPham.
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user?.Id_TaiKhoan == null)
            {
                return RedirectToAction("dangnhap", "Authen");
            }
            var sp = await _servicesSanPham.SanPhamChiTiet(param.Id_SP);
            if (sp == null)
            {
                return RedirectToAction("ChiTietSanPham", "TrangChu", new { id = param.Id_SP });
            }
            var spct = sp.SanPhamChiTiet?.FirstOrDefault(x => x.MauSac?.Id == param.Id_MS && x.KichThuoc?.Id == param.Id_KT);
            var resp = await _servicesGioHang.ThemSanPhamVaoGioHang(new()
            {
                Id_KhachHang = user.Id_KhachHang,
                Id_SanPhamChiTiet = spct?.Id,
                SoLuong = 1
            });
            if (!resp.Flag)
            {
                return RedirectToAction("ChiTietSanPham", "TrangChu", new { id = param.Id_SP });
            }
            return RedirectToAction("ChiTietGioHang", "GioHang", new { id = user.Id_TaiKhoan });
        }
    }
}
