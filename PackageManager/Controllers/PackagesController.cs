﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PackageManager.Data;
using PackageManager.Models;

namespace PackageManager.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PackageManagerContext _context;

        public PackagesController(PackageManagerContext context)
        {
            _context = context;
        }

        // GET: Packages
        public async Task<IActionResult> Index()
        {
              return _context.Package != null ? 
                          View(await _context.Package.ToListAsync()) :
                          Problem("Entity set 'PackageManagerContext.Package'  is null.");
        }

        // GET: Packages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package
                .FirstOrDefaultAsync(m => m.Id == id);

            if (package == null)
            {
                return NotFound();
            }

            package.Items = await _context.Item.AsQueryable().Where(i => i.PackageID == id).ToListAsync();

            return View(package);
        }

        // GET: Packages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreationDate,City,IsSealed,SealDate")] Package package)
        {
            if (ModelState.IsValid)
            {
                _context.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        // GET: Packages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreationDate,City,IsSealed,SealDate")] Package package)
        {
            if (id != package.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.Id))
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
            return View(package);
        }

        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package
                .FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'PackageManagerContext.Package'  is null.");
            }
            var package = await _context.Package.FindAsync(id);
            if (package != null)
            {
                _context.Package.Remove(package);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
          return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
