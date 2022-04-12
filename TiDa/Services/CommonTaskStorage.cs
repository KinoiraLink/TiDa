using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TiDa.Models;
using Xamarin.Essentials;

namespace TiDa.Services
{
    /// <summary>
    /// 一般task数据库实现
    /// </summary>
    public class CommonTaskStorage : ICommonTaskStorage
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
        //******** 继承方法

        /// <summary>
        /// 初始化数据库  
        /// </summary>
        public async Task InitializeAsync()
        {
            //using (var dbFileStream = new FileStream(CommonTaskDbPath, FileMode.Create))
            //{
            //    using (var dbAssertStream =
            //           Assembly.GetExecutingAssembly().GetManifestResourceStream(DbName))
            //    {
            //        await dbAssertStream.CopyToAsync(dbFileStream);
            //    }
            //}
            await Connection.CreateTableAsync<CommonTask>();

            _preferenceStorage.Set(CommonTaskStorageConstants.VersionKey, CommonTaskStorageConstants.Version);
        }

        /// <summary>
        /// 判断数据库已经初始化
        /// </summary>
        /// <returns></returns>
        public bool IsInitialized() =>
            _preferenceStorage.Get(CommonTaskStorageConstants.VersionKey, CommonTaskStorageConstants.DefultVersion) == CommonTaskStorageConstants.Version;

        /// <summary>
        /// 获取一般task列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CommonTask>> GetCommonsTasksAsync()
        => await Connection.Table<CommonTask>().ToListAsync();


        public async Task<CommonTask> GetCommonTaskAsync(CommonTask commonTask)
            => //await Connection.GetAsync<CommonTask>(commonTask);
            await Connection.Table<CommonTask>().FirstOrDefaultAsync( c =>c.Id==commonTask.Id);


        public async Task SaveCommonTaskAsync(CommonTask commonTask)
        => await Connection.InsertOrReplaceAsync(commonTask);

        public async Task DeleteCommonTaskAsync(CommonTask commonTask)
            => await Connection.DeleteAsync(commonTask);








        //******** 公开方法
        /// <summary>
        /// 收藏存储
        /// </summary>
        /// <param name="preferenceStorage">偏好存储</param>
        public CommonTaskStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync() => await Connection.CloseAsync();
    }
}
