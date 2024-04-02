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
        public List<DateTime> WeekDates { get; set; }
 
        [BindProperty]
        public TimeEntry[] TimeEntries { get; set; } = new TimeEntry[7];
        private readonly ApplicationDbContext _context;

        public TimeEntryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            WeekDates = new List<DateTime>();
            DateTime today = DateTime.Today;
            int dayOfWeek = (int)today.DayOfWeek;
            DateTime startOfWeek = today.AddDays(-dayOfWeek);

            for (int i = 0; i < 7; i++)
            {
                WeekDates.Add(startOfWeek.AddDays(i));
            }
            TimeEntries = new TimeEntry[7];
            
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