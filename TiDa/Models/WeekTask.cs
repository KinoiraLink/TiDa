using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TiDa.Models
{
    [SQLite.Table("week_task")]
    public class WeekTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        public int UserCookie { get; set; }
        public int WeekDay { get; set; }

        public int Row { get; set; }

        public string TaskTitle { get; set; }

        public string TaskDescribe { get; set; }

        public string TaskTime { get; set; }

        public string Site { get; set; }



        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
    }
}


