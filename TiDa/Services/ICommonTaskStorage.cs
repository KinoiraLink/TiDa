﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;

namespace TiDa.Services
{
    /// <summary>
    /// 一般task数据库接口
    /// </summary>
    public interface ICommonTaskStorage
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

        /// <summary>
        /// 获取一般task列表
        /// </summary>
        /// <returns></returns>
        Task<List<CommonTask>> GetCommonTaskAsync();
    }

    public static class CommonTaskStorageConstants
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
        public const string VersionKey = nameof(CommonTaskStorageConstants) + "." + nameof(Version);
    }
}
