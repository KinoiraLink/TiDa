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
    public class SimpleWrPoDataStorage : IDataStore<SimpleWrPo>
    {
        //******** 私有变量

        public IPreferenceStorage _Preference => DependencyService.Get<IPreferenceStorage>();
        //******** 私有方法
        
        
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
        public SimpleWrPoDataStorage()
        {
        }



        //****** 继承方法
        public async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<SimpleWrPo>();

            _Preference.Set(SimpleWrPoStorageConstants.VersionKey, SimpleWrPoStorageConstants.Version);
        }

        public bool IsInitialized()
        =>
            _Preference.Get(SimpleWrPoStorageConstants.VersionKey, SimpleWrPoStorageConstants.DefultVersion) == SimpleWrPoStorageConstants.Version;
        

        public async Task<bool> AddItemAsync(SimpleWrPo item)
        {
            throw new NotImplementedException();
        }

        public async Task InserItemAsync(SimpleWrPo item)
            =>
                await Connection.InsertAsync(item);

        public async Task<bool> UpdateItemAsync(SimpleWrPo item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertorReplace(SimpleWrPo item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteItemAsync(SimpleWrPo item)
        {
            throw new NotImplementedException();
        }

        public async Task<SimpleWrPo> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SimpleWrPo>> GetItemsAsync(bool forceRefresh = false)
            => await Connection.Table<SimpleWrPo>().ToListAsync();
    }
}
