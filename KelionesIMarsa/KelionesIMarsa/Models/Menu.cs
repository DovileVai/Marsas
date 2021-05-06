using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace KelionesIMarsa.Models
{
    public class Menu
    {
        [DisplayName("Savaitės diena")]
        public string weekDay { get; set; }

        [DisplayName("Pradžios laikas")]
        public int startTime { get; set; }
        [DisplayName("ID")]
        public int id_Menu { get; set; }

        [DisplayName("Patiekalo id")]
        public int fk_Mealid_Meal { get; set; }

        [DisplayName("Užsakymo ID")]
        public int fk_OrderAid_OrderA{ get; set; }

    }
}