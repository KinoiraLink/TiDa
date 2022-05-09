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
    public class TargetWeb : IDataWeb<TargetTask>
    {
        //******** 私有变量
        private HttpClient httpClient;
        private IList<TargetTask> targetTasks;
        private HttpResponseMessage response;
        public IDataStore<TargetTask> TargetDataStore => DependencyService.Get<IDataStore<TargetTask>>();

        //******** 构造函数
        public TargetWeb()
        {
            httpClient = new HttpClient();
            targetTasks = new List<TargetTask>();
            response = new HttpResponseMessage();
        }

        //******** 继承方法
        public async Task<IList<TargetTask>> UploadAsync(IList<TargetTask> targetTasks)
        {
            response = null;

            var localTargetList = await TargetDataStore.GetAllItemsAsync();
            IList<TargetTask> romoteTargetList = new List<TargetTask>();
            await Task.Run(() =>
            {
                foreach (TargetTask task in localTargetList)
                {
                    task.UserCookie = Preferences.Get("token", "undefined");
                }
            });

            //请求一个远端的列表
            try
            {
                var targetTask = new TargetTask {UserCookie = Preferences.Get("token", "undefined")};
                var json = JsonConvert.SerializeObject(targetTask);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/targetget", content);
                json = null;
                json = await response.Content.ReadAsStringAsync();
                if (json != null)
                {
                    romoteTargetList = JsonConvert.DeserializeObject<IList<TargetTask>>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var localInsert = new List<TargetTask>();
            var localUpdate = new List<TargetTask>();
            var romoteInsert = new List<TargetTask>();
            var romoteUpdate = new List<TargetTask>();

            await Task.Run(() =>
            {
                var localDictionary = localTargetList.ToDictionary(p => p.Id, p => p);

                foreach (var remoteTargetTask in romoteTargetList)
                {
                    if (localDictionary.TryGetValue(remoteTargetTask.Id, out var localTargetTask))
                    {
                        if (remoteTargetTask.Timestamp > localTargetTask.Timestamp)
                        {
                            remoteTargetTask.Timestamp = DateTime.Now.Ticks;
                            localUpdate.Add(remoteTargetTask);
                        }

                    }
                    else
                    {
                        localInsert.Add(remoteTargetTask);
                    }

                }
            });

            await Task.Run(() =>
            {
                var romoteDictionary = romoteTargetList.ToDictionary(p => p.Id, p => p);
                foreach (var localTargetTask in localTargetList)
                {
                    if (romoteDictionary.TryGetValue(localTargetTask.Id, out var remoteTargetTask))
                    {
                        if (localTargetTask.Timestamp >= remoteTargetTask.Timestamp)
                        {
                            localTargetTask.Timestamp = DateTime.Now.Ticks;
                            romoteUpdate.Add(localTargetTask);
                        }

                    }
                    else
                    {
                        romoteInsert.Add(localTargetTask);
                    }
                }

            });

            foreach (var targetTask in localInsert)
            {
                await TargetDataStore.InserWebAsync(targetTask);
            }

            foreach (var targetTask in localUpdate)
            {
                await TargetDataStore.InsertorReplace(targetTask);
            }

            //网络服务
            if (romoteInsert.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteInsert);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/targetsave", content);
                    json = await response.Content.ReadAsStringAsync();
                    var targetTaskList = JsonConvert.DeserializeObject<IList<TargetTask>>(json);

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
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/targetupdate", content);
                    json = await response.Content.ReadAsStringAsync();
                    var targetTaskList = JsonConvert.DeserializeObject<IList<TargetTask>>(json);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }








            return targetTasks;
        }

        public async Task DeleteAsync(TargetTask item)
        {
            var deleteList = new List<TargetTask>();
            deleteList.Add(item);
            var json = JsonConvert.SerializeObject(deleteList);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/targetdelete", content);
        }
    }
}
