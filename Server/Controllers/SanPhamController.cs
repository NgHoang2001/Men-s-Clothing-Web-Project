using Microsoft.AspNetCore.Mvc;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using ServerLibary.Respositories.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        ISanPhamServices services;
        public SanPhamController(ISanPhamServices services)
        {
            this.services = services;
        }

        [HttpPost("danh-sach-danh-muc")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<DanhMucResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachDanhMuc(DanhMucParam? param)
        {
            //if (id_danhmuc == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachDanhMuc(param);
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-banner")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<HinhAnhResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachBanner()
        {
            //if (id_danhmuc == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachHinhAnhBanner();
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-kich-thuoc")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<KichThuocResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachKichThuoc(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachKichThuoc(id);
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-mau-sac")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<MauSacResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachMauSac(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachMauSac(id);
            return new OkObjectResult(result);
        }

        [HttpPost("danh-sach-san-pham")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<SanPhamResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachSanPham(SanPhamParam? req)
        {
            //if (req == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachSanPham(req);
            return new OkObjectResult(result);
        }

        [HttpGet("san-pham-chi-tiet")]
        [SwaggerResponse(statusCode: 200, type: typeof(SanPhamChiTietResp), description: "thành công")]
        public async Task<IActionResult> GetSanPhamChiTiet(int? id_Sp)
        {
            if (id_Sp == null) { return BadRequest("Model trong"); }
            var result = await services.SanPhamChiTiet(id_Sp);
            return new OkObjectResult(result);
        }
    }
}
