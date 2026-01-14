using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClinicaVeterinaraMobile.Models
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserId { get; set; }
        public string NumePacient { get; set; }
        public string NumeStapan { get; set; }
        public string TelefonStapan { get; set; }
        public DateTime DataProgramare { get; set; }
        public string Motiv { get; set; }
        public int VetID { get; set; }
        [Ignore]
        public string VetDetails { get; set; }
    }
}