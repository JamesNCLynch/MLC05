using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ClassTimetableGridItemViewModel
    {
        public string ScheduledClassTypeId { get; set; }
        public string ClassName { get; set; }
        public string TimeSpan { get; set; }
        public string ScheduledClassColour { get; set; }
        public DayOfWeek Weekday { get; set; }
        public int StartHour { get; set; }
    }
}