using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Activitymode
    {
        [DisplayName("Pradžia")]
        public int startTime { get; set; }

        [DisplayName("ID")]
        public int id_activityMode { get; set; }
        [DisplayName("Veiklos ID")]
        public int fk_Activityid_Activity { get; set; }

        [DisplayName("Tvarkaraščio ID")]
        public int fk_ActivitiesScheduleid_ActivitiesSchedule { get; set; }




    }
}