using Client.Models;
using ClientLibary.Services.Client.Constracts;
using Microsoft.AspNetCore.Mvc;
//using ServerLibary.Model.Params;

namespace Client.Controllers
{
    [Route("ThanhToan/[action]")]
    public class ThanhToanController : Controller
    {
        //private readonly ITh _servicesThanhToan;
        private readonly ITaiKhoanClientServices _servicesTaiKhoan;
        private readonly IGioHangClientServices _servicesGioHang;
        private readonly IThanhToanClientServices _servicesThanhToan;
        public ThanhToanController(IGioHangClientServices servicesGioHang, ITaiKhoanClientServices servicesTaiKhoan, IThanhToanClientServices servicesThanhToan)
        {
            //_servicesThanhToan = services;
            _servicesGioHang = servicesGioHang;
            _servicesTaiKhoan = servicesTaiKhoan;
            _servicesThanhToan = servicesThanhToan;
        }

        [HttpPost]
        public async Task<IActionResult> ThanhToanSanPham(ThanhToanSubmit param)
        {
            try
            {
                if (param == null)
                {
                    return RedirectToAction("XacNhanDonHang");
                }
                var resp = await _servicesThanhToan.ThanhToanGioHang(param);
                return RedirectToAction("Index", "TrangChu");
            }
            catch (Exception)
            {

                return RedirectToAction("XacNhanDonHang");
            }
        }
        // GET: ThanhToanController
        public async Task<IActionResult> XacNhanDonHang()
        {
            try
            {
                var listDiaChi = await _servicesTaiKhoan.DanhSachDiaChiGiaoHang();
                var lstGioHang = await _servicesGioHang.DanhSachSanPhamGioHang();
                ViewData["lstDiaChi"] = listDiaChi.Select(x => new DiaChiDatHangClientResp
                {
                    Id = x.Id,
                    DiaChiChiTiet = x.DiaChiChiTiet,
                    IsDefault = x.IsDefault,
                    Sdt = x.Sdt,
                    TenNguoiNhan = x.TenNguoiNhan,
                    Url = x.Url,
                    Province_code = x.ThanhPho_Code,
                    District_code = x.Huyen_Code,
                    Ward_code = x.Xa_Code,
                    Email = x.Email,
                    GhiChu = x.GhiChu,
                    IsChon = x.IsDefault ?? false
                }).ToList();
                ViewData["lstGioHang"] = lstGioHang;
                ViewData["dsThanhPho"] = await _servicesTaiKhoan.DanhSachThanhPho();
                //var listSpGioHang = await
                return View("XacNhanDonHang");
            }
            catch
            {
                return View("XacNhanDonHang");

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThayDoiThongTinDiaChi(ThanhToanParam param)
        {
            try
            {

                var resp = await _servicesTaiKhoan.CapNhatDiaChiGiaoHang(new()
                {
                    DiaChiChiTiet = param?.DiaChiParam?.DiaChiChiTiet,
                    Email = param?.DiaChiParam?.Email,
                    ThanhPho_Code = param?.DiaChiParam?.ThanhPho_Code,
                    Huyen_Code = param?.DiaChiParam?.Huyen_Code,
                    Xa_Code = param?.DiaChiParam?.Xa_Code,
                    Sdt = param?.DiaChiParam?.Sdt,
                    GhiChu = param?.DiaChiParam?.GhiChu,
                    TenNguoiNhan = param?.DiaChiParam?.TenNguoiNhan,
                    isDefault = param?.DiaChiParam?.isDefault ?? false,
                });
                return RedirectToAction("XacNhanDonHang");
            }
            catch
            {
                return RedirectToAction("XacNhanDonHang");
            }
        }
        // POST: ThanhToanController/ThemMoiDiaChi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoiDiaChi(ThanhToanParam param)
        {
            try
            {

                var resp = await _servicesTaiKhoan.ThemDiaChiGiaoHang(new()
                {
                    DiaChiChiTiet = param?.DiaChiParam?.DiaChiChiTiet,
                    Email = param?.DiaChiParam?.Email,
                    ThanhPho_Code = param?.DiaChiParam?.ThanhPho_Code,
                    Huyen_Code = param?.DiaChiParam?.Huyen_Code,
                    Xa_Code = param?.DiaChiParam?.Xa_Code,
                    GhiChu = param?.DiaChiParam?.GhiChu,
                    Sdt = param?.DiaChiParam?.Sdt,
                    TenNguoiNhan = param?.DiaChiParam?.TenNguoiNhan,
                    isDefault = param?.DiaChiParam?.isDefault ?? false,
                });
                return RedirectToAction("XacNhanDonHang");
            }
            catch
            {
                return RedirectToAction("XacNhanDonHang");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ThayDoiDiaChiChon(int? idDiaChi)
        {
            try
            {
                var listDiaChi = await _servicesTaiKhoan.DanhSachDiaChiGiaoHang();
                var lstGioHang = await _servicesGioHang.DanhSachSanPhamGioHang();
                ViewData["lstDiaChi"] = listDiaChi.ToList().Select(x => new DiaChiDatHangClientResp
                {
                    Id = x?.Id,
                    DiaChiChiTiet = x?.DiaChiChiTiet,
                    IsDefault = x?.IsDefault,
                    Sdt = x?.Sdt,
                    TenNguoiNhan = x?.TenNguoiNhan,
                    Province_code = x?.ThanhPho_Code,
                    District_code = x?.Huyen_Code,
                    Ward_code = x?.Xa_Code,
                    Email = x?.Email,
                    GhiChu = x?.GhiChu,
                    Url = x?.Url,
                    IsChon = x.Id == idDiaChi ? true : false
                }).ToList();
                ViewData["lstGioHang"] = lstGioHang;
                ViewData["dsThanhPho"] = await _servicesTaiKhoan.DanhSachThanhPho();
                return View("XacNhanDonHang");
            }
            catch
            {
                return RedirectToAction("XacNhanDonHang");
            }
        }

        [HttpGet]
        public async Task<IActionResult> XoaDiaChiGiaoHang(int? id)
        {
            try
            {
                var resp = await _servicesTaiKhoan.XoaDiaChiGiaoHang(id);
                return RedirectToAction("XacNhanDonHang");
            }
            catch
            {
                return RedirectToAction("XacNhanDonHang");
            }
        }

    }
}
