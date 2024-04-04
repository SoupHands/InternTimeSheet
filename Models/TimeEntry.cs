using System;

namespace Last_Try.Models
{
    public class TimeEntry
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public bool Approved { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public DayOfWeek Day { get; set; }

        public TimeSpan TimeWorked => TimeOut - TimeIn;

        public IList<TimeEntry> TimeEntries { get; set; }

    }

}
