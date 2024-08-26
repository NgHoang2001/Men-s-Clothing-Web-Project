using BaseLibary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibary.Model.Params;
using ServerLibary.Respositories.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ThanhToanController : ControllerBase
    {
        IThanhToanServices services;
        public ThanhToanController(IThanhToanServices services)
        {
            this.services = services;
        }

        [HttpPost("thanh-toan-gio-hang")]
        [SwaggerResponse(statusCode: 200, type: typeof(GeneralRespone), description: "thành công")]
        public async Task<IActionResult> ThanhToanGioHang([FromBody] ThanhToanParam? req)
        {
            if (req == null) { return BadRequest("Model trong"); }
            var result = await services.ThanhToanGioHang(req);
            return new OkObjectResult(result);
        }

    }
}
