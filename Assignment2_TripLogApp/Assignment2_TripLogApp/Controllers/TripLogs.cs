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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TripLogs.ToListAsync());
        }
        

        // GET: TripLogs/BasicInfo
        [HttpGet]
        public IActionResult BasicInfo()
        {
            return View();
        }

        // POST: TripLogs/BasicInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BasicInfo([Bind("TripId,Destination,Accommodation,StartDate,EndDate")] TripLog tripLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripLog);
        }
        
        // GET: TripLogs/AccommodationInfo
        [HttpGet]
        public async Task<IActionResult> AccommodationInfo()
        {
            return View();
        }

        // GET: TripLogs/ThingsTodo
        [HttpGet]
        public async Task<IActionResult> ThingsTodo()
        {
            return View();
        }

        // POST: TripLogs/ThingsTodo
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThingsTodo(int id, [Bind("TripId,Destination,Accommodation,StartDate,EndDate,AccommodationPhoneNumber,AccommodationEmailAddress,ToDo1,ToDo2,ToDo3")] TripLog tripLog)
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

        private bool TripLogExists(int id)
        {
            return _context.TripLogs.Any(e => e.TripId == id);
        }
    }
}
