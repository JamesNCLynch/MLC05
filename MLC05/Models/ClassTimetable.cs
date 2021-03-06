﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLC05.Models
{
    public class ClassTimetable
    {
        public string Id { get; set; }
        public ScheduledClassType ScheduledClassType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek Weekday { get; set; }
    }
}