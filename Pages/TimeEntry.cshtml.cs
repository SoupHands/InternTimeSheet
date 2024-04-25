using Last_Try.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Last_Try.Data;
using Last_Try.Controllers;
using Last_Try;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Last_Try.Pages

{
    

   
    public class TimeEntryModel(ApplicationDbContext context) : PageModel
    {
     
        public List<DateTime> WeekDates { get; set; } = new List<DateTime>();
 
       
       public TimeEntryModel TimeEntry { get; set; }


        [BindProperty]

        public TimeEntry[] TimeEntries { get; set; } = new TimeEntry[7];
        public TimeSpan TimeOut { get; private set; }
        public TimeSpan TimeIn { get; private set; }

        private readonly ApplicationDbContext _context = context;
        public List<TimeEntry> GetTimeEntries()
        {
            return _context.TimeEntries.ToList();
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

        public async Task<IActionResult> OnPostAsync(List<TimeEntry> times )
        {
            //TimeSpan TimeIn = TimeEntry.TimeIn;
            //TimeSpan TimeOut = TimeEntry.TimeOut;
            var TimeEntries = new TimeEntry
            {
                Day = DateTime.Today.DayOfWeek,
                TimeIn = TimeIn,
                Time_Out = TimeOut
            
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

             if (TimeEntry != null)
            {
                var Date = TimeEntry.WeekDates;
                
            }

            List<TimeEntry> timeEntries = GetTimeEntries();

            foreach (var time in times)
            {
                _context.TimeEntries.Add(time);
            }


    

           


            await _context.SaveChangesAsync();

            return RedirectToPage("./Success");

        }
    }

}