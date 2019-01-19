using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MLC05.Models
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {

        }
        public EditUserViewModel(ApplicationUser applicationUser)
        {
            UserId = applicationUser.Id;
            UserName = applicationUser.UserName;
            Email = applicationUser.Email;
            PhoneNumber = applicationUser.PhoneNumber;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            RoleId = applicationUser.Roles.FirstOrDefault().RoleId;
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleId { get; set; }
    }
}