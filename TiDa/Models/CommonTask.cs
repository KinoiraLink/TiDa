using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TiDa.Models
{
    [SQLite.Table("common_task")]
    public class CommonTask
    {

        /// <summary>
        /// 一般task序列
        /// </summary>
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 用户Cookie
        /// </summary>
        public string UserCookie { get; set; }

        /// <summary>
        /// task标题
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// task描述
        /// </summary>
        public string TaskDescribe { get; set; }

        /// <summary>
        /// task日期
        /// </summary>
        public string TaskDate { get; set; }

        /// <summary>
        /// task时间
        /// </summary>
        public string TaskTime { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool Done { get; set; }





    }
}
