namespace Scheduler_MVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class appointment
    {
        public int appt_client_id { get; set; }

        public int customer_id { get; set; }

        [Key]
        public int appt_id { get; set; }

        public DateTime appt_date_time { get; set; }

        [StringLength(200)]
        public string recording_uri { get; set; }

        public DateTime time_stamp { get; set; }

        [Required]
        [StringLength(20)]
        public string appt_status { get; set; }

        [Required]
        [StringLength(5)]
        public string appt_type { get; set; }

        public int? appt_cm_id { get; set; }

        [Required]
        [StringLength(50)]
        public string appt_zone { get; set; }

        [StringLength(20)]
        public string agent_name { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }

        public bool? h2 { get; set; }

        public int? old_appt_id { get; set; }

        public bool? released { get; set; }

        [StringLength(50)]
        public string appt_result { get; set; }

        public DateTime? appt_result_datetime { get; set; }

        public DateTime? appt_update_status_date { get; set; }

        public int? OldCustomerID { get; set; }
    }
}
