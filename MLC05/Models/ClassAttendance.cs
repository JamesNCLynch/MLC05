using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ClassAttendance
    {
        public string Id { get; set; }
        public DateTime EnrolledDate { get; set; }
        public virtual ApplicationUser Attendee { get; set; }
        public virtual ScheduledClass ScheduledClasses { get; set; }
        public bool NoShow { get; set; }
    }
}