using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ScheduledClass
    {
        public string Id { get; set; }
        public virtual ScheduledClassType ScheduledClassType  { get; set; }
        public virtual ICollection<ClassAttendee> ClassAttendees { get; set; }
        public virtual ApplicationUser InstructorId { get; set; }
    }
}