using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Remontyash.Models;

namespace Remontyash.Controllers
{
    [Authorize]
    public class TypeTechnicsController : Controller
    {
        private readonly RemontDBContext _context;

        public TypeTechnicsController(RemontDBContext context)
        {
            _context = context;
        }

        // GET: TypeTechnics
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeTechnics.ToListAsync());
        }

        // GET: TypeTechnics/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeTechnic = await _context.TypeTechnics
                .FirstOrDefaultAsync(m => m.TypeTechnicId == id);
            if (typeTechnic == null)
            {
                return NotFound();
            }

            return View(typeTechnic);
        }

        // GET: TypeTechnics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeTechnics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeTechnicId,Description")] TypeTechnic typeTechnic)
        {
            if (ModelState.IsValid)
            {
                typeTechnic.TypeTechnicId = Guid.NewGuid();
                _context.Add(typeTechnic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeTechnic);
        }

        // GET: TypeTechnics/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeTechnic = await _context.TypeTechnics.FindAsync(id);
            if (typeTechnic == null)
            {
                return NotFound();
            }
            return View(typeTechnic);
        }

        // POST: TypeTechnics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TypeTechnicId,Description")] TypeTechnic typeTechnic)
        {
            if (id != typeTechnic.TypeTechnicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeTechnic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeTechnicExists(typeTechnic.TypeTechnicId))
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
            return View(typeTechnic);
        }

        // GET: TypeTechnics/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeTechnic = await _context.TypeTechnics
                .FirstOrDefaultAsync(m => m.TypeTechnicId == id);
            if (typeTechnic == null)
            {
                return NotFound();
            }

            return View(typeTechnic);
        }

        // POST: TypeTechnics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var typeTechnic = await _context.TypeTechnics.FindAsync(id);
            _context.TypeTechnics.Remove(typeTechnic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeTechnicExists(Guid id)
        {
            return _context.TypeTechnics.Any(e => e.TypeTechnicId == id);
        }
    }
}
