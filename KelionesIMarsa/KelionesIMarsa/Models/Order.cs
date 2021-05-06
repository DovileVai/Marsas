using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Order
    {
        [DisplayName("Užsakymo data")]
        public DateTime dateOfOrder { get; set; }

        [DisplayName("Asmens kodas")]
        public string personalCode { get; set; }

        [DisplayName("Telefonas")]
        public string phone { get; set; }

        [DisplayName("AR sumokėta")]
        public bool payment { get; set; }

        [DisplayName("ID")]
        [Required]
        public int id_OrderA { get; set; }

        [DisplayName("keliones ID")]
        public int fk_Journeyid_Journey { get; set; }
 
    }
}