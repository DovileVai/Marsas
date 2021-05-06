using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Meal
    {
        [DisplayName("Patiekalo pavadinimas")]
        public string name { get; set; }

        [DisplayName("Kalorijos")]
        public int calories { get; set; }

        [DisplayName("Tipas")]
        public string type { get; set; }

        [DisplayName("ID")]
        public int id_Meal { get; set; }



       

    }
}