using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ClassAttendee
    {
        public int Id { get; set; }
        public virtual ICollection<ApplicationUser> User { get; set; }
        public virtual ICollection<ScheduledClass> ScheduledClasses { get; set; }
    }
}