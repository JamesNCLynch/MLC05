using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MLC05.Models;
using MLC05.Helpers;
using NLog;
using NLog.Fluent;

namespace MLC05.Controllers
{
    public class ScheduledClassController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        // GET: ScheduledClass
        [Authorize(Roles = RoleNameHelper.AdminName)]
        public async Task<ActionResult> Index()
        {
            var now = DateTime.UtcNow;

            var timetabledClasses = await _db.ClassTimetable.Include("ScheduledClassType").ToListAsync();
            var scheduledClasses = await _db.ScheduledClasses.ToListAsync();
            var upcomingScheduledClassesRequiringCreation = new List<ScheduledClass>();

            for (var dayOffset = 1; dayOffset < 7; dayOffset++)
            {
                for (var hour = 7; hour < 22; hour++)
                {
                    if (!scheduledClasses.Any(sc => sc.ClassStartTime.Hour == hour && sc.ClassStartTime.Day == now.AddDays(dayOffset).Day))
                    {
                        var timetableSlotTime = new DateTime(now.Year, now.Month, now.Day, hour, 0, 0).AddDays(dayOffset);
                        var timetabledClass = timetabledClasses.FirstOrDefault(tc => tc.Weekday == timetableSlotTime.DayOfWeek && tc.StartTime.Hour == timetableSlotTime.Hour);
                        if (timetabledClass != null)
                        {
                            upcomingScheduledClassesRequiringCreation.Add(new ScheduledClass()
                            {
                                ScheduledClassType = timetabledClass.ScheduledClassType,
                                ClassStartTime = new DateTime(now.Year, now.Month, now.Day, timetabledClass.StartTime.Hour, timetabledClass.StartTime.Minute, 0).AddDays(dayOffset)
                            });
                        }
                    }
                }
            }

            // does not exclude when a scheduled class exists in the timetable slot

            // also need to create instructor accounts

            return View(upcomingScheduledClassesRequiringCreation.OrderBy(x => x.ClassStartTime).ToList());
        }

        // GET: ScheduledClass/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClass scheduledClass = await _db.ScheduledClasses.FindAsync(id);
            if (scheduledClass == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClass);
        }

        // GET: ScheduledClass/Create
        public ActionResult Create(string type, DateTime start)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            var instructorRoleId = _db.Roles.FirstOrDefault(r => r.Name == RoleNameHelper.AdminName)?.Id;
            var instructors = userManager.Users.Where(x => x.Roles.Any(s => s.RoleId == instructorRoleId)).ToList();

            var classType = _db.ScheduledClassTypes.FirstOrDefault(x => x.Name == type);

            var vm = new ScheduledClassViewModel
            {
                InstructorList = instructors,
                StartDateTime = start,
                ScheduledClassType = classType
            };

            return View(vm);
        }

        // POST: ScheduledClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = RoleNameHelper.AdminName)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ScheduledClassViewModel scvm)
        {
            
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
                var scheduledClass = new ScheduledClass
                {
                    Id = Guid.NewGuid().ToString(),
                    Instructor = userManager.Users.FirstOrDefault(u => u.Id == scvm.InstructorId),
                    ClassStartTime = scvm.StartDateTime,
                    ScheduledClassType = _db.ScheduledClassTypes.FirstOrDefault(x => x.Name == scvm.ScheduledClassType.Name)
                };

                try
                {
                    _db.ScheduledClasses.Add(scheduledClass);
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }

                
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: ScheduledClass/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClass scheduledClass = await _db.ScheduledClasses.FindAsync(id);
            if (scheduledClass == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClass);
        }

        // POST: ScheduledClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ClassStartTime")] ScheduledClass scheduledClass)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(scheduledClass).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(scheduledClass);
        }

        // GET: ScheduledClass/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClass scheduledClass = await _db.ScheduledClasses.FindAsync(id);
            if (scheduledClass == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClass);
        }

        // POST: ScheduledClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ScheduledClass scheduledClass = await _db.ScheduledClasses.FindAsync(id);
            _db.ScheduledClasses.Remove(scheduledClass);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
