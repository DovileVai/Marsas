using KelionesIMarsa.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KelionesIMarsa.Controllers
{
    public class AdminTripsController : Controller
    {

        public ActionResult openAdminTripsList()
        {
            Journey j = new Journey();
            List<Journey> journeys = new List<Journey>();
            journeys = j.All();
            return View("AdminTripsList",journeys);
        }
        public ActionResult openEditForm(int id)
        {
            Journey j = new Journey();
            j = j.findTrip(id);
            return View("editTripForm",j);
        }


        // GET: AdminTrips/Create
        public ActionResult openAddForm()
        {
            Journey journey = new Journey();
            return View("AddNewTripForm",journey);
        }

        // POST: AdminTrips/Create
        [HttpPost]
        public ActionResult CreateTrip(FormCollection form)
        {
            try
            {
                if (validate(form))
                {
                    Journey journey = new Journey();
                    // dateOfJourney,flightDuration,duration,numberOfSeats, price, points,fk_Locationid_Location
                    journey.dateOfJourney = Convert.ToDateTime(form["dateOfJourney"]);
                    journey.flightDuration = Convert.ToInt32(form["flightDuration"]);
                    journey.duration = Convert.ToInt32(form["duration"]);
                    journey.numberOfSeats = Convert.ToInt32(form["numberOfSeats"]);
                    journey.price = Convert.ToDouble(form["price"]);
                    journey.points = Convert.ToInt32(form["points"]);
                    journey.fk_Locationid_Location = Convert.ToInt32(form["fk_Locationid_Location"]);
                    journey.AddJourney(journey);
                    TempData.Add("success", "Kelionė pridėta sėkmingai!");
                    return RedirectToAction("openAddForm");
                }
                else {
                    TempData.Add("error", "Visi įvedimo laukai turi būti užpildyti ir negali būti 0!");
                    return View("openAddForm");
                }

            }
            catch
            {
                return View("openAddForm");
            }
        }
        private bool validate(FormCollection form) 
        {
            foreach (var key in form.AllKeys) {
                if (form[key].Length < 1 || form[key].StartsWith("0")) 
                {
                    return false;
                }
            }
            return true;
        }
        private bool validate(Journey form)
        {
            if (form.dateOfJourney >= DateTime.Today && form.duration > 0 && form.fk_Locationid_Location != 0 && form.flightDuration > 0 && form.numberOfSeats > 0 && form.price > 0 && form.points > 0 && form.id_Journey > 0) 
            {

                return true;
            }
            return false;
        }

        // GET: AdminTrips/Edit/5
        

        // POST: AdminTrips/Edit/5
        [HttpPost]
        public ActionResult update(Journey journey)
        {
            try
            {
                if (validate(journey))
                {
                    journey.update(journey);
                    TempData.Add("success", "Kelionės duomenys sėkingai atnaujinti!");
                    return View("editTripForm", journey);
                }
                else {
                    TempData.Add("error", "Negalima palikti tuščių laukų arba reikšmių lygių 0!");
                    return View("editTripForm", journey);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminTrips/Delete/5
        public ActionResult deleteTrip(int id)
        {

            try {
                Journey j = new Journey();
                j.destroy(id);
                return RedirectToAction("openAdminTripsList");
            }
            catch 
            {
                return View("TripsList");
            }
        }

    }
}
