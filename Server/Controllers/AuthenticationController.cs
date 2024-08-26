using BaseLibary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibary.Respositories.Contracts;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        IUserAuthentication services;
        public AuthenticationController(IUserAuthentication services)
        {
            this.services = services;
        }
        // Tạo một tài khoản
        // Tham số: Register
        // 
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> TaoTaiKhoanAsync(Register user)
        {
            if (user == null) { return BadRequest("Model trong"); }
            var result = await services.Register(user);
            return Ok(result);
        }

        // sử dụng httponly cookies để lưu trữ token
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> DangNhapAsync(Login user)
        {
            if (user == null) { return BadRequest("Model trong"); }
            var result = await services.Login(user);
            if (!result.Flag)
            {
                return NotFound("Đăng nhập thất bại");
            }
            var tokenModel = new UserSession
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            };
            services.SetTokensInsideCookie(tokenModel, HttpContext);
            return Ok("Đăng nhập thành công");
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> DangNhapAsync(Login user)
        //{
        //    if (user == null) { return BadRequest("Model trong"); }
        //    var result = await services.Login(user);

        //    return Ok(result);
        //}

        // sử dụng refresh token với httponly cookies
        // refresh token sữ được tự động gửi thông qua cookie -> client ko cần truyền tham số
        [AllowAnonymous]
        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            //if (token == null) { return BadRequest("Model trong"); }
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            var result = await services.RefreshToken(new RefreshToken
            {
                Token = refreshToken
            });
            if (!result.Flag)
            {
                return NotFound("Thất bại tạo mới token");
            }
            var tokenModel = new UserSession
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            };
            services.SetTokensInsideCookie(tokenModel, HttpContext);
            return Ok("Thành công tạo mới token");
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUserInfor()
        {
            var resp = await services.GetUserInfoServices(HttpContext);
            if (resp.Id_TaiKhoan == null)
            {
                return NotFound();
            }
            return Ok(resp);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            services.SetOffTokensInsideCookie(HttpContext);
            return Ok("Đăng xuất thành công");
        }
        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        //{
        //    if (token == null) { return BadRequest("Model trong"); }
        //    var result = await services.RefreshToken(token)
        //    return Ok(result);
        //}

    }
}
