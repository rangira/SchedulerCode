namespace Scheduler_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class valid_clients
    {
        
      

        [Key]
        public int client_id { get; set; }

        [Required]
        [StringLength(30)]
        public string client_name { get; set; }

        public bool valid_client { get; set; }

        public bool outbound_hv { get; set; }

        public bool inbound_hv { get; set; }

        public bool brc_hv { get; set; }

        public bool outbound_cm { get; set; }

        public bool inbound_cm { get; set; }

        public bool brc_cm { get; set; }

        public bool has_cm { get; set; }

        public bool schedule_hv_by_zone { get; set; }

        public bool exclude_hv_during_cm { get; set; }

        public int? cm_exclude_duration { get; set; }

        public bool schedule_cm_by_zone { get; set; }

        [Required]
        [StringLength(50)]
        public string AmcatDatabaseName { get; set; }

        public bool? has_hv { get; set; }

        public bool? has_hispanic_hv { get; set; }

        public bool? Portal { get; set; }

        
    }
}
