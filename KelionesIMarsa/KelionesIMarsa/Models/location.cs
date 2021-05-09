using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Location
    {
        [DisplayName("Vietos pavadinimas")]
        public string name { get; set; }

        [DisplayName("ID")]
        public int id_Location { get; set; }

        [DisplayName("Koordinatės")]
        public string coordinates { get; set; }


        


    }
}