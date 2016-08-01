

namespace Scheduler_MVC.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [NotMapped]
    public partial class cmsViewModel : appointments
    {
        public  string rows { get; set; }
        public  new DateTime appt_date_time { get; set; }
        public  new string appt_status { get; set; }
        public  new string appt_type { get; set; }
        public  new int appt_id { get; set; }
        public  new int appt_client_id { get; set; }
        public  List<community_meeting_schedule> lst { get; set; }
        public cmsViewModel()
        {
            rows = null;
            appt_date_time = DateTime.Today;
            lst = null;
            appt_id = -1;
            appt_type = null;
            appt_client_id = -1;
            appt_status = null;
        }

    }
}