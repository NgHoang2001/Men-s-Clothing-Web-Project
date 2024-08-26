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
    public class SanPhamController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public SanPhamController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: SanPham
        public async Task<IActionResult> Index()
        {
            var net105_AssignmentDbContext = _context.SanPhams.Include(s => s.DanhMuc).Include(s => s.DoiTuong);
            return View(await net105_AssignmentDbContext.ToListAsync());
        }

        // GET: SanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.DoiTuong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPham/Create
        public IActionResult Create()
        {
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id");
            ViewData["Id_DoiTuong"] = new SelectList(_context.DoiTuongs, "Id", "Id");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_DanhMuc,Id_DoiTuong,Ten,MoTa,ChatLieu,HuongDanSuDung,DonGia,TrangThai")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id", sanPham.Id_DanhMuc);
            ViewData["Id_DoiTuong"] = new SelectList(_context.DoiTuongs, "Id", "Id", sanPham.Id_DoiTuong);
            return View(sanPham);
        }

        // GET: SanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id", sanPham.Id_DanhMuc);
            ViewData["Id_DoiTuong"] = new SelectList(_context.DoiTuongs, "Id", "Id", sanPham.Id_DoiTuong);
            return View(sanPham);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_DanhMuc,Id_DoiTuong,Ten,MoTa,ChatLieu,HuongDanSuDung,DonGia,TrangThai")] SanPham sanPham)
        {
            if (id != sanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.Id))
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
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id", sanPham.Id_DanhMuc);
            ViewData["Id_DoiTuong"] = new SelectList(_context.DoiTuongs, "Id", "Id", sanPham.Id_DoiTuong);
            return View(sanPham);
        }

        // GET: SanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.DoiTuong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.Id == id);
        }
    }
}
