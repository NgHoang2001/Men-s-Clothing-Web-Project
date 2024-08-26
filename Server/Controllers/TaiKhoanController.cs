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
    [Authorize()]
    public class TaiKhoanController : ControllerBase
    {
        ITaiKhoanServices services;
        public TaiKhoanController(ITaiKhoanServices services)
        {
            this.services = services;
        }

        [HttpGet("chi-tiet-thong-tin-khach-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(ThongTinKhachHangResp), description: "thành công")]
        public async Task<IActionResult> GetChiTietThongTinKhachHang(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.ChiTietThongTinKhachHang(id);
            return new OkObjectResult(result);
        }

        [HttpPost("cap-nhat-thong-tin-tai-khoan")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<KichThuocResp>), description: "thành công")]
        public async Task<IActionResult> PostCapNhatThongTinTaiKhoan([FromBody] ThongTinKhachHangParam? req)
        {
            if (req == null) { return BadRequest("Model trong"); }
            var result = await services.CapNhatThongTinTaiKhoan(req);
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-hon-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<DonHangResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachDonHang(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachDonHang(id);
            return new OkObjectResult(result);
        }

        [HttpGet("chi-tiet-don-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(DonHangChiTietResp), description: "thành công")]
        public async Task<IActionResult> GetDonHangChiTiet(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.DonHangChiTiet(id);
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-dia-chi-giao-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<DiaChiDatHangResp>), description: "thành công")]
        public async Task<IActionResult> GetDanhSachDiaChiGiaoHang(int? id)
        {
            if (id == null) { return BadRequest("Model trong"); }
            var result = await services.DanhSachDiaChiGiaoHang(id);
            return new OkObjectResult(result);
        }

        [HttpPost("cap-nhat-dia-chi-giao-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> CapNhatDiaChiGiaoHang([FromBody] DiaChiDatHangParam? req)
        {
            if (req == null) { return BadRequest("Model trong"); }
            var result = await services.CapNhatDiaChiGiaoHang(req);
            return new OkObjectResult(result);
        }
        [HttpPost("them-moi-dia-chi-giao-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> ThemMoiDiaChiGiaoHang([FromBody] DiaChiDatHangParam? req, int? id)
        {
            if (req == null) { return BadRequest("Model trong"); }
            //var user = HttpContext.User.Identity;
            var result = await services.ThemMoiDiaChiGiaoHang(req, id);
            return new OkObjectResult(result);
        }
        [HttpDelete("xoa-dia-chi-giao-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> XoaDiaChiGiaoHang(int? idDiaChi, int? id)
        {
            if (idDiaChi == null || id == null) { return BadRequest("Model trong"); }
            var result = await services.XoaDiaChiGiaoHang(idDiaChi, id);
            return new OkObjectResult(result);
        }

        [HttpPost("cap-nhat-mat-khau")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> CapNhatMatKhau([FromBody] CapNhatMatKhauParam? req)
        {
            if (req == null) { return BadRequest("Model trong"); }
            var result = await services.CapNhatMatKhau(req);
            return new OkObjectResult(result);
        }

        [HttpGet("danh-sach-thanh-pho")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<DiaChiDatHangResp>), description: "thành công")]
        public async Task<IActionResult> DanhSachThanhPho()
        {
            var result = await services.DanhSachThanhPho();
            return new OkObjectResult(result);
        }
        [HttpGet("danh-sach-huyen")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<DiaChiDatHangResp>), description: "thành công")]
        public async Task<IActionResult> DanhSachHuyen(string? code)
        {
            var result = await services.DanhSachHuyenTheoId(code);
            return new OkObjectResult(result);
        }
        [HttpGet("danh-sach-xa")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<DiaChiDatHangResp>), description: "thành công")]
        public async Task<IActionResult> DanhSachXa(string? code)
        {
            var result = await services.DanhSachXaTheoId(code);
            return new OkObjectResult(result);
        }
    }
}
