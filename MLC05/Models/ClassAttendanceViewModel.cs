using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ClassAttendanceViewModel
    {
        public string ScheduledClassId { get; set; }
        public DateTime ClassStartTime { get; set; }
        public string ClassTypeName { get; set; }
    }
}