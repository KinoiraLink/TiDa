﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TiDa.Models;
using Xamarin.Forms;

namespace TiDa.Services
{
    internal class WeekTaskDataStorage : IDataStore<WeekTask>
    {
        //******** 公有变量

        /// <summary>
        /// 数据库文件路径
        /// </summary>
        public static readonly string CommonTaskDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);
        //******** 私有变量

        /// <summary>
        /// 数据库文件名
        /// </summary>
        private const string DbName = "commontaskdb.sqlite3";

        private SQLiteAsyncConnection _connection;

        public SQLiteAsyncConnection Connection => _connection ?? new SQLiteAsyncConnection(CommonTaskDbPath);

        /// <summary>
        /// 偏好存储
        /// </summary>
        /// <summary>
        /// 偏好存储
        /// </summary>
        private IPreferenceStorage _preferenceStorage;
        public IPreferenceStorage _Preference => DependencyService.Get<IPreferenceStorage>();

        //******** 继承方法
        public Task<bool> AddItemAsync(WeekTask item)
        {
            throw new NotImplementedException();
        }

        public async Task InserItemAsync(WeekTask item)
            => await Connection.InsertAsync(item);

        public async Task InserWebAsync(WeekTask item)
        {
            string sql = $"INSERT INTO week_task VALUES ({item.Id},'{item.TaskTitle}','{item.TaskDescribe}','{item.TaskTime}','{item.Site}',{item.Timestamp},'{item.UserCookie}')";
            await Connection.ExecuteAsync(sql);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertorReplace(WeekTask week)
        => await Connection.UpdateAsync(week);
        

        

        public Task<int> DeleteItemAsync(WeekTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<WeekTask> GetItemAsync(string id)
        {
            var Id = int.Parse(id);
            return await Connection.Table<WeekTask>().FirstOrDefaultAsync(w => w.Id == Id);
        }

        public async Task<IEnumerable<WeekTask>> GetItemsAsync(bool forceRefresh = false)
            => await Connection.Table<WeekTask>().ToListAsync();

        public async Task<IList<WeekTask>> GetAllItemsAsync()
            => await Connection.Table<WeekTask>().ToListAsync();

        public async Task InserAllItem(IList<WeekTask> list)
            => await Connection.InsertAllAsync(list);

        public async Task RomoveItemAsync(WeekTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetItemsCount()
        => await Connection.Table<WeekTask>().CountAsync();

        public async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<WeekTask>();
            _Preference.Set(WeekTaskStorageConstants.VersionKey,WeekTaskStorageConstants.Version);
        }

        public bool IsInitialized()
            =>
                _Preference.Get(WeekTaskStorageConstants.VersionKey, WeekTaskStorageConstants.DefultVersion) ==
                WeekTaskStorageConstants.Version;
        

        public Task<bool> UpdateItemAsync(WeekTask item)
        {
            throw new NotImplementedException();
        }
    }
}
