using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClinicaVeterinaraMobile.Models
{
    public class Pet
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
