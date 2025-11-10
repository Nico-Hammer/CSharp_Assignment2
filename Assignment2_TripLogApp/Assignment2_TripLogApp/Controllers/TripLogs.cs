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
    /* add the database to the context of the controller */
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
            /*
             * create a new TripLog object and return the view with the entered data if invalid data is entered
             * otherwise this TripLog object is passed to the post request of this page
             */
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
            /*
             * if the model is valid i.e. no data that violates the validations of the model is entered, save all the info
             * into TempData then call .Keep() so that it persists to the next page
             */
            if (ModelState.IsValid)
            {
                TempData["Destination"] = tripLog.Destination;
                TempData["Accommodation"] = tripLog.Accommodation;
                TempData["StartDate"] = tripLog.StartDate;
                TempData["EndDate"] = tripLog.EndDate;
                TempData.Keep();
                /*
                 * check if the Accommodation field is empty, if not then take the user to the AccommodationInfo page
                 * otherwise send the user to the ThingsTodo page
                 */
                if (!tripLog.Accommodation.IsNullOrEmpty())
                {
                    return RedirectToAction("AccommodationInfo");
                }
                else
                {
                    return RedirectToAction("ThingsTodo");
                }
            }
            return View(tripLog); // return the view with the entered data if invalid data is entered
        }
        
        // GET: TripLogs/AccommodationInfo
        [HttpGet]
        public async Task<IActionResult> AccommodationInfo()
        {
            TripLog tripLog = new TripLog(); // create a new TripLog object instance
            /*
             * put the TempData Accommodation info into the TripLog object so it can be used to fill out the subheader
             * on the view, then call .Keep() to make sure all the TempData data persists to the next request
             */
            tripLog.Accommodation = TempData["Accommodation"].ToString();
            TempData.Keep();
            return View(tripLog); // if any data violates the validations then return the view with the entered data
        }
        
        // POST: TripLogs/AccommodationInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccommodationInfo(TripLog tripLog)
        {
            /* store the entered data in TempData then call .Keep() so it persists to the next request*/
            TempData["AccommodationPhoneNumber"] = tripLog.AccommodationPhoneNumber;
            TempData["AccommodationEmailAddress"] = tripLog.AccommodationEmailAddress;
            TempData.Keep();
            return RedirectToAction("ThingsTodo"); // send the user to the ThingsTodo page
        }

        // GET: TripLogs/ThingsTodo
        [HttpGet]
        public async Task<IActionResult> ThingsTodo()
        {
            TripLog tripLog = new TripLog(); // create a new TripLog object instance
            /*
             * put the TempData Destination info into the TripLog object so it can be used to fill out the subheader
             * on the view, then call .Keep() to make sure all the TempData data persists to the next request
             */
            tripLog.Destination = TempData["Destination"]?.ToString();
            TempData.Keep();
            return View(tripLog); // if any data violates the validations then return the view with the entered data
        }

        // POST: TripLogs/ThingsTodo
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThingsTodo(TripLog tripLog)
        {
            /* store the form data in TempData */
            TempData["ToDo1"] = tripLog.ToDo1;
            TempData["ToDo2"] = tripLog.ToDo2;
            TempData["ToDo3"] = tripLog.ToDo3;
            /* create the final TripLog object instance with all the data in TempData */
            tripLog.Destination = TempData["Destination"].ToString();
            tripLog.Accommodation = TempData["Accommodation"]?.ToString();
            tripLog.StartDate = Convert.ToDateTime(TempData["StartDate"]);
            tripLog.EndDate = Convert.ToDateTime(TempData["EndDate"]);
            tripLog.AccommodationPhoneNumber = TempData["AccommodationPhoneNumber"]?.ToString();
            tripLog.AccommodationEmailAddress = TempData["AccommodationEmailAddress"]?.ToString();
            tripLog.ToDo1 = TempData["ToDo1"]?.ToString();
            tripLog.ToDo2 = TempData["ToDo2"]?.ToString();
            tripLog.ToDo3 = TempData["ToDo3"]?.ToString();
            TempData.Keep(); // persist TempData to the next request for the "Trip to {Destination} added" subheader on the index page
            /* try to add the TripLog object to the database, if it cant get an id then return a NotFound page otherwise return the error */
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
            return RedirectToAction(nameof(Index)); // send the user back to the index page
        }

        /* check if the TripLog already exists in the database (since we dont edit the logs, this is kind of pointless)*/
        private bool TripLogExists(int id)
        {
            return _context.TripLogs.Any(e => e.TripId == id);
        }
    }
};