using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Last_Try.Models;
using Last_Try.Pages;
using System;
using Last_Try.Data;
using EllipticCurve;


namespace Last_Try
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public List<DayOfWeek> DaysOfWeek { get; set; }

        public List<TimeEntry> TimeEntries { get; set; }

        public void OnGet()
        {
            DaysOfWeek = new List<DayOfWeek>();
            DateTime today = DateTime.Today;
            int dayOfWeek = (int)today.DayOfWeek;
            DateTime startOfWeek = today.AddDays(-dayOfWeek);

            for (int i = 0; i < 7; i++)
            {
                DaysOfWeek.Add(startOfWeek.AddDays(i).DayOfWeek);
            }

            
               TimeEntries = new List<TimeEntry>();
               foreach (var day in DaysOfWeek)
            {
                TimeEntries.Add(new TimeEntry
                {
                    Day = day,
                    TimeIn = TimeSpan.FromHours(8.25),
                    TimeOut = TimeSpan.FromHours(12), 
                    Approved = true
                });
            }
        

                ViewData["Title"] = "Your Time Entries";
        }


    }


}
