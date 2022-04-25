using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TiDa.Models;
using Xamarin.Forms;

namespace TiDa.Services
{
    internal class CommonTaskDataStorage : IDataStore<CommonTask>
    {
        //******** 私有变量

        public IPreferenceStorage _Preference => DependencyService.Get<IPreferenceStorage>();
        //******** 私有方法
        public bool IsInitialized() =>
            _Preference.Get(CommonTaskStorageConstants.VersionKey, CommonTaskStorageConstants.DefultVersion) == CommonTaskStorageConstants.Version;

        /// <summary>
        /// 初始化数据库  
        /// </summary>
        public async Task InitializeAsync()
        {
             await Connection.CreateTableAsync<CommonTask>();

            _Preference.Set(CommonTaskStorageConstants.VersionKey, CommonTaskStorageConstants.Version);
        }
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
        public CommonTaskDataStorage()
        {
        }

        //******** 继承方法
        public Task<bool> AddItemAsync(CommonTask item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();

        }

        public async Task<int> DeleteItemAsync(CommonTask commonTask)
        =>
            await Connection.DeleteAsync(commonTask);
        

        public async Task<CommonTask> GetItemAsync(string id)
        {
            int Id = int.Parse(id);
            return await Connection.Table<CommonTask>().
                FirstOrDefaultAsync(c => c.Id == Id);
        }

        

            //=> await Connection.Table<CommonTask>().FirstOrDefaultAsync(c => c.Id == 2);
        public async Task<IEnumerable<CommonTask>> GetItemsAsync(bool forceRefresh = false)
        => await Connection.Table<CommonTask>().ToListAsync();
        

        public Task<bool> UpdateItemAsync(CommonTask item)
        {
            throw new NotImplementedException();
        }
    }
}
