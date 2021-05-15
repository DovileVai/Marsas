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

        private DataTable ConnectToDb(string table)
        {
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            string mysql = "SELECT * From marsodb."+ table;
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand command = new MySqlCommand(mysql, conn);
            conn.Open();
            MySqlDataAdapter dtb = new MySqlDataAdapter();
            dtb.SelectCommand = command;
            DataTable dt = new DataTable();
            dtb.Fill(dt);
            conn.Close();
            return dt;
        }
        public ActionResult TripsList()
        {
            Journey j = new Journey();
            List<Journey> journeys = new List<Journey>();
            journeys = j.getAllRecords();
            return View(journeys.ToList());
        }

        // GET: AdminTrips/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminTrips/Create
        public ActionResult CreateTrip()
        {
            Journey journey = new Journey();
            return View(journey);
        }

        // POST: AdminTrips/Create
        [HttpPost]
        public ActionResult CreateTrip(FormCollection form)
        {
            try
            {
                Journey journey = new Journey();
                Activitiesschedule actS = new Activitiesschedule();
                // dateOfJourney,flightDuration,duration,numberOfSeats, price, points,fk_Locationid_Location
                journey.dateOfJourney = Convert.ToDateTime(form["dateOfJourney"]);
                journey.flightDuration = Convert.ToInt32(form["flightDuration"]);
                journey.duration = Convert.ToInt32(form["duration"]);
                journey.numberOfSeats = Convert.ToInt32(form["numberOfSeats"]);
                journey.price = Convert.ToDouble(form["price"]);
                journey.points = Convert.ToInt32(form["points"]);
                journey.fk_Locationid_Location = Convert.ToInt32(form["fk_Locationid_Location"]);
                actS.maxWorkTime = Convert.ToInt32(form["maxWorkTime"]);
                actS.maxRestTime = Convert.ToInt32(form["maxRestHours"]);
                // validate metodas
                journey.AddJourney(journey);
                return RedirectToAction("TripsList");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminTrips/Edit/5
        public ActionResult EditTrip(int id)
        {
            Journey j = new Journey();
            j = j.GetJourney(id);
            return View(j);
        }

        // POST: AdminTrips/Edit/5
        [HttpPost]
        public ActionResult EditTrip(Journey journey)
        {
            try
            {
                journey.UpdateTrip(journey);
                return RedirectToAction("TripsList");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminTrips/Delete/5
        public ActionResult DeleteTrip(int id)
        {

            try {
                Journey j = new Journey();
                j.DeleteJourney(id);
                return RedirectToAction("TripsList");
            }
            catch 
            {
                return View("TripsList");
            }
        }

        // POST: AdminTrips/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
