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
    public class KichThuocController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public KichThuocController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: KichThuoc
        public async Task<IActionResult> Index()
        {
            return View(await _context.KichThuocs.ToListAsync());
        }

        // GET: KichThuoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kichThuoc = await _context.KichThuocs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kichThuoc == null)
            {
                return NotFound();
            }

            return View(kichThuoc);
        }

        // GET: KichThuoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KichThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten")] KichThuoc kichThuoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kichThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kichThuoc);
        }

        // GET: KichThuoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kichThuoc = await _context.KichThuocs.FindAsync(id);
            if (kichThuoc == null)
            {
                return NotFound();
            }
            return View(kichThuoc);
        }

        // POST: KichThuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten")] KichThuoc kichThuoc)
        {
            if (id != kichThuoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kichThuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KichThuocExists(kichThuoc.Id))
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
            return View(kichThuoc);
        }

        // GET: KichThuoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kichThuoc = await _context.KichThuocs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kichThuoc == null)
            {
                return NotFound();
            }

            return View(kichThuoc);
        }

        // POST: KichThuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kichThuoc = await _context.KichThuocs.FindAsync(id);
            if (kichThuoc != null)
            {
                _context.KichThuocs.Remove(kichThuoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KichThuocExists(int id)
        {
            return _context.KichThuocs.Any(e => e.Id == id);
        }
    }
}
