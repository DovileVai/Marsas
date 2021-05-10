using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using KelionesIMarsa.Models;
using MySql.Data.MySqlClient;

namespace KelionesIMarsa.Models
{
    public class Meal
    {
        [DisplayName("Patiekalo pavadinimas")]
        public string name { get; set; }

        [DisplayName("Kalorijos")]
        public int calories { get; set; }

        [DisplayName("Tipas")]
        public string type { get; set; }

        [DisplayName("ID")]
        public int id_Meal { get; set; }

        public List<Meal> MealList()
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
            return meals;
        }
        public void CreateMeal(Meal newMeal)
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
        public void updateMeal(Meal meal)
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

    }
}