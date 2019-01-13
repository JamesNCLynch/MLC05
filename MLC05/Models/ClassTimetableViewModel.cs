using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MLC05.Models
{
    public class ClassTimetableViewModel
    {
        public IList<ScheduledClassType> ScheduledClassType { get; set; }

        public IList<SelectListItem> ScheduledClassTypeSelectListItems
        {
            get { return ScheduledClassType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToList(); }
            set { }
        }
        public string ScheduledClassId { get; set; }

        public int StartTimeHours { get; set; }
        public int StartTimeMinutes { get; set; }

        public int EndTimeHours { get; set; }
        public int EndTimeMinutes { get; set; }

        public DayOfWeek Weekday { get; set; }
    }
}