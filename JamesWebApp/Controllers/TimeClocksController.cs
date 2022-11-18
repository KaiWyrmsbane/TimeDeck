using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesWebApp.Data;
using JamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace JamesWebApp.Controllers
{
    public class TimeClocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeClocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
              return View(await _context.TimeClock.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeClock == null)
            {
                return NotFound();
            }

            var timeClock = await _context.TimeClock
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeClock == null)
            {
                return NotFound();
            }
            
            return View(timeClock);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,StartTime,EndTime,TimeWorked")] TimeClock timeClock)
        {
            try
            {
                //throw new Exception("Error");
                if (ModelState.IsValid)
                {
                    _context.Add(timeClock);
                    if (timeClock.EndTime.HasValue)
                    {
                        //calculates the difference between the startime and endtime
                        timeClock.TimeWorked = timeClock.EndTime.Value.Subtract(timeClock.StartTime);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch (Exception Error)
            {
                var logger = new ErrorLog();
                logger.ErrorLogger(Error);
                
            }
       
            return View(timeClock);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
          
            if (id == null || _context.TimeClock == null)
            {
                return NotFound();
            }

            var timeClock = await _context.TimeClock.FindAsync(id);
            if (timeClock == null)
            {
                return NotFound();
            }
            return View(timeClock);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,StartTime,EndTime,TimeWorked")] TimeClock timeClock)
        {
            try
            {
                if (id != timeClock.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(timeClock);
                        if (timeClock.EndTime.HasValue)
                        {
                            timeClock.TimeWorked = timeClock.EndTime.Value.Subtract(timeClock.StartTime);
                        }
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TimeClockExists(timeClock.Id))
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
            catch(Exception Error)
            {
                var logger = new ErrorLog();
                logger.ErrorLogger(Error);
            }
        
            
            return View(timeClock);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeClock == null)
            {
                return NotFound();
            }

            var timeClock = await _context.TimeClock
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeClock == null)
            {
                return NotFound();
            }

            return View(timeClock);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeClock == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TimeClock'  is null.");
            }
            var timeClock = await _context.TimeClock.FindAsync(id);
            if (timeClock != null)
            {
                _context.TimeClock.Remove(timeClock);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        private bool TimeClockExists(int id)
        {
          return _context.TimeClock.Any(e => e.Id == id);
        }
    }
}
