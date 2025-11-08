using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2_TripLogApp.Data;
using Assignment2_TripLogApp.Models;

namespace Assignment2_TripLogApp.Controllers
{
    public class TripLogs : Controller
    {
        private readonly AppDbContext _context;

        public TripLogs(AppDbContext context)
        {
            _context = context;
        }

        // GET: TripLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TripLogs.ToListAsync());
        }

        // GET: TripLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripLog = await _context.TripLogs
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (tripLog == null)
            {
                return NotFound();
            }

            return View(tripLog);
        }

        // GET: TripLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TripLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,Destination,Accommodation,StartDate,EndDate,AccommodationPhoneNumber,AccommodationEmailAddress,ToDo1,ToDo2,ToDo3")] TripLog tripLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripLog);
        }

        // GET: TripLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripLog = await _context.TripLogs.FindAsync(id);
            if (tripLog == null)
            {
                return NotFound();
            }
            return View(tripLog);
        }

        // POST: TripLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripId,Destination,Accommodation,StartDate,EndDate,AccommodationPhoneNumber,AccommodationEmailAddress,ToDo1,ToDo2,ToDo3")] TripLog tripLog)
        {
            if (id != tripLog.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripLogExists(tripLog.TripId))
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
            return View(tripLog);
        }

        // GET: TripLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripLog = await _context.TripLogs
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (tripLog == null)
            {
                return NotFound();
            }

            return View(tripLog);
        }

        // POST: TripLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripLog = await _context.TripLogs.FindAsync(id);
            if (tripLog != null)
            {
                _context.TripLogs.Remove(tripLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripLogExists(int id)
        {
            return _context.TripLogs.Any(e => e.TripId == id);
        }
    }
}
