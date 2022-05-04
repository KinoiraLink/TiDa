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
    public class MarkDownTaskDataStorage : IDataStore<MarkDownTask>
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
        public async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<MarkDownTask>();
            _Preference.Set(MarkDownTaskStorageConstants.VersionKey, MarkDownTaskStorageConstants.Version);
        }

        public bool IsInitialized() 
            =>
            _Preference.Get(MarkDownTaskStorageConstants.VersionKey, MarkDownTaskStorageConstants.DefultVersion) ==
                MarkDownTaskStorageConstants.Version;
        

        public async Task<bool> AddItemAsync(MarkDownTask item)
        {
            throw new NotImplementedException();
        }

        public async Task InserItemAsync(MarkDownTask item)
        {
            await Connection.InsertAsync(item);
        }

        public async Task InserWebAsync(MarkDownTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemAsync(MarkDownTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertorReplace(MarkDownTask item)
        {
            await Connection.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(MarkDownTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<MarkDownTask> GetItemAsync(string id)
        {
            {
                var Id = int.Parse(id);
                return await Connection.Table<MarkDownTask>().FirstOrDefaultAsync(w => w.Id == Id);
            }
        }

        public async Task<IEnumerable<MarkDownTask>> GetItemsAsync(bool forceRefresh = false)
        =>
            await Connection.Table<MarkDownTask>().ToArrayAsync();

        public async Task<IList<MarkDownTask>> GetAllItemsAsync()
            => await Connection.Table<MarkDownTask>().ToListAsync();

        public async Task InserAllItem(IList<MarkDownTask> list)
        {
            throw new NotImplementedException();
        }
    }
}
