using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class UpcomingClassesViewModel
    {
        private static readonly ApplicationDbContext context = new ApplicationDbContext();
        private readonly string UserId;

        public UpcomingClassesViewModel(string userId)
        {
            UserId = userId;
        }

        public static List<ScheduledClass> GetUpcomingScheduledClasses()
        {
            return context.ScheduledClasses
                .Include("ScheduledClassType")
                .Where(x => x.ClassStartTime > DateTime.UtcNow)
                .OrderBy(t => t.ClassStartTime).ToList();
        }

        public static List<ScheduledClass> GetUpcomingScheduledClasses(IEnumerable<ClassAttendance> classAttendances)
        {
            return context.ScheduledClasses
                .Include("ScheduledClassType")
                .Where(x => x.ClassStartTime > DateTime.UtcNow && !classAttendances.Select(ca => ca.ScheduledClasses).Contains(x))
                .OrderBy(t => t.ClassStartTime).ToList();
        }
    }
}