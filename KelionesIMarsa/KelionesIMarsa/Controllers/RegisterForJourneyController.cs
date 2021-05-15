using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KelionesIMarsa.Models;
using MySql.Data.MySqlClient;

namespace KelionesIMarsa.Controllers
{
    public class RegisterForJourneyController : Controller
    {
       
        public ActionResult Journey()
        {
            return View();
        }
        public ActionResult RegisterForJourney()
        {
            Order order = new Order();
            return View(order);
        }
        [HttpPost]
        public ActionResult RegisterForJourney(Order order)
        {
            try
            {
                int id = order.RegisterForJourney(order);
                HttpCookie cookie = new HttpCookie("order_id", id.ToString());
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);
                TempData.Add("message", "Jūsų užsakymo informacija buvo išsaugota");
                return View();
            }
            catch
            {
                return View(order);
            }
        }
        public ActionResult TimeTableCreate()
        {
            HttpCookie cookie = Request.Cookies["order_id"];
            int order_id = Convert.ToInt32(cookie.Value);
            string status = new Activitiesschedule().CreateSchedule(order_id);
            return View();
        }
    }
}
