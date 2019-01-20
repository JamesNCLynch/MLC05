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
    [Authorize(Roles = RoleNameHelper.AdminName)]
    public class HolidayController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Holiday
        public async Task<ActionResult> Index()
        {
            return View(await db.Holiday.ToListAsync());
        }

        // GET: Holiday/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = await db.Holiday.FindAsync(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // GET: Holiday/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holiday/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Holidays holidays)
        {
            if (ModelState.IsValid)
            {
                holidays.Id = Guid.NewGuid().ToString();

                db.Holiday.Add(holidays);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(holidays);
        }

        // GET: Holiday/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = await db.Holiday.FindAsync(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // POST: Holiday/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,HolidayDate")] Holidays holidays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holidays).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(holidays);
        }

        // GET: Holiday/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = await db.Holiday.FindAsync(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // POST: Holiday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Holidays holidays = await db.Holiday.FindAsync(id);
            db.Holiday.Remove(holidays);
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
