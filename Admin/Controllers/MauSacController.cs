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
    public class MauSacController : Controller
    {
        private readonly Net105_AssignmentDbContext _context;

        public MauSacController(Net105_AssignmentDbContext context)
        {
            _context = context;
        }

        // GET: MauSac
        public async Task<IActionResult> Index()
        {
            return View(await _context.MauSacs.ToListAsync());
        }

        // GET: MauSac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _context.MauSacs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mauSac == null)
            {
                return NotFound();
            }

            return View(mauSac);
        }

        // GET: MauSac/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MauSac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Url")] MauSac mauSac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mauSac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mauSac);
        }

        // GET: MauSac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _context.MauSacs.FindAsync(id);
            if (mauSac == null)
            {
                return NotFound();
            }
            return View(mauSac);
        }

        // POST: MauSac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,Url")] MauSac mauSac)
        {
            if (id != mauSac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mauSac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MauSacExists(mauSac.Id))
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
            return View(mauSac);
        }

        // GET: MauSac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _context.MauSacs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mauSac == null)
            {
                return NotFound();
            }

            return View(mauSac);
        }

        // POST: MauSac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mauSac = await _context.MauSacs.FindAsync(id);
            if (mauSac != null)
            {
                _context.MauSacs.Remove(mauSac);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MauSacExists(int id)
        {
            return _context.MauSacs.Any(e => e.Id == id);
        }
    }
}
