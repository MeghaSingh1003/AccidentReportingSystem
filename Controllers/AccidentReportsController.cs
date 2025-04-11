using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AccidentReportingSystem.Controllers
{
    public class AccidentReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccidentReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccidentReports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AccidentReports.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AccidentReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccidentReports == null)
            {
                return NotFound();
            }

            var accidentReport = await _context.AccidentReports
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accidentReport == null)
            {
                return NotFound();
            }

            return View(accidentReport);
        }

        // GET: AccidentReports/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AccidentReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,Date,Description,UserId")] AccidentReport accidentReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accidentReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", accidentReport.UserId);
            return View(accidentReport);
        }

        // GET: AccidentReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccidentReports == null)
            {
                return NotFound();
            }

            var accidentReport = await _context.AccidentReports.FindAsync(id);
            if (accidentReport == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", accidentReport.UserId);
            return View(accidentReport);
        }

        // POST: AccidentReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Location,Date,Description,UserId")] AccidentReport accidentReport)
        {
            if (id != accidentReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accidentReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccidentReportExists(accidentReport.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", accidentReport.UserId);
            return View(accidentReport);
        }

        // GET: AccidentReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AccidentReports == null)
            {
                return NotFound();
            }

            var accidentReport = await _context.AccidentReports
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accidentReport == null)
            {
                return NotFound();
            }

            return View(accidentReport);
        }

        // POST: AccidentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AccidentReports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AccidentReports'  is null.");
            }
            var accidentReport = await _context.AccidentReports.FindAsync(id);
            if (accidentReport != null)
            {
                _context.AccidentReports.Remove(accidentReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccidentReportExists(int id)
        {
          return (_context.AccidentReports?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}