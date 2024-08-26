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
    public class SanPhamChiTietController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public SanPhamChiTietController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: SanPhamChiTiet
        public async Task<IActionResult> Index()
        {
            var net105_AssignmentDbContext = _context.SanPhamChiTiets.Include(s => s.KichThuoc).Include(s => s.MauSac).Include(s => s.SanPham);
            return View(await net105_AssignmentDbContext.ToListAsync());
        }

        // GET: SanPhamChiTiet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPhamChiTiet = await _context.SanPhamChiTiets
                .Include(s => s.KichThuoc)
                .Include(s => s.MauSac)
                .Include(s => s.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPhamChiTiet == null)
            {
                return NotFound();
            }

            return View(sanPhamChiTiet);
        }

        // GET: SanPhamChiTiet/Create
        public IActionResult Create()
        {
            ViewData["Id_KichThuoc"] = new SelectList(_context.KichThuocs, "Id", "Id");
            ViewData["Id_MauSac"] = new SelectList(_context.MauSacs, "Id", "Id");
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id");
            return View();
        }

        // POST: SanPhamChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_SanPham,Id_MauSac,Id_KichThuoc,SoLuong")] SanPhamChiTiet sanPhamChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPhamChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_KichThuoc"] = new SelectList(_context.KichThuocs, "Id", "Id", sanPhamChiTiet.Id_KichThuoc);
            ViewData["Id_MauSac"] = new SelectList(_context.MauSacs, "Id", "Id", sanPhamChiTiet.Id_MauSac);
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id", sanPhamChiTiet.Id_SanPham);
            return View(sanPhamChiTiet);
        }

        // GET: SanPhamChiTiet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPhamChiTiet = await _context.SanPhamChiTiets.FindAsync(id);
            if (sanPhamChiTiet == null)
            {
                return NotFound();
            }
            ViewData["Id_KichThuoc"] = new SelectList(_context.KichThuocs, "Id", "Id", sanPhamChiTiet.Id_KichThuoc);
            ViewData["Id_MauSac"] = new SelectList(_context.MauSacs, "Id", "Id", sanPhamChiTiet.Id_MauSac);
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id", sanPhamChiTiet.Id_SanPham);
            return View(sanPhamChiTiet);
        }

        // POST: SanPhamChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_SanPham,Id_MauSac,Id_KichThuoc,SoLuong")] SanPhamChiTiet sanPhamChiTiet)
        {
            if (id != sanPhamChiTiet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPhamChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamChiTietExists(sanPhamChiTiet.Id))
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
            ViewData["Id_KichThuoc"] = new SelectList(_context.KichThuocs, "Id", "Id", sanPhamChiTiet.Id_KichThuoc);
            ViewData["Id_MauSac"] = new SelectList(_context.MauSacs, "Id", "Id", sanPhamChiTiet.Id_MauSac);
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id", sanPhamChiTiet.Id_SanPham);
            return View(sanPhamChiTiet);
        }

        // GET: SanPhamChiTiet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPhamChiTiet = await _context.SanPhamChiTiets
                .Include(s => s.KichThuoc)
                .Include(s => s.MauSac)
                .Include(s => s.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPhamChiTiet == null)
            {
                return NotFound();
            }

            return View(sanPhamChiTiet);
        }

        // POST: SanPhamChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPhamChiTiet = await _context.SanPhamChiTiets.FindAsync(id);
            if (sanPhamChiTiet != null)
            {
                _context.SanPhamChiTiets.Remove(sanPhamChiTiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamChiTietExists(int id)
        {
            return _context.SanPhamChiTiets.Any(e => e.Id == id);
        }
    }
}
