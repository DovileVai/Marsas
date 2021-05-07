using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Mvc;
using KelionesIMarsa.Models;
using MySql.Data.MySqlClient;

namespace KelionesIMarsa.Controllers
{
    public class AdminMealsController : Controller
    {
        // GET: AdminMeals
        public ActionResult MealList()
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
                    id_Meal = Convert.ToInt32(item["id_Meal"])
                });
            }
            return View(meals.ToList());
        }

        // GET: AdminMeals/Create
        public ActionResult CreateMeal()
        {
            Meal meal = new Meal();
            return View(meal);
        }

        // POST: AdminMeals/Create
        [HttpPost]
        public ActionResult CreateMeal(Meal newMeal)
        {
            try
            {
                Random rnd = new Random();
                int id = rnd.Next(0, 99999);
                string conn = "datasource=localhost;port=3306;username=root;password=";
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO marsodb.meal(name,calories,type,id_Meal)VALUES(?n,?c,?t,?id);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?n", MySqlDbType.VarChar).Value = newMeal.name;
                mySqlCommand.Parameters.Add("?c", MySqlDbType.Int32).Value = newMeal.calories;
                mySqlCommand.Parameters.Add("?t", MySqlDbType.VarChar).Value = newMeal.type;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return RedirectToAction("MealList");
            }
            catch
            {
                return View(newMeal);
            }
        }
        public Meal getMeal(int id)
        {
            Meal meal = new Meal();
            string conn = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from marsodb.meal where id_Meal=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                meal.id_Meal = Convert.ToInt32(item["id_Meal"]);
                meal.type = Convert.ToString(item["type"]);
                meal.calories = Convert.ToInt32(item["calories"]);
                meal.name = Convert.ToString(item["name"]);
            }

            return meal;
        }
        public bool updateMeal(Meal meal)
        {
            try
            {
                string conn = "datasource=localhost;port=3306;username=root;password=";
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE marsodb.meal a SET a.type=?t, a.calories=?c,a.name=?n WHERE a.id_Meal=?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?t", MySqlDbType.VarChar).Value = meal.type;
                mySqlCommand.Parameters.Add("?c", MySqlDbType.VarChar).Value = meal.calories;
                mySqlCommand.Parameters.Add("?n", MySqlDbType.VarChar).Value = meal.name;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = meal.id_Meal;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void deleteM(int id)
        {
            string conn = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM marsodb.meal where id_Meal=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
        // GET: AdminMeals/Edit/5
        public ActionResult EditMeal(int id)
        {
            return View(getMeal(id));
        }

        // POST: AdminMeals/Edit/5
        [HttpPost]
        public ActionResult EditMeal(int id, Meal meal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    updateMeal(meal);
                }

                return RedirectToAction("MealList");
            }
            catch
            {
                return View(meal);
            }
        }

        // GET: AdminMeals/Delete/5
        public ActionResult DeleteMeal(int id)
        {
            return View(getMeal(id));
        }

        // POST: AdminMeals/Delete/5
        [HttpPost]
        public ActionResult DeleteMeal(int id, FormCollection collection)
        {
            try
            {
                deleteM(id);
                return RedirectToAction("MealList");
            }
            catch
            {
                return View();
            }
        }
    }
}
