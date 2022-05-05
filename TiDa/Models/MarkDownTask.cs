using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TiDa.Models
{
    [SQLite.Table("markdown_task")]
    public class MarkDownTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TaskTitle { get; set; }

        public string TaskContent { get; set; }

        public long Timestamp { get; set; }

        public string UserCookie { get; set; }


    }

}
