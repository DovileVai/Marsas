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
    public class Activity
    {
        [DisplayName("Veiklos tipas")]
        public string type { get; set; }

        [DisplayName("Veiklos trukmė")]
        public int duration { get; set; }

        [DisplayName("Pavadinimas")]
        public string name { get; set; }

        [DisplayName("ID")]
        public int id_Activity { get; set; }

        public List<Activity> ActivitiesList()
        {
            List<Activity> activities = new List<Activity>();
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            string mysql = "SELECT * From marsodb.activity";
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
                activities.Add(new Activity
                {
                    type = Convert.ToString(item["type"]),
                    duration = Convert.ToInt32(item["duration"]),
                    name = Convert.ToString(item["name"]),
                    id_Activity = Convert.ToInt32(item["id_Activity"])
                });
            }
            return activities;
        }
        public void CreateActivity(Activity newActivity)
        {
                Random rnd = new Random();
                int id = rnd.Next(0, 99999);
                string conn = "datasource=localhost;port=3306;username=root;password=";
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO marsodb.activity(type,duration,name,id_Activity)VALUES(?t,?d,?n,?id);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?t", MySqlDbType.VarChar).Value = newActivity.type;
                mySqlCommand.Parameters.Add("?d", MySqlDbType.Int32).Value = newActivity.duration;
                mySqlCommand.Parameters.Add("?n", MySqlDbType.VarChar).Value = newActivity.name;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

        }
        public Activity getActivity(int id)
        {
            Activity activity = new Activity();
            string conn = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from marsodb.activity where id_Activity=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                activity.id_Activity = Convert.ToInt32(item["id_Activity"]);
                activity.type = Convert.ToString(item["type"]);
                activity.duration = Convert.ToInt32(item["duration"]);
                activity.name = Convert.ToString(item["name"]);
            }

            return activity;
        }
        public void updateActivity(Activity activity)
        {
                string conn = "datasource=localhost;port=3306;username=root;password=";
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE marsodb.activity a SET a.type=?t, a.duration=?d,a.name=?n WHERE a.id_Activity=?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?t", MySqlDbType.VarChar).Value = activity.type;
                mySqlCommand.Parameters.Add("?d", MySqlDbType.VarChar).Value = activity.duration;
                mySqlCommand.Parameters.Add("?n", MySqlDbType.VarChar).Value = activity.name;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = activity.id_Activity;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

        }
        public void deleteA(int id)
        {
            string conn = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM marsodb.activity where id_Activity=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }




    }
}