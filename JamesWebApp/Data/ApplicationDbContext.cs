using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JamesWebApp.Models;

namespace JamesWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JamesWebApp.Models.TimeClock> TimeClock { get; set; }
        public DbSet<JamesWebApp.Models.TimeOff> TimeOff { get; set; }
    }
}