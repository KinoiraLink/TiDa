using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace TiDa.Models
{
    public class User
    {
        public int id { get; set; }
        public string account { get; set; }
        public string pwd { get; set; }
        public string token { get; set; }

        public string msg { get; set; }
    }
}
