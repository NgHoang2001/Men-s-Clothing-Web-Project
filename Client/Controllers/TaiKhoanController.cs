using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            ViewData["type"] = "donhang";

            return View("TaiKhoan");
        }

        public IActionResult TaiKhoanCuaToi()
        {
            ViewData["type"] = "taikhoan";

            return View("TaiKhoan");
        }
        public IActionResult SoDiaChi()
        {
            ViewData["type"] = "sodiachi";

            return View("TaiKhoan");
        }
        public IActionResult DoiMatKhau()
        {
            ViewData["type"] = "doimatkhau";

            return View("TaiKhoan");
        }
    }
}
