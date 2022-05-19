using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TiDa.Models;

namespace TiDa.Services
{
    public class RegisterInfoWebService : IWebService<UserInfo>
    {
        //******** 私有变量
        private HttpClient httpClient;
        private UserInfo userInfo;
        private HttpResponseMessage response;
        private IList<UserInfo> users;

        //******** 构造函数
        public RegisterInfoWebService()
        {
            httpClient = new HttpClient();
            userInfo = new UserInfo();
            response = new HttpResponseMessage();
            users = new List<UserInfo>();
        }

        public async Task<UserInfo> LgoinAsync(UserInfo userInfo)
        {
            users.Add(userInfo);
            response = null;
            try
            {
                var json = JsonConvert.SerializeObject(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://121.37.91.77:3000/login/getinfo", content);
                json = await response.Content.ReadAsStringAsync();
                var infolList = JsonConvert.DeserializeObject<List<UserInfo>>(json);
                var info = infolList[0];
                return info;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<UserInfo> RegisterAsync(UserInfo userInfo)
        {
            users.Add(userInfo);
            response = null;
            try
            {
                var json = JsonConvert.SerializeObject(users);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://121.37.91.77:3000/login/saveinfo", content);
                return userInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
