using Last_Try.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Last_Try.Data;
using Last_Try.Controllers;
using Last_Try;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Last_Try.Pages

{



    public class TimeEntryModel(TimeDbContext context) : PageModel
    {

        public string UserId { get; set; } = "Tony";
        public List<DateTime> Date { get; set; } = new List<DateTime>();

        public String Id { get; set; } = "Tony";
       public TimeEntryModel? TimeEntry { get; set; }

        
        [BindProperty]

        public TimeEntry[] TimeEntries { get; set; } = new TimeEntry[7];
        public String? TimeOut { get; set; }
        public String? TimeIn { get; set; }

        private readonly TimeDbContext _context = context;
       // public DateTime date;


        public List<TimeEntry> GetTimeEntries()
        {
            return _context.TimeEntries.ToList();
        }

        public void OnGet()
        {
            Date = new List<DateTime>();
            DateTime today = DateTime.Today;
            DateTime dayOfWeek = today;
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek+1);


            for (int i = 0; i < 7; i++)
            {
                Date.Add(startOfWeek.AddDays(i));
            }
            TimeEntries = new TimeEntry[7];
            
        }



        public async Task<IActionResult> OnPostAsync(List<TimeEntry> times)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var time in times)
            {
                var existingEntry = _context.TimeEntries.Local.FirstOrDefault(e => e.Id == time.Id);
                if (existingEntry != null)
                {
                    _context.Entry(existingEntry).CurrentValues.SetValues(time);
                    _context.Entry(existingEntry).State = EntityState.Modified;
                }
                else
                {
                    _context.TimeEntries.Add(time);
                }
            }



            foreach (var time in times)
            {
             
                _context.TimeEntries.Add(time);
            }

           _context.TimeEntries.AddRange(TimeEntries);

            await _context.SaveChangesAsync();


            if (!ModelState.IsValid)
            {
                return Page();
            }
   

            if (TimeEntry != null)
            {
                        _ = TimeEntry.Date;
            }

            List<TimeEntry> timeEntries = GetTimeEntries();


     
    
           

            // _context.TimeEntries.AddRange(times);


            return RedirectToPage("./Success");

        }
    }

}