using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Scheduler_MVC.Models
{
    public class appointmentContext :DbContext
    {
        public appointmentContext()
            : base("name=appointmentConn")
        {
        }
        public DbSet<appointments> appointments { get; set; }
        public DbSet< appointment_schedule> appointment_schedule { get; set; }
        public DbSet<valid_clients> valid_clients { get; set; }
        public DbSet<zones> zones { get; set; }
        public DbSet<appointment_exclusions> appt_ecxlusions { get; set; }
        public DbSet<community_meeting_schedule> community_meetings { get; set; }

    }
    
}