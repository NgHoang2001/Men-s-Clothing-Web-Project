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
    public class DonHangChiTietController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public DonHangChiTietController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: DonHangChiTiet
        public async Task<IActionResult> Index()
        {
            var net105_AssignmentDbContext = _context.DonHangChiTiets.Include(d => d.DonHang).Include(d => d.SanPhamChiTiet);
            return View(await net105_AssignmentDbContext.ToListAsync());
        }

        // GET: DonHangChiTiet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHangChiTiet = await _context.DonHangChiTiets
                .Include(d => d.DonHang)
                .Include(d => d.SanPhamChiTiet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHangChiTiet == null)
            {
                return NotFound();
            }

            return View(donHangChiTiet);
        }

        // GET: DonHangChiTiet/Create
        public IActionResult Create()
        {
            ViewData["Id_DonHang"] = new SelectList(_context.DonHangs, "Id", "Id");
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id");
            return View();
        }

        // POST: DonHangChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_DonHang,Id_SanPhamChiTiet,SoLuong,DonGia,ThoiGianTao")] DonHangChiTiet donHangChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHangChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_DonHang"] = new SelectList(_context.DonHangs, "Id", "Id", donHangChiTiet.Id_DonHang);
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id", donHangChiTiet.Id_SanPhamChiTiet);
            return View(donHangChiTiet);
        }

        // GET: DonHangChiTiet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHangChiTiet = await _context.DonHangChiTiets.FindAsync(id);
            if (donHangChiTiet == null)
            {
                return NotFound();
            }
            ViewData["Id_DonHang"] = new SelectList(_context.DonHangs, "Id", "Id", donHangChiTiet.Id_DonHang);
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id", donHangChiTiet.Id_SanPhamChiTiet);
            return View(donHangChiTiet);
        }

        // POST: DonHangChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_DonHang,Id_SanPhamChiTiet,SoLuong,DonGia,ThoiGianTao")] DonHangChiTiet donHangChiTiet)
        {
            if (id != donHangChiTiet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHangChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangChiTietExists(donHangChiTiet.Id))
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
            ViewData["Id_DonHang"] = new SelectList(_context.DonHangs, "Id", "Id", donHangChiTiet.Id_DonHang);
            ViewData["Id_SanPhamChiTiet"] = new SelectList(_context.SanPhamChiTiets, "Id", "Id", donHangChiTiet.Id_SanPhamChiTiet);
            return View(donHangChiTiet);
        }

        // GET: DonHangChiTiet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHangChiTiet = await _context.DonHangChiTiets
                .Include(d => d.DonHang)
                .Include(d => d.SanPhamChiTiet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHangChiTiet == null)
            {
                return NotFound();
            }

            return View(donHangChiTiet);
        }

        // POST: DonHangChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHangChiTiet = await _context.DonHangChiTiets.FindAsync(id);
            if (donHangChiTiet != null)
            {
                _context.DonHangChiTiets.Remove(donHangChiTiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangChiTietExists(int id)
        {
            return _context.DonHangChiTiets.Any(e => e.Id == id);
        }
    }
}
