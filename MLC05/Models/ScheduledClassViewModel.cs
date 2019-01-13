using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MLC05.Helpers;

namespace MLC05.Models
{
    public class ScheduledClassViewModel
    {
        public ScheduledClassViewModel()
        {
        }

        public ScheduledClassType ScheduledClassType { get; set; }
        public DateTime StartDateTime { get; set; }
        public IList<ApplicationUser> InstructorList{ get; set; }

        public IList<SelectListItem> InstructorListSelectListItems
        {
            get
            {
                return InstructorList.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id }).ToList();
            }
            set { }
        }
        public string InstructorId { get; set; }
    }
}