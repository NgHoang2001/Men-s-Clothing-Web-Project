using BaseLibary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using ServerLibary.Respositories.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GioHangController : ControllerBase
    {
        IGioHangServices services;
        public GioHangController(IGioHangServices services)
        {
            this.services = services;
        }

        [HttpGet("danh-sach-san-pham-gio-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<SanPhamGioHangResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachSanPhamGioHang(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachSanPhamGioHang(id);
            return new OkObjectResult(result);
        }

        [HttpPost("them-san-pham-vao-gio-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> PostThemSanPhamVaoGioHang([FromBody] ThemSanPhamGioHangParam? model)
        {
            if (model == null) { return BadRequest("Model trong"); }
            var result = await services.ThemSanPhamVaoGioHang(model);
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-kich-thuoc-va-mau-sac-san-pham-don-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<KichThuocVaMauSacSanPhamDonHangResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachKichThuocVaMauSacSanPhamDonHang(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachKichThuocVaMauSacSanPhamDonHang(id);
            return new OkObjectResult(result);
        }

        [HttpPost("cap-nhat-san-pham-gio-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> PostCapNhatSanPhamGioHang(CapNhatSoLuongSanPhamGioHang param)
        {
            if (param == null) { return BadRequest("Model trong"); }
            var result = await services.CapNhatSanPhamGioHang((int)param.id, param.idKichThuoc, param.idMauSac);
            return new OkObjectResult(result);
        }

        [HttpPost("cap-nhat-so-luong-san-pham-gio-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> PostCapNhatSoLuongSanPhamGioHang(CapNhatSoLuongParam param)
        {
            if (param == null || param?.Id == null) { return BadRequest("Model trong"); }
            var result = await services.CapNhatSoLuongSanPhamGioHang(param);
            return new OkObjectResult(result);
        }

        [HttpDelete("xoa-san-pham-gio-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> DeleteXoaSanPhamGioHang(int? id)
        {
            //if (id == null) { return BadRequest("Model trong"); }
            var result = await services.XoaSanPhamGioHang(id);
            return new OkObjectResult(result);
        }

        [HttpGet("thay-doi-trang-thai-san-pham-gio-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> ThayDoiTrangThaiSp(int? id)
        {
            //if (id == null) { return BadRequest("Model trong"); }
            var result = await services.ThayDoiTrangThaiGioHang(id);
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-kich-thuoc")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<KichThuocGiohangResp>), description: "thành công")]
        public async Task<IActionResult> DanhSachKichThuocGioHang(int? id)
        {
            var resp = new List<KichThuocGiohangResp>();
            if (id == null)
            {
                return BadRequest("Model thiếu");
            }
            resp = await services.DanhSachKichThuocSanPhamGioHang((int)id);
            return new OkObjectResult(resp);
        }

        [HttpGet("danh-sach-mau-sac")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<MauSacGioHangResp>), description: "thành công")]
        public async Task<IActionResult> DanhSachMauSacGioHang(int? id)
        {
            var resp = new List<MauSacGioHangResp>();
            if (id == null)
            {
                return BadRequest("Model thiếu");
            }
            resp = await services.DanhSachMauSacSanPhamGioHang((int)id);
            return new OkObjectResult(resp);
        }
    }
}
