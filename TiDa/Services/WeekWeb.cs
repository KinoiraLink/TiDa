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
    internal class WeekWeb : IDataWeb<WeekTask>
    {
        //******** 私有变量
        private HttpClient httpClient;
        private IList<WeekTask> weekTasks;
        private HttpResponseMessage response;
        public IDataStore<WeekTask> WeekDataStore => DependencyService.Get<IDataStore<WeekTask>>();

        //******** 构造函数
        public WeekWeb()
        {
            httpClient = new HttpClient();
            weekTasks = new List<WeekTask>();
            response = new HttpResponseMessage();
        }

        //******** 继承方法
        public async Task<IList<WeekTask>> UploadAsync(IList<WeekTask> weekTasks)
        {
            response = null;
            //获取一个本地的Wee列表
            var localWeekList = await WeekDataStore.GetAllItemsAsync();
            IList<WeekTask> romoteWeekList = new List<WeekTask>();
            await Task.Run(() =>
            {
                foreach (WeekTask task in localWeekList)
                {
                    task.UserCookie = Preferences.Get("token", "undefined");
                }
            });

            //请求一个远端的列表
            try
            {
                var weekTask = new WeekTask { UserCookie = Preferences.Get("token", "undefined") };
                var json = JsonConvert.SerializeObject(weekTask);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/weekget", content);
                json = null;
                json = await response.Content.ReadAsStringAsync();
                if (json != null)
                {
                    romoteWeekList = JsonConvert.DeserializeObject<IList<WeekTask>>(json);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var localInsert = new List<WeekTask>();
            var localUpdate = new List<WeekTask>();
            var romoteInsert = new List<WeekTask>();
            var romoteUpdate = new List<WeekTask>();


            await Task.Run(() =>
            {
                var localDictionary = localWeekList.ToDictionary(p => p.Id, p => p);

                foreach (var remoteWeekTask in romoteWeekList)
                {
                    if (localDictionary.TryGetValue(remoteWeekTask.Id, out var localWeekTask))
                    {
                        if (remoteWeekTask.Timestamp > localWeekTask.Timestamp)
                        {
                            remoteWeekTask.Timestamp = DateTime.Now.Ticks;
                            localUpdate.Add(remoteWeekTask);
                        }

                    }
                    else
                    {
                        localInsert.Add(remoteWeekTask);
                    }

                }
            });

            await Task.Run(() =>
            {
                var romoteDictionary = romoteWeekList.ToDictionary(p => p.Id, p => p);
                foreach (var localWeekTask in localWeekList)
                {
                    if (romoteDictionary.TryGetValue(localWeekTask.Id, out var remoteWeekTask))
                    {
                        if (localWeekTask.Timestamp > remoteWeekTask.Timestamp)
                        {
                            localWeekTask.Timestamp = DateTime.Now.Ticks;
                            romoteUpdate.Add(localWeekTask);
                        }

                    }
                    else
                    {
                        romoteInsert.Add(localWeekTask);
                    }
                }

            });
            foreach (var weekTask in localInsert)
            {
                await WeekDataStore.InserWebAsync(weekTask);
            }

            foreach (var weekTask in localUpdate)
            {
                await WeekDataStore.InsertorReplace(weekTask);
            }


            //网络服务
            if (romoteInsert.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteInsert);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/weeksave", content);
                    json = await response.Content.ReadAsStringAsync();
                    var weekTaskList = JsonConvert.DeserializeObject<IList<CommonTask>>(json);

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
                    response = await httpClient.PostAsync("http://121.37.91.77:3000/upload/weekupdate", content);
                    json = await response.Content.ReadAsStringAsync();
                    var weekTaskList = JsonConvert.DeserializeObject<IList<CommonTask>>(json);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return weekTasks;
        }

        public async Task DeleteAsync(WeekTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(WeekTask item)
        {
            throw new NotImplementedException();
        }
    }
}
