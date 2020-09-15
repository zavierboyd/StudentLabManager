using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public TestScheduleController(TestScheduleData context)
        {
            _context = context;
        }

        // GET: TestSchedule
        public async Task<IActionResult> Index()
        {
            return View(await _context.Schedule.ToListAsync());
        }

        // GET: TestSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

        // GET: TestSchedule/Create
        public IActionResult Create()
        {
            ViewBag.classlist = string.Join(", ", new string[] {"class1", "class2"});
            return View();
        }

        // POST: TestSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("exam,group,duration,schedule")] TestSchedule testSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testSchedule);
        }

        // GET: TestSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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

        // POST: TestSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,exam,group,duration,schedule")] TestSchedule testSchedule)
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

        // GET: TestSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

        // POST: TestSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testSchedule = await _context.Schedule.FindAsync(id);
            _context.Schedule.Remove(testSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.ID == id);
        }
    }
}
