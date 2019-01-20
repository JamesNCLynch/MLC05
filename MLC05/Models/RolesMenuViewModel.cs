using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MLC05.Models
{
    
    public class RolesMenuViewModel
    {
        private static readonly ApplicationDbContext context = new ApplicationDbContext();

        public static List<IdentityRole> GetRoles()
        {
            return context.Roles.ToList();
        }
    }
}