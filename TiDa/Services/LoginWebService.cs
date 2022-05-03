using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;

namespace TiDa.Services
{
    public class LoginWebService : IWebService<User>
    {

        //******** 私有变量
        private HttpClient httpClient;
        private User user;
        private HttpResponseMessage response;

        //******** 构造函数
        public LoginWebService()
        {
            httpClient = new HttpClient();
            user = new User();
            response = new HttpResponseMessage();
        }

        //继承方法
        public async Task<User> LgoinAsync(User item)
        {
            response = null;

            try
            {
                var json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://121.37.91.77:3000/login/register", content); 
                json = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(json);
                //user = users[0];
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> RegisterAsync(User item)
        {
            response = null;

            try
            {
                var json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://10.36.12.2:3000/login/register", content);
                var Json = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<IList<User>>(Json);
                user = users[0];
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
