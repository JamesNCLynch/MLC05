using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MLC05.Models;
using MLC05.Helpers;
using Microsoft.AspNet.Identity;

namespace MLC05.Controllers
{
    public class ClassAttendanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassAttendance
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole(RoleNameHelper.AdminName))
            {
                var retValue = await db.ClassAttendances
                    .Include("Attendee")
                    .Include("ScheduledClasses")
                    .ToListAsync();
                return View(retValue);
            }
            else if (User.IsInRole(RoleNameHelper.InstructorName))
            {
                var retValue = await db.ClassAttendances
                    .Include("ScheduledClasses")
                    .Where(x => x.ScheduledClasses.Instructor.UserName == User.Identity.Name)
                    .ToListAsync();
                return View(retValue);
            }
            else if (User.IsInRole(RoleNameHelper.AttendeeName))
            {
                var retValue = await db.ClassAttendances
                    .Include("Attendee")
                    .Include("ScheduledClasses")
                    .Where(x => x.Attendee.UserName == User.Identity.Name)
                    .ToListAsync();
                return View(retValue);
            }

            return View(await db.ClassAttendances.ToListAsync());
        }

        // GET: ClassAttendance/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassAttendance classAttendance = await db.ClassAttendances.FindAsync(id);
            if (classAttendance == null)
            {
                return HttpNotFound();
            }
            return View(classAttendance);
        }

        // GET: ClassAttendance/Create
        [Authorize(Roles = RoleNameHelper.AttendeeName)]
        public ActionResult Create(string classId)
        {
            if (classId == null) {
                return HttpNotFound();
            }

            var scheduledClass = db.ScheduledClasses.Include("ScheduledClassType").FirstOrDefault(x => x.Id == classId);
            if (scheduledClass == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ClassAttendanceViewModel()
            {
                ScheduledClassId = scheduledClass.Id,
                ClassStartTime = scheduledClass.ClassStartTime,
                ClassTypeName = scheduledClass.ScheduledClassType.Name
            };

            return View(viewModel);
        }

        // POST: ClassAttendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNameHelper.AttendeeName)]
        public async Task<ActionResult> Create(ClassAttendanceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
                var scheduledClass = await db.ScheduledClasses.FirstOrDefaultAsync(x => x.Id == viewModel.ScheduledClassId);

                var classAttendance = new ClassAttendance()
                {
                    Id = Guid.NewGuid().ToString(),
                    EnrolledDate = DateTime.UtcNow,
                    ScheduledClasses = scheduledClass,
                    NoShow = false,
                    Attendee = user
                };

                db.ClassAttendances.Add(classAttendance);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: ClassAttendance/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassAttendance classAttendance = await db.ClassAttendances.FindAsync(id);
            if (classAttendance == null)
            {
                return HttpNotFound();
            }
            return View(classAttendance);
        }

        // POST: ClassAttendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EnrolledDate,NoShow")] ClassAttendance classAttendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classAttendance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(classAttendance);
        }

        // GET: ClassAttendance/Delete/5
        [Authorize(Roles = RoleNameHelper.AttendeeName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassAttendance classAttendance = await db.ClassAttendances.FindAsync(id);
            if (classAttendance == null)
            {
                return HttpNotFound();
            }
            return View(classAttendance);
        }

        // POST: ClassAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNameHelper.AttendeeName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ClassAttendance classAttendance = await db.ClassAttendances.FindAsync(id);
            db.ClassAttendances.Remove(classAttendance);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
