using System;

namespace Last_Try.Models
{
    public class TimeEntry
    {

        public int? Id { get; set; }
       // public DateTime? Date { get; set; }
        public TimeSpan? Hours { get; set; }
        public bool Approved { get; set; }
        public string? UserId { get; set; }

        public TimeSpan? TimeIn { get; set; } 
        public TimeSpan? Time_Out { get; set; }
        public DayOfWeek? Day { get; set; }

        public TimeSpan? TimeWorked
        { get
            {
                if (TimeIn != null && Time_Out != null)

                {
                    return (TimeSpan)(Time_Out.Value - TimeIn.Value);
                }
                else
                    return null;
            }
        }

        //public IList<TimeEntry> TimeEntries { get; set; }

        //public TimeEntry()
        //{
        //    Date = new List<DateTime>();
        //}
        public List<DateTime>? Date { get; set; }
}

}
