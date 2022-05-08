using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TiDa.Models
{
    [SQLite.Table("simple_wrpo")]
    public class SimpleWrPo
    {
        [PrimaryKey,AutoIncrement] 
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public long Timestamp { get; set; }


        public string UserCookie { get; set; }

    }
}
