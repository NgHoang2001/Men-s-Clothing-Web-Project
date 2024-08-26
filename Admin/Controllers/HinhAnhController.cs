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
    public class HinhAnhController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public HinhAnhController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: HinhAnh
        public async Task<IActionResult> Index()
        {
            var net105_AssignmentDbContext = _context.HinhAnhs.Include(h => h.DanhMuc).Include(h => h.SanPham);
            return View(await net105_AssignmentDbContext.ToListAsync());
        }

        // GET: HinhAnh/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hinhAnh = await _context.HinhAnhs
                .Include(h => h.DanhMuc)
                .Include(h => h.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hinhAnh == null)
            {
                return NotFound();
            }

            return View(hinhAnh);
        }

        // GET: HinhAnh/Create
        public IActionResult Create()
        {
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id");
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id");
            return View();
        }

        // POST: HinhAnh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_SanPham,Id_DanhMuc,Url,Kieu")] HinhAnh hinhAnh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hinhAnh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id", hinhAnh.Id_DanhMuc);
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id", hinhAnh.Id_SanPham);
            return View(hinhAnh);
        }

        // GET: HinhAnh/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hinhAnh = await _context.HinhAnhs.FindAsync(id);
            if (hinhAnh == null)
            {
                return NotFound();
            }
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id", hinhAnh.Id_DanhMuc);
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id", hinhAnh.Id_SanPham);
            return View(hinhAnh);
        }

        // POST: HinhAnh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_SanPham,Id_DanhMuc,Url,Kieu")] HinhAnh hinhAnh)
        {
            if (id != hinhAnh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hinhAnh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HinhAnhExists(hinhAnh.Id))
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
            ViewData["Id_DanhMuc"] = new SelectList(_context.DanhMucs, "Id", "Id", hinhAnh.Id_DanhMuc);
            ViewData["Id_SanPham"] = new SelectList(_context.SanPhams, "Id", "Id", hinhAnh.Id_SanPham);
            return View(hinhAnh);
        }

        // GET: HinhAnh/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hinhAnh = await _context.HinhAnhs
                .Include(h => h.DanhMuc)
                .Include(h => h.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hinhAnh == null)
            {
                return NotFound();
            }

            return View(hinhAnh);
        }

        // POST: HinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hinhAnh = await _context.HinhAnhs.FindAsync(id);
            if (hinhAnh != null)
            {
                _context.HinhAnhs.Remove(hinhAnh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HinhAnhExists(int id)
        {
            return _context.HinhAnhs.Any(e => e.Id == id);
        }
    }
}
