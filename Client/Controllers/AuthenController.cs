using BaseLibary.DTOs;
using ClientLibary.Services.Client.Constracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IUserAuthentication services;
        //private readonly AuthenticationStateProvider AuthenStateProvider;
        //private readonly CustomAuthentication AuthenStateProvider;
        //public AuthenController(IUserAuthentication services, CustomAuthentication authenticationStateProvider)
        //{
        //    AuthenStateProvider = authenticationStateProvider;
        //    this.services = services;
        //}

        public AuthenController(IUserAuthentication services)
        {
            this.services = services;
        }
        // GET: AuthenController/DangNhap
        [HttpGet("dangnhap")]
        public ActionResult DangNhap()
        {

            return View("dangnhap");
        }

        // POST: AuthenController/DangNhap
        [HttpPost("dangnhap")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangNhap([Bind("Email", "Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                var modelLogin = new Login
                {
                    Email = login.Email,
                    Password = login.Password,
                };
                var resp = await services.Login(login);
                if (!resp.Flag)
                {
                    return View("DangNhap", login);
                }

            }
            return RedirectToAction("Index", "TrangChu");
        }

        // GET: AuthenController/DangKy
        [HttpGet("dangky")]
        public ActionResult DangKy()
        {
            return View("dangky");
        }

        // POST: AuthenController/DangNhap
        [HttpPost("dangky")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy([Bind("Email", "Password", "PasswordTry")] Register register)
        {
            if (ModelState.IsValid)
            {
                if (!register.Password.Equals(register.PasswordTry))
                {
                    return View("dangky", register);
                }
                var resp = await services.Register(register);
                if (resp.Flag == true)
                {
                    return RedirectToAction(nameof(DangNhap));
                }
                return View("dangky", register);


            }
            return View(register);
        }
        //[Authorize]
        [HttpGet("getuserinfo")]
        public async Task<IActionResult> GetUserCurrentInfo()
        {
            var resp = await services.GetUserInfoClient();
            if (resp == null || resp.Id_TaiKhoan == null)
            {
                return NotFound();
            }
            ViewData["currentUserInfo"] = resp;
            return View("UserInfo");

        }
        [HttpGet("refrest-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var resp = await services.RefreshToken();
            if (resp.Flag)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            await Logout();
            return RedirectToAction("Index", "TrangChu");
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var resp = await services.Logout();
            return RedirectToAction("Index", "TrangChu");
        }

    }
}
