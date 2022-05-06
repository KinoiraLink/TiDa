using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TiDa.Models
{
    [SQLite.Table("target_task")]
    public class TargetTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string UserCookie { get; set; }

        public string MainTitle { get; set; }

        public string MinorTitle { get; set; }

        public long Timestamp { get; set; }


        public bool IsDone { get; set; }

        public bool IsDelete { get; set; }

        
        
    }
}
