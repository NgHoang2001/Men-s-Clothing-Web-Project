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
    public class VaiTroController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public VaiTroController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: VaiTro
        public async Task<IActionResult> Index()
        {
            return View(await _context.VaiTros.ToListAsync());
        }

        // GET: VaiTro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaiTro = await _context.VaiTros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaiTro == null)
            {
                return NotFound();
            }

            return View(vaiTro);
        }

        // GET: VaiTro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaiTro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten")] VaiTro vaiTro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaiTro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaiTro);
        }

        // GET: VaiTro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaiTro = await _context.VaiTros.FindAsync(id);
            if (vaiTro == null)
            {
                return NotFound();
            }
            return View(vaiTro);
        }

        // POST: VaiTro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten")] VaiTro vaiTro)
        {
            if (id != vaiTro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaiTro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaiTroExists(vaiTro.Id))
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
            return View(vaiTro);
        }

        // GET: VaiTro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaiTro = await _context.VaiTros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaiTro == null)
            {
                return NotFound();
            }

            return View(vaiTro);
        }

        // POST: VaiTro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaiTro = await _context.VaiTros.FindAsync(id);
            if (vaiTro != null)
            {
                _context.VaiTros.Remove(vaiTro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaiTroExists(int id)
        {
            return _context.VaiTros.Any(e => e.Id == id);
        }
    }
}
