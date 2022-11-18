using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesWebApp.Data;
using JamesWebApp.Models;

namespace JamesWebApp.Controllers
{
    public class TimeOffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeOffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.TimeOff.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeOff == null)
            {
                return NotFound();
            }

            var timeOff = await _context.TimeOff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeOff == null)
            {
                return NotFound();
            }

            return View(timeOff);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PaidTimeOff,DateOne,DateTwo,Vacation")] TimeOff timeOff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(timeOff);
                    if (timeOff.DateTwo.HasValue)
                    {
                        timeOff.Vacation = timeOff.DateTwo.Value.Subtract(timeOff.DateOne).Days;
                        timeOff.PaidTimeOff = timeOff.PaidTimeOff - timeOff.Vacation;
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(timeOff);
            }
            catch (Exception Error)
            {
                var logger = new ErrorLog();
                logger.ErrorLogger(Error);
            }
           
            return View(timeOff);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimeOff == null)
            {
                return NotFound();
            }

            var timeOff = await _context.TimeOff.FindAsync(id);
            if (timeOff == null)
            {
                return NotFound();
            }
            return View(timeOff);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,PaidTimeOff,DateOne,DateTwo,Vacation")] TimeOff timeOff)
        {
            try
            {
                if (id != timeOff.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(timeOff);
                        if (timeOff.DateTwo.HasValue)
                        {
                            //Gives us the number of days the user will be taking off
                            timeOff.Vacation = timeOff.DateTwo.Value.Subtract(timeOff.DateOne).Days;
                            timeOff.PaidTimeOff = timeOff.PaidTimeOff - timeOff.Vacation;
                        }
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TimeOffExists(timeOff.Id))
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
            }
            catch (Exception Error)
            {
                var logger = new ErrorLog();
                logger.ErrorLogger(Error);
            }
           
            return View(timeOff);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeOff == null)
            {
                return NotFound();
            }

            var timeOff = await _context.TimeOff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeOff == null)
            {
                return NotFound();
            }
            return View(timeOff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeOff == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TimeOff'  is null.");
            }
            var timeOff = await _context.TimeOff.FindAsync(id);
            if (timeOff != null)
            {
                _context.TimeOff.Remove(timeOff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeOffExists(int id)
        {
          return _context.TimeOff.Any(e => e.Id == id);
        }
    }
}
