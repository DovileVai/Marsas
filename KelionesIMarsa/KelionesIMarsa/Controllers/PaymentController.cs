using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using KelionesIMarsa.Models;

namespace KelionesIMarsa.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Bank()
        {
            return View();
        }
        // GET: Payment
        public ActionResult Payment()
        {
            Journey journey = countPrice(5);
            return View(journey);
        }
        public Journey countPrice(int b)
        {
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            string mysql = "SELECT id_Journey From marsodb.journey order by RAND() LIMIT 1";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand command = new MySqlCommand(mysql, conn);
            conn.Open();
            MySqlDataAdapter dtb = new MySqlDataAdapter();
            dtb.SelectCommand = command;
            DataTable dt = new DataTable();
            dtb.Fill(dt);
            conn.Close();
            int a = 0;
            foreach (DataRow item in dt.Rows)
            {
                a = Convert.ToInt32(item["id_Journey"]);
            }
            Journey journey = get(a);
            return journey;
        }
        public Journey get(int id)
        {
            Journey journey = new Journey();
            string conn = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from marsodb.journey where id_Journey=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                journey.id_Journey = Convert.ToInt32(item["id_Journey"]);
                journey.dateOfJourney = Convert.ToDateTime(item["dateOfJourney"]);
                journey.duration = Convert.ToInt32(item["duration"]);
                journey.numberOfSeats = Convert.ToInt32(item["numberOfSeats"]);
                journey.price = Convert.ToDouble(item["price"]);
                journey.points = Convert.ToInt32(item["points"]);
                journey.flightDuration = Convert.ToInt32(item["flightDuration"]);
                journey.fk_Locationid_Location = Convert.ToInt32(item["fk_Locationid_Location"]);


            }

            return journey;
        }
        public ActionResult countPrice()
        {
            return View();
        }

    }
}