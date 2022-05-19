using System;
using System.Collections.Generic;
using System.Text;

namespace TiDa.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string UserCookie { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }
        public string msg { get; set; }
    }
}
