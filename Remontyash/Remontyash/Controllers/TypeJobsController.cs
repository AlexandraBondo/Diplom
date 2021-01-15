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
    public class TypeJobsController : Controller
    {
        private readonly RemontDBContext _context;

        public TypeJobsController(RemontDBContext context)
        {
            _context = context;
        }

        // GET: TypeJobs
        public async Task<IActionResult> Index()
        {
            var remontDBContext = _context.TypeJobs.Include(t => t.TypeTechnic);
            return View(await remontDBContext.ToListAsync());
        }

        // GET: TypeJobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeJob = await _context.TypeJobs
                .Include(t => t.TypeTechnic)
                .FirstOrDefaultAsync(m => m.TypeJobId == id);
            if (typeJob == null)
            {
                return NotFound();
            }

            return View(typeJob);
        }

        // GET: TypeJobs/Create
        public IActionResult Create()
        {
            ViewData["TypeTechnicId"] = new SelectList(_context.TypeTechnics, "TypeTechnicId", "Description");
            return View();
        }

        // POST: TypeJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeJobId,Description,TypeTechnicId,Cost")] TypeJob typeJob)
        {
            if (ModelState.IsValid)
            {
                typeJob.TypeJobId = Guid.NewGuid();
                _context.Add(typeJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeTechnicId"] = new SelectList(_context.TypeTechnics, "TypeTechnicId", "Description", typeJob.TypeTechnicId);
            return View(typeJob);
        }

        // GET: TypeJobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeJob = await _context.TypeJobs.FindAsync(id);
            if (typeJob == null)
            {
                return NotFound();
            }
            ViewData["TypeTechnicId"] = new SelectList(_context.TypeTechnics, "TypeTechnicId", "Description", typeJob.TypeTechnicId);
            return View(typeJob);
        }

        // POST: TypeJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TypeJobId,Description,TypeTechnicId,Cost")] TypeJob typeJob)
        {
            if (id != typeJob.TypeJobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeJobExists(typeJob.TypeJobId))
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
            ViewData["TypeTechnicId"] = new SelectList(_context.TypeTechnics, "TypeTechnicId", "Description", typeJob.TypeTechnicId);
            return View(typeJob);
        }

        // GET: TypeJobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeJob = await _context.TypeJobs
                .Include(t => t.TypeTechnic)
                .FirstOrDefaultAsync(m => m.TypeJobId == id);
            if (typeJob == null)
            {
                return NotFound();
            }

            return View(typeJob);
        }

        // POST: TypeJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var typeJob = await _context.TypeJobs.FindAsync(id);
            _context.TypeJobs.Remove(typeJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeJobExists(Guid id)
        {
            return _context.TypeJobs.Any(e => e.TypeJobId == id);
        }
    }
}
