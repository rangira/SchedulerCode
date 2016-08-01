namespace Scheduler_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class community_meeting_schedule
    {
        public int cm_client_id { get; set; }

        [Key]
        public int cm_id { get; set; }

        public DateTime date_time { get; set; }

        [Required]
        [StringLength(100)]
        public string location { get; set; }

        [Required]
        [StringLength(100)]
        public string address { get; set; }

        [Required]
        [StringLength(50)]
        public string city { get; set; }

        [Required]
        [StringLength(10)]
        public string state { get; set; }

        public bool? cancelled { get; set; }

        public int? capacity { get; set; }

        [StringLength(50)]
        public string zone { get; set; }

        public int? SeminarID { get; set; }
    }
}
