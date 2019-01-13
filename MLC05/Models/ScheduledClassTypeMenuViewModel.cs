using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ScheduledClassTypeMenuViewModel
    {
        private static readonly ApplicationDbContext Db  = new ApplicationDbContext();
        
        public static List<ScheduledClassType> GetMenu()
        {
            return Db.ScheduledClassTypes.Where(x => x.IsActive).ToList();
        }
    }
}