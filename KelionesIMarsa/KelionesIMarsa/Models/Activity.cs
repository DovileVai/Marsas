using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Activity
    {
        [DisplayName("Veiklos tipas")]
        public string type { get; set; }

        [DisplayName("Veiklos trukmė")]
        public int duration { get; set; }

        [DisplayName("Pavadinimas")]
        public string name { get; set; }

        [DisplayName("ID")]
        public int id_Activity { get; set; }
        
        




    }
}