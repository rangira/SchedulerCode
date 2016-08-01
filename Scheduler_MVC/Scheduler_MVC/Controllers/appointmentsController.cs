using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Scheduler_MVC.Models;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;
using Scheduler_MVC.Controllers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Reflection;


//System.Web.Mvc Copy Local = True
//System.Web.Abstractions Copy Local = True
//System.Web.Routing Copy Local = True
namespace Scheduler_MVC.Controllers
{
    //This is just a test comment to see if it gets committed to the scheduler_mvc branch in thr git repository
    public class appointmentsController : Controller
    {
        private appointmentContext db = new appointmentContext();

        // GET: appointments
        public ActionResult Index()
        {
            Console.WriteLine("fgjbdfgbhd");
            return View(db.appointments.ToList());
        }

        // Method with Tasks for Preprocess Appointments
        public async Task PreprocessAppointment()
        {
            var task_1 = GetApptTask();
            var task_2 = GetZoneTask();
            await Task.WhenAll(task_1, task_2);

            /*var query = task_1.Join(task_2,
                        t1 => t1.appt_client_id,
                        t2 => int.Parse(t2.zone_client_id),
                        (t1, t2) =>
                        new { t1, t2 }

               ).Where(o => o.t2.zip == "60827");*/
           
        }

        public async Task<IEnumerable<appointments>> GetApptTask(){
            using (var db = new appointmentContext())
            {

                var result = db.appointments.Where(d => d.appt_client_id == 15 && d.customer_id == 68009);
                //eturn Task.Run(()=>result);
                return await result.ToListAsync();
            }


        }

        public async Task <IEnumerable<zones>> GetZoneTask()
        {
            //var result = db.zones.Where(d => d.zone_client_id == "15").AsEnumerable().Distinct<zones>(new zones.Comparer());
            //return await result.ToListAsync();
            using (var db = new appointmentContext())
            {
                var allZones = db.zones.Where(d => d.zone_client_id == "15").ToList();
                var az = await db.zones.Where(d => d.zone_client_id == "15").ToListAsync<zones>().ConfigureAwait(false);
                return az.Distinct<zones>(new zones.Comparer()).ToList();
            }
        }


        // This method picks all the slots which should be excluded based on the given date and the given zone (which is retreived from the join of appointments and zones) appointments slots need to be excluded
        public void ApptEcxlusions(int id)
        {
            DateTime dt1 = new DateTime(2006,11,23,7,0,0);
            DateTime chkDate = new DateTime(2006,11,23,13,0,0);
            DateTime dt2 = new DateTime(2006, 11, 24, 20, 0, 0);
            IEnumerable<appointment_exclusions> result = db.appt_ecxlusions.Where(d=> d.appt_excl_client_id==id && System.Data.Entity.SqlServer.SqlFunctions.DateDiff("hour",d.appt_start_date_time,chkDate)>0 &&
            System.Data.Entity.SqlServer.SqlFunctions.DateDiff("hour", chkDate,d.appt_end_date_time) > 0);
            

        }

        // This method will pick the number of duplicate slots on a given date and time 
        public void AppointmentCheck() {
            DateTime dt = new DateTime(2015, 10, 16, 9, 0, 0);
            int clientid = 51;
            int customer_id = 2180;
            IEnumerable<appointments> result = db.appointments.Where(d => d.appt_client_id == clientid && d.customer_id == customer_id && System.Data.Entity.SqlServer.SqlFunctions.DateDiff("hour", dt, d.appt_date_time) == 0);
            var cnt = db.appointments.Where(d => d.appt_client_id == clientid && d.customer_id == customer_id && System.Data.Entity.SqlServer.SqlFunctions.DateDiff("hour", dt, d.appt_date_time) == 0).ToList().Count();
            System.Diagnostics.Debug.WriteLine("I am a debug comment in AppointCheck");
            System.Diagnostics.Debug.WriteLine("I am a debug comment in AppointCheck for hold yet again");


        }
       
        //This method joins the zones and the appointments and results can be extracted from that 
        public void PreProcessAppointments(IEnumerable<appointments>draft)
        {
            /* System.Data.Entity.Core.Objects.ObjectContext obj= ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
             var connection = obj.Connection as System.Data.Entity.Core.EntityClient.EntityConnection;
             System.Data.Entity.Core.EntityClient.EntityCommand command = connection.CreateCommand();

             command.CommandText = "Select value P  from zones as P where P.zone_client_id="+"51";
             connection.Open();
             System.Data.Entity.Core.EntityClient.EntityDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);*/


            var mytask = PreprocessAppointment();
            Task.Run(async () => { await PreprocessAppointment(); }).Wait();

            draft = db.appointments.Where(d => d.appt_client_id == 15 && d.customer_id == 68009);
            IEnumerable<zones> result = db.zones.Where(d => d.zone_client_id == "15").AsEnumerable().Distinct<zones>(new zones.Comparer());
            var query = draft.Join(result,
                         t1=> t1.appt_client_id,
                         t2 => int.Parse(t2.zone_client_id),
                         (t1,t2)=>
                         new {t1,t2}

                ).Where(o=>o.t2.zip=="60827");

            IEnumerable<Tuple< string,int,string>> query1 = draft.Join(result,
                         t1 => t1.appt_client_id,
                         t2 => int.Parse(t2.zone_client_id),
                         (t1, t2) =>
                         new { t1, t2 }

                ).Where(o => o.t2.zip == "60827").Select(c=> new Tuple<string,int,string>(c.t1.agent_name,c.t1.appt_client_id,c.t2.zip)).ToList();


            DataTable MyDataTable = DT.ToDataTable(query1);

            foreach (var obj in query1)
            {

                System.Diagnostics.Debug.WriteLine(
                    "{0} - {1} - {2}",
                    obj.Item1.ToString(),
                    obj.Item2.ToString(),
                    obj.Item3.ToString());
                    

            }
           /*foreach (var obj in query)
            {
                
                System.Diagnostics.Debug.WriteLine(
                    "{0} - {1} - {2} - {3}",
                    obj.t1.Client.client_id.ToString(),
                    obj.t1.appt_zone,
                    obj.t1.appt_date_time,
                    obj.t2.zip);
            }*/
            
            
            System.Diagnostics.Debug.WriteLine(query);
            System.Diagnostics.Debug.WriteLine("I am in the PreProcessAppointmet function .Hello!!");
            System.Diagnostics.Debug.WriteLine("I am here for debugging. Thats it");
            //IEnumerable<zones> result1 =  db.zones.Select(e => new { e.zip, e.zone, e.zone_client_id }).Where(u => u.zone_client_id=="15").Distinct(new zones.Comparer());


            //List<zones> zones=result.ToList();
            //return View("View",result.ToList());


        }

        // GET: appointments/Details/?clientid=51&custid=15287
        public ActionResult Details(int clientid,int custid)

        {
            if (clientid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //Commented insertAppointments() . Its a working code. Can be included later

            //insertAppointments();
            ApptEcxlusions(15);

            AppointmentCheck();
            IEnumerable <appointments> draft = db.appointments.Where(d => d.appt_client_id == clientid && d.customer_id == custid);
            PreProcessAppointments(draft);
            foreach (appointments s in draft)
            {
                System.Diagnostics.Debug.WriteLine(" I am the client information here in the function. Displayed for test"+" " +s.Client.client_id + " " + s.Client.inbound_hv);
            }

            /*appointment appointment = db.appointments.Find(clientid);
            if (appointment == null)
            {
                return HttpNotFound();
            }*/

            /*if (draft == null)
                return View("Create");*/

            return View(draft.ToList());
        }

        // GET: appointments/Create
        public ActionResult Create()
        {
            System.Diagnostics.Debug.WriteLine("I am a commnet in Create action method");
            return View();
        }

        // POST: appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "appt_id,appt_client_id,customer_id,appt_date_time,recording_uri,time_stamp,appt_status,appt_type,appt_cm_id,appt_zone,agent_name,notes,h2,old_appt_id,released,appt_result,appt_result_datetime,appt_update_status_date,OldCustomerID")] appointments appointment)
         {
             if (ModelState.IsValid)
             {
                 db.appointments.Add(appointment);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             return View(appointment);
         }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( appointments appointment)
        {

            IEnumerable<appointments>draft = db.appointments.Where(d => d.appt_client_id == appointment.appt_client_id && d.customer_id == appointment.customer_id);
            IEnumerable<zones> result = db.zones.Where(d => d.zone_client_id == appointment.appt_client_id.ToString() && d.zip == appointment.zipcode.ToString()).AsEnumerable().Distinct<zones>(new zones.Comparer());
            /*IEnumerable<Tuple<string>> query1 = draft.Join(result,
                       t1 => t1.appt_client_id,
                       t2 => int.Parse(t2.zone_client_id),
                       (t1, t2) =>
                       new { t1, t2 }

              ).Where(o => o.t2.zip == appointment.zipcode.ToString()).Select(c => new Tuple<string>(c.t2.zone)).ToList();*/
            foreach (var obj in result)
            {


                appointment.appt_zone = obj.zone;
                    


            }

            appointments appt = new appointments
            {
                appt_client_id = appointment.appt_client_id,
                customer_id = appointment.customer_id,
                appt_date_time = appointment.appt_date_time,
                recording_uri = "",
                time_stamp = DateTime.Now,
                appt_status = "NEW",
                appt_type = appointment.appt_type,
                appt_cm_id = null,
                appt_zone = appointment.appt_zone,
                agent_name = null,
                notes = appointment.notes,
                h2 = appointment.h2,
                old_appt_id = null,
                released = null,
                appt_result = null,
                appt_result_datetime = null,
                appt_update_status_date = null,

            };

            db.appointments.Add(appt);
            db.SaveChanges();

            if (ModelState.IsValid)
            {
                db.appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View("Create");
        }



        // GET: appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointments appointment = db.appointments.Find(id);
            appointment.time_stamp=DateTime.Now;
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }




        // GET: appointments/CMSchedule/30337040
        public ActionResult CMSchedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointments appointment = db.appointments.Find(id);
            appointment.time_stamp = DateTime.Now;
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        public ActionResult CMSchedule(cmsViewModel model)
        {
            System.Diagnostics.Debug.WriteLine("This is a debug commment"+model);
            List <community_meeting_schedule> lst = JsonConvert.DeserializeObject<List<community_meeting_schedule>>(model.rows);
            model.lst = lst;
            return View("CMSConfim", model);
        }



        [HttpPost]
        public ActionResult CMSConfim(cmsViewModel model)
        {


            foreach (community_meeting_schedule m in model.lst)
            {
                using (var db1 = new appointmentContext())
                {
                    appointments app = db1.appointments.Single(d => d.appt_id == model.appt_id);
                    //app = appointment;
                    app.appt_status = "RESCHED";
                    app.time_stamp = DateTime.Now;
                    app.appt_date_time = model.appt_date_time;

                    //Saving the changes in the database
                    try
                    {
                        
                        db1.SaveChanges();
                    }

                    catch (System.Data.Entity.Validation.DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                }
            }

            return View("CMSchedule", model);
            
        }

        [HttpPost]
        public JsonResult GetClients()
        {
            SelectList clients = new SelectList(db.valid_clients.Select(d=> d.client_id));
            return Json(clients,JsonRequestBehavior.AllowGet);
        }

        //This method is for Edit.cshtml
        [HttpPost]
        public JsonResult GetTime(string day,int id,string zone)
        {
            
            System.Diagnostics.Debug.WriteLine("This is a constant string in the function of get time"+ "day"+day+"id"+id);
            SelectList slots = new SelectList(db.appointment_schedule.Where(d => d.appt_sched_client_id == id && d.day_of_week == day && d.zone == zone).Select(d => d.time));
            return Json(slots, JsonRequestBehavior.AllowGet);
            //return Json(slots);
        }


        //This method is for create.cshtml
        [HttpPost]
        public JsonResult GetTime_(string day, int id, int zip)
        {
            string zone = null;
            IEnumerable<zones> result = db.zones.Where(d => d.zone_client_id == id.ToString() && d.zip == zip.ToString()).AsEnumerable().Distinct<zones>(new zones.Comparer());
           
            foreach (var obj in result)
            {


                zone = obj.zone;



            }
            System.Diagnostics.Debug.WriteLine("This is a constant string in the function of get time" + "day" + day + "id" + id);
            SelectList slots = new SelectList(db.appointment_schedule.Where(d => d.appt_sched_client_id == id && d.day_of_week == day && d.zone == zone).Select(d => d.time));
            return Json(slots, JsonRequestBehavior.AllowGet);
            //return Json(slots);
        }

        [HttpPost]
        public JsonResult GetCms(string day, int id)
        {
            var result = "";
            DateTime dt = new DateTime(2014, 11, 06, 10, 0, 0);
            int clientid = 47;
            IEnumerable<community_meeting_schedule> meetings = db.community_meetings.Where(d => d.cm_client_id == clientid && System.Data.Entity.SqlServer.SqlFunctions.DateDiff("day", d.date_time, dt) == 0);
            var meetingLst = db.community_meetings.Where(d => d.cm_client_id == clientid && System.Data.Entity.SqlServer.SqlFunctions.DateDiff("day", d.date_time, dt) == 0)
                                                    .Select(d => new
                                                    {
                                                        CMID= d.cm_id,
                                                        CMDateTime = d.date_time,
                                                        CMLocation = d.location,
                                                        CMAddress = d.address,
                                                        CMCity = d.city,
                                                        CMState = d.state,
                                                        CMCapacity = d.capacity,
                                                        CMZone = d.zone
                                                    }
                                                    
                                                    );// date_time, location, address, city, state, capacity, zone });
            System.Diagnostics.Debug.WriteLine("I am a debug comment in the GetCms()");
            System.Diagnostics.Debug.WriteLine("T am additionla commnet in GetCms()");
            return Json(meetingLst);
        }



        [HttpPost]
        public JsonResult GetCms_(string day, int id, DateTime date)
        {
            var result = "";
            //DateTime dt = new DateTime(2015, 06, 25, 12, 0, 0);
            DateTime dt = date;
            //int clientid = 47;
            int clientid = id;
            IEnumerable<community_meeting_schedule> meetings = db.community_meetings.Where(d => d.cm_client_id == clientid && System.Data.Entity.SqlServer.SqlFunctions.DateDiff("day", d.date_time, dt) == 0);
            var meetingLst = db.community_meetings.Where(d => d.cm_client_id == clientid && System.Data.Entity.SqlServer.SqlFunctions.DateDiff("day", d.date_time, dt) == 0)
                                                    .Select(d => new
                                                    {
                                                        CMID = d.cm_id,
                                                        CMDateTime = d.date_time,
                                                        CMLocation = d.location,
                                                        CMAddress = d.address,
                                                        CMCity = d.city,
                                                        CMState = d.state,
                                                        CMCapacity = d.capacity,
                                                        CMZone = d.zone
                                                    }

                                                    );// date_time, location, address, city, state, capacity, zone });
            System.Diagnostics.Debug.WriteLine("I am a debug comment in the GetCms()");
            System.Diagnostics.Debug.WriteLine("T am additionla commnet in GetCms()");
            return Json(meetingLst);
        }

       
        //****Edit returning the Json string
        [HttpPost]
          public ActionResult EditJson(appointments appointment)
          {

              appointment.notes = "updated json notes";
              appointment.appt_client_id = 5123;
              appointment.customer_id = 12345;
              //return RedirectToAction("Edit",appointment); // only works for the Get request not for the post so not used
              return Json(appointment);
          }


       //****Edit returning ActionResult
       [HttpPost]
        public ActionResult Edit(appointments appointment)
        {

            appointment.notes = "updated json notes";
            appointment.appt_client_id = 5123;
            appointment.customer_id = 12345;
            //return RedirectToAction("Edit",appointment);
            return View("Confirm1",appointment);
            //return RedirectToAction("Confirm1",appointment);
        }

        [HttpPost]
        public ActionResult Details(FormCollection c)
        {
            System.Diagnostics.Debug.WriteLine("In the Details function with argument FormCollcetion returning ActionResult" +
                                               "line break 1 for debugging diagnostics " +
                                               "line break 2 for debugging diagnostics" +
                                               "line break 3 for debugging diagnostics");


            return View();
        }

        [HttpPost]
        public JsonResult Details1(appointments appointments)
        {
            System.Diagnostics.Debug.WriteLine("In the Details 1 with the appointment object type argument" +
                                               "line break 1 for debugging diagnostics " +
                                               "line break 2 for debugging diagnostics" +
                                               "line break 3 for debugging diagnostics");


            //return View("Details");
            return Json(appointments);
        }

        //The method for rendering the partial view
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Details2(FormCollection col)
        {
            System.Diagnostics.Debug.WriteLine("In the Details2 for the formcollection -- chceking what the formcollection has" +
                                               "line break 1 for debugging diagnostics " +
                                               "line break 2 for debugging diagnostics" +
                                               "line break 3 for debugging diagnostics");
            appointments newModel = new appointments() { appt_id = 30338161, appt_client_id = 3, customer_id = 51, appt_type = "Cm", notes = "jhgjhfhfsjfhgsjhgshfgsfshgdhgfjdfgjdhfgdg", appt_zone = "zone1", appt_status = "NEW", appt_date_time = new DateTime(), time_stamp = new DateTime() };
            System.Diagnostics.Debug.WriteLine(col["appt_client_id"] + col["notes"]);

            //return View("Details");
            //return Json(newModel);
            return PartialView("Confirm", newModel);
        }


        //This method is for the Model binding through the formCollection in post request
        [HttpPost]
        public JsonResult Details3(FormCollection col)
        {
            System.Diagnostics.Debug.WriteLine("In the Details3 for the formcollection  function jvdkjdfjd" +
                                               "line break 1 for debugging diagnostics " +
                                               "line break 2 for debugging diagnostics" +
                                               "line break 3 for debugging diagnostics");
            appointments newModel = new appointments() { appt_id = 30338161, appt_client_id = 3, customer_id = 51, appt_type = "Cm", notes = "jhgjhfhfsjfhgsjhgshfgsfshgdhgfjdfgjdhfgdg", appt_zone = "zone1", appt_status = "NEW", appt_date_time = new DateTime(), time_stamp = new DateTime() };
            System.Diagnostics.Debug.WriteLine(col["appt_client_id"] + col["notes"]);
            //Confirm(newModel);
            //return View("Confirm",newModel);


            //Model Binding usimg TryUpdateModel
            var testAppt = new appointments();
            var tstVal = TryUpdateModel<appointments>(testAppt,col);
            //End Code Model Binding
            //Model Binding mehod2
            //Resolved the errors for AmcatDatabase in Client and appt_zone in appointments model
            var apt = Type.GetType("Scheduler_MVC.Models.appointments");
            var apt1 = Activator.CreateInstance(apt);

            var binder = Binders.GetBinder(apt);

            var bindingContext = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => apt1, apt),
                ModelState = ModelState,
                ValueProvider = col
            };

            binder.BindModel(ControllerContext, bindingContext);
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Model is valid here");
            }

                //Model Binding method 2 ends here

                var obj = new Dictionary<string, string> { { "val", "Val" } };
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(obj));

            //Line below sends the dictionary value as a json object 
            //return Json(JsonConvert.SerializeObject(obj));
            return Json(apt1);
           
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Confirm(appointments appointment)
        {
            System.Diagnostics.Debug.WriteLine("I am in Confirm and this is a comment for debugging");
            System.Diagnostics.Debug.WriteLine("skhfshfhs".ToString());
            return View("Edit",appointment);
        }

       
       [HttpPost] 
        public ActionResult Confirm1(appointments appointment,string submit)
        {
            System.Diagnostics.Debug.WriteLine("I am in Confirm and this is a comment for debugging");
            System.Diagnostics.Debug.WriteLine("skhfshfhs".ToString());
            //Execute the update query here 
            //db.AsNoTracking().Where
            using (var db1 = new appointmentContext()){
                appointments app = db1.appointments.Single(d => d.appt_id == appointment.appt_id);
                //app = appointment;
                app.appt_status = "RESCHED";
                app.time_stamp = DateTime.Now;
                app.notes = appointment.notes;
                app.recording_uri = appointment.recording_uri;
                app.h2 = appointment.h2;
                app.appt_date_time = appointment.appt_date_time;
                
                //Saving the changes in the database
                try
                {
                    //db.appointments.Attach(appointment);
                    //db.Entry<appointments>(appointment).State = EntityState.Modified;

                    //db.appointments.Add(app);
                    db1.SaveChanges();
                }

                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
            //return View("Edit", appointment);
            //return View("Details");
            return RedirectToAction("Details","appointments",new { clientid=appointment.appt_client_id,custid =00000 });
        }


        // GET: appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointments appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            appointments appointment = db.appointments.Find(id);
            db.appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //action method to insert in appointments table. Later to be invoked
        public void insertAppointments()
        {
            appointments appt = new appointments
            {
                appt_client_id = 51 ,
                customer_id = 123456,
                appt_date_time = DateTime.Now,
                recording_uri = "test recording uri",
                time_stamp = DateTime.Today,
                appt_status = "NEW" ,
                appt_type = "CM",
                appt_cm_id =null ,
                appt_zone = "ZONE2",
                agent_name = null,
                notes = "test notes for test data",
                h2 = null,
                old_appt_id =null ,
                released = null,
                appt_result =  null,
                appt_result_datetime = null,
                appt_update_status_date = null,
               
            };

            db.appointments.Add(appt);
            db.SaveChanges();
        }

        public void updateAppointment()
        {
            //The query is same as the insertAppointments method but
            //the property of the returned entity will have to be changed

        }

        public void deleteAppointment()
        {

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



    //class for multiple attrubite selection. To be used later with multiple submit buttons
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
     class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var isValidName = false;
            var keyValue = string.Format("{0}:{1}", Name, Argument);
            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value != null)
            {
                controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                isValidName = true;
            }

            return isValidName;
        }

        
    }

    /* The MultipleAttributeSelection class ends */
}
