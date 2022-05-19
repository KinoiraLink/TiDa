using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TiDa.Models;
using Xamarin.Forms;

namespace TiDa.Services
{
    public class TomatoDataStorage : IDataStore<TomatoTask>
    {
        //******** 私有变量

        public IPreferenceStorage _Preference => DependencyService.Get<IPreferenceStorage>();

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

        //******** 构造函数
        public TomatoDataStorage()
        {
        }

        //继承方法
        public async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<TomatoTask>();
            _Preference.Set(TomatoTaskStorageConstants.VersionKey, TomatoTaskStorageConstants.Version);
        }

        public bool IsInitialized()
        =>_Preference.Get(TomatoTaskStorageConstants.VersionKey, TomatoTaskStorageConstants.DefultVersion) == TomatoTaskStorageConstants.Version;

        public async Task<bool> AddItemAsync(TomatoTask item)
        {
            throw new NotImplementedException();
        }

        public async Task InserItemAsync(TomatoTask item)
        {
            await Connection.InsertAsync(item);
        }

        public async Task InserWebAsync(TomatoTask item)
        {
            string sql = $"INSERT INTO tomato_task VALUES ({item.Id},'{item.UserCookie}','{item.TaskTitle}','{item.TaskDescribe}','{item.TaskTime}',{item.Timestamp})";
            //string sql = $"INSERT INTO common_task VALUES ({item.Id},'{item.UserCookie}','{item.TaskTitle}','{item.TaskDescribe}','{item.TaskDate}','{item.TaskTime}',{item.Done},{item.Timestamp},{item.IsDeleted})";
            await Connection.ExecuteAsync(sql);
        }

        public async Task<bool> UpdateItemAsync(TomatoTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertorReplace(TomatoTask item)
        {
            await Connection.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(TomatoTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<TomatoTask> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TomatoTask>> GetItemsAsync(bool forceRefresh = false)
            => await Connection.Table<TomatoTask>().ToListAsync();

        public async Task<IList<TomatoTask>> GetAllItemsAsync()
            => await Connection.Table<TomatoTask>().ToListAsync();

        public async Task InserAllItem(IList<TomatoTask> list)
        {
            throw new NotImplementedException();
        }

        public async Task RomoveItemAsync(TomatoTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetItemsCount()
        => await Connection.Table<TomatoTask>().CountAsync();
    }
}
