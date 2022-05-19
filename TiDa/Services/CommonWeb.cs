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
using Xamarin.Forms.Internals;

namespace TiDa.Services
{
    internal class CommonWeb : IDataWeb<CommonTask>
    {
        //******** 私有变量
        private HttpClient httpClient;
        private IList<CommonTask> commonTasks;
        private HttpResponseMessage response;
        public IDataStore<CommonTask> CommonDataStore => DependencyService.Get<IDataStore<CommonTask>>();
        //******** 构造函数
        public CommonWeb()
        {
            httpClient = new HttpClient();
            commonTasks = new List<CommonTask>();
            response = new HttpResponseMessage();
        }

        //继承方法
        public async Task<IList<CommonTask>> UploadAsync(IList<CommonTask> commonTasks)
        {
            response = null;
            //获取一个本地的Common列表
            var localCommonList = await CommonDataStore.GetAllItemsAsync();
            IList<CommonTask> romoteCommonList = new List<CommonTask>();
            await Task.Run(() =>
            {
                foreach (CommonTask task in localCommonList)
                {
                    task.UserCookie = Preferences.Get("token", "undefined");
                }
            });

            //请求一个远端的列表
            try
            {
                var commonTask = new CommonTask { UserCookie = Preferences.Get("token", "undefined") };
                var json = JsonConvert.SerializeObject(commonTask);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/commonget", content);
                json = null;
                json = await response.Content.ReadAsStringAsync();
                if (json != null)
                {
                    romoteCommonList = JsonConvert.DeserializeObject<IList<CommonTask>>(json);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var localInsert = new List<CommonTask>();
            var localUpdate = new List<CommonTask>();
            var romoteInsert = new List<CommonTask>();
            var romoteUpdate = new List<CommonTask>();

            await Task.Run(() =>
            {
                var localDictionary = localCommonList.ToDictionary(p => p.Id, p => p);

                foreach (var remotecommonTask in romoteCommonList)
                {
                    if (localDictionary.TryGetValue(remotecommonTask.Id, out var localCommonTask))
                    {
                        if(remotecommonTask.Timestamp >= localCommonTask.Timestamp)
                        {
                            remotecommonTask.Timestamp = DateTime.Now.Ticks;
                            localUpdate.Add(remotecommonTask);
                        }
                    
                    }
                    else
                    {
                        localInsert.Add(remotecommonTask);
                    }

                }
            });


            await Task.Run(()=>
            {
                var romoteDictionary = romoteCommonList.ToDictionary(p => p.Id, p => p);
                foreach (var localCommonTask in localCommonList)
                {
                    if (romoteDictionary.TryGetValue(localCommonTask.Id,out var remoteCommonTask))
                    {
                        if (localCommonTask.Timestamp >= remoteCommonTask.Timestamp)
                        {
                            localCommonTask.Timestamp = DateTime.Now.Ticks;
                            romoteUpdate.Add(localCommonTask);
                        }

                    }
                    else
                    {
                        romoteInsert.Add(localCommonTask);
                    }
                }

            });



            foreach (var commonTask in localInsert)
            {
                await CommonDataStore.InserWebAsync(commonTask);
            }

            foreach (var commonTask in localUpdate)
            {
                await CommonDataStore.InsertorReplace(commonTask);
            }


            //网络服务
            if (romoteInsert.Count >0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteInsert);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/commonsave", content);
                    json = await response.Content.ReadAsStringAsync();
                    var commonTaskList = JsonConvert.DeserializeObject<IList<CommonTask>>(json);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            if (romoteUpdate.Count>0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteUpdate);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/commonupdate", content);
                    json = await response.Content.ReadAsStringAsync();
                    var commonTaskList = JsonConvert.DeserializeObject<IList<CommonTask>>(json);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return commonTasks;

        }

        public async Task DeleteAsync(CommonTask item)
        {
            var deleteList = new List<CommonTask>();
            deleteList.Add(item);
            var json = JsonConvert.SerializeObject(deleteList);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/commondelete", content);
        }

        public async Task<int> CountAsync(CommonTask item)
        {
            var json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/commonget", content);
            json = null;
            json = await response.Content.ReadAsStringAsync();
            var i = JsonConvert.DeserializeObject<IList<CommonTask>>(json);
            return i.Count;

        }
    }
}
