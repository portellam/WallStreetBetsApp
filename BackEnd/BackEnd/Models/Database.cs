using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public class Database
    {

    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
    }

    public class Favorite
    {
        public int id { get; set; }
        public string username { get; set; }
        public string ticker { get; set; }
        public string company { get; set; }
    }

    public class Note
    {
        public int id { get; set; }
        public int favorite_id { get; set; }
        public string description { get; set; }
        public DateTime last_updated { get; set; }
    }
}
