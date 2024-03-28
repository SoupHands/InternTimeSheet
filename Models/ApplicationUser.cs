using Microsoft.AspNetCore.Identity;
using System;

namespace Last_Try.Models
{
    public class ApplicationUser : IdentityUser
    {
        public class TimeEntry
        {
            public static implicit operator TimeEntry(Models.TimeEntry v)
            {
                throw new NotImplementedException();
            }
        }
    }
}
