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
    public class CreateMenuController : Controller
    {
        // GET: CreateMenu
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
            int a = 0;
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
        public void Fill(Menu menu)
        {
            List<Meal> meals = getMealList();
            List<SelectListItem> selectListMeals = new List<SelectListItem>();
            foreach (var item in meals)
            {
                selectListMeals.Add(new SelectListItem() { Value = Convert.ToString(item.id_Meal), Text = item.name });
            }
            menu.MealList = selectListMeals;
        }
        public ActionResult CreateMenu()
        {
            Menu menu = new Menu();
            //Užpildomi pasirinkimų sąrašai duomenimis iš duomenų saugyklų
            Fill(menu);
            return View(menu);
        }
        [HttpPost]
        public ActionResult CreateMenu(Menu menu)
        {
            try
            {
                int a = getOrderID();
                Random rnd = new Random();
                int id = rnd.Next(0, 99999);
                //Jei darbuotojo su tabelio nr neranda prideda naują
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

                return View();
            }
            catch
            {
                Fill(menu);
                return View(menu);
            }
        }
    }
}