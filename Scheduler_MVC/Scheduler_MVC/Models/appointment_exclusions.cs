namespace Scheduler_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class appointment_exclusions
    {
        public int appt_excl_client_id { get; set; }

        [Key]
        public int appt_excl_id { get; set; }

        [Required]
        [StringLength(50)]
        public string zone { get; set; }

        public DateTime appt_start_date_time { get; set; }

        public DateTime appt_end_date_time { get; set; }

        public int excluded_time_slots { get; set; }

        [ForeignKey("appt_excl_client_id")]
        public virtual valid_clients valid_clients { get; set; }
    }
}
