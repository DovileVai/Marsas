using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [DisplayName("Patiekalas")]
        public int fk_Mealid_Meal { get; set; }

        [DisplayName("Užsakymas")]
        public int fk_OrderAid_OrderA{ get; set; }

        public IList<SelectListItem> MealList { get; set; }

    }
}