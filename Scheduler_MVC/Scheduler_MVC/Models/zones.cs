using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Scheduler_MVC.Models
{
    public class zones
    {
        [Column(Order=0),Key]
        [StringLength(50)]
        public string zone_client_id { get; set; }
        //public int zone_client_id { get; set; }

        [Column(Order = 1), Key]
        [StringLength(5)]
        public string zip { get; set; }

       
        [StringLength(50)]
        public string zone { get; set; }

        public class Comparer : IEqualityComparer<zones>
        {
            public bool Equals(zones x, zones y)
            {
                return x.zone_client_id == y.zone_client_id
                     && x.zip == y.zip
                     && x.zone == y.zone;
            }

            public int GetHashCode(zones obj)
            {
                return obj.zone_client_id.GetHashCode() ^
                      obj.zip.GetHashCode() ^
                      obj.zone.GetHashCode();   
            }
        }
    }
}