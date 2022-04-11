using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace TiDa.Models
{
    [SQLite.Table("user")]
    public class User
    {
        /// <summary>
        /// 用户序列
        /// </summary>
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 用户cookie
        /// </summary>
        public string UserCookie { get; set; }


    }
}
