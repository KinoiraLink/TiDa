using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TiDa.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.Services
{
    public class TomatoWeb : IDataWeb<TomatoTask>
    {

        //******** 私有变量
        private HttpClient httpClient;
        private IList<TomatoTask> tomatoTasks;
        private HttpResponseMessage response;
        public IDataStore<TomatoTask> TomatoDataStorage => DependencyService.Get<IDataStore<TomatoTask>>();


        //构造函数
        public TomatoWeb()
        {
            httpClient = new HttpClient();
            tomatoTasks = new List<TomatoTask>();
            response = new HttpResponseMessage();
        }


        //******** 继承方法
        public async Task<IList<TomatoTask>> UploadAsync(IList<TomatoTask> tomatoTasks)
        {
            response = null;
            //获取一个本地的Tomato列表
            var localTomatoList = await TomatoDataStorage.GetAllItemsAsync();
            IList<TomatoTask> romoteTomatoList = new List<TomatoTask>();
            await Task.Run(() =>
            {
                foreach (TomatoTask task in localTomatoList)
                {
                    task.UserCookie = Preferences.Get("token", "undefined");
                }
            });
            try
            {
                var tomatoTask = new TomatoTask { UserCookie = Preferences.Get("token", "undefined") };
                var json = JsonConvert.SerializeObject(tomatoTask);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/tomatoget", content);
                json = null;
                json = await response.Content.ReadAsStringAsync();
                if (json != null)
                {
                    romoteTomatoList = JsonConvert.DeserializeObject<IList<TomatoTask>>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var localInsert = new List<TomatoTask>();
            var localUpdate = new List<TomatoTask>();
            var romoteInsert = new List<TomatoTask>();
            var romoteUpdate = new List<TomatoTask>();


            await Task.Run(() =>
            {
                var localDictionary = localTomatoList.ToDictionary(p => p.Id, p => p);

                foreach (var remotetomatoTask in romoteTomatoList)
                {
                    if (localDictionary.TryGetValue(remotetomatoTask.Id, out var localTomatoTask))
                    {
                        if (remotetomatoTask.Timestamp > localTomatoTask.Timestamp)
                        {
                            remotetomatoTask.Timestamp = DateTime.Now.Ticks;
                            localUpdate.Add(remotetomatoTask);
                        }

                    }
                    else
                    {
                        localInsert.Add(remotetomatoTask);
                    }

                }
            });

            await Task.Run(() =>
            {
                var romoteDictionary = romoteTomatoList.ToDictionary(p => p.Id, p => p);
                foreach (var localTomatoTask in localTomatoList)
                {
                    if (romoteDictionary.TryGetValue(localTomatoTask.Id, out var remoteTomatoTask))
                    {
                        if (localTomatoTask.Timestamp > remoteTomatoTask.Timestamp)
                        {
                            localTomatoTask.Timestamp = DateTime.Now.Ticks;
                            romoteUpdate.Add(localTomatoTask);
                        }

                    }
                    else
                    {
                        romoteInsert.Add(localTomatoTask);
                    }
                }

            });

            foreach (var tomatoTask in localInsert)
            {
                await TomatoDataStorage.InserWebAsync(tomatoTask);
            }

            foreach (var tomatoTask in localUpdate)
            {
                await TomatoDataStorage.InsertorReplace(tomatoTask);
            }

            //网络服务
            if (romoteInsert.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteInsert);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/tomatosave", content);
                    json = await response.Content.ReadAsStringAsync();
                    var tomatoTaskList = JsonConvert.DeserializeObject<IList<TomatoTask>>(json);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            if (romoteUpdate.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteUpdate);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/tomatoupdate", content);
                    json = await response.Content.ReadAsStringAsync();
                    var tomatoTaskList = JsonConvert.DeserializeObject<IList<TomatoTask>>(json);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return tomatoTasks;
        }

        public async Task DeleteAsync(TomatoTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(TomatoTask item)
        {
            var json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/tomatoget", content);
            json = null;
            json = await response.Content.ReadAsStringAsync();
            var i = JsonConvert.DeserializeObject<IList<TomatoTask>>(json);
            return i.Count;
        }
    }
}
