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
    

   
    public class TimeEntryModel(TimeDbContext context) : PageModel
    {

        public string UserId { get; set; } = "Tony";
        public List<DateTime> Date { get; set; } = new List<DateTime>();
 
       
       public TimeEntryModel TimeEntry { get; set; }


        [BindProperty]

        public TimeEntry[] TimeEntries { get; set; } = new TimeEntry[7];
        public TimeSpan TimeOut { get; private set; }
        public TimeSpan TimeIn { get; private set; }

        private readonly TimeDbContext _context = context;
        public List<TimeEntry> GetTimeEntries()
        {
            return _context.TimeEntries.ToList();
        }

        public void OnGet()
        {
            Date = new List<DateTime>();
            DateTime today = DateTime.Today;
            int dayOfWeek = (int)today.DayOfWeek;
            DateTime startOfWeek = today.AddDays(-dayOfWeek);

            for (int i = 0; i < 7; i++)
            {
                Date.Add(startOfWeek.AddDays(i));
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
                Time_Out = TimeOut,
                
            
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            
                
              _context.TimeEntries.AddRange(TimeEntries);
                

                if (TimeEntry != null)
            {
                var Date = TimeEntry.Date;
                
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