using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ScheduledClass
    {
        public string Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClassStartTime { get; set; }
        public virtual ScheduledClassType ScheduledClassType  { get; set; }
        public virtual ICollection<ClassAttendance> ClassAttendances { get; set; }
        [Required]
        public virtual ApplicationUser Instructor { get; set; }
        public bool IsCancelled { get; set; }
    }
}