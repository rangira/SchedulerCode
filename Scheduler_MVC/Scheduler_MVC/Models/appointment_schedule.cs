namespace Scheduler_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class appointment_schedule
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int appt_sched_client_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string zone { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int appt_slots { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string day_of_week { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string time { get; set; }

        public int? hispanic_slots { get; set; }

        [Key]
        [Column(Order = 5)]
        public int appt_schedule_id { get; set; }

        
    }
}
