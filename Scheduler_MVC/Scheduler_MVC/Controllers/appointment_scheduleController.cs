using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scheduler_MVC.Models;

namespace Scheduler_MVC.Controllers
{
    public class appointment_scheduleController : Controller
    {
        private appointmentContext db = new appointmentContext();

        // GET: appointment_schedule
        public ActionResult Index()
        {
            return View(db.appointment_schedule.ToList());
        }

        // GET: appointment_schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment_schedule appointment_schedule = db.appointment_schedule.Find(id);
            if (appointment_schedule == null)
            {
                return HttpNotFound();
            }
            return View(appointment_schedule);
        }

        // GET: appointment_schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: appointment_schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "appt_sched_client_id,zone,appt_slots,day_of_week,time,appt_schedule_id,hispanic_slots")] appointment_schedule appointment_schedule)
        {
            if (ModelState.IsValid)
            {
                db.appointment_schedule.Add(appointment_schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment_schedule);
        }

        // GET: appointment_schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment_schedule appointment_schedule = db.appointment_schedule.Find(id);
            if (appointment_schedule == null)
            {
                return HttpNotFound();
            }
            return View(appointment_schedule);
        }

        // POST: appointment_schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "appt_sched_client_id,zone,appt_slots,day_of_week,time,appt_schedule_id,hispanic_slots")] appointment_schedule appointment_schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment_schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment_schedule);
        }

        // GET: appointment_schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment_schedule appointment_schedule = db.appointment_schedule.Find(id);
            if (appointment_schedule == null)
            {
                return HttpNotFound();
            }
            return View(appointment_schedule);
        }

        // POST: appointment_schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            appointment_schedule appointment_schedule = db.appointment_schedule.Find(id);
            db.appointment_schedule.Remove(appointment_schedule);
            db.SaveChanges();
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
