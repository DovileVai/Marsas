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
                order.RegisterForJourney(order);
                return View();
            }
            catch
            {
                return View(order);
            }
        }
        public ActionResult TimeTableCreate()
        {
            return View();
        }
    }
}
