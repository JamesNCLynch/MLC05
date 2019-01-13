using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace MLC05.Models
{
    public class ScheduledClassType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Colour ClassColour { get; set; }
        public enum Colour
        {
            Aqua,
            DeepPink,
            DeepSkyBlue,
            MediumOrchid,
            Lime,
            MidnightBlue,
            Navy,
            Plum,
            RebeccaPurple,
            Red,
            Salmon,
            SkyBlue,
            Thistle,
            Violet,
            YellowGreen
        }
    }
}