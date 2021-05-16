using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace KelionesIMarsa.Models
{
    public class Activitymode
    {
        [DisplayName("Pradžia")]
        public int startTime { get; set; }

        [DisplayName("ID")]
        public int id_activityMode { get; set; }
        [DisplayName("Veiklos ID")]
        public int fk_Activityid_Activity { get; set; }

        [DisplayName("Tvarkaraščio ID")]
        public int fk_ActivitiesScheduleid_ActivitiesSchedule { get; set; }

        [DisplayName("Pabaiga")]
        public int endTime { get; set; }

        private string table = "activitymode";

        public string AddActivity(int scheduleId, int activityId, int startTime, int endTime) 
        {
            MySqlConnection conn = GetConnection();
            string query = @"INSERT INTO marsodb." + this.table +
                                                " (startTime,endTime,fk_Activityid_Activity,fk_ActivitiesScheduleid_ActivitiesSchedule) " +
                                                "VALUES(?st, ?end, ?fk_act, ?fk_sch);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("?st", MySqlDbType.Int32).Value = startTime;
            command.Parameters.Add("?end", MySqlDbType.Int32).Value = endTime;
            command.Parameters.Add("?fk_act", MySqlDbType.Int32).Value = activityId;
            command.Parameters.Add("?fk_sch", MySqlDbType.Int32).Value = scheduleId;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            return "Succesfuly Added";
        }
        private MySqlConnection GetConnection()
        {
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            return conn;
        }
        public List<Activitymode> getAllActivitiesOfDay(int id) {
            List<Activitymode> activities = new List<Activitymode>();
            MySqlConnection conn = GetConnection();
            string query = "SELECT * FROM marsodb." + this.table + " WHERE fk_ActivitiesScheduleid_ActivitiesSchedule=?fkid";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("?fkid", MySqlDbType.Int32).Value = id;
            conn.Open();
            MySqlDataAdapter dtb = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            dtb.Fill(dt);
            conn.Close();
            foreach (DataRow dr in dt.Rows) 
            {
                activities.Add(new Activitymode
                {
                    id_activityMode = Convert.ToInt32(dr["id_activityMode"]),
                    startTime = Convert.ToInt32(dr["startTime"]),
                    endTime = Convert.ToInt32(dr["endTime"]),
                    fk_ActivitiesScheduleid_ActivitiesSchedule = Convert.ToInt32(dr["fk_ActivitiesScheduleid_ActivitiesSchedule"]),
                    fk_Activityid_Activity = Convert.ToInt32(dr["fk_Activityid_Activity"])
                }) ;
            }

            return activities;
        }


    }
}