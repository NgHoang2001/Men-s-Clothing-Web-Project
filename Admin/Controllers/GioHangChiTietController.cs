using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Net105_Assignment_Lb.Models;

namespace Admin.Controllers
{
    public class GioHangChiTietController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public GioHangChiTietController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: GioHangChiTiet
        public async Task<IActionResult> Index()
        {
            var net105_AssignmentDbContext = _context.GioHangChiTiets.Include(g => g.KhachHang).Include(g => g.SanPhamChiTiet).Include(g => g.TrangThaiGioHang);
            return View(await net105_AssignmentDbContext.ToListAsync());
        }

        // GET: GioHangChiTiet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHangChiTiet = await _context.GioHangChiTiets
                .Include(g => g.KhachHang)
                .Include(g => g.SanPhamChiTiet)
                .Include(g => g.TrangThaiGioHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioHangChiTiet == null)
            {
                return NotFound();
            }

            return View(gioHangChiTiet);
        }

        // GET: GioHangChiTiet/Create
        public IActionResult Create()
        {
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id");
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id");
            ViewData["Id_TrangThaiGioHang"] = new SelectList(_context.TrangThaiGioHangs, "Id", "Id");
            return View();
        }

        // POST: GioHangChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_KhachHang,Id_SanPhamChiTiet,Id_TrangThaiGioHang,SoLuong")] GioHangChiTiet gioHangChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioHangChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id", gioHangChiTiet.Id_KhachHang);
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id", gioHangChiTiet.Id_SanPhamChiTiet);
            ViewData["Id_TrangThaiGioHang"] = new SelectList(_context.TrangThaiGioHangs, "Id", "Id", gioHangChiTiet.Id_TrangThaiGioHang);
            return View(gioHangChiTiet);
        }

        // GET: GioHangChiTiet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHangChiTiet = await _context.GioHangChiTiets.FindAsync(id);
            if (gioHangChiTiet == null)
            {
                return NotFound();
            }
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id", gioHangChiTiet.Id_KhachHang);
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id", gioHangChiTiet.Id_SanPhamChiTiet);
            ViewData["Id_TrangThaiGioHang"] = new SelectList(_context.TrangThaiGioHangs, "Id", "Id", gioHangChiTiet.Id_TrangThaiGioHang);
            return View(gioHangChiTiet);
        }

        // POST: GioHangChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_KhachHang,Id_SanPhamChiTiet,Id_TrangThaiGioHang,SoLuong")] GioHangChiTiet gioHangChiTiet)
        {
            if (id != gioHangChiTiet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioHangChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioHangChiTietExists(gioHangChiTiet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id", gioHangChiTiet.Id_KhachHang);
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id", gioHangChiTiet.Id_SanPhamChiTiet);
            ViewData["Id_TrangThaiGioHang"] = new SelectList(_context.TrangThaiGioHangs, "Id", "Id", gioHangChiTiet.Id_TrangThaiGioHang);
            return View(gioHangChiTiet);
        }

        // GET: GioHangChiTiet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHangChiTiet = await _context.GioHangChiTiets
                .Include(g => g.KhachHang)
                .Include(g => g.SanPhamChiTiet)
                .Include(g => g.TrangThaiGioHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioHangChiTiet == null)
            {
                return NotFound();
            }

            return View(gioHangChiTiet);
        }

        // POST: GioHangChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioHangChiTiet = await _context.GioHangChiTiets.FindAsync(id);
            if (gioHangChiTiet != null)
            {
                _context.GioHangChiTiets.Remove(gioHangChiTiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioHangChiTietExists(int id)
        {
            return _context.GioHangChiTiets.Any(e => e.Id == id);
        }
    }
}
