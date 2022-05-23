using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Matrix_Task_anotherSolution.Models;

namespace Matrix_Task_anotherSolution.Controllers
{
    public class CategoryPropsController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryPropsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CategoryProps
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CategoryProps.Include(c => c.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CategoryProps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryProps == null)
            {
                return NotFound();
            }

            var categoryProps = await _context.CategoryProps
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryProps == null)
            {
                return NotFound();
            }

            return View(categoryProps);
        }

        // GET: CategoryProps/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: CategoryProps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId")] CategoryProps categoryProps)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryProps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categoryProps.CategoryId);
            return View(categoryProps);
        }

        // GET: CategoryProps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryProps == null)
            {
                return NotFound();
            }

            var categoryProps = await _context.CategoryProps.FindAsync(id);
            if (categoryProps == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categoryProps.CategoryId);
            return View(categoryProps);
        }

        // POST: CategoryProps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId")] CategoryProps categoryProps)
        {
            if (id != categoryProps.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryProps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryPropsExists(categoryProps.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categoryProps.CategoryId);
            return View(categoryProps);
        }

        // GET: CategoryProps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryProps == null)
            {
                return NotFound();
            }

            var categoryProps = await _context.CategoryProps
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryProps == null)
            {
                return NotFound();
            }

            return View(categoryProps);
        }

        // POST: CategoryProps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryProps == null)
            {
                return Problem("Entity set 'AppDbContext.CategoryProps'  is null.");
            }
            var categoryProps = await _context.CategoryProps.FindAsync(id);
            if (categoryProps != null)
            {
                _context.CategoryProps.Remove(categoryProps);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryPropsExists(int id)
        {
          return _context.CategoryProps.Any(e => e.Id == id);
        }
    }
}
