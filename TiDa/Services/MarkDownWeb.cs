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
    internal class MarkDownWeb : IDataWeb<MarkDownTask>
    {
        //******** 私有变量
        private HttpClient httpClient;

        private IList<MarkDownTask> markDownTasks;

        private HttpResponseMessage response;

        public IDataStore<MarkDownTask> MarkDownDataStore => DependencyService.Get<IDataStore<MarkDownTask>>();

        //******** 构造函数
        public MarkDownWeb()
        {
            httpClient = new HttpClient();
            markDownTasks = new List<MarkDownTask>();
            response = new HttpResponseMessage();
        }

        //******** 继承方法
        public async Task<IList<MarkDownTask>> UploadAsync(IList<MarkDownTask> markDownTasks)
        {
            response = null;
            //获取一个本地的Markdown列表
            var localMarkDownList = await MarkDownDataStore.GetAllItemsAsync();
            //获取一个远端的Markdown列表
            IList<MarkDownTask> remoteMarkDownList = new List<MarkDownTask>();

            await Task.Run(() =>
            {
                foreach (MarkDownTask task in localMarkDownList)
                {
                    task.UserCookie = Preferences.Get("token", "undefined");
                }
            });
            //获取一个远端的Markdown列表
            try
            {
                var markDownTask = new MarkDownTask { UserCookie = Preferences.Get("token", "undefined") };
                var json = JsonConvert.SerializeObject(markDownTask);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://localhost:3000/upload/markdownget", content);
                json = null;
                json = await response.Content.ReadAsStringAsync();

                if (json != null)
                {
                    remoteMarkDownList = JsonConvert.DeserializeObject<IList<MarkDownTask>>(json);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var localInsert = new List<MarkDownTask>();
            var localUpdate = new List<MarkDownTask>();
            var romoteInsert = new List<MarkDownTask>();
            var romoteUpdate = new List<MarkDownTask>();

            await Task.Run(() =>
            {
                var localDictionary = localMarkDownList.ToDictionary(p => p.Id, p => p);

                foreach (var remoteMarkDownTask in remoteMarkDownList)
                {
                    if (localDictionary.TryGetValue(remoteMarkDownTask.Id, out var localMarkDownTask))
                    {
                        if (remoteMarkDownTask.Timestamp >= localMarkDownTask.Timestamp)
                        {
                            remoteMarkDownTask.Timestamp = DateTime.Now.Ticks;
                            localUpdate.Add(remoteMarkDownTask);
                        }

                    }
                    else
                    {
                        localInsert.Add(remoteMarkDownTask);
                    }

                }
            });


            await Task.Run(() =>
            {
                var romoteDictionary = remoteMarkDownList.ToDictionary(p => p.Id, p => p);
                foreach (var localMarkDownTask in localMarkDownList)
                {
                    if (romoteDictionary.TryGetValue(localMarkDownTask.Id, out var remoteMarkDownTask))
                    {
                        if (localMarkDownTask.Timestamp >= remoteMarkDownTask.Timestamp)
                        {
                            localMarkDownTask.Timestamp = DateTime.Now.Ticks;
                            romoteUpdate.Add(localMarkDownTask);
                        }

                    }
                    else
                    {
                        romoteInsert.Add(localMarkDownTask);
                    }
                }

            });

            foreach (var markDownTask in localInsert)
            {
                await MarkDownDataStore.InserWebAsync(markDownTask);
            }

            foreach (var markDownTask in localUpdate)
            {
                await MarkDownDataStore.InsertorReplace(markDownTask);
            }

            //网络服务
            if (romoteInsert.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteInsert);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://localhost:3000/upload/markdownsave", content);
                    json = await response.Content.ReadAsStringAsync();
                    var markDownTaskList = JsonConvert.DeserializeObject<IList<MarkDownTask>>(json);

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
                    response = await httpClient.PostAsync("http://localhost:3000/upload/markdownupdate", content);
                    json = await response.Content.ReadAsStringAsync();
                    var markDownTaskList = JsonConvert.DeserializeObject<IList<MarkDownTask>>(json);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return markDownTasks;
        }
    }
}
