
using Client.Models;
using ClientLibary.Services.Client.Constracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class TestController : Controller
    {
        private ITaiKhoanClientServices _services;
        public TestController(ITaiKhoanClientServices services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["dsThanhPho"] = await _services.DanhSachThanhPho();
            return View();
        }
        public async Task<IActionResult> GetParatial1()
        {
            //var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            ViewData["Type"] = "paratial1";
            ViewData["dsThanhPho"] = await _services.DanhSachThanhPho();
            return View("Index");
        }
        public IActionResult GetParatial2()
        {
            //var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            ViewData["Type"] = "paratial2";
            return View("Index");
        }
        [HttpPost]
        public IActionResult TestModel(TestModel model)
        {
            //var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            //ViewData["Type"] = "paratial2";
            return View("Index");
        }
    }
}
