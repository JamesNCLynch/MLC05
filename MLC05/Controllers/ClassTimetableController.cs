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

namespace MLC05.Controllers
{
    public class ClassTimetableController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassTimetable
        public async Task<ActionResult> Index()
        {
            var timetabledClassesByDay = await db.ClassTimetable.Include("ScheduledClassType").ToListAsync();

            var gridItems = timetabledClassesByDay.Select(x => new ClassTimetableGridItemViewModel()
            {
                ScheduledClassTypeId = x.Id,
                ClassName = x.ScheduledClassType.Name,
                ScheduledClassColour = GetHtmlCodeFromColourName(x.ScheduledClassType.ClassColour),
                TimeSpan = $"{x.StartTime.ToShortTimeString()} - {x.EndTime.ToShortTimeString()}",
                Weekday = x.Weekday,
                StartHour = x.StartTime.Hour
            }).ToList();

            var classTimetableGridRows = Enumerable.Range(7, 15).Select(hour => new ClassTimetableGridViewModel
            {
                TimeRange = $"{new DateTime(2050, 1, 1, hour, 0, 0).ToShortTimeString()} - {new DateTime(2050, 1, 1, (hour + 1), 0, 0).ToShortTimeString()}",
                MondayClass = gridItems.FirstOrDefault(x => x.Weekday == DayOfWeek.Monday && x.StartHour == hour),
                TuesdayClass = gridItems.FirstOrDefault(x => x.Weekday == DayOfWeek.Tuesday && x.StartHour == hour),
                WednesdayClass = gridItems.FirstOrDefault(x => x.Weekday == DayOfWeek.Wednesday && x.StartHour == hour),
                ThursdayClass = gridItems.FirstOrDefault(x => x.Weekday == DayOfWeek.Thursday && x.StartHour == hour),
                FridayClass = gridItems.FirstOrDefault(x => x.Weekday == DayOfWeek.Friday && x.StartHour == hour),
                SaturdayClass = gridItems.FirstOrDefault(x => x.Weekday == DayOfWeek.Saturday && x.StartHour == hour),
                SundayClass = gridItems.FirstOrDefault(x => x.Weekday == DayOfWeek.Sunday && x.StartHour == hour)
            }).ToList();

            return View(classTimetableGridRows);
        }

        private string GetHtmlCodeFromColourName(ScheduledClassType.Colour classColour)
        {
            var retValue = "";
            switch (classColour)
            {
                case ScheduledClassType.Colour.Aqua:
                    retValue = "#00FFFF";
                    break;
                case ScheduledClassType.Colour.DeepPink:
                    retValue = "#FF1493";
                    break;
                case ScheduledClassType.Colour.DeepSkyBlue:
                    retValue = "#00BFFF";
                    break;
                case ScheduledClassType.Colour.MediumOrchid:
                    retValue = "#BA55D3 ";
                    break;
                case ScheduledClassType.Colour.Lime:
                    retValue = "#00FF00";
                    break;
                case ScheduledClassType.Colour.MidnightBlue:
                    retValue = "#191970";
                    break;
                case ScheduledClassType.Colour.Navy:
                    retValue = "#000080";
                    break;
                case ScheduledClassType.Colour.Plum:
                    retValue = "#DDA0DD";
                    break;
                case ScheduledClassType.Colour.RebeccaPurple:
                    retValue = "#663399";
                    break;
                case ScheduledClassType.Colour.Red:
                    retValue = "#FF0000";
                    break;
                case ScheduledClassType.Colour.Salmon:
                    retValue = "#FA8072";
                    break;
                case ScheduledClassType.Colour.SkyBlue:
                    retValue = "#87CEEB";
                    break;
                case ScheduledClassType.Colour.Thistle:
                    retValue = "#D8BFD8";
                    break;
                case ScheduledClassType.Colour.Violet:
                    retValue = "#EE82EE";
                    break;
                case ScheduledClassType.Colour.YellowGreen:
                    retValue = "#9ACD32";
                    break;
                default:
                    retValue = "#FFFFFF";
                    break;
            }

            return retValue;
        }

        // GET: ClassTimetable/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTimetable classTimetable = await db.ClassTimetable.FindAsync(id);
            if (classTimetable == null)
            {
                return HttpNotFound();
            }
            return View(classTimetable);
        }

//        // GET: ClassTimetable/Create
//        public ActionResult Create()
//        {
//            var classTypes = db.ScheduledClassTypes.Where(x => x.IsActive).ToList();
//            var viewModel = new ClassTimetableViewModel()
//            {
//                ScheduledClassType = classTypes
//            };
//            return View(viewModel);
//        }

        // GET: ClassTimetable/Create
        public ActionResult Create(int weekday, int startHour)
        {
            var classTypes = db.ScheduledClassTypes.Where(x => x.IsActive).ToList();
            var selectedWeekday = (DayOfWeek)weekday;
            var viewModel = new ClassTimetableViewModel()
            {
                ScheduledClassType = classTypes,
                StartTimeHours = startHour,
                EndTimeHours = startHour + 1,
                Weekday = selectedWeekday
            };

            // Weekday drop down is not defaulting to the correct value - works

            // properly parse the starthour from Index.cshtml

            return View(viewModel);
        }

        // POST: ClassTimetable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassTimetableViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var classTimetable = new ClassTimetable();
                classTimetable.Id = Guid.NewGuid().ToString();
                classTimetable.ScheduledClassType = db.ScheduledClassTypes.FirstOrDefault(x => x.Id == viewModel.ScheduledClassId);
                classTimetable.StartTime = new DateTime(2050, 1, 1, viewModel.StartTimeHours, viewModel.StartTimeMinutes, 0);
                classTimetable.EndTime = new DateTime(2050, 1, 1, viewModel.EndTimeHours, viewModel.EndTimeMinutes, 0);
                classTimetable.Weekday = viewModel.Weekday;
                db.ClassTimetable.Add(classTimetable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: ClassTimetable/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTimetable classTimetable = await db.ClassTimetable.FindAsync(id);
            if (classTimetable == null)
            {
                return HttpNotFound();
            }
            return View(classTimetable);
        }

        // POST: ClassTimetable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StartTime,EndTime,Weekday")] ClassTimetable classTimetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classTimetable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(classTimetable);
        }

        // GET: ClassTimetable/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTimetable classTimetable = await db.ClassTimetable.FindAsync(id);
            if (classTimetable == null)
            {
                return HttpNotFound();
            }
            return View(classTimetable);
        }

        // POST: ClassTimetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var classTimetable = await db.ClassTimetable.FindAsync(id);
            var scheduledClasses = await db.ScheduledClasses
                .Where(x => x.ScheduledClassType == classTimetable.ScheduledClassType && x.ClassStartTime.Hour == classTimetable.StartTime.Hour).ToListAsync();
            db.ScheduledClasses.RemoveRange(scheduledClasses);
            db.ClassTimetable.Remove(classTimetable);
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
