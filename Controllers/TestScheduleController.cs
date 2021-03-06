﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentLabManager.Data;
using StudentLabManager.Models;

namespace StudentLabManager.Controllers
{
    public class TestScheduleController : Controller
    {
        private readonly TestScheduleData _context;

        private Tuple<ActiveDirectory, string> AuthenticateUser(HttpContext httpContext)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value; // Gets Username from a claim in cookies
                var User =  new ActiveDirectory(UserName);
                if (User.role == "Staff") // Tests if the authenticated user is a staff member
                {
                    return new Tuple<ActiveDirectory, string>( User, UserName );
                }
            }
            return null;

        }
        private string[] GetStaffGroup(Tuple<ActiveDirectory, string> tuple)
        {
            ActiveDirectory User = tuple.Item1;
            string[] ClassGroup = User.GetGroup();
            //ViewBag.ClassList = ClassGroup;
            return ClassGroup;

        }
        
        public TestScheduleController(TestScheduleData context)
        {
            _context = context;
        }

        // GET: TestSchedule
        public async Task<IActionResult> Index()
        {
            
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) {
                ViewBag.ClassList = GetStaffGroup(tuple);
                return View(await _context.Schedule.ToListAsync());
            }
            return View("_InvalidationPage");
         
            
        }

        // GET: TestSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) // Tests if user is authenticated and is a staff member
            {
                if (id == null)
                {
                    return NotFound();
                }

                var testSchedule = await _context.Schedule
                    .FirstOrDefaultAsync(m => m.ID == id);
                // Gets the testschedule which has the ID of id
                if (testSchedule == null)
                {
                    return NotFound();
                }

                return View(testSchedule);
            }
            return View("_InvalidationPage");

            
        }

        // GET: TestSchedule/Create
        public IActionResult Create()
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) // Tests if user is authenticated and is a staff member
            {
                ViewBag.ClassList = String.Join(", ", GetStaffGroup(tuple));
                // Adds all the classes the Staff member manages for the cshtml script to use.
                return View();
            }
            return View("_InvalidationPage");
            
        }

        // POST: TestSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("exam,group,duration,schedule")] TestSchedule testSchedule)
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) // Tests if user is authenticated and is a staff member
            {
                if (ModelState.IsValid)
                { // Saves the testschedule to the database
                    _context.Add(testSchedule);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(testSchedule);
            }
            return View("_InvalidationPage");
        }

        // GET: TestSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) // Tests if user is authenticated and is a staff member
            {
                ViewBag.ClassList = String.Join(", ", GetStaffGroup(tuple));
                // Adds all the classes the Staff member manages for the cshtml script to use.

                if (id == null)
                {
                    return NotFound();
                }

                var testSchedule = await _context.Schedule.FindAsync(id);
                // Gets the testschedule which has the ID of id
                if (testSchedule == null)
                {
                    return NotFound();
                }
                return View(testSchedule);
            }
            return View("_InvalidationPage");
           
        }

        // POST: TestSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,exam,group,duration,schedule")] TestSchedule testSchedule)
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) // Tests if user is authenticated and is a staff member
            {
                if (id != testSchedule.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(testSchedule);
                        await _context.SaveChangesAsync();
                        // Saves the updated testschedule to the database
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TestScheduleExists(testSchedule.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                        // Adds an error handler for when there the testschedule referenced does not exist
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(testSchedule);
            }
            return View("_InvalidationPage");
        }

        // GET: TestSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) // Tests if user is authenticated and is a staff member
            {
                if (id == null)
                {
                    return NotFound();
                }

                var testSchedule = await _context.Schedule
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);
                // Get testschedule to delete
                if (testSchedule == null)
                {
                    return NotFound();
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewData["ErrorMessage"] =
                        "Delete Failed, Try again, if problems persist" +
                        "see your System Administrator.";
                    // If there was an error when deleting, notify the user.
                }

                return View(testSchedule);
                // Show testschedule they will delete
            }
            return View("_InvalidationPage");
        }

        // POST: TestSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null) // Tests if user is authenticated and is a staff member
            {
                var testSchedule = await _context.Schedule.FindAsync(id);
                if (testSchedule == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                try
                {
                    _context.Schedule.Remove(testSchedule);
                    await _context.SaveChangesAsync();
                    // Delete testschedule from database
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    // As there was an error, redirect the user to the delete screen and notify them that there was an error.
                    return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
                }
            }
            return View("_InvalidationPage");
        }

        private bool TestScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.ID == id);
        }
    }
}
