using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClinicaVeterinaraMobile.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
