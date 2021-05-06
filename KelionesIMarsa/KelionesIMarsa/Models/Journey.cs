using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Journey
    {
        [DisplayName("Kelionės data")]
        public DateTime dateOfJourney { get; set; }

        [DisplayName("Kelionės trukmė")]
        public int duration { get; set; }

        [DisplayName("Vietų skaičius")]
        public int numberOfSeats { get; set; }

        [DisplayName("Kaina")]
        public double price { get; set; }

        [DisplayName("Taškai")]
        public int points { get; set; }

        [DisplayName("Skrydžio trukmė")]
        public int flightDuration { get; set; }
        [Required]

        [DisplayName("Kelionės ID")]
        public int id_Journey { get; set; }
        [Required]

        [DisplayName("Vieta")]
        public int fk_Locationid_Location { get; set; }
        








    }
}