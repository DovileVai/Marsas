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
        public int getJourneyID()
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
            foreach(DataRow item in dt.Rows)
            {
                a = Convert.ToInt32(item["id_Journey"]);
            }
            return a;
        }
        public ActionResult Journey()
        {
            return View();
        }
        public ActionResult RegisterForJourney()
        {
            //Grazinama darbuotojo kūrimo forma
            Order order = new Order();
            return View(order);
        }
        [HttpPost]
        public ActionResult RegisterForJourney(Order order)
        {
            try
            {
                DateTime today = DateTime.Today;
                int a = getJourneyID();
                Random rnd = new Random();
                int id = rnd.Next(0, 99999);
                //Jei darbuotojo su tabelio nr neranda prideda naują
                string conn = "datasource=localhost;port=3306;username=root;password=";
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO marsodb.ordera(dateOfOrder,id_OrderA,personalCode,phone,payment,fk_Journeyid_Journey)VALUES(?date,?id,?code,?phone,?payment,?fk);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?date", MySqlDbType.Date).Value = today;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                mySqlCommand.Parameters.Add("?code", MySqlDbType.VarChar).Value = order.personalCode;
                mySqlCommand.Parameters.Add("?phone", MySqlDbType.VarChar).Value = order.phone;
                mySqlCommand.Parameters.Add("?payment", MySqlDbType.Int32).Value = 0;
                mySqlCommand.Parameters.Add("?fk", MySqlDbType.Int32).Value = a;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

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
