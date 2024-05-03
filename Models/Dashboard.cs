using Last_Try.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Last_Try.Models
{
    [Authorize(Roles = "Intern")]
    public class DashboardModel : PageModel
    {
        private readonly TimeDbContext _context;

        public DashboardModel(TimeDbContext context)
        {
            _context = context;
        }

        public IList<TimeEntry> TimeEntries { get; set; }

        //public async Task OnGetAsync()
        //{
        //    TimeEntries = await _context.TimeEntries
        //        .Where(e =>
        //        {
        //            return e.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        })
        //        .ToListAsync();
        //}

        public async Task<IActionResult> OnPostSubmitTimeEntryAsync(List<DateTime> date, String hours)
        {
            var timeEntry = new TimeEntry
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Date = date,
                Hours = hours,
                Approved = false
            };

            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Dashboard");
        }
    }
}
