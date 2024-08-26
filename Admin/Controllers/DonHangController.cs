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
    public class DonHangController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public DonHangController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: DonHang
        public async Task<IActionResult> Index()
        {
            var net105_AssignmentDbContext = _context.DonHangs.Include(d => d.DiaChi).Include(d => d.KhachHang);
            return View(await net105_AssignmentDbContext.ToListAsync());
        }

        // GET: DonHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs
                .Include(d => d.DiaChi)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: DonHang/Create
        public IActionResult Create()
        {
            ViewData["Id_DiaChi"] = new SelectList(_context.DiaChis, "Id", "Id");
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id");
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_KhachHang,Id_DiaChi,DiaChiChiTiet,ThoiGianTao")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_DiaChi"] = new SelectList(_context.DiaChis, "Id", "Id", donHang.Id_DiaChi);
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id", donHang.Id_KhachHang);
            return View(donHang);
        }

        // GET: DonHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["Id_DiaChi"] = new SelectList(_context.DiaChis, "Id", "Id", donHang.Id_DiaChi);
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id", donHang.Id_KhachHang);
            return View(donHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_KhachHang,Id_DiaChi,DiaChiChiTiet,ThoiGianTao")] DonHang donHang)
        {
            if (id != donHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.Id))
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
            ViewData["Id_DiaChi"] = new SelectList(_context.DiaChis, "Id", "Id", donHang.Id_DiaChi);
            ViewData["Id_KhachHang"] = new SelectList(_context.KhachHangs, "Id", "Id", donHang.Id_KhachHang);
            return View(donHang);
        }

        // GET: DonHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs
                .Include(d => d.DiaChi)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang != null)
            {
                _context.DonHangs.Remove(donHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHangs.Any(e => e.Id == id);
        }
    }
}
