using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JamesWebApp.Models;

namespace JamesWebApp.Data
{
    public class RandomContext : DbContext
    {
        public RandomContext (DbContextOptions<RandomContext> options)
            : base(options)
        {
        }

        public DbSet<JamesWebApp.Models.AspNetUsers> AspNetUsers { get; set; } = default!;
    }
}
