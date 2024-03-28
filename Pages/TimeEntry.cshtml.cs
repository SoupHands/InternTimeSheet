using Last_Try.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Last_Try.Data;
using Last_Try.Controllers;
using Last_Try;

namespace Last_Try.Pages

{
    public class TimeEntryModel : PageModel
    {
        [BindProperty]
        public TimeEntry[] TimeEntries { get; set; } = new TimeEntry[7];
        private readonly ApplicationDbContext _context;

        public TimeEntryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            TimeEntries = new TimeEntry[7];
            for (int i = 0; i < 7; i++)
            {
                TimeEntries[i] = new TimeEntry
                {
                    Day = (DayOfWeek)i,
                    TimeIn = TimeSpan.Zero,
                    TimeOut = TimeSpan.Zero
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var timeEntry in TimeEntries)
            {
                _context.TimeEntries.Add(timeEntry);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Success");

        }
    }

}