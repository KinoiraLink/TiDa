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
    public  class SimpleWrPoWeb : IDataWeb<SimpleWrPo>
    {
        //******** 私有变量
        private HttpClient httpClient;
        private IList<SimpleWrPo> simpleWrPos;
        private HttpResponseMessage response;
        public IDataStore<SimpleWrPo> SimpleWrPoDataStore => DependencyService.Get<IDataStore<SimpleWrPo>>();
        //******** 构造函数
        public SimpleWrPoWeb()
        {
            httpClient = new HttpClient();
            simpleWrPos = new List<SimpleWrPo>();
            response = new HttpResponseMessage();
        }


        //******** 继承方法
        public async Task<IList<SimpleWrPo>> UploadAsync(IList<SimpleWrPo> simpleWrPos)
        {
            var localSimList = await SimpleWrPoDataStore.GetAllItemsAsync();

            IList<SimpleWrPo> romoteSimList = new List<SimpleWrPo>();

            await Task.Run(() =>
            {
                foreach (SimpleWrPo task in localSimList)
                {
                    task.UserCookie = Preferences.Get("token", "undefined");
                }
            });

            //请求一个远端的列表
            try
            {
                var simpleWrPo = new SimpleWrPo {UserCookie = Preferences.Get("token", "undefined")};
                var json = JsonConvert.SerializeObject(simpleWrPo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("http://localhost:3000/upload/simpleget", content);
                json = null;
                json = await response.Content.ReadAsStringAsync();
                if (json != null)
                {
                    romoteSimList = JsonConvert.DeserializeObject<IList<SimpleWrPo>>(json);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            var localInsert = new List<SimpleWrPo>();
            var localUpdate = new List<SimpleWrPo>();
            var romoteInsert = new List<SimpleWrPo>();
            var romoteUpdate = new List<SimpleWrPo>();

            await Task.Run(() =>
            {
                var localDictionary = localSimList.ToDictionary(p => p.Id, p => p);

                foreach (var remotesimTask in romoteSimList)
                {
                    if (localDictionary.TryGetValue(remotesimTask.Id, out var localsimTask))
                    {
                        if (remotesimTask.Timestamp >= localsimTask.Timestamp)
                        {
                            remotesimTask.Timestamp = DateTime.Now.Ticks;
                            localUpdate.Add(remotesimTask);
                        }

                    }
                    else
                    {
                        localInsert.Add(remotesimTask);
                    }

                }
            });


            await Task.Run(() =>
            {
                var romoteDictionary = romoteSimList.ToDictionary(p => p.Id, p => p);
                foreach (var localsimTask in localSimList)
                {
                    if (romoteDictionary.TryGetValue(localsimTask.Id, out var remotesimTask))
                    {
                        if (localsimTask.Timestamp >= remotesimTask.Timestamp)
                        {
                            localsimTask.Timestamp = DateTime.Now.Ticks;
                            romoteUpdate.Add(localsimTask);
                        }

                    }
                    else
                    {
                        romoteInsert.Add(localsimTask);
                    }
                }

            });


            foreach (var simpleWrPo in localInsert)
            {
                await SimpleWrPoDataStore.InserWebAsync(simpleWrPo);
            }

            foreach (var simpleWrPo in localUpdate)
            {
                await SimpleWrPoDataStore.InsertorReplace(simpleWrPo);
            }


            //网络服务
            if (romoteInsert.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(romoteInsert);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync("http://localhost:3000/upload/simplesave", content);
                    json = await response.Content.ReadAsStringAsync();
                    var simpleWrPoTaskList = JsonConvert.DeserializeObject<IList<SimpleWrPo>>(json);

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
                    response = await httpClient.PostAsync("http://localhost:3000/upload/simpleupdate", content);
                    json = await response.Content.ReadAsStringAsync();
                    var simpleWrPoTaskList = JsonConvert.DeserializeObject<IList<SimpleWrPo>>(json);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return simpleWrPos;
        }

        public async Task DeleteAsync(SimpleWrPo item)
        {
            throw new NotImplementedException();
        }
    }
}
