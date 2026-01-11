using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Newtonsoft.Json;

namespace ClinicaVeterinaraMobile.Models
{
    public class Vet
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string VetName { get; set; }
        public string Specialization { get; set; }
        public string AdresaCabinet { get; set; }
    }
}
