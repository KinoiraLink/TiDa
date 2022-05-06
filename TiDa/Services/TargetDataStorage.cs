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
    internal class TargetDataStorage : IDataStore<TargetTask>
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

        //******** 私有变量

        public IPreferenceStorage _Preference => DependencyService.Get<IPreferenceStorage>();


        //******** 构造函数
        public TargetDataStorage()
        {

        }
        //继承方法
        public async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<TargetTask>();

            _Preference.Set(TargetTaskStorageConstants.VersionKey, TargetTaskStorageConstants.Version);
        }

        public bool IsInitialized()
        =>
            _Preference.Get(TargetTaskStorageConstants.VersionKey, TargetTaskStorageConstants.DefultVersion) == TargetTaskStorageConstants.Version;
        

        public async Task<bool> AddItemAsync(TargetTask item)
        {
            throw new NotImplementedException();
        }

        public async Task InserItemAsync(TargetTask item)
        {
            Connection.InsertAsync(item);
        }

        public async Task InserWebAsync(TargetTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemAsync(TargetTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertorReplace(TargetTask item)
        {
            await Connection.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(TargetTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<TargetTask> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TargetTask>> GetItemsAsync(bool forceRefresh = false)
        =>
            await Connection.Table<TargetTask>().Where(p =>p.IsDelete==false).ToListAsync();
        

        public async Task<IList<TargetTask>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InserAllItem(IList<TargetTask> list)
        {
            throw new NotImplementedException();
        }
    }
}
