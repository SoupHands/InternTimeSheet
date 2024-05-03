using Last_Try.Models;
using Last_Try.Pages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using Last_Try;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

namespace Last_Try.Data
{
    public class TimeDbContext : IdentityDbContext
    {
        public TimeDbContext()
        {
        }

        public TimeDbContext(DbContextOptions<TimeDbContext> options)
            : base(options)
        {
        }
        public DbSet<TimeEntry> TimeEntries { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Last_Try;Trusted_Connection=Yes;Integrated Security=True;TrustServerCertificate=True;");
            optionsBuilder.EnableSensitiveDataLogging();
            
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<TimeEntry>().ToTable("TimeEntries");

        //    builder.Entity<TimeEntry>().HasKey(e => e.Id);

        //}
    }

}
