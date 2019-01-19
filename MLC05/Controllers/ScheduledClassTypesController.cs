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

namespace MLC05.Controllers
{
    public class ScheduledClassTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScheduledClassTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ScheduledClassTypes.ToListAsync());
        }

        // GET: ScheduledClassTypes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClassType scheduledClassType = await db.ScheduledClassTypes.FindAsync(id);
            if (scheduledClassType == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClassType);
        }

        [Authorize(Roles = RoleNameHelper.AdminName)]
        // GET: ScheduledClassTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = RoleNameHelper.AdminName)]
        // POST: ScheduledClassTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ScheduledClassType scheduledClassType)
        {
            if (ModelState.IsValid)
            {
                scheduledClassType.Id = Guid.NewGuid().ToString();
                db.ScheduledClassTypes.Add(scheduledClassType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(scheduledClassType);
        }

        [Authorize(Roles = RoleNameHelper.AdminName)]
        // GET: ScheduledClassTypes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClassType scheduledClassType = await db.ScheduledClassTypes.FindAsync(id);
            if (scheduledClassType == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClassType);
        }

        [Authorize(Roles = RoleNameHelper.AdminName)]
        // POST: ScheduledClassTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ScheduledClassType scheduledClassType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduledClassType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(scheduledClassType);
        }

        [Authorize(Roles = RoleNameHelper.AdminName)]
        // GET: ScheduledClassTypes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClassType scheduledClassType = await db.ScheduledClassTypes.FindAsync(id);
            if (scheduledClassType == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClassType);
        }

        [Authorize(Roles = RoleNameHelper.AdminName)]
        // POST: ScheduledClassTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ScheduledClassType scheduledClassType = await db.ScheduledClassTypes.FindAsync(id);
            db.ScheduledClassTypes.Remove(scheduledClassType);
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
