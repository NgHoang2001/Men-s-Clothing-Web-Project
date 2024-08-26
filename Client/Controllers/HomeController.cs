using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult TaiKhoan()
        {
            return View();
        }
        public IActionResult ChiTietGioHang()
        {
            return View();
        }

        public IActionResult ChiTietSanPham()
        {
            return View();
        }

        public IActionResult TimKiem()
        {
            return View();
        }

        public IActionResult XacNhanDonHang()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
