using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ClassAttendanceDetailsViewModel
    {
        public static readonly ApplicationDbContext context = new ApplicationDbContext();

        public static ScheduledClassType GetClassTypeDetails(string classTypeId)
        {
            return context.ScheduledClassTypes.FirstOrDefault(x => x.Id == classTypeId);
        }
    }
}