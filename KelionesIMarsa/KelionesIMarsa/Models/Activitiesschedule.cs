using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Activitiesschedule
    {
        [DisplayName("Savaitės diena")]
        public string weekDay { get; set; }

        [DisplayName("Maksimalus miego laikas")]
        public int maxSleepTime { get; set; }
        [DisplayName("Maksimalus darbo laikas")]
        public int maxWorkTime { get; set; }

        [DisplayName("Maksimalus poilsio laikas")]
        public int maxRestTime { get; set; }

        [DisplayName("ID")]
        public int id_ActivitiesSchedule { get; set; }

    }
}