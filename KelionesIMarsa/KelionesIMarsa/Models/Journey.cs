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
    public class Journey
    {
        [DisplayName("Kelionės data")]
        public DateTime dateOfJourney { get; set; }

        [DisplayName("Kelionės trukmė")]
        public int duration { get; set; }

        [DisplayName("Vietų skaičius")]
        public int numberOfSeats { get; set; }

        [DisplayName("Kaina")]
        public double price { get; set; }

        [DisplayName("Taškai")]
        public int points { get; set; }

        [DisplayName("Skrydžio trukmė")]
        public int flightDuration { get; set; }
        [Required]

        [DisplayName("Kelionės ID")]
        public int id_Journey { get; set; }
        [Required]

        [DisplayName("Vieta")]
        public int fk_Locationid_Location { get; set; }

        private string table = "journey";

        public Journey GetJourney(int id)
        {
            Journey journey = new Journey().GetJourneyWhere(id);
            return journey;
        }
        private Journey GetJourneyWhere(int id) {
            Journey j = new Journey();
            MySqlConnection conn = GetConnection();
            string mysql = "SELECT * From marsodb." + this.table + " WHERE  id_Journey=?id";
            MySqlCommand command = new MySqlCommand(mysql, conn);
            command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            conn.Open();
            MySqlDataAdapter dtb = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            dtb.Fill(dt);
            conn.Close();
            foreach (DataRow dr in dt.Rows) 
            {
                j.numberOfSeats = Convert.ToInt16(dr["numberOfSeats"]);
                j.points = Convert.ToInt16(dr["points"]);
                j.dateOfJourney = Convert.ToDateTime(dr["dateOfJourney"]);
                j.flightDuration = Convert.ToInt16(dr["flightDuration"]);
                j.duration = Convert.ToInt16(dr["duration"]);
                j.fk_Locationid_Location = Convert.ToInt16(dr["fk_Locationid_Location"]);
                j.id_Journey = Convert.ToInt16(dr["id_Journey"]);
                j.price = Convert.ToInt32(dr["price"]);
            }
            return j;
        }
        public List<Journey> getAllRecords() 
        {
            List<Journey> journeys = new List<Journey>();
            DataTable dt = getAll();
            foreach (DataRow dr in dt.Rows) 
            {
                journeys.Add(new Journey
                {
                    dateOfJourney = Convert.ToDateTime(dr["dateOfJourney"]),
                    flightDuration = Convert.ToInt16(dr["flightDuration"]),
                    numberOfSeats = Convert.ToInt16(dr["numberOfSeats"]),
                    duration = Convert.ToInt16(dr["duration"]),
                    price = Convert.ToDouble(dr["price"]),
                    points = Convert.ToInt32(dr["points"]),
                    id_Journey = Convert.ToInt32(dr["id_Journey"])
                });
            }
            return journeys;
        }

        private DataTable getAll()
        {
            MySqlConnection conn = GetConnection();
            string mysql = "SELECT * From marsodb." + this.table;
            MySqlCommand command = new MySqlCommand(mysql, conn);
            MySqlDataAdapter dtb = new MySqlDataAdapter();
            dtb.SelectCommand = command;
            DataTable dt = new DataTable();
            dtb.Fill(dt);
            conn.Close();
            return dt;
        }
        private MySqlConnection GetConnection() 
        {
            string connectionstring = "datasource=localhost;port=3306;username=root;password=";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            return conn;
        }

        public string AddJourney(Journey data) 
        {
            string status = "Error proccessing request :(";
            MySqlConnection conn = this.GetConnection();
            string query = @"INSERT INTO marsodb."+this.table+ 
                                                " (dateOfJourney,flightDuration,duration,numberOfSeats, price, points,fk_Locationid_Location) " +
                                                "VALUES(?date,?fdur,?dur,?num,?pr,?pts, ?loc);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("?date", MySqlDbType.DateTime).Value = data.dateOfJourney;
            command.Parameters.Add("?fdur", MySqlDbType.Int32).Value = data.flightDuration;
            command.Parameters.Add("?dur", MySqlDbType.Int32).Value = data.duration;
            command.Parameters.Add("?num", MySqlDbType.Int32).Value = data.numberOfSeats;
            command.Parameters.Add("?pr", MySqlDbType.Double).Value = data.price;
            command.Parameters.Add("?pts", MySqlDbType.Int32).Value = data.points;
            command.Parameters.Add("?loc", MySqlDbType.Int32).Value = data.fk_Locationid_Location;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            return status;
        }

    }
}