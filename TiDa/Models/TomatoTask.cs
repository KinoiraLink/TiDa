using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TiDa.Models
{
    [SQLite.Table("tomato_task")]
    public class TomatoTask
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        public string UserCookie { get; set; }

        public string TaskTitle { get; set; }

        public string TaskDescribe { get; set; }

        public string TaskTime { get; set; }

        public long Timestamp { get; set; }
    }
}
