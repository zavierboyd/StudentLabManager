using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
                var User =  new ActiveDirectory(UserName);
                if (User.role == "Staff")
                {
                    return new Tuple<ActiveDirectory, string>( User, UserName );
                }
            }
                return null;

        }
        private string[] GetStaffGroup(Tuple<ActiveDirectory, string> tuple)
        {
            string UserName = tuple.Item2;
            ActiveDirectory User = tuple.Item1;
            string[] ClassGroup = User.GetGroup(UserName);
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
            if (tuple != null)
            {
                //ViewBag.ClassList = GetStaffGroup(tuple);
                if (id == null)
                {
                    return NotFound();
                }

                var testSchedule = await _context.Schedule
                    .FirstOrDefaultAsync(m => m.ID == id);
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
            if (tuple != null)
            {
                ViewBag.ClassList = String.Join(", ", GetStaffGroup(tuple));
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
            if (tuple != null)
            {
                if (ModelState.IsValid)
                {
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
            if (tuple != null)
            {
                ViewBag.ClassList = String.Join(", ", GetStaffGroup(tuple));
                if (id == null)
                {
                    return NotFound();
                }

                var testSchedule = await _context.Schedule.FindAsync(id);
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
            if (tuple != null)
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
            if (tuple != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var testSchedule = await _context.Schedule
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (testSchedule == null)
                {
                    return NotFound();
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewData["ErrorMessage"] =
                        "Delete Failed, Try again, if problems persist" +
                        "see your System Administrator.";
                }

                return View(testSchedule);
            }
            return View("_InvalidationPage");
        }

        // POST: TestSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null)
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
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    // Log the error
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
