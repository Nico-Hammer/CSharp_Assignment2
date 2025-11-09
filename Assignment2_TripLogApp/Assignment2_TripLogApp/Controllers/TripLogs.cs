using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2_TripLogApp.Data;
using Assignment2_TripLogApp.Models;
using Microsoft.IdentityModel.Tokens;

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
            var tripLog = new TripLog();
            return View(tripLog);
        }

        // POST: TripLogs/BasicInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BasicInfo(TripLog tripLog)
        {
            if (ModelState.IsValid)
            {
                TempData["Destination"] = tripLog.Destination;
                TempData["Accommodation"] = tripLog.Accommodation;
                TempData["StartDate"] = tripLog.StartDate;
                TempData["EndDate"] = tripLog.EndDate;
                TempData.Keep();
                if (!tripLog.Accommodation.IsNullOrEmpty())
                {
                    return RedirectToAction("AccommodationInfo");
                }
                else
                {
                    return RedirectToAction("ThingsTodo");
                }
            }
            return View(tripLog);
        }
        
        // GET: TripLogs/AccommodationInfo
        [HttpGet]
        public async Task<IActionResult> AccommodationInfo()
        {
            TripLog tripLog = new TripLog();
            tripLog.Accommodation = TempData["Accommodation"].ToString();
            TempData.Keep();
            return View(tripLog);
        }
        
        // POST: TripLogs/AccommodationInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccommodationInfo(TripLog tripLog)
        {
            TempData["AccommodationPhoneNumber"] = tripLog.AccommodationPhoneNumber;
            TempData["AccommodationEmailAddress"] = tripLog.AccommodationEmailAddress;
            TempData.Keep();
            return RedirectToAction("ThingsTodo");
        }

        // GET: TripLogs/ThingsTodo
        [HttpGet]
        public async Task<IActionResult> ThingsTodo()
        {
            TripLog tripLog = new TripLog();
            tripLog.Destination = TempData["Destination"]?.ToString();
            TempData.Keep();
            return View(tripLog);
        }

        // POST: TripLogs/ThingsTodo
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThingsTodo(TripLog tripLog)
        {
            TempData["ToDo1"] = tripLog.ToDo1;
            TempData["ToDo2"] = tripLog.ToDo2;
            TempData["ToDo3"] = tripLog.ToDo3;
            tripLog.Destination = TempData["Destination"].ToString();
            tripLog.Accommodation = TempData["Accommodation"]?.ToString();
            tripLog.StartDate = Convert.ToDateTime(TempData["StartDate"]);
            tripLog.EndDate = Convert.ToDateTime(TempData["EndDate"]);
            tripLog.AccommodationPhoneNumber = TempData["AccommodationPhoneNumber"]?.ToString();
            tripLog.AccommodationEmailAddress = TempData["AccommodationEmailAddress"]?.ToString();
            tripLog.ToDo1 = TempData["ToDo1"]?.ToString();
            tripLog.ToDo2 = TempData["ToDo2"]?.ToString();
            tripLog.ToDo3 = TempData["ToDo3"]?.ToString();
            TempData.Keep();
            try
            {
                _context.Add(tripLog);
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
            return View(tripLog);
        }

        private bool TripLogExists(int id)
        {
            return _context.TripLogs.Any(e => e.TripId == id);
        }
    }
};