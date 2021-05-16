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
    public class Activitiesschedule
    {
        [DisplayName("Savaitės diena")]
        public string weekDay { get; set; }

        [DisplayName("Maksimalus miego laikas")]
        public int maxSleepTime { get; set; }
        [DisplayName("Maksimalus darbo laikas")]
        public int maxWorkTime { get; set; }

        [DisplayName("Maksimalus poilsio laikas")]
        public int maxRestTime { get; set; }

        [DisplayName("ID")]
        public int id_ActivitiesSchedule { get; set; }

        [DisplayName("Užsakymo numeris")]
        public int fk_OrderAid_OrderA { get; set; }

        private string table = "activitiesschedule";
        private MySqlConnection GetConnection()
        {
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            return conn;
        }
        public List<Activitiesschedule> getOrderSchedules(int orderId) 
        {
            List<Activitiesschedule> ids = new List<Activitiesschedule>();
            MySqlConnection conn = this.GetConnection();
            string query = "SELECT * FROM marsodb." + this.table + " WHERE fk_OrderAid_OrderA=?fkO";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("?fkO", MySqlDbType.Int32).Value = orderId;
            conn.Open();
            MySqlDataAdapter dtb = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            dtb.Fill(dt);
            conn.Close();
            foreach (DataRow dr in dt.Rows)
            {
                ids.Add(new Activitiesschedule
                {
                    id_ActivitiesSchedule = Convert.ToInt32(dr["id_ActivitiesSchedule"]),
                    maxSleepTime = Convert.ToInt32(dr["maxSleepTime"]),
                    maxWorkTime = Convert.ToInt32(dr["maxWorkTime"]),
                    maxRestTime = Convert.ToInt32(dr["maxRestTime"]),
                    weekDay = Convert.ToString(dr["weekDay"])
                });
            }
            return ids;
        }
        public string CreateSchedule(int id) 
        {
            MySqlConnection conn = this.GetConnection();
            string query = @"INSERT INTO marsodb." + this.table +
                                                " (weekDay, maxSleepTime, maxWorkTime, maxRestTime,fk_OrderAid_OrderA) " +
                                                "VALUES(?wk,?slp,?wrk,?rst, ?fk);";
            conn.Open();
            for (int i = 1; i <= 7; i++) 
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add("?wk", MySqlDbType.Int32).Value = i;
                command.Parameters.Add("?slp", MySqlDbType.Int32).Value = 8;
                command.Parameters.Add("?wrk", MySqlDbType.Int32).Value = 6;
                command.Parameters.Add("?rst", MySqlDbType.Int32).Value = 6;
                command.Parameters.Add("?fk", MySqlDbType.Int32).Value = id;
                command.ExecuteNonQuery();
            }
            conn.Close();
            return "Success!";
        }
        
    }
}