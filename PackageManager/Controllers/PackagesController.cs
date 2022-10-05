using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IActionResult Index()
        {
            return View();
        }

        // GET: Package list
        public async Task<ActionResult> PackageList(int page=1, IsSealedFilter filter=IsSealedFilter.ANY)
        {
            int recordsPerPage = 5;
            int offset = (page - 1) * recordsPerPage;

            var filterPredicate = filter.GetFilter();

            var filteredPackages = _context.Package
                .OrderBy(x => x.CreationDate)
                .Where(filterPredicate);
                
            ViewBag.page = page;
            ViewBag.nPages = (filteredPackages.Count() - 1) / recordsPerPage + 1;

            var displayedPackages = await filteredPackages
                .Skip(offset)
                .Take(recordsPerPage)
                .ToListAsync();

            return PartialView("PackageList", displayedPackages);
        }

        // GET: Packages/Edit/5
        public async Task<IActionResult> Edit(int? id, bool unseal=false)
        {
            // If a new package is being created, show an empty Edit View
            if (id == null)
            {
                return View(new Package());
            }
            

            // Otherwise fetch package from database
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            package.Items = await _context.Item.AsQueryable().Where(i => i.PackageID == id).ToListAsync();

            // If package is not to be unsealed, show Details view
            if (!unseal && package.IsSealed) return View("Details", package);

            // The package was just unsealed, save that to database immediately
            package.IsSealed = false;
            _context.Update(package);
            await _context.SaveChangesAsync();

            return View(package);
        }

        // POST: Packages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            bool seal,
            [Bind("Id,Name,CreationDate,City,IsSealed,SealDate")] Package package,
            [Bind("Id,Name,CreationDate,Address,Mass")] IEnumerable<Item> items
        )
        {
            if (id != package.Id) return NotFound();
            if (!ModelState.IsValid) return View(package);

            // Set seal status
            if (seal)
            {
                package.IsSealed = true;
                package.SealDate = DateTime.Now;
            }
                

            // Add Package if it does not exists
            if (id == 0)
            {
                package.Items = (List<Item>) items;
                _context.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 

            try
            {
                // Remove deleted items
                foreach (var oldItem in _context.Item.Where(i => i.PackageID == id).ToList())
                {
                    if (items.Select(item => item.Id).Contains(oldItem.Id)) continue;

                    _context.Item.Remove(oldItem);
                }

                foreach (var newItem in items)
                {
                    if (newItem.Id != 0) // Update existing items
                    {
                        var targetItem = _context.Item.SingleOrDefault(i => i.Id == newItem.Id);
                        if (targetItem == null) continue;
                        _context.Entry(targetItem).CurrentValues.SetValues(newItem);
                    }
                    else // Add new items
                    {
                        newItem.PackageID = package.Id;
                        _context.Item.Attach(newItem);
                    }
                }

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


        // GET: Packages/LoadItemField/5
        [HttpGet]
        public virtual ActionResult LoadItemField(int? id)
        {
            if (id == null) return NotFound();

            ViewBag.id = id;
            return PartialView("AddItemForm");
        }


    }
}
