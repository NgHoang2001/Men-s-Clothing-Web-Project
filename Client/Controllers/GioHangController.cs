using Client.Models;
using ClientLibary.Services.Client.Constracts;
using Microsoft.AspNetCore.Mvc;
using ServerLibary.Model.Params;

namespace Client.Controllers
{
    [Route("GioHang/[action]")]
    public class GioHangController : Controller
    {
        private readonly IGioHangClientServices _services;
        private readonly IUserAuthentication _servicesAuth;
        public GioHangController(IGioHangClientServices services, IUserAuthentication servicesAuth)
        {
            _services = services;
            _servicesAuth = servicesAuth;
        }
        //[Route("ChiTietGioHang")]
        [HttpGet]
        public async Task<IActionResult> ChiTietGioHang(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var danhSachSanPhamGioHang = await _services.DanhSachSanPhamGioHang();
            //var dsKichThuocvaMauSac = await _services.DanhSachKichThuocVaMauSacSanPhamDonHang(id);
            var dsMauSac = await _services.DanhSachMauSacGioHangClient(id);
            var dsKichThuoc = await _services.DanhSachKichThuocGioHangClient(id);
            ViewData["dsSpGioHang"] = danhSachSanPhamGioHang.OrderByDescending(x => x.IsChon).ThenByDescending(x => x.Id).ToList();
            ViewData["dsKichThuoc"] = dsKichThuoc;
            ViewData["dsMauSac"] = dsMauSac;
            return View("ChiTietGioHang");
        }

        //[HttpPost("capnhatsanphamgiohang")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> CapNhatSPGioHang([Bind("MauSacvsKichThuocModel.Id_GioHang,MauSacvsKichThuocModel.Id_MauSac,MauSacvsKichThuocModel.Id_KichThuoc")] CapNhatGioHangModel param)
        public async Task<IActionResult> CapNhatSPGioHang(CapNhatGioHangModel param)
        {
            if (ModelState.IsValid)
            {
                var model = new ServerLibary.Model.Params.CapNhatSoLuongSanPhamGioHang
                {
                    id = int.Parse(param?.MauSacvsKichThuocModel?.Id_GioHang),
                    idKichThuoc = int.Parse(param?.MauSacvsKichThuocModel?.Id_KichThuoc),
                    idMauSac = int.Parse(param?.MauSacvsKichThuocModel?.Id_MauSac)
                };
                var resp = await _services.CapNhatSanPhamGioHang(model);
                TempData["Message"] = string.IsNullOrEmpty(resp?.Message) ? null : resp.Message;
                TempData["Flag"] = resp.Flag ? true : false;

            }
            var userInfo = await _servicesAuth.GetUserInfoClient();
            return RedirectToAction("ChiTietGioHang", "GioHang", new { id = userInfo.Id_KhachHang });
        }

        //[HttpPost("capnhatsoluongsanpham")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> CapNhatSoLuongSPGioHang([Bind(new[] { "SoLuongGioHangModel.Id_GioHang", "SoLuongGioHangModel.isCong" })] CapNhatGioHangModel param)
        public async Task<IActionResult> CapNhatSoLuongSPGioHang(CapNhatGioHangModel param)
        {
            if (ModelState.IsValid)
            {
                var resp = await _services.CapNhatSoLuongSanPhamGioHang(new CapNhatSoLuongParam
                {
                    Id = int.Parse(param?.SoLuongGioHangModel?.Id_GioHang),
                    IsTang = param.SoLuongGioHangModel.isCong
                });
                TempData["Message"] = string.IsNullOrEmpty(resp?.Message) ? null : resp.Message;
                TempData["Flag"] = resp.Flag ? true : false;
            }
            var userInfo = await _servicesAuth.GetUserInfoClient();
            return RedirectToAction("ChiTietGioHang", "GioHang", new { id = userInfo.Id_KhachHang });
        }
        // GioHang/XoaSanPhamGioHang?id=
        [HttpGet]
        public async Task<IActionResult> XoaSanPhamGioHang(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resp = await _services.XoaSanPhamGioHang(id);
            TempData["Message"] = string.IsNullOrEmpty(resp?.Message) ? null : resp.Message;
            TempData["Flag"] = resp.Flag ? true : false;
            //ViewData["Message"] = resp;

            var userInfo = await _servicesAuth.GetUserInfoClient();
            return RedirectToAction("ChiTietGioHang", "GioHang", new { id = userInfo.Id_KhachHang });
        }

        [HttpGet]
        public async Task<IActionResult> ThayDoiChonSpGioHang(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resp = await _services.ThayDoiTrangThaiGioHang(id);
            TempData["Message"] = string.IsNullOrEmpty(resp?.Message) ? null : resp.Message;
            TempData["Flag"] = resp.Flag ? true : false;
            //ViewData["Message"] = resp;

            var userInfo = await _servicesAuth.GetUserInfoClient();
            return RedirectToAction("ChiTietGioHang", "GioHang", new { id = userInfo.Id_KhachHang });
        }
    }
}
