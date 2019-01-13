using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ClassTimetableGridViewModel
    {
        public string TimeRange { get; set; }
        public ClassTimetableGridItemViewModel MondayClass { get; set; }
        public ClassTimetableGridItemViewModel TuesdayClass { get; set; }
        public ClassTimetableGridItemViewModel WednesdayClass { get; set; }
        public ClassTimetableGridItemViewModel ThursdayClass { get; set; }
        public ClassTimetableGridItemViewModel FridayClass { get; set; }
        public ClassTimetableGridItemViewModel SaturdayClass { get; set; }
        public ClassTimetableGridItemViewModel SundayClass { get; set; }

        // create new grid item view model to replace the ClassTimetables from above
        // ClassTimetableGridItemViewModel should have name, string with start/end times, Id of class type and colour
    }
}