using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace Last_Try.Models
{
    public class TimeEntry
    {
        [Key]
        [Required]
        public String Id { get; set; } = "Tony";
     
        public bool Approved { get; set; }
        public string? UserId { get; set; }

        public String? TimeIn { get; set; } 
        public String? Time_Out { get; set; }
        public DateTime? Day { get; set; }


        public string? Hours
        {
            get
            {
                if (!string.IsNullOrEmpty(TimeIn) && !string.IsNullOrEmpty(Time_Out))
                {
                    if (TimeSpan.TryParse(TimeIn, out TimeSpan timeIn) && TimeSpan.TryParse(Time_Out, out TimeSpan timeOut))
                    {
                        TimeSpan difference = timeOut - timeIn;

                        return difference.ToString(@"hh\:mm");
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (TimeSpan.TryParse(value, out TimeSpan timeIn) && TimeSpan.TryParse(value, out TimeSpan timeOut))
                    {
                        TimeIn = timeIn.ToString();
                        Time_Out = timeOut.ToString();
                    }
                    else
                    {
                        TimeIn = null;
                        Time_Out = null;
                    }
                }
                else
                {
                    TimeIn = null;
                    Time_Out = null;
                }
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
