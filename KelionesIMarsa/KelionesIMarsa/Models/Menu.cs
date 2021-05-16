using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;

namespace KelionesIMarsa.Models
{
    public class Menu
    {
        [DisplayName("Savaitės diena")]
        public string weekDay { get; set; }

        [DisplayName("Pradžios laikas")]
        public int startTime { get; set; }
        [DisplayName("ID")]
        public int id_Menu { get; set; }

        [DisplayName("Patiekalas")]
        public int fk_Mealid_Meal { get; set; }

        [DisplayName("Užsakymas")]
        public int fk_OrderAid_OrderA{ get; set; }

        public IList<SelectListItem> MealList { get; set; }

        public int getOrderID()
        {
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            string mysql = "SELECT id_OrderA From marsodb.ordera order by RAND() LIMIT 1";
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
                a = Convert.ToInt32(item["id_OrderA"]);
            }
            return a;
        }
        public List<Meal> getMealList()
        {
            List<Meal> meals = new List<Meal>();
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            string mysql = "SELECT * From marsodb.meal";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand command = new MySqlCommand(mysql, conn);
            conn.Open();
            MySqlDataAdapter dtb = new MySqlDataAdapter();
            dtb.SelectCommand = command;
            DataTable dt = new DataTable();
            dtb.Fill(dt);
            conn.Close();
            foreach (DataRow item in dt.Rows)
            {
                meals.Add(new Meal
                {
                    name = Convert.ToString(item["name"]),
                    calories = Convert.ToInt32(item["calories"]),
                    type = Convert.ToString(item["type"]),
                    id_Meal = Convert.ToInt32(item["id_Meal"]),
                });
            }
            return meals;
        }
        public void CreateMenu(Menu menu)
        {
                int a = menu.getOrderID();
                Random rnd = new Random();
                int id = rnd.Next(0, 99999);
                string conn = "datasource=localhost;port=3306;username=root;password=";
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO marsodb.menu(weekDay,startTime,id_Menu,fk_Mealid_Meal,fk_OrderAid_OrderA)VALUES(?day,?time,?id,?meal,?order);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?day", MySqlDbType.VarChar).Value = menu.weekDay;
                mySqlCommand.Parameters.Add("?time", MySqlDbType.Int32).Value = menu.startTime;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                mySqlCommand.Parameters.Add("?meal", MySqlDbType.Int32).Value = menu.fk_Mealid_Meal;
                mySqlCommand.Parameters.Add("?order", MySqlDbType.Int32).Value = a;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

        }
    }
    
}