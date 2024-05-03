using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Last_Try.Models;
using Last_Try.Pages;
using System;
using Last_Try.Data;
using EllipticCurve;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Last_Try
{
    public class DashboardModel : PageModel
    {
        private readonly TimeDbContext _context;

        public DashboardModel(TimeDbContext context)
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
            DateTime startOfWeek = today.AddDays(-dayOfWeek + 1);

            for (int i = 0; i < 7; i++)
            {
                DaysOfWeek.Add(startOfWeek.AddDays(i).DayOfWeek);

            }
            

            using (var context = new TimeDbContext())
            {
                foreach (var days in DaysOfWeek)
                {
                    var TimeEntryForDay = context.TimeEntries
                        .Where(te => te.TimeIn.Length>0)
                        .ToList();
                }
            }

                ViewData["Title"] = "Your Time Entries";
        }


    }}



