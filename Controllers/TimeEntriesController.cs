using Microsoft.AspNetCore.Mvc;
using Last_Try.Data;
using Last_Try.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Last_Try.Pages;


namespace Last_Try.Controllers
{
    public class TimeEntriesController : Controller
    {
        private readonly TimeDbContext _context;

        public List<DateTime> WeekDates()
        {
            var dates = new List<DateTime>();
            DateTime today = DateTime.Today;
            int dayOfWeek = (int)today.DayOfWeek;
            DateTime startOfWeek = today.AddDays(-dayOfWeek);

            for (int i = 0; i < 7; i++)
            {
                dates.Add(startOfWeek.AddDays(i));
            }

            return dates;
        }
        [HttpPost]
        public IActionResult Submit(TimeEntryModel model)
        {
            if (ModelState.IsValid)
            {
                
            return RedirectToAction("Dashboard");
            }
            return View("TimeEntry",model);
        }
        //public IActionResult Dashboard()
        //{
        //    var timeWorkedData = GetTimeWorkedData();

        //    return View(timeWorkedData);
        //}


        public TimeEntriesController(TimeDbContext context)
        {  _context = context; }
        public IActionResult Index()
        {
            var timeEntries = _context.TimeEntries.ToList();
            return View(timeEntries);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Hours,Approved")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeEntry);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeEntry = await _context.TimeEntries.FindAsync(id);
            if (timeEntry == null)
            {
                return NotFound();
            }
            return View(timeEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Date,Hours,Approved")] TimeEntry timeEntry)
        {
            if (id != timeEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeEntryExists(timeEntry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(timeEntry);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeEntry == null)
            {
                return NotFound();
            }

            _context.TimeEntries.Remove(timeEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeEntryExists(String id)
        {
            return _context.TimeEntries.Any(e => e.Id == id);
        }

     
        }
    }
