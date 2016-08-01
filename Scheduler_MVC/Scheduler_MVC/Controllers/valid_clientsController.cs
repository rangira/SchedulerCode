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
    public class valid_clientsController : Controller
    {
        private appointmentContext db = new appointmentContext();

        // GET: valid_clients
        public ActionResult Index()
        {
            return View(db.valid_clients.ToList());
        }

        // GET: valid_clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            valid_clients valid_clients = db.valid_clients.Find(id);
            if (valid_clients == null)
            {
                return HttpNotFound();
            }
            return View(valid_clients);
        }

        // GET: valid_clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: valid_clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "client_id,client_name,valid_client,outbound_hv,inbound_hv,brc_hv,outbound_cm,inbound_cm,brc_cm,has_cm,schedule_hv_by_zone,exclude_hv_during_cm,cm_exclude_duration,schedule_cm_by_zone,AmcatDatabaseName,has_hv,has_hispanic_hv,Portal")] valid_clients valid_clients)
        {
            if (ModelState.IsValid)
            {
                db.valid_clients.Add(valid_clients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valid_clients);
        }

        // GET: valid_clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            valid_clients valid_clients = db.valid_clients.Find(id);
            if (valid_clients == null)
            {
                return HttpNotFound();
            }
            return View(valid_clients);
        }

        // POST: valid_clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "client_id,client_name,valid_client,outbound_hv,inbound_hv,brc_hv,outbound_cm,inbound_cm,brc_cm,has_cm,schedule_hv_by_zone,exclude_hv_during_cm,cm_exclude_duration,schedule_cm_by_zone,AmcatDatabaseName,has_hv,has_hispanic_hv,Portal")] valid_clients valid_clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valid_clients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valid_clients);
        }

        // GET: valid_clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            valid_clients valid_clients = db.valid_clients.Find(id);
            if (valid_clients == null)
            {
                return HttpNotFound();
            }
            return View(valid_clients);
        }

        // POST: valid_clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            valid_clients valid_clients = db.valid_clients.Find(id);
            db.valid_clients.Remove(valid_clients);
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
