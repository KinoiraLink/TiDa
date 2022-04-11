using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TiDa.Services
{
    /// <summary>
    /// 用户登录业务接口
    /// </summary>
    public interface IUserLogin
    {
        /// <summary>
        /// 初始化数据库  
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// 判断数据库已经初始化
        /// </summary>
        /// <returns></returns>
        bool IsInitialized();

        
    }

    public static class UserStorageConstants
    {
        /// <summary>
        /// 收藏数据库版本号
        /// </summary>
        public const int Version = 1;

        /// <summary>
        /// 默认版本号
        /// </summary>
        public const int DefultVersion = 0;
        /// <summary>
        /// 收藏数据库版本号的键。
        /// </summary>
        public const string VersionKey = nameof(UserStorageConstants) + "." + nameof(Version);
    }
}
