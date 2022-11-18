using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JamesWebApp.Data;
using JamesWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace JamesWebApp.Controllers
{
    //------- 1 --------
    //My Controllers use the "Dependency Inversion Principle" of Solid principles
    //I'm injecting my ApplicationDbContext into the controllers themselves
    //Having my controllers deal with the rules or logic of the system
    //while the CRUD methods deal with the finer detailed operations.
    //------- 2 --------
    //My Controllers, CRUD methods, and ErrorLog class use the "Single Responsibility Principle" of Solid principles
    //Each have their own unique job that no other method, or class replicates that job across the entire Web App
    
    public class AspNetUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AspNetUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var IdentityUsers =await _context.Users.ToListAsync();
            //Created a list
            var Aspnetusers = new List<AspNetUsers>();
            foreach (var user in IdentityUsers)
            {
                Aspnetusers.Add(ConvertUser(user));
            }
            return View(Aspnetusers);
        }

        private static AspNetUsers ConvertUser(IdentityUser user)
        {
            return new AspNetUsers
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NormalizedUserName = user.NormalizedUserName,
                NormalizedEmail = user.NormalizedEmail,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                ConcurrencyStamp = user.ConcurrencyStamp,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                AccessFailedCount = user.AccessFailedCount,
            };
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(ConvertUser(user));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] IdentityUser aspNetUsers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(aspNetUsers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            
            catch (Exception Error)
            {
                var logger = new ErrorLog();
                logger.ErrorLogger(Error);
            }
            return View(aspNetUsers);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var Users = await _context.Users.FindAsync(id);
            if (Users == null)
            {
                return NotFound();
            }
            return View(ConvertUser(Users));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] IdentityUser aspNetUsers)
        {
            try
            {
                if (id != aspNetUsers.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {

                        ConvertUser(aspNetUsers);
                        _context.Update(aspNetUsers);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AspNetUsersExists(aspNetUsers.Id))
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
            
            return View(aspNetUsers);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(ConvertUser(user));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'RandomContext.AspNetUsers'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user!= null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}
